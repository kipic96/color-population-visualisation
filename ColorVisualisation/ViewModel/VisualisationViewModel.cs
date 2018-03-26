using ColorVisualisation.Model;
using ColorVisualisation.ViewModel.Base;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ColorVisualisation.Properties;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ColorVisualisation.Model.Helper.Generator;
using ColorVisualisation.Model.Helper.Conversion;
using ColorVisualisation.Model.Entity;
using ColorVisualisation.Model.Crossing;
using ColorVisualisation.Model.Scoring;

namespace ColorVisualisation.ViewModel
{
    class VisualisationViewModel : BaseViewModel
    {
        #region Properties And Fields

        private GeneticManager _geneticManager;

        private BackgroundWorker _backgroundWorker;

        private int _valuesInPixel = int.Parse(Resources.NumberOfValuesInPixel);

        private PixelCollection _pixels;
        public PixelCollection PixelCollection
        {
            get { return _pixels; }
            set
            {
                _pixels = value;
                var converter = new BitmapConverter(_pixels);
                PixelsImage = converter.ToBitmap();
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

        public int PixelsToSelect
        {
            get { return NumberConversion.ToEvenNumber((int)(Width * Height / 2)); }
        }

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
                        () =>
                        {
                            PixelCollection = PixelsGenerator.Generate(Width, Height);
                            IsSizeChanged = false;
                        });
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
                        () =>
                        {
                            _geneticManager = new GeneticManager()
                            {
                                PixelCollection = PixelCollection,
                                ScoringTable = ScoringFactory.Create(CurrentScoringType),
                                Crossing = CrossingFactory.Create(CurrentCrossingType,
                                    PixelCollection, PixelsToSelect, HowManyChildren),  
                                PixelsToSelect = PixelsToSelect,
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
                        });
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
                        () =>
                        {
                            IsVisualisationEnabled = false;
                            IsBitmapReady = true;
                            if (_backgroundWorker != null && _backgroundWorker.IsBusy)
                                _backgroundWorker.CancelAsync();                            
                        });
                }
                return _pauseVisualisation;
            }
        }

        #endregion

        #region Methods

        private void DoVisualisation(object sender, DoWorkEventArgs args)
        {
            while (true)
            {
                if (_backgroundWorker.CancellationPending == false)
                {
                    var newPixels = _geneticManager.NextGeneration();
                    _backgroundWorker.ReportProgress(0, newPixels);
                    if (newPixels.AreAllPixelsEqual())
                    {
                        MessageBox.Show("O, koniec xD");
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
        }

        #endregion
    }
}
