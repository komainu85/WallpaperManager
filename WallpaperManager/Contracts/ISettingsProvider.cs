namespace MikeRobbins.WallpaperManager.Contracts
{
    public interface ISettingsProvider
    {
        string GetSetting(string name, string defaultValue);
    }
}