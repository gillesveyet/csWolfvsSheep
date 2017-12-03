using System;
using System.Collections.Generic;
using System.Text;

namespace WolfvsSheep
{
    enum GameStatus : sbyte
    {
        SheepWon = -1,
        NotFinished = 0,
        WolfWon = 1
    }

    class GameState : IEquatable<GameState>
    {
        const int NB_SHEEP = 5;
        const int MAX_SCORE = 1000;

        public byte NbMoves;
        public byte W, S0, S1, S2, S3, S4;

        public GameStatus Status { get; private set; }

        public bool IsGameOver { get { return Status != GameStatus.NotFinished; } }

        public bool IsWolf
        {
            get { return NbMoves % 2 == 0; }
        }

        public int NegaMaxScore
        {
            get
            {
                return MAX_SCORE - NbMoves;
            }
        }



        // GameState need 16 bytes in x86 mode  
        //  8 : base allocation
        //  8 : Status, NbMoves, W, S0, S1, S2, S3, S4 ( 1 byte each)
        //
        // Memory allocation is always rounded to next multiple of 4.
        //
        // In x64 mode (AnyCPU), it would be 24
        // 16 : base allocation
        //  8 : Status, NbMoves, W, S0, S1, S2, S3, S4 ( 1 byte each)

        // An object reference (pointer) is 4 bytes in x86 mode, 8 bytes in x64 mode


        public Pos PosWolf
        {
            get { return new Pos(W); }
        }

        public Pos[] PosSheep
        {
            get { return new Pos[] { new Pos(S0), new Pos(S1), new Pos(S2), new Pos(S3), new Pos(S4) }; }
        }

        public static GameState InitialGameState
        {
            get { return new GameState(0, new Pos(5, 0), new Pos[] { new Pos(0, 9), new Pos(2, 9), new Pos(4, 9), new Pos(6, 9), new Pos(8, 9) }); }
        }

        private GameState()
        {
        }

        public GameState MakeNewGameStateWolf(Pos posWolf)
        {
            GameState gs = new GameState();
            gs.NbMoves = (byte)(this.NbMoves + 1);
            gs.W = posWolf.ByteVal;
            gs.S0 = S0;
            gs.S1 = S1;
            gs.S2 = S2;
            gs.S3 = S3;
            gs.S4 = S4;

            return gs;
        }

        public GameState MakeNewGameStateSheep(byte[] sheep, byte olds, byte news)
        {
            byte[] newSheep = new byte[NB_SHEEP];

            bool shift = false;

            for (int i = 0, j = 0; i < NB_SHEEP; )
            {
                byte by = sheep[i++];

                if (by == olds)
                {
                    shift = true;
                    continue;
                }

                if (shift && news < by)
                {
                    newSheep[j++] = news;
                    shift = false;
                }

                newSheep[j++] = by;
            }

            if (shift)
                newSheep[NB_SHEEP - 1] = news;


            GameState gs = new GameState();
            gs.NbMoves = (byte)(this.NbMoves + 1);
            gs.W = W;
            gs.S0 = newSheep[0];
            gs.S1 = newSheep[1];
            gs.S2 = newSheep[2];
            gs.S3 = newSheep[3];
            gs.S4 = newSheep[4];

            return gs;
        }

        public void CheckStatus()
        {
            this.Status = GetStatus();
        }

        private GameStatus GetStatus()
        {
            if (WolfHasWon())
                return GameStatus.WolfWon;

            foreach (GameState gs in Play())
                return GameStatus.NotFinished;

            // no move
            return IsWolf ? GameStatus.SheepWon : GameStatus.WolfWon;
        }

        public IEnumerable<GameState> Play()
        {
            if (IsWolf)
            {
                foreach (Pos p in PosWolf.GetWolfMoves())
                {
                    byte w = p.ByteVal;

                    if (w != S0 && w != S1 && w != S2 && w != S3 && w != S4)
                        yield return MakeNewGameStateWolf(p);
                }
            }
            else
            {
                byte[] sheep = new byte[] { S0, S1, S2, S3, S4 };

                foreach (byte olds in sheep)
                {
                    foreach (Pos pNewSheep in new Pos(olds).GetSheepMoves())
                    {
                        byte news = pNewSheep.ByteVal;

                        if (news == W || news == S0 || news == S1 || news == S2 || news == S3 || news == S4)
                            continue;

                        yield return MakeNewGameStateSheep(sheep, olds, news);
                    }
                }
            }
        }

        public bool WolfHasWon()
        {
            //return new Pos(W).Y >= new Pos(S0).Y;
            return W / 5 <= S0 / 5;
        }

        public bool WolfWillWin()
        {
            Pos wolf = PosWolf;
            int px = wolf.X;

            int wy = wolf.Y + 1;
            int wxa;
            int wxb = -1;
            byte wb=0;

            if (px == 0)
            {
                wxa = px + 1;
            }
            else if (px == 9)
            {
                wxa = px - 1;
            }
            else
            {
                wxa = px - 1;
                wxb = px + 1;
                wb = new Pos( wxb, wy).ByteVal;
            }

            byte[] sheep = new byte[] { S0, S1, S2, S3, S4 };

            bool[] sheepPresent = new bool[Pos.MAX_POSITIONS];

            foreach (byte s in sheep)
                sheepPresent[s] = true;

            foreach (byte s in sheep)
            {
                Pos p = new Pos(s);

                int dy = p.Y - wy;

                if (dy < 0)
                    return true;

                int dx = wxa - p.X;
                if ((dx >= 0 ? dx : -dx) <= dy)
                {
                    if (wxb < 0 || sheepPresent[wb])
                        return false;

                    dx = wxb - p.X;
                    if ((dx >= 0 ? dx : -dx) <= dy)
                        return false;
                }
            }

            return true;
        }


        public GameState MakePlayerMove(Pos oldPos, Pos newPos)
        {
            GameState gs;

            if (IsWolf)
            {
                gs = MakeNewGameStateWolf(newPos);
            }
            else
            {
                byte[] sheep = new byte[] { S0, S1, S2, S3, S4 };
                gs = MakeNewGameStateSheep(sheep, oldPos.ByteVal, newPos.ByteVal);
            }

            gs.CheckStatus();
            return gs;
        }

        public Pos[] GetValidWolfMoves()
        {
            List<Pos> list = new List<Pos>();

            foreach (Pos p in PosWolf.GetWolfMoves())
            {
                byte by = p.ByteVal;
                if (by != S0 && by != S1 && by != S2 && by != S3 && by != S4)
                    list.Add(p);
            }

            return list.ToArray();
        }

        public Pos[] GetValidSheepMoves(Pos pOldSheep)
        {
            List<Pos> list = new List<Pos>();

            foreach (Pos p in pOldSheep.GetSheepMoves())
            {
                byte by = p.ByteVal;

                if (!(by == W || by == S0 || by == S1 || by == S2 || by == S3 || by == S4))
                    list.Add(p);
            }

            return list.ToArray();
        }

        public GameState(int nbMoves, Pos posWolf, Pos[] posSheep)
        {
            if (posSheep == null || posSheep.Length != NB_SHEEP)
                throw new Exception("Invalid number of sheep!");

            if (!posWolf.IsValid)
                throw new Exception("Invalid starting state (invalid wolf position)");

            foreach (Pos p in posSheep)
            {
                if (!p.IsValid)
                    throw new Exception("Invalid starting state : invalid sheep position " + p.ToString());

                if (p.Equals(posWolf))
                    throw new Exception("Invalid starting state : wolf at same position than Sheep " + p.ToString());
            }


            Pos[] sheep = new Pos[NB_SHEEP];
            posSheep.CopyTo(sheep, 0);
            Array.Sort<Pos>(sheep);

            for (int i = 0; i < NB_SHEEP - 1; ++i)
                if (sheep[i].Equals(sheep[i + 1]))
                    throw new Exception("Invalid starting state : several sheep at same position " + sheep[i].ToString());

            NbMoves = (byte)nbMoves;
            W = posWolf.ByteVal;
            S0 = sheep[0].ByteVal;
            S1 = sheep[1].ByteVal;
            S2 = sheep[2].ByteVal;
            S3 = sheep[3].ByteVal;
            S4 = sheep[4].ByteVal;

            CheckStatus();
        }

        public SheepState SheepState
        {
            get { return new SheepState(S0, S1, S2, S3, S4); }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GameState);
        }

        public bool Equals(GameState other)
        {
            if (other == null)
                return false;

            return this.W == other.W && this.S0 == other.S0 && this.S1 == other.S1 && this.S2 == other.S2 && this.S3 == other.S3 && this.S4 == other.S4;
        }


        // Compute hash as bit field
        //  
        //   6 : Wolf position  (0 .. 49)
        //   6 : Sheep Lowest   (0 .. 49) 
        //
        // For each remaining sheep (x 4)
        //   5 : offset to previous sheep - 1 (offset cannot be 0 so we can substract 1)
        public override int GetHashCode()
        {
            return W << 26 | S0 << 20 | (S1 - S0 - 1) << 15 | (S2 - S1 - 1) << 10 | (S3 - S2 - 1) << 5 | (S4 - S3 - 1);
        }
    }
}
