using System;
using System.Collections.Generic;
using System.Linq;
using ColorVisualisation.Model.Scoring;
using ColorVisualisation.Model.Helper.Extension;

namespace ColorVisualisation.Model.Entity
{
    class PixelCollection
    {
        public IList<Pixel> Pixels { get; set; }

        public int Width { get; internal set; }

        public int Height { get; internal set; }
        
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
                return (int)Pixels.Average(pixel => pixel.Green );
            }
        }
        public int AverageRed
        {
            get
            {
                return (int)Pixels.Average(pixel => pixel.Red);
            }
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

        public void OrderByPointsAscending()
        {
            Pixels = Pixels.OrderBy(pixel => pixel.RankingPoints).ToList();
        }

        public void Shuffle()
        {
            Pixels.Shuffle();
        }

        public void AddPointsToValues(IScoringTable scoringTable)
        {
            int pixelIndex = 0;
            foreach (var pixel in Pixels)
            {
                pixel.RankingPoints += scoringTable.GetScore(Pixels.Count, pixelIndex);
                pixelIndex++;
            }
        }
        public bool AreAllPixelsEqual()
        {
            var pis = Pixels.Where(pixel => pixel.Blue == AverageBlue &&
                                    pixel.Red == AverageRed &&
                                    pixel.Green == AverageGreen).ToList();
            var count = pis.Count();
            return Pixels.Count == count;
        }

        public void RemoveWeakPixels(int pixelsToSelect)
        {
            OrderByPointsAscending();
            for (int pixelIndex = pixelsToSelect; pixelIndex < Pixels.Count; pixelIndex++)
            {
                Pixels[pixelIndex] = new Pixel()
                {
                    Blue = 0,
                    Green = 0,
                    Red = 0,
                    Alpha = byte.MaxValue,
                    IndexRow = Pixels[pixelIndex].IndexRow,
                    IndexColumn = Pixels[pixelIndex].IndexColumn,
                    IndexGlobal = Pixels[pixelIndex].IndexGlobal,
                    RankingPoints = 0,
                };
            }            
        }
    }
}
