using ColorVisualisation.Model.Entity;
using ColorVisualisation.Properties;

namespace ColorVisualisation.Model.Helper.Conversion
{
    static class PixelsConverter
    {
        private static int pixelValues = int.Parse(Resources.NumberOfValuesInPixel);

        public static byte[] ToByteArray(this PixelCollection pixels)
        {
            byte[] pixelsArray = new byte[pixels.Height * pixels.Width * pixelValues];
            int index = 0;
            lock (pixels)
            {
                foreach (var pixel in pixels)
                {
                    pixelsArray[index] = (byte)pixel.Blue;
                    pixelsArray[index + 1] = (byte)pixel.Green;
                    pixelsArray[index + 2] = (byte)pixel.Red;
                    pixelsArray[index + 3] = (byte)pixel.Alpha;
                    index += pixelValues;
                }
            }
            return pixelsArray;
        }
    }
}
