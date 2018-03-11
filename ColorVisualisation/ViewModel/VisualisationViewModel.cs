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
                            RawPixels = new BitmapGenerator(_width, _width).Generate();                            
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
                            _geneticManager = new GeneticManager(RawPixels, _width, _height);

                            IsVisualisationEnabled = true;
                            _backgroundWorker = new BackgroundWorker();
                            _backgroundWorker.WorkerReportsProgress = true;
                            _backgroundWorker.WorkerSupportsCancellation = true;
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

        private void DoVisualisation(object sender, DoWorkEventArgs args)
        {
            while (true)
            {
                Thread.Sleep(400);
                if (_backgroundWorker.CancellationPending == true)
                {
                    args.Cancel = true;
                    return;
                }   
                else
                {                    
                    var newRawPixels = _geneticManager.NextGeneration();
                    _backgroundWorker.ReportProgress(0, newRawPixels);
                } 
            }
        }

        private void OnNextGeneration(object sender, ProgressChangedEventArgs args)
        {
            RawPixels = (byte[,,])args.UserState;
        }

        private bool AreRawPixelsEmpty()
        {
            if (RawPixels == null)
                return true;
            
            int pixelValues = int.Parse(Properties.Resources.NumberOfValuesInPixel);
            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    for (int i = 0; i < pixelValues; i++)
                        if (RawPixels[row, col, i] != byte.MaxValue)
                            return false;
                }
            }
            return true;
        }
    }
}
