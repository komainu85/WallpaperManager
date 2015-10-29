using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using MikeRobbins.WallpaperManager.Contracts;
using MikeRobbins.WallpaperManager.ImageManagement;
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

        private readonly ISettingsProvider _settingsProvider;
        private readonly IImageRepository _imageRepository;

        private readonly Container _container = new StructureMap.Container(new IoC.Registry());

        public ResizeWallpaper()
        {
            _settingsProvider = _container.GetInstance<ISettingsProvider>();
            _imageRepository = _container.GetInstance<IImageRepository>();
        }

        public void Process(UploadArgs args)
        {
            foreach (MediaItem uploadedItem in args.UploadedItems)
            {
                if (uploadedItem.MediaPath.StartsWith("/Wallpapers/"))
                {
                    Sitecore.Diagnostics.Log.Info("Process Resize Wallpapers: File: " + uploadedItem.DisplayName, this);

                    Image image = Image.FromStream(uploadedItem.GetMediaStream());

                    int maxHeight = GetMaxMeasurements(WallpaperManagerMaxHeight, "500");
                    int maxWidth = GetMaxMeasurements(WallpaperManagerMaxWidth, "1000");

                    Image resizedImage = _imageRepository.ResizeImage(image, maxWidth, maxHeight);

                    UpdateMediaItem(uploadedItem, resizedImage);
                }
            }
        }

        private void UpdateMediaItem(MediaItem uploadedItem, Image resizedImage)
        {
            Media media = MediaManager.GetMedia(uploadedItem);

            ImageFormat imageFormat = _imageRepository.GetImageFormat(uploadedItem);

            media.SetStream(_imageRepository.CovertImageToStream(resizedImage, imageFormat), uploadedItem.Extension);
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