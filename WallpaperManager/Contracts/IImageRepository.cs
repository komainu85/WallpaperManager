using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Sitecore.Data.Items;

namespace MikeRobbins.WallpaperManager.Contracts
{
    public interface IImageRepository
    {
        Image ResizeImage(Image image, int maxWidth, int maxHeight);
        ImageFormat GetImageFormat(MediaItem uploadedItem);
        Stream CovertImageToStream(Image image, ImageFormat imageFormat);
    }
}