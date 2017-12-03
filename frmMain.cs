using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WolfvsSheep
{
    public partial class frmMain : Form
    {
        List<GameState> _history;
        Solver _solver;

        GameState _gs
        {
            get { return _history[_history.Count - 1]; }
        }

        bool IsSinglePlayer { get { return mnuSinglePlayer.Checked; } }
        bool IsPlayerWolf { get { return mnuPlayWolf.Checked; } }

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            mnuLevel3.Checked = true;
            mnuSinglePlayer.Checked = true;
            mnuPlayWolf.Checked = true;

            mnuDiagnostic.Visible = Program.ExpertMode;

            Reset();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelAsyncPlay();
        }

        #region Status
        void DisplayStatus(string msg)
        {
            this.toolStripStatusLabel1.Text = msg;
        }

        void DisplayStatus(string format, params object[] args)
        {
            DisplayStatus(string.Format(format, args));
        }

        void ClearStatus()
        {
            DisplayStatus(null);
        }
        #endregion


        void ShowVictory()
        {
            string msg;

            if (IsSinglePlayer)
            {
                if ((_gs.Status == GameStatus.WolfWon) == IsPlayerWolf)
                    msg = "Vous avez gagné !";
                else
                    msg = "Vous avez perdu !";
            }
            else
            {
                if (_gs.Status == GameStatus.SheepWon)
                    msg = "Les agneaux ont gagné !";
                else
                    msg = "Le loup a gagné !";
            }

            DisplayStatus(msg);
            MessageBox.Show(msg);
        }

        void DisplayInfo()
        {
            if (Program.ExpertMode)
                return;

            if (_solver != null)
                DisplayStatus("L'ordinateur réfléchit...");
            else if (!_gs.IsGameOver)
            {
                if (_gs.IsWolf)
                    DisplayStatus("Loup: Sélectionnez la case où vous voulez déplacer le loup");
                else
                    DisplayStatus("Agneaux: Sélectionnez un pion (blanc) puis cliquez sur la case où vous voulez le déplacer");
            }

        }

        private void OnComputerPlayDone(bool autoPlayNext)
        {
            if (_solver == null || _solver.Canceled)
                return;

            if (Program.ExpertMode)
            {
                string status = _solver.Status;
                DisplayStatus(status);
                System.Windows.Forms.Clipboard.SetText(status);
            }

            GameState gs = _solver.SelectedChoice;

            _history.Add(gs);
            this.panelChecker.SetPositions(gs, !gs.IsGameOver);

            _solver = null;
            UpdateContext();

            if (gs.IsGameOver)
                ShowVictory();
            else if (autoPlayNext)
                ComputerPlay(false);
        }

        private void ComputerPlay(bool autoPlayNext)
        {
            _solver = new Solver(_gs);

            UpdateContext();

            new Thread(delegate()
                {
                    _solver.Play(GetCpuLevel());
                    this.Invoke(new MethodInvoker(delegate() { OnComputerPlayDone(autoPlayNext); }));
                }
                ).Start();
        }

        private void CancelAsyncPlay()
        {
            if (_solver != null)
            {
                _solver.Cancel();
                _solver = null;
            }
        }

        int GetCpuLevel()
        {
            int level;

            if (mnuLevel1.Checked)
                level = 6;
            else if (mnuLevel2.Checked)
                level = 12;
            else
                level = 16;

            return level;
        }


        void Reset()
        {
            _history = new List<GameState>();
            _history.Add(mnuSpecialStart.Checked ? Program.GameStateSpecialStart : GameState.InitialGameState);

            bool computer = IsSinglePlayer && !IsPlayerWolf;

            this.panelChecker.SetPositions(_gs, !computer);

            UpdateContext();

            if (computer)
                ComputerPlay(false);
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && mnuNew.Enabled)
            {
                mnuNew.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F3 && mnuBack.Enabled)
            {
                mnuBack.PerformClick();
                e.Handled = true;
            }
        }

        private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space && mnuLetComputerPlay.Enabled)
            {
                mnuLetComputerPlay.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyChar == (char)Keys.Back && mnuBack.Enabled)
            {
                mnuBack.PerformClick();
                e.Handled = true;
            }
        }

        private void panelChecker_SelectPiece(object sender, SelectPieceArgs e)
        {
            if (_gs.IsWolf)
                e.ValidMoves = _gs.GetValidWolfMoves();
            else
                e.ValidMoves = _gs.GetValidSheepMoves(e.SelectedPiece);
        }

        private void panelChecker_MovePiece(object sender, MovePieceArgs e)
        {
            GameState gs = _gs.MakePlayerMove(e.OldPos, e.NewPos);

            _history.Add(gs);
            this.panelChecker.SetPositions(gs, !IsSinglePlayer && !gs.IsGameOver);

            if (gs.IsGameOver)
            {
                UpdateContext();
                ShowVictory();
            }
            else if (mnuSinglePlayer.Checked)
                ComputerPlay(false);
            else
                UpdateContext();
        }

        private bool IsInitial
        {
            get { return _gs.NbMoves < 2; }
        }

        private void UpdateContext()
        {
            DisplayInfo();

            bool busy = _solver != null;
            bool initial = IsInitial;

            //UseWaitCursor = false does not work as expected : mouse cursor is not restored until mouse is moved.
            //this.panelChecker.UseWaitCursor = busy;
            this.panelChecker.Cursor = busy ? Cursors.WaitCursor : Cursors.Default;

            mnuLetComputerPlay.Enabled = !busy && Program.ExpertMode && !_gs.IsGameOver;

            mnuOptions.Enabled = !busy && (initial || Program.ExpertMode);
            mnuLevel.Enabled = IsSinglePlayer;
            mnuPlayWolf.Visible = IsSinglePlayer;
            mnuPlaySheep.Visible = IsSinglePlayer;

            mnuBack.Enabled = !busy && !initial && (IsSinglePlayer || Program.ExpertMode);
            mnuHelp.Enabled = !busy;
            mnuNew.Enabled = !busy && !initial;
            mnuQuit.Enabled = !busy;
        }

        private void mnuBack_Click(object sender, EventArgs e)
        {
            if (!IsSinglePlayer || IsPlayerWolf != _gs.IsWolf)
                _history.RemoveAt(_history.Count - 1);          // Remove last move when sheep loose on their own move OR [ 2 Players mode && expert mode ]
            else
                _history.RemoveRange(_history.Count - 2, 2);    // Standard case : remove both player move and computer move

            this.panelChecker.SetPositions(_gs, true);
            UpdateContext();
        }

        private void mnuSinglePlayer_Click(object sender, EventArgs e)
        {
            mnuTwoPlayers.Checked = false;
            Reset();
        }

        private void mnuTwoPlayers_Click(object sender, EventArgs e)
        {
            mnuSinglePlayer.Checked = false;

            if (Program.ExpertMode)
                UpdateContext();
            else
                Reset();
        }

        private void mnuPlayWolf_Click(object sender, EventArgs e)
        {
            mnuPlaySheep.Checked = false;
            Reset();
        }

        private void mnuPlaySheep_Click(object sender, EventArgs e)
        {
            mnuPlayWolf.Checked = false;
            Reset();
        }

        private void mnuLevel_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mnu = sender as ToolStripMenuItem;

            if (mnu == null)
                return;

            foreach (ToolStripMenuItem ts in new ToolStripMenuItem[] { mnuLevel1, mnuLevel2, mnuLevel3 })
                if (ts != mnu)
                    ts.Checked = false;
        }


        private void mnuQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quitter le jeu ?", this.Text, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void mnuHelp_Click(object sender, EventArgs e)
        {
            Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string sversion = string.Format("V{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build);

            MessageBox.Show(this, "Le loup et les agneaux " + sversion + "  par Gilles Veyet", this.Text);
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            if (_gs.Status != GameStatus.NotFinished || MessageBox.Show("Abandonner la partie en cours et recommencer une nouvelle partie ?", this.Text, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Reset();
            }
        }

        private void mnuPerformanceTest_Click(object sender, EventArgs e)
        {
            Test.Run(TestModel.MaxPerf, true);
        }

        private void mnuSpecialStart_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void mnuLetComputerPlay_Click(object sender, EventArgs e)
        {
            ComputerPlay(IsSinglePlayer);
        }
    }
}
