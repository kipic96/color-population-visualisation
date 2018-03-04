using ColorVisualisation.Model;
using ColorVisualisation.Model.Conversion;
using ColorVisualisation.Model.Generator;
using ColorVisualisation.ViewModel.Base;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorVisualisation.ViewModel
{
    class VisualisationViewModel : BaseViewModel
    {
        private GeneticManager _geneticManager;

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

                            while (true)
                            {
                                RawPixels = _geneticManager.NextGeneration();
                            }
                        });
                }
                return _startVisualisation;
            }
        }
    }
}
