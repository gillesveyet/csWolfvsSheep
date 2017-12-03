//References:
// An Introduction to Game Tree Algorithms : http://www.hamedahmadi.com/gametree/
// Negamax @CopyPasteCode (C++) http://www.copypastecode.com/22577/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;

namespace WolfvsSheep
{

    class Solver
    {
        const int MIN_VALUE = -int.MaxValue; // Do not use int.MinValue because -int.MinValue will cause an overflow. 

        private static Dictionary<SheepState, int> _dictSheep;
        private Dictionary<GameState, int> _dictTmp;
        private GameState _root;
        private bool _cancel;
        private int _depthPrune;
        private int _depthMax;

        //stats
        public int TmpDictCount;
        public double TimeRun;
        public long PeakMemory;
        private int Score;
        private int NbFoundInDictTmp;
        private int NbFoundInDictSheep;
        private int NbIterations;
        private int NbPrune;
        private int NbSkipMaxNegamaxScore;

        //Result
        public bool Canceled { get; private set; }
        public GameState SelectedChoice { get; private set; }


        public Solver(GameState gs)
        {
            _root = gs;

            if (_dictSheep == null)
                InitDictSheep();
        }

        //Initialize dictionary with sheep best moves to speed up the negamax search.
        static void InitDictSheep()
        {
            _dictSheep = new Dictionary<SheepState, int>();

            GameState gs = GameState.InitialGameState; // start position

            //We skip wolf moves so we start with nbmove = 1 (sheep turn) and continue with: nbMoves += 2 (skip wolf turn)
            for (int nbMoves = 1; nbMoves < 60; nbMoves += 2)
            {
                gs.NbMoves = (byte)nbMoves; // adjust nb moves

                //The Play function is coded so the first sheep move is always the best one provided that the wolf does not prevent the sheep to move into that position.
                //So call Play enumarator once. 
                IEnumerator<GameState> enumerator = gs.Play().GetEnumerator();
                enumerator.MoveNext();
                gs = enumerator.Current;

                _dictSheep.Add(gs.SheepState, 100 - nbMoves);  // Store sheep move : score is positive because it is a negamax score (sheep are winning).
            }
        }

        public void Play(int depth)
        {
            Play(depth, 10);
        }

        public void Play(int depth, int depthPrune)
        {
            _dictTmp = new Dictionary<GameState, int>(150000);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            _depthMax = _root.NbMoves + depth;
            _depthPrune = _root.NbMoves + depthPrune;

            SelectedChoice = null;
            int max = MIN_VALUE;

            foreach (GameState gsChild in _root.Play())
            {
                int val = -NegaMax(gsChild, MIN_VALUE, -max);
                if (val > max)
                {
                    max = val;
                    SelectedChoice = gsChild;
                }
            }

            Debug.Assert(SelectedChoice != null, "SelectedChoice == null - Game over before Play");

            SelectedChoice.CheckStatus();

            Score = _root.IsWolf ? max : -max;
            TmpDictCount = _dictTmp.Count;
            PeakMemory = GC.GetTotalMemory(false);
            _dictTmp = null;

            TimeRun = watch.Elapsed.TotalSeconds;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }


        public void Cancel()
        {
            _cancel = true;
        }

        public string Status
        {
            get
            {
                return string.Format("Moves={0} Score={1} Nb={2:N0} Time={3:F2} Prune={4:N0} DictTmp={5:N0} FoundTmp={6:N0} FoundSheep={7:N0} SkipMaxNegamaxScore={8:N0} PeakMemory={9:N0}", _root.NbMoves, Score, NbIterations, TimeRun, NbPrune, TmpDictCount, NbFoundInDictTmp, NbFoundInDictSheep, NbSkipMaxNegamaxScore, PeakMemory);
            }
        }

        int NegaMax(GameState gsParent, int alpha, int beta)
        {
            ++NbIterations;

            if (_cancel)
            {
                Canceled = true;
                return 0;
            }

            bool wolfTurn = gsParent.IsWolf;
            bool bDepthMax = gsParent.NbMoves >= _depthMax;
            bool okSkip = gsParent.NbMoves - 1 <= _depthPrune;

            int max = int.MinValue;

            foreach (GameState gsChild in gsParent.Play())
            {
                int x;

                if (!wolfTurn && _dictSheep.TryGetValue(gsChild.SheepState, out x) && !gsChild.WolfHasWon())
                {
                    // sheep played a "best" move: use score from dictionary
                    ++NbFoundInDictSheep;
                }
                else
                {
                    if (wolfTurn)
                    {
                        if (gsChild.WolfWillWin())
                        {
                            int score = gsChild.NegaMaxScore;
                            _dictTmp[gsParent] = -score;   //negate the score before store so it is not necessary to do this after retrieving from dictionary
                            return score;
                        }
                    }

                    if (_dictTmp.TryGetValue(gsChild, out x))
                    {
                        ++NbFoundInDictTmp;
                    }
                    else
                    {
                        if (bDepthMax)  //optimisation: eliminate last recursion
                            x = 0;
                        else if (okSkip && gsChild.NegaMaxScore <= alpha)
                        {
                            ++NbSkipMaxNegamaxScore;
                            x = gsChild.NegaMaxScore;
                        }
                        else
                            x = -NegaMax(gsChild, -beta, -alpha); //Note the "-" before "NegaMax"
                    }
                }


                if (x > max)
                    max = x;

                if (x > alpha)
                    alpha = x;

                //  OK to prune when NbMoves == PRUNE_DEPTH because nothing will be stored in dictionary if this tree is pruned
                if (gsParent.NbMoves <= _depthPrune && alpha >= beta)
                {
                    ++NbPrune;
                    return alpha;               // prune tree
                }
            }

            if (max == int.MinValue)   // no moves : game over 
            {
                int score = -gsParent.NegaMaxScore;
                _dictTmp[gsParent] = -score;        //negate the score before store so it is not necessary to do this after retrieving from dictionary
                return score;
            }

            if (gsParent.NbMoves >= _depthPrune)
                _dictTmp[gsParent] = -max;          //negate the score before store so it is not necessary to do this after retrieving from dictionary

            return max;
        }
    }
}
