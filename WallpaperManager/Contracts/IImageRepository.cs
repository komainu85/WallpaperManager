using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Sitecore.Data.Items;

namespace MikeRobbins.WallpaperManager.Contracts
{
    public interface IImageRepository
    {
        Image ResizeImage(Image image);
        ImageFormat GetImageFormat(MediaItem uploadedItem);
        Stream CovertImageToStream(Image image, MediaItem mediaItem);
    }
}