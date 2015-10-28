using MikeRobbins.WallpaperManager.Contracts;
using Sitecore.Configuration;

namespace MikeRobbins.WallpaperManager.Providers
{
    public class SettingsProvider : ISettingsProvider
    {
        public string GetSetting(string name, string defaultValue)
        {
            return Settings.GetSetting(name, defaultValue);
        }
    }
}