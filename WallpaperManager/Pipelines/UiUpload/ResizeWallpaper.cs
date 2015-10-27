using System;
using System.Drawing;
using System.IO;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Pipelines.Upload;

namespace MikeRobbins.WallpaperManager.Pipelines.UiUpload
{
    public class ResizeWallpaper : UploadProcessor
    {
        //TODO: DI this
        private readonly ImageResizer _imageResizer = new ImageResizer();

        public void Process(UploadArgs args)
        {
            Sitecore.Diagnostics.Log.Info("Process Resize Wallpapers:", this);

            foreach (MediaItem uploadedItem in args.UploadedItems)
            {
                //TODO: Check this comes from wallpaper manager
                Image image = Image.FromStream(uploadedItem.GetMediaStream());


                //TODO: Get Sizes from setting file
                Image resizedImage = _imageResizer.ResizeImage(image, 1000, 500);

                //TODO: Update File on uploadedItem;
            }
        }
    }
}