using ColorVisualisation.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorVisualisation.Model.Reporting
{
    class ReportingManager
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int AllPixels { get; set; }
        public string ScoringType { get; set; }
        public string CrossoverType { get; set; }
        public string MutationType { get; set; }
        public int MutationRate { get; set; }
        IDictionary<int, int> PixelsDeviationByTurn { get; set; } = new Dictionary<int, int>();

        public void TurnReport(int turn, int deviation)
        {
            if (!PixelsDeviationByTurn.ContainsKey(turn))
                PixelsDeviationByTurn.Add(turn, deviation);
        }

        public void SaveToFile(string pathFile)
        {
            using (var writer = new StreamWriter(pathFile))
            {
                writer.WriteLine(Resources.Height + "=" + Height);
                writer.WriteLine(Resources.Width + "=" + Width);
                writer.WriteLine(Resources.AllPixelsCount + "=" + AllPixels);
                writer.WriteLine(Resources.ScoringType + "=" + ScoringType);
                writer.WriteLine(Resources.CrossoverType + "=" + CrossoverType);
                writer.WriteLine(Resources.MutationType + "=" + MutationType);
                writer.WriteLine(Resources.MutationRate + "=" + MutationRate);
                foreach(var turnDeviation in PixelsDeviationByTurn)
                {
                    writer.WriteLine(turnDeviation.Key + ";" + turnDeviation.Value);
                }               
            }
        }

        public void ShowDialogAndSave()
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                FileName = Resources.DialogDefaultFileName,
                DefaultExt = Resources.DialogExtension,
                Filter = Resources.DialogFilter
            };

            var result = dialog.ShowDialog();

            if (result == true)
            {
                SaveToFile(dialog.FileName);
            }
        }
    }
}
