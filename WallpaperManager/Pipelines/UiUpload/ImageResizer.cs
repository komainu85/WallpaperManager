using System;
using System.Drawing;

namespace MikeRobbins.WallpaperManager.Pipelines.UiUpload
{
    public class ImageResizer
    {
        public Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            double ratioX = (double)maxWidth / image.Width;
            double ratioY = (double)maxHeight / image.Height;
            double ratio = Math.Min(ratioX, ratioY);

            var targetWidth = GetTargetSize(image.Width, ratio);
            var targetHeight = GetTargetSize(image.Height, ratio);

            var resizedImage = new Bitmap(targetWidth, targetHeight);

            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, targetWidth, targetHeight);
            }

            return resizedImage;
        }

        private int GetTargetSize(int size, double ratio)
        {
            return (int)(size * ratio);
        }
    }
}