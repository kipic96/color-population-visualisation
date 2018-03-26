using ColorVisualisation.Model.Entity;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorVisualisation.Model.Helper.Conversion
{
    class BitmapConverter
    {
        private int _width;
        private int _height;
        private int _pixelValues = int.Parse(Properties.Resources.NumberOfValuesInPixel);
        private int _dpi = int.Parse(Properties.Resources.DPI);

        private PixelCollection _pixels;

        public BitmapConverter(PixelCollection pixels)
        {
            _pixels = pixels;
            _width = pixels.Width;
            _height = pixels.Height;
        }

        public WriteableBitmap ToBitmap()
        {
            var newBitmap = new WriteableBitmap(
                    _width, _height, _dpi, _dpi, PixelFormats.Bgra32, null);
            byte[] pixels1d = _pixels.ToByteArray();
            Int32Rect rect = new Int32Rect(0, 0, _width, _height);
            int stride = _pixelValues * _width;
            newBitmap.WritePixels(rect, pixels1d, stride, 0);
            return newBitmap;
        }
    }
}
