using ColorVisualisation.Properties;

namespace ColorVisualisation.Model.Conversion
{
    static class PixelsConverter
    {
        private static int pixelValues = int.Parse(Resources.NumberOfValuesInPixel);

        public static byte[] ToByteArray(this PixelContainer pixels)
        {
            byte[] pixelsArray = new byte[pixels.Height * pixels.Width * pixelValues];
            int index = 0;
            foreach (var pixel in pixels.Pixels)
            {
                pixelsArray[index] = (byte)pixel.Blue;
                pixelsArray[index + 1] = (byte)pixel.Green;
                pixelsArray[index + 2] = (byte)pixel.Red;
                pixelsArray[index + 3] = (byte)pixel.Alpha;
                index += pixelValues;
            }
            return pixelsArray;
        }
    }
}
