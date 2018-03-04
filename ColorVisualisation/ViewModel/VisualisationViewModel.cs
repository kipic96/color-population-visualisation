using ColorVisualisation.Model;
using ColorVisualisation.Model.Conversion;
using ColorVisualisation.Model.Generator;
using ColorVisualisation.ViewModel.Base;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ColorVisualisation.ViewModel
{
    class VisualisationViewModel : BaseViewModel
    {
        private GeneticManager _geneticManager;

        private BackgroundWorker _backgroundWorker;

        private byte[,,] _initialPixels;

        private byte[,,] _rawPixels;
        private byte[,,] RawPixels
        {
            get
            {
                return _rawPixels;
            }
            set
            {
                _rawPixels = value;
                var converter = new BitmapConverter(_rawPixels);
                PixelsImage = converter.ToBitmap();                    
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
                            var bitmapGenerator = new BitmapGenerator();
                            RawPixels = bitmapGenerator.Generate();
                            _initialPixels = RawPixels;
                        },
                        () =>
                        {
                            return (_backgroundWorker == null || !_backgroundWorker.IsBusy);
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
                            _geneticManager = new GeneticManager(RawPixels);

                            IsVisualisationEnabled = true;
                            _backgroundWorker = new BackgroundWorker();
                            _backgroundWorker.WorkerReportsProgress = true;
                            _backgroundWorker.WorkerSupportsCancellation = true;
                            _backgroundWorker.DoWork += DoVisualisation;
                            _backgroundWorker.ProgressChanged += OnNextGeneration;                               
                            _backgroundWorker.RunWorkerAsync();
                        },
                        () =>
                        {
                            return (_backgroundWorker == null || !_backgroundWorker.IsBusy);
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
                            if (_backgroundWorker != null && _backgroundWorker.IsBusy)
                                _backgroundWorker.CancelAsync();
                            IsVisualisationEnabled = false;
                        },
                        () =>
                        {
                            return (_backgroundWorker != null && _backgroundWorker.IsBusy);
                        });
                }
                return _pauseVisualisation;
            }
        }

        private ICommand _restartVisualisation;
        public ICommand RestartVisualisation
        {
            get
            {
                if (_restartVisualisation == null)
                {
                    _restartVisualisation = new NoParameterCommand(
                        () =>
                        {
                            if (_backgroundWorker != null && _backgroundWorker.IsBusy)
                            {
                                IsVisualisationEnabled = false;
                                _backgroundWorker.CancelAsync();                                                                
                            }                        
                        },
                        () =>
                        {
                            return (_backgroundWorker != null && _backgroundWorker.IsBusy);
                        });
                }
                return _restartVisualisation;
            }
        }

        private void DoVisualisation(object worker, DoWorkEventArgs args)
        {
            while (true)
            {
                Thread.Sleep(500);
                var newRawPixels = _geneticManager.NextGeneration();
                _backgroundWorker.ReportProgress(0, newRawPixels);
                if (_backgroundWorker.CancellationPending == true)
                {
                    args.Cancel = true;
                    var generator = new BitmapGenerator();
                    _backgroundWorker.ReportProgress(0, _initialPixels);
                    return;
                }    
            }
        }

        private void OnNextGeneration(object o, ProgressChangedEventArgs args)
        {
            RawPixels = (byte[,,])args.UserState;
        }
    }
}
