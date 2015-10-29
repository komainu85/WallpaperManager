using System;
using System.Drawing;
using MikeRobbins.WallpaperManager.Contracts;

namespace MikeRobbins.WallpaperManager.ImageManagement
{
    public class ImageResizer : IImageResizer
    {
        public Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            try
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
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error(ex.Message, ex, this);
                throw ex;
            }
        }

        private int GetTargetSize(int size, double ratio)
        {
            return (int)(size * ratio);
        }
    }
}