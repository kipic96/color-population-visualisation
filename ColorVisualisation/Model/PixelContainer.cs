using ColorVisualisation.Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ColorVisualisation.Model
{
    class PixelContainer
    {
        /// <summary>
        /// Container for blue pixels values.
        /// First Pair (key) contains in row (first position) and column (second position) of a pixel in bitmap.
        /// Second Pair (value) contains blue pixel value (first position) and points in ranking of a selection (second position).
        /// </summary>
        public IDictionary<Pair<int, int>, Pair<int, int>> BluePixels { get; set; }
        /// <summary>
        /// Container for green pixels values.
        /// First Pair (key) contains in row (first position) and column (second position) of a pixel in bitmap.
        /// Second Pair (value) contains green pixel value (first position) and points in ranking of a selection (second position).
        /// </summary>
        public IDictionary<Pair<int, int>, Pair<int, int>> GreenPixels { get; set; }
        /// <summary>
        /// Container for red pixels values.
        /// First Pair (key) contains in row (first position) and column (second position) of a pixel in bitmap.
        /// Second Pair (value) contains red pixel value (first position) and points in ranking of a selection (second position).
        /// </summary>
        public IDictionary<Pair<int, int>, Pair<int, int>> RedPixels { get; set; }

        public int Width { get; }

        public int Height { get; }
        
        public int AllPixels { get; }

        public int AverageBlue
        {
            get
            {
                return (int)BluePixels.Average(x => x.Value.First);
            }
        }
        public int AverageGreen
        {
            get
            {
                return (int)GreenPixels.Average(x => x.Value.First);
            }
        }
        public int AverageRed
        {
            get
            {
                return (int)RedPixels.Average(x => x.Value.First);
            }
        }

        public PixelContainer(byte[,,] rawPixels, int width, int height)
        {
            Width = width;
            Height = height;
            AllPixels = width * height;
            BluePixels = new Dictionary<Pair<int, int>, Pair<int, int>>();
            GreenPixels = new Dictionary<Pair<int, int>, Pair<int, int>>();
            RedPixels = new Dictionary<Pair<int, int>, Pair<int, int>>();
            int pixelIndex = 0;
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    BluePixels.Add(new Pair<int, int>(row, col),
                        new Pair<int, int>(rawPixels[row, col, 0], 0));
                    GreenPixels.Add(new Pair<int, int>(row, col),
                        new Pair<int, int>(rawPixels[row, col, 1], 0));
                    RedPixels.Add(new Pair<int, int>(row, col),
                        new Pair<int, int>(rawPixels[row, col, 2], 0));
                    pixelIndex++;
                }
            }
        }

        public void CreateRanking()
        {
            var orderedBluePixels = BluePixels.OrderBy(bluePixel => Math.Abs(bluePixel.Value.First - AverageBlue)).
                ToDictionary(bluePixel => bluePixel.Key, bluePixel => bluePixel.Value);
            var scoringTable = new SelectionScoringTable();
            int place = 0;
            foreach (var bluePixel in orderedBluePixels)
            {
                bluePixel.Value.Second += scoringTable.GetScore(place);
                place++;
            }

            /*_bluePixels = _bluePixels.OrderBy(x => Math.Abs(x - _averageBlue)).ToArray();
            _greenPixels.OrderBy(x => Math.Abs(x - _averageGreen)).ToArray();
            _redPixels.OrderBy(x => Math.Abs(x - _averageRed)).ToArray();*/
        } 
    }
}
