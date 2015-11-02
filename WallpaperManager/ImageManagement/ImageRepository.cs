using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MikeRobbins.WallpaperManager.Contracts;
using Sitecore.Data.Items;

namespace MikeRobbins.WallpaperManager.ImageManagement
{
    public class ImageRepository : IImageRepository
    {
        private const string WallpaperManagerMaxHeight = "WallpaperManager.MaxHeight";
        private const string WallpaperManagerMaxWidth = "WallpaperManager.MaxWidth";

        private IImageResizer _imageResizer;
        private readonly ISettingsProvider _settingsProvider;

        public ImageRepository(IImageResizer imageResizer)
        {
            _imageResizer = imageResizer;
        }

        public Image ResizeImage(Image image)
        {
            int maxWidth = GetMaxMeasurements(WallpaperManagerMaxWidth, "1000");
            int maxHeight = GetMaxMeasurements(WallpaperManagerMaxHeight, "500");

            return _imageResizer.ResizeImage(image, maxWidth, maxHeight);
        }

        public ImageFormat GetImageFormat(MediaItem uploadedItem)
        {
            ImageFormat imageFormat = ImageFormat.Jpeg;

            switch (uploadedItem.Extension.ToLower())
            {
                case "gif":
                    imageFormat = ImageFormat.Gif;
                    break;
                case "jpg":
                case "jpeg":
                    imageFormat = ImageFormat.Jpeg;
                    break;
                case "png":
                    imageFormat = ImageFormat.Png;
                    break;
            }
            return imageFormat;
        }

        public Stream CovertImageToStream(Image image, MediaItem mediaItem)
        {
            ImageFormat imageFormat = GetImageFormat(mediaItem);

            MemoryStream stream = new MemoryStream();
            image.Save(stream, imageFormat);
            stream.Position = 0;
            return stream;
        }

        private int GetMaxMeasurements(string key, string defaultValue)
        {
            int measurement;
            string maxValue = _settingsProvider.GetSetting(key, defaultValue);

            int.TryParse(maxValue, out measurement);

            return measurement;
        }
    }
}