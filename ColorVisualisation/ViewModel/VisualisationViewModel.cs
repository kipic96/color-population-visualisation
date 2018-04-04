using ColorVisualisation.Model;
using ColorVisualisation.ViewModel.Base;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ColorVisualisation.Properties;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ColorVisualisation.Model.Helper.Generator;
using ColorVisualisation.Model.Helper.Conversion;
using ColorVisualisation.Model.Entity;
using ColorVisualisation.Model.Crossing;
using ColorVisualisation.Model.Scoring;
using System.Threading;
using System.Windows;
using ColorVisualisation.Model.Reporting;
using Microsoft.Win32;

namespace ColorVisualisation.ViewModel
{
    class VisualisationViewModel : BaseViewModel
    {
        #region Properties And Fields

        private GeneticManager _geneticManager;

        private ReportingManager _reportingManager;

        private int _valuesInPixel = int.Parse(Resources.NumberOfValuesInPixel);

        private PixelCollection _pixels;
        public PixelCollection PixelCollection
        {
            get { return _pixels; }
            set
            {
                _pixels = value;
                PixelsImage = BitmapConverter.ToBitmap(_pixels);
                RaisePropertyChanged(nameof(PixelsDeviation));
                IsBitmapReady = true;
            }
        }

        private WriteableBitmap _pixelsImage;
        public WriteableBitmap PixelsImage
        {
            get { return _pixelsImage; }
            set
            {
                _pixelsImage = value;
                RaisePropertyChanged(nameof(PixelsImage));
            }
        }

        private WriteableBitmap _averageColor;
        public WriteableBitmap AverageColor
        {
            get { return _averageColor; }
            set
            {
                _averageColor = value;
                RaisePropertyChanged(nameof(AverageColor));
            }
        }

        #region Threading 

        private BackgroundWorker _backgroundWorker;

        #endregion

        #region View Based Properties

        private int _width = int.Parse(Resources.BitmapWidth);
        public int Width
        {
            get { return _width; }
            set
            {
                _width = NumberConversion.ToEvenNumber(value);
                IsSizeChanged = true;
                RaisePropertyChanged(nameof(Width));
                RaisePropertyChanged(nameof(AllPixelsCount));
                RaisePropertyChanged(nameof(PixelsToSelect));
            }
        }
        private int _height = int.Parse(Resources.BitmapHeight);
        public int Height
        {
            get { return _height; }
            set
            {
                _height = NumberConversion.ToEvenNumber(value);
                IsSizeChanged = true;
                RaisePropertyChanged(nameof(Height));
                RaisePropertyChanged(nameof(AllPixelsCount));
                RaisePropertyChanged(nameof(PixelsToSelect));
            }
        }

        public int AllPixelsCount
        {
            get { return Height * Width; }
            set
            {
                RaisePropertyChanged(nameof(AllPixelsCount));
                RaisePropertyChanged(nameof(PixelsToSelect));
            }
        }

        public int PixelsToSelect
        {
            get { return NumberConversion.ToEvenNumber((int)(Width * Height / 2)); }
            set { RaisePropertyChanged(nameof(PixelsToSelect)); }
        }

        public int PixelsDeviation
        {
            get
            { 
                if (PixelCollection != null)
                    return PixelCollection.Deviation;
                return 0;
            }
            set { RaisePropertyChanged(nameof(PixelsDeviation)); }
        }

        private int _howManyChildren = int.Parse(Resources.HowManyChildrenDefault);
        public int HowManyChildren
        {
            get { return _howManyChildren; }
            set
            {
                _howManyChildren = value;
                RaisePropertyChanged(nameof(HowManyChildren));
            }
        }

        private int _turnsNumber;
        public int TurnsNumber
        {
            get { return _turnsNumber; }
            set
            {
                _turnsNumber = value;
                RaisePropertyChanged(nameof(TurnsNumber));
            }
        }

        private string _currentScoringType = Resources.LinearScoring;
        public string CurrentScoringType
        {
            get { return _currentScoringType; }
            set
            {
                _currentScoringType = value;
                RaisePropertyChanged(nameof(CurrentScoringType));
            }
        }

        private string _currentMutationType = Resources.ValueMutation;
        public string CurrentMutationType
        {
            get { return _currentMutationType; }
            set
            {
                _currentMutationType = value;
                RaisePropertyChanged(nameof(CurrentMutationType));
            }
        }

        private string _currentCrossingType = Resources.AverageCrossing;
        public string CurrentCrossingType
        {
            get { return _currentCrossingType; }
            set
            {
                _currentCrossingType = value;
                RaisePropertyChanged(nameof(CurrentCrossingType));
            }
        }

        private int _currentMutationRate = 0;
        public int CurrentMutationRate
        {
            get { return _currentMutationRate; }
            set
            {
                _currentMutationRate = value;
                RaisePropertyChanged(nameof(CurrentMutationRate));
            }
        }

        public ICollection<string> ScoringTypes { get; set; } = new Collection<string>()
        {
            Resources.AdjustedScoring,
            Resources.LinearScoring,
        };

        public ICollection<string> MutationTypes { get; set; } = new Collection<string>()
        {
            Resources.ValueMutation,
            Resources.BitMutation,
        };

        public ICollection<string> CrossingTypes { get; set; } = new Collection<string>()
        {
            Resources.AverageCrossing,
            Resources.BitCrossing,
        };

        #endregion

        #endregion

        #region Bool Flags

        private bool _isSizeChanged = false;
        public bool IsSizeChanged
        {
            get
            {
                return _isSizeChanged;
            }
            set
            {
                _isSizeChanged = value;
                RaisePropertyChanged(nameof(IsBitmapReady));
            }
        }

        private bool _isVisualisationEnabled = false;
        public bool IsVisualisationEnabled
        {
            get
            {
                return _isVisualisationEnabled;
            }
            set
            {
                _isVisualisationEnabled = value;
                IsVisualisationDisabled = !_isVisualisationEnabled;
                RaisePropertyChanged(nameof(IsVisualisationEnabled));
            }
        }

        private bool _isVisualisationDisabled = true;
        public bool IsVisualisationDisabled
        {
            get
            {
                return _isVisualisationDisabled;
            }
            set
            {
                _isVisualisationDisabled = value;
                RaisePropertyChanged(nameof(IsVisualisationDisabled));
            }
        }

        private bool _isBitmapReady;
        public bool IsBitmapReady
        {
            get
            {
                return _isVisualisationDisabled && _isBitmapReady && !_isSizeChanged;
            }
            set
            {
                _isBitmapReady = value;
                RaisePropertyChanged(nameof(IsBitmapReady));
            }
        }

        #endregion 

        #region Commands

        private ICommand _newVisualisation;
        public ICommand NewVisualisation
        {
            get
            {
                if (_newVisualisation == null)
                {
                    _newVisualisation = new NoParameterCommand(
                        () => New());
                }
                return _newVisualisation;
            }
        }

        private ICommand _startVisualisation;
        public ICommand StartVisualisation
        {
            get
            {
                if (_startVisualisation == null)
                {
                    _startVisualisation = new NoParameterCommand(
                        () => Start());
                }
                return _startVisualisation;
            }
        }

        private ICommand _pauseVisualisation;
        public ICommand PauseVisualisation
        {
            get
            {
                if (_pauseVisualisation == null)
                {
                    _pauseVisualisation = new NoParameterCommand(
                        () => Pause());
                }
                return _pauseVisualisation;
            }
        }

        private ICommand _generateReport;
        public ICommand GenerateReport
        {
            get
            {
                if (_generateReport == null)
                {
                    _generateReport = new NoParameterCommand(
                        () => _reportingManager.ShowDialogAndSave());
                }
                return _generateReport;
            }
        }

        #endregion

        #region Methods

        private void New()
        {
            PixelCollection = PixelsGenerator.Generate(Width, Height);
            IsSizeChanged = false;
            TurnsNumber = 0;
            UpdateAverageColor();
            _reportingManager = new ReportingManager()
            {
                Height = Height,
                Width = Width,
                AllPixels = AllPixelsCount,
                ScoringType = CurrentScoringType,
                MutationType = CurrentMutationType,
                CrossoverType = CurrentCrossingType,
                MutationRate = CurrentMutationRate,
            };
            _reportingManager.TurnReport(1, PixelsDeviation);
        }

        private void Start()
        {
            _geneticManager = new GeneticManager()
            {
                PixelCollection = PixelCollection,
                ScoringTable = ScoringFactory.Create(CurrentScoringType),
                Crossing = CrossingFactory.Create(CurrentCrossingType),
                PixelsToSelect = PixelsToSelect,
                HowManyChildren = HowManyChildren,
            };

            IsVisualisationEnabled = true;
            _backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _backgroundWorker.DoWork += DoVisualisation;
            _backgroundWorker.ProgressChanged += OnNextGeneration;
            _backgroundWorker.RunWorkerAsync();
        }

        private void Pause()
        {
            IsVisualisationEnabled = false;
            IsBitmapReady = true;
            if (_backgroundWorker != null && _backgroundWorker.IsBusy)
                _backgroundWorker.CancelAsync();
            _reportingManager.TurnReport(TurnsNumber, PixelsDeviation);
        }        

        private void UpdateAverageColor()
        {
            var newAveragePixel = new List<Pixel>
            {
                new Pixel()
                {
                    Blue = PixelCollection.AverageBlue,
                    Red = PixelCollection.AverageRed,
                    Green = PixelCollection.AverageGreen,
                    Alpha = byte.MaxValue,
                }
            };
            AverageColor = BitmapConverter.ToBitmap(new PixelCollection()
            {
                Height = 1,
                Width = 1,
                Pixels = newAveragePixel,
            });
    }

        private void DoVisualisation(object sender, DoWorkEventArgs args)
        {
            while (true)
            {
                TurnsNumber++;
                Thread.Sleep(20);
                if (_backgroundWorker.CancellationPending == false)
                {
                    var newPixels = _geneticManager.NextGeneration();
                    _backgroundWorker.ReportProgress(0, newPixels);                                       
                    if (newPixels.AreAllPixelsEqual())
                    {
                        MessageBox.Show(Resources.VisualisationEnded);
                        Pause();
                    }
                }
                else
                {
                    args.Cancel = true;
                    return;
                }                
            }
        }

        private void OnNextGeneration(object sender, ProgressChangedEventArgs args)
        {
            PixelCollection = (PixelCollection)args.UserState;
            UpdateAverageColor();
            _reportingManager.TurnReport(TurnsNumber, PixelsDeviation);
        }

        #endregion
    }
}
