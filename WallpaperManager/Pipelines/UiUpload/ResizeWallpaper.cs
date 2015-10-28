using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using MikeRobbins.WallpaperManager.Contracts;
using Sitecore.Data.Items;
using Sitecore.Pipelines.Upload;
using Sitecore.Resources.Media;
using StructureMap;

namespace MikeRobbins.WallpaperManager.Pipelines.UiUpload
{
    public class ResizeWallpaper : UploadProcessor
    {
        private const string WallpaperManagerMaxHeight = "WallpaperManager.MaxHeight";
        private const string WallpaperManagerMaxWidth = "WallpaperManager.MaxWidth";

        private readonly IImageResizer _imageResizer;
        private readonly ISettingsProvider _settingsProvider;

        private readonly Container _container = new StructureMap.Container(new IoC.Registry());

        public ResizeWallpaper()
        {
            _imageResizer = _container.GetInstance<IImageResizer>();
            _settingsProvider = _container.GetInstance<ISettingsProvider>();
        }

        public void Process(UploadArgs args)
        {
            Sitecore.Diagnostics.Log.Info("Process Resize Wallpapers:", this);

            foreach (MediaItem uploadedItem in args.UploadedItems)
            {
                if (uploadedItem.FilePath.Contains("Wallpapers"))
                {
                    Image image = Image.FromStream(uploadedItem.GetMediaStream());

                    int maxHeight = GetMaxMeasurements(WallpaperManagerMaxHeight, "500");
                    int maxWidth = GetMaxMeasurements(WallpaperManagerMaxWidth, "1000");

                    Image resizedImage = _imageResizer.ResizeImage(image, maxWidth, maxHeight);

                    UpdateMediaItem(uploadedItem, resizedImage);
                }
            }
        }

        private void UpdateMediaItem(MediaItem uploadedItem, Image resizedImage)
        {
            Media media = MediaManager.GetMedia(uploadedItem);

            media.SetStream(CovertImageToStream(resizedImage, resizedImage.RawFormat), uploadedItem.Extension);
        }

        public Stream CovertImageToStream(Image image, ImageFormat imageFormat)
        {
            var stream = new MemoryStream();
            image.Save(stream, imageFormat);
            stream.Position = 0;
            return stream;
        }

        public int GetMaxMeasurements(string key, string defaultValue)
        {
            int measurement;
            string maxValue = _settingsProvider.GetSetting(key, defaultValue);

            int.TryParse(maxValue, out measurement);

            return measurement;
        }
    }
}