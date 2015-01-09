namespace MikeRobbins.WallpaperManager.Models
{
    public class Wallpaper : Sitecore.Services.Core.Model.EntityIdentity
    {
        public string Path { get; set; }
        public string Name { get; set; }
    }
}