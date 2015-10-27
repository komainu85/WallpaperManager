using System.IO;
using MikeRobbins.WallpaperManager.Models;

namespace MikeRobbins.WallpaperManager.Contracts
{
    public interface IFileAccess
    {
        FileInfo[] GetFiles();
        FileInfo GetFile(string fileName);
        void CreateFile(Wallpaper wallpaper);
        void DeleteFile(Wallpaper wallpaper);
    }
}