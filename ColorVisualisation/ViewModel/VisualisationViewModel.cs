using ColorVisualisation.Model;
using ColorVisualisation.Model.Conversion;
using ColorVisualisation.Model.Generator;
using ColorVisualisation.ViewModel.Base;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ColorVisualisation.Properties;

namespace ColorVisualisation.ViewModel
{
    class VisualisationViewModel : BaseViewModel
    {
        private GeneticManager _geneticManager;

        private BackgroundWorker _backgroundWorker;

        private int _width = int.Parse(Resources.BitmapWidth);
        private int _height = int.Parse(Resources.BitmapHeight);

        private PixelContainer _pixels;
        public PixelContainer PixelContainer
        {
            get
            {
                return _pixels;
            }
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

        #region Bool Flags

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
                return _isVisualisationDisabled && _isBitmapReady;
            }
            set
            {
                _isBitmapReady = value;
                RaisePropertyChanged(nameof(IsBitmapReady));
            }
        }

        #endregion Bool Flags

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
                            PixelContainer = new PixelsGenerator(_width, _height).Generate();                            
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
                            _geneticManager = new GeneticManager(PixelContainer);

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

        #endregion Commands

        private void DoVisualisation(object sender, DoWorkEventArgs args)
        {
            while (true)
            {
                Thread.Sleep(100);
                if (_backgroundWorker.CancellationPending == true)
                {
                    args.Cancel = true;
                    return;
                }   
                else
                {                    
                    var newPixels = _geneticManager.NextGeneration();
                    _backgroundWorker.ReportProgress(0, newPixels);
                } 
            }
        }

        private void OnNextGeneration(object sender, ProgressChangedEventArgs args)
        {
            PixelContainer = (PixelContainer)args.UserState;
        }
    }
}
