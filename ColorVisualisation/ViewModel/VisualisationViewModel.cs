using ColorVisualisation.ViewModel.Base;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorVisualisation.ViewModel
{
    class VisualisationViewModel : BaseViewModel
    {
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
                            Visu();
                        });
                }
                return _newVisualisation;
            }
        }

        private void Visu()
        {
            const int width = 240;
            const int height = 240;

            PixelsImage = new WriteableBitmap(
                width, height, 96, 96, PixelFormats.Bgra32, null);
            byte[,,] pixels = new byte[height, width, 4];

            // Clear to black.
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    for (int i = 0; i < 3; i++)
                        pixels[row, col, i] = 0;
                    pixels[row, col, 3] = 255;
                }
            }

            // Blue.
            for (int row = 0; row < 80; row++)
            {
                for (int col = 0; col <= row; col++)
                {
                    pixels[row, col, 0] = 255;
                }
            }

            // Green.
            for (int row = 80; row < 160; row++)
            {
                for (int col = 0; col < 80; col++)
                {
                    pixels[row, col, 1] = 255;
                }
            }

            // Red.
            for (int row = 160; row < 240; row++)
            {
                for (int col = 0; col < 80; col++)
                {
                    pixels[row, col, 2] = 255;
                }
            }

            // Copy the data into a one-dimensional array.
            byte[] pixels1d = new byte[height * width * 4];
            int index = 0;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    for (int i = 0; i < 4; i++)
                        pixels1d[index++] = pixels[row, col, i];
                }
            }

            // Update writeable bitmap with the colorArray to the image.
            Int32Rect rect = new Int32Rect(0, 0, width, height);
            int stride = 4 * width;
            PixelsImage.WritePixels(rect, pixels1d, stride, 0);
        }
    }
}
