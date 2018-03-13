using System;
using System.Collections.Generic;
using System.Linq;

namespace ColorVisualisation.Model
{
    class PixelContainer
    {
        private PixelContainer pixelContainer;

        public IList<Pixel> Pixels { get; set; }

        public int Width { get; }

        public int Height { get; }
        
        public int AllPixelsCount { get { return Width * Height; } }

        public int AverageBlue
        {
            get
            {
                return (int)Pixels.Average(pixel => pixel.Blue);
            }
        }
        public int AverageGreen
        {
            get
            {
                return (int)Pixels.Average(pixel => pixel.Green);
            }
        }
        public int AverageRed
        {
            get
            {
                return (int)Pixels.Average(pixel => pixel.Red);
            }
        }

        public PixelContainer(byte[,,] rawPixels, int width, int height)
        {
            Width = width;
            Height = height;
            Pixels = new List<Pixel>();
            int pixelIndex = 0;
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    Pixels.Add(new Pixel()
                    {
                        Blue = rawPixels[row, col, 0],
                        Green = rawPixels[row, col, 1],
                        Red = rawPixels[row, col, 2],
                        Alpha = rawPixels[row, col, 3],
                        IndexGlobal = pixelIndex,
                        IndexRow = row,
                        IndexColumn = col,
                        RankingPoints = 0,
                    });
                    pixelIndex++;
                }
            }
        }

        public PixelContainer(PixelContainer pixelContainer)
        {
            this.pixelContainer = pixelContainer;
        }

        public void OrderAscending()
        {
            Pixels = Pixels.OrderBy(pixel => pixel.IndexGlobal).ToList();
        }

        public void OrderByBlue()
        {
            Pixels = Pixels.OrderBy(pixel => Math.Abs(pixel.Blue - AverageBlue)).ToList();
        }

        public void OrderByGreen()
        {
            Pixels = Pixels.OrderBy(pixel => Math.Abs(pixel.Green - AverageGreen)).ToList();
        }

        public void OrderByRed()
        {
            Pixels = Pixels.OrderBy(pixel => Math.Abs(pixel.Red - AverageRed)).ToList();
        }

        public void OrderByPoints()
        {
            Pixels = Pixels.OrderByDescending(pixel => pixel.RankingPoints).ToList();
        }

        public void AddPointsToValues(SelectionScoringTable scoringTable)
        {
            int pixelIndex = 0;
            foreach (var pixel in Pixels)
            {
                pixel.RankingPoints += scoringTable.GetScore(pixelIndex);
                pixelIndex++;
            }
        }

        public PixelContainer GetTopPixels(int pixelsToSelect)
        {
            var topPixels = new PixelContainer(this);
            topPixels.OrderByPoints();
            topPixels.RemoveRange(pixelsToSelect);
            return topPixels;         
        }

        private void RemoveRange(int pixelsToSelect)
        {
            // TODO select only best pixels
            //Pixels = (Pixels as List<Pixel>).RemoveRange(pixelsToSelect - 1, Pixels.Count - pixelsToSelect);
        }
    }
}
