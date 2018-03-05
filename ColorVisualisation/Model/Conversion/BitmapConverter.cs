using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorVisualisation.Model.Conversion
{
    class BitmapConverter
    {
        private int _width = int.Parse(Properties.Resources.BitmapWidth);
        private int _height = int.Parse(Properties.Resources.BitmapHeight);
        private int _pixelValues = int.Parse(Properties.Resources.NumberOfValuesInPixel);
        private int _dpi = int.Parse(Properties.Resources.DPI);

        private byte[,,] _rawPixels;

        public BitmapConverter(byte[,,] rawPixels)
        {
            _rawPixels = rawPixels;
        }

        public WriteableBitmap ToBitmap()
        {
            var newBitmap = new WriteableBitmap(
                    _width, _height, _dpi, _dpi, PixelFormats.Bgra32, null);
            byte[] pixels1d = ToByteArray(_rawPixels);
            Int32Rect rect = new Int32Rect(0, 0, _width, _height);
            int stride = _pixelValues * _width;
            newBitmap.WritePixels(rect, pixels1d, stride, 0);
            return newBitmap;
        }

        private byte[] ToByteArray(byte[,,] rawPixels)
        {
            byte[] pixels1d = new byte[_height * _width * _pixelValues];
            int index = 0;
            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    for (int i = 0; i < _pixelValues; i++)
                        pixels1d[index++] = rawPixels[row, col, i];
                }
            }
            return pixels1d;
        }
    }
}
