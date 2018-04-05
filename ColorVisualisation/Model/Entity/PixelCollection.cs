using ColorVisualisation.Model.Scoring;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ColorVisualisation.Model.Entity
{
    class PixelCollection : IEnumerable<Pixel>
    {
        private IList<Pixel> _pixels;

        public Pixel this[int key]
        {
            get { lock (_pixels) { return _pixels[key]; } }
            set { lock (_pixels) { _pixels[key] = value; } }
        }

        public IEnumerator<Pixel> GetEnumerator()
        {
            lock (_pixels)
            {
                return _pixels.GetEnumerator();
            }            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (_pixels)
            {
                return _pixels.GetEnumerator();
            }            
        }

        public PixelCollection(IList<Pixel> pixels)
        {
            _pixels = pixels;
        }

        public int Width { get; internal set; }

        public int Height { get; internal set; }
        
        public int AllPixelsCount { get { lock (this) { return Width * Height; } } }

        public int AverageBlue
        {
            get
            {
                lock (_pixels)
                {
                    return (int)_pixels.Average(pixel => pixel.Blue);
                }
            }
        }
        public int AverageGreen
        {
            get
            {
                lock (_pixels)
                {
                    return (int)_pixels.Average(pixel => pixel.Green);
                }
            }
        }
        public int AverageRed
        {
            get
            {
                lock (_pixels)
                {
                    return (int)_pixels.Average(pixel => pixel.Red);
                }
            }
        }

        public int Deviation
        {
            get
            {
                lock (_pixels)
                {
                    int averageBlue = AverageBlue;
                    int averageRed = AverageRed;
                    int averageGreen = AverageGreen;
                    return _pixels.Sum(pixel => 
                        Math.Abs(pixel.Blue - AverageBlue) + 
                        Math.Abs(pixel.Red - AverageRed) + 
                        Math.Abs(pixel.Green - AverageGreen));
                }
            }
        }

        public void OrderAscending()
        {
            lock (_pixels)
            {
                _pixels = _pixels.OrderBy(pixel => pixel.IndexGlobal).ToList();
            }            
        }

        public void OrderByBlue()
        {
            lock (_pixels)
            {
                int averageBlue = AverageBlue;
                _pixels = _pixels.OrderBy(pixel => Math.Abs(pixel.Blue - averageBlue)).ToList();
            }            
        }

        public void OrderByGreen()
        {
            lock (_pixels)
            {
                int averageGreen = AverageGreen;
                _pixels = _pixels.OrderBy(pixel => Math.Abs(pixel.Green - averageGreen)).ToList();
            }            
        }

        public void OrderByRed()
        {
            lock (_pixels)
            {
                int averageRed = AverageRed;
                _pixels = _pixels.OrderBy(pixel => Math.Abs(pixel.Red - averageRed)).ToList();
            }            
        }

        public void OrderByPoints()
        {
            lock (_pixels)
            {
                _pixels = _pixels.OrderByDescending(pixel => pixel.RankingPoints).ToList();
            }            
        }

        public void OrderByPointsAscending()
        {
            lock (_pixels)
            {
                _pixels = _pixels.OrderBy(pixel => pixel.RankingPoints).ToList();
            }            
        }

        public void AddPointsToValues(IScoringTable scoringTable)
        {
            lock (_pixels)
            {
                int pixelIndex = 0;
                foreach (var pixel in _pixels)
                {
                    pixel.RankingPoints += scoringTable.GetScore(_pixels.Count, pixelIndex);
                    pixelIndex++;
                }
            }            
        }
        public bool AreAllPixelsEqual()
        {
            lock (_pixels)
            {
                int averageBlue = AverageBlue;
                int averageRed = AverageRed;
                int averageGreen = AverageGreen;
                var equalPixelsCount = _pixels.Where(pixel => pixel.Blue == averageBlue &&
                                        pixel.Red == averageRed &&
                                        pixel.Green == averageGreen).ToList().Count();
                return _pixels.Count == equalPixelsCount;
            }            
        }

        public void RemoveWeakPixels(int pixelsToSelect)
        {
            lock (_pixels)
            {
                OrderByPointsAscending();
                for (int pixelIndex = pixelsToSelect; pixelIndex < _pixels.Count; pixelIndex++)
                {
                    _pixels[pixelIndex] = new Pixel()
                    {
                        Blue = 0,
                        Green = 0,
                        Red = 0,
                        Alpha = byte.MaxValue,
                        IndexRow = _pixels[pixelIndex].IndexRow,
                        IndexColumn = _pixels[pixelIndex].IndexColumn,
                        IndexGlobal = _pixels[pixelIndex].IndexGlobal,
                        RankingPoints = 0,
                    };
                }
            }                       
        }

        
    }
}
