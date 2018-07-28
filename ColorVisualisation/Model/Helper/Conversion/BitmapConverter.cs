using ColorVisualisation.Model.Entity;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorVisualisation.Model.Helper.Conversion
{
    class BitmapConverter
    {
        private static int _pixelValues = int.Parse(Properties.Resources.NumberOfValuesInPixel);
        private static int _dpi = int.Parse(Properties.Resources.DPI);

        public static WriteableBitmap ToBitmap(PixelCollection pixels)
        {
            lock (pixels)
            {
                var newBitmap = new WriteableBitmap(
                        pixels.Width, pixels.Height, _dpi, _dpi, PixelFormats.Bgra32, null);
                byte[] pixels1d = pixels.ToByteArray();
                Int32Rect rect = new Int32Rect(0, 0, pixels.Width, pixels.Height);
                int stride = _pixelValues * pixels.Width;
                newBitmap.WritePixels(rect, pixels1d, stride, 0);
                return newBitmap;
            }
        }
    }
}
