using System.Drawing;

namespace MikeRobbins.WallpaperManager.Contracts
{
    public interface IImageResizer
    {
        Image ResizeImage(Image image, int maxWidth, int maxHeight);
    }
}