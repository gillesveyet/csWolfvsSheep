using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WolfvsSheep
{

    static class Program
    {
        public static bool ExpertMode { get; private set; }

        public static GameState GameStateSpecialStart = new GameState(18, new Pos(2, 7), new Pos[] { new Pos(3, 8), new Pos(1, 8), new Pos(6, 7), new Pos(8, 7), new Pos(3, 6) });

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool testPerf = false;
            bool testMaxPerf = false;
            bool testCheck = false;
            bool testBestScore = false;

            foreach (string arg in args)
            {
                if (arg.Equals("expert", StringComparison.OrdinalIgnoreCase))
                    ExpertMode = true;

                if (arg.Equals("Test:MaxPerf", StringComparison.OrdinalIgnoreCase))
                    testMaxPerf = true;

                if (arg.Equals("Test:Perf", StringComparison.OrdinalIgnoreCase))
                    testPerf = true;

                if (arg.Equals("Test:Check", StringComparison.OrdinalIgnoreCase))
                    testCheck = true;

                if (arg.Equals("Test:BestScore", StringComparison.OrdinalIgnoreCase))
                    testBestScore = true;

                if (arg.Equals("Test:All", StringComparison.OrdinalIgnoreCase))
                {
                    testMaxPerf = true;
                    testCheck = true;
                    testBestScore = true;
                    testPerf = true;
                }
            }

            if (!(testMaxPerf || testCheck || testBestScore || testPerf))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
                return;
            }

            if (testMaxPerf)
                Test.Run(TestModel.MaxPerf, false);

            if (testPerf)
                Test.Run(TestModel.Perf, false);

            if (testCheck)
                Test.Run(TestModel.Check, false);

            if (testBestScore)
                Test.Run(TestModel.BestScore, false);
        }
    }
}
