using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MikeRobbins.WallpaperManager.Contracts;
using Sitecore.Data.Items;

namespace MikeRobbins.WallpaperManager.ImageManagement
{
    public class ImageRepository : IImageRepository
    {
        private IImageResizer _imageResizer;

        public ImageRepository(IImageResizer imageResizer)
        {
            _imageResizer = imageResizer;
        }

        public Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
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

        public Stream CovertImageToStream(Image image, ImageFormat imageFormat)
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, imageFormat);
            stream.Position = 0;
            return stream;
        }
    }
}