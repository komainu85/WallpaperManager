using Sitecore.Data.Items;

namespace MikeRobbins.WallpaperManager.Contracts
{
    public interface IDataAccess
    {
        MediaItem GetMediaItem(string id);
    }
}