using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WolfvsSheep
{
    public enum TestModel
    {
        /// <summary>
        /// Check if sheep depth is sufficient : Wolf runs deeper than sheep. 
        /// </summary>
        Check,

        /// <summary>
        /// Max performance (normal mode). Score is not optimal. Sheep win on step 75.
        /// </summary>
        MaxPerf,

        /// <summary>
        /// Perf Test. Score is not optimal
        /// </summary>
        Perf,

        /// <summary>
        /// Use deeper level for optimal score. Sheep win on step 71.
        /// </summary>
        BestScore
    }

    public static class Test
    {

        public static void Run(TestModel test, bool showResult)
        {
            int sheepDepth = 16;
            int sheepPrune = 10;
            int wolfDepth = sheepDepth;
            int wolfPrune = sheepPrune;

            switch (test)
            {
                case TestModel.Perf:
                    sheepDepth = wolfDepth = 20;
                    sheepPrune = wolfPrune = 14;
                    break;

                case TestModel.Check:
                    wolfDepth = 24;
                    wolfPrune = 18;
                    break;

                case TestModel.BestScore:
                    sheepDepth = wolfDepth = 24;
                    sheepPrune = wolfPrune = 18;
                    break;
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0} depth:{1} depthPrune:{2}", DateTime.Now.ToString("G"), sheepDepth, sheepPrune);
            sb.AppendLine();

            double tsTotal=0;
            double tsMax = 0;
            long peakMem = 0;
            int peakDict = 0;

            GameState gs = GameState.InitialGameState;

            for (; gs.Status == GameStatus.NotFinished; )
            {
                Solver solver = new Solver(gs);

                if (gs.IsWolf)
                    solver.Play(wolfDepth, wolfPrune);
                else
                    solver.Play(sheepDepth, sheepPrune);

                tsTotal += solver.TimeRun;
                
                if (solver.TimeRun > tsMax)
                    tsMax = solver.TimeRun;

                if (solver.PeakMemory > peakMem)
                    peakMem = solver.PeakMemory;

                if (solver.TmpDictCount > peakDict)
                    peakDict = solver.TmpDictCount;

                gs = solver.SelectedChoice;

                sb.Append(solver.Status);
                sb.AppendLine();
            }


            string result = string.Format("Done in {0:F2} sec  Max={1:F3}  PeakMem={2:N0}  PeakDict={3:N0} - Result:{4} {5}", tsTotal, tsMax, peakMem, peakDict, gs.Status, gs.Status == GameStatus.SheepWon ? "OK" : "FAIL");

            sb.Append(result);
            sb.AppendLine();
            sb.AppendLine();

            System.IO.File.AppendAllText("Test" + test.ToString() + ".log", sb.ToString());

            if (showResult)
                MessageBox.Show(result);
        }
    }
}
