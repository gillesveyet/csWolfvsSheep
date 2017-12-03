using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace WolfvsSheep
{
    class CheckerPanel : Panel
    {
        // Normal panel size is 600 x 600 but this is only true when when using "Smaller - 100 % (default)" Windows resolution.
        // So we have to compute XMAG & YMAG. See OnClientSizeChanged
        // Form "AutoScaleMode" property was set (in design) to "Dpi" because it works better then default "Font" mode. "Inherit" and "None" modes do not work at all.
        float XMAG;
        float YMAG;

        static Image _checker, _sheep, _wolf;
        static Pen _penSelect, _penValid;

        private bool IsPlayEnabled;
        private GameState _gameState;

        private Pos? _selectedPiece;
        private Pos[] _validMoves;

        public delegate void SelectPieceHandler(object sender, SelectPieceArgs e);
        public event SelectPieceHandler SelectPiece;

        public delegate void MovePieceHandler(object sender, MovePieceArgs e);
        public event MovePieceHandler MovePiece;


        static CheckerPanel()
        {
            _checker = GetImageRessource("Checker");
            _sheep = GetImageRessource("White");
            _wolf = GetImageRessource("Black");

            _penSelect = new Pen(Brushes.Black, 2);
            _penValid = new Pen(Brushes.Red, 2);
        }

        private static Image GetImageRessource(string name)
        {
            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            return Image.FromStream(a.GetManifestResourceStream(a.GetName().Name + ".Images." + name + ".png"));
        }


        public CheckerPanel()
        {
            _gameState = GameState.InitialGameState;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            XMAG = this.ClientSize.Width / 10.0F;
            YMAG = this.ClientSize.Height / 10.0F;
        }

        public void SetPositions(GameState gs, bool enablePlay)
        {
            _selectedPiece = null;
            _validMoves = null;

            this.IsPlayEnabled = enablePlay;
            this._gameState = gs;

            if (enablePlay && gs.IsWolf)
                UpdateSelected(gs.PosWolf, false);

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics gr;

            if (e != null)
                gr = e.Graphics;
            else
                gr = this.CreateGraphics();

            gr.DrawImageUnscaled(_checker, 0, 0);

            gr.DrawImageUnscaled(_wolf, Pos2Point(_gameState.PosWolf));

            foreach (Pos pt in _gameState.PosSheep)
                gr.DrawImageUnscaled(_sheep, Pos2Point(pt));

            if (Program.ExpertMode)
            {
                for (int i = 0; i < _gameState.PosSheep.Length; ++i)
                    gr.DrawString((i + 1).ToString(), DefaultFont, Brushes.Blue, Pos2Point(_gameState.PosSheep[i]));
            }

            if (_selectedPiece != null)
                DrawSelected(gr, _selectedPiece.Value, _penSelect);

            if (_validMoves != null)
                foreach (Pos p in _validMoves)
                    DrawSelected(gr, p, _penValid);

            if (e == null)
                gr.Dispose();
        }

        Point Pos2Point(Pos p)
        {
            return new Point((int)(p.X * XMAG), (int)((p.Y * YMAG)));
        }

        private void DrawSelected(Graphics gr, Pos p, Pen pen)
        {
            gr.DrawRectangle(pen, new Rectangle(Pos2Point(p), new Size((int)XMAG, (int)YMAG)));
        }


        private void UpdateSelected(Pos? selected, bool refresh)
        {
            if (_selectedPiece == null && selected == null)
                return;

            if (selected != null && _selectedPiece != null && selected.Value.Equals(_selectedPiece.Value))
            {
                //click on selected piece
                //  -wolf : do nothing
                //  -sheep : unselect
                if (_gameState.IsWolf)
                    return;

                _selectedPiece = null;
            }
            else
                _selectedPiece = selected;

            if (_selectedPiece != null)
            {
                if (SelectPiece != null)
                {
                    SelectPieceArgs e = new SelectPieceArgs(_selectedPiece.Value);
                    SelectPiece(this, e);
                    _validMoves = e.ValidMoves;
                }
            }
            else
                _validMoves = null;

            if (refresh)
                Invalidate();
        }

        private bool IsSelectedValid(Pos selected)
        {
            if (!selected.IsValid)
                return false;

            if (_gameState.IsWolf)
                return _gameState.PosWolf.Equals(selected);
            else
            {
                foreach (Pos p in _gameState.PosSheep)
                    if (p.Equals(selected))
                        return true;

                return false;
            }
        }

        private bool IsMoveValid(Pos selected)
        {
            if (_validMoves == null)
                return false;

            foreach (Pos p in _validMoves)
                if (p.Equals(selected))
                    return true;

            return false;
        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!IsPlayEnabled)
                return;

            Pos p = new Pos((int)( e.X / XMAG), (int)(e.Y / YMAG));

            if (IsSelectedValid(p))
                UpdateSelected(p, true);
            else if (_selectedPiece != null && IsMoveValid(p))
            {
                if (MovePiece != null)
                    MovePiece(this, new MovePieceArgs(_selectedPiece.Value, p));
            }
        }
    }

    class SelectPieceArgs : EventArgs
    {
        public SelectPieceArgs(Pos selectedPiece) { SelectedPiece = selectedPiece; }

        public Pos SelectedPiece { get; private set; }
        public Pos[] ValidMoves { get; set; }
    }

    class MovePieceArgs : EventArgs
    {
        public MovePieceArgs(Pos oldPos, Pos newPos) { OldPos = oldPos; NewPos = newPos; }

        public Pos OldPos { get; private set; }
        public Pos NewPos { get; private set; }
    }

}
