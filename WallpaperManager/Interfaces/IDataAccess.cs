using Sitecore.Data.Items;

namespace MikeRobbins.WallpaperManager.Interfaces
{
    public interface IDataAccess
    {
        MediaItem GetMediaItem(string id);
    }
}