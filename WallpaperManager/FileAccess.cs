using MikeRobbins.WallpaperManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MikeRobbins.WallpaperManager.Contracts;
using Sitecore.Diagnostics;

namespace MikeRobbins.WallpaperManager
{
    public class FileAccess : IFileAccess
    {
        private const string _wallpaperDirectory = @"sitecore\shell\Themes\Backgrounds";

        private readonly IDataAccess _iDataAccess;

        public FileAccess(IDataAccess iDataAccess)
        {
            _iDataAccess = iDataAccess;
        }

        public FileInfo[] GetFiles()
        {
            return GetWallpaperDirectory().GetFiles();
        }

        public FileInfo GetFile(string fileName)
        {
            return GetWallpaperDirectory().GetFiles().FirstOrDefault(x => x.Name == fileName);
        }

        public void CreateFile(Wallpaper wallpaper)
        {
            var mediaItem = _iDataAccess.GetMediaItem(wallpaper.itemId);

            var mediaStream = mediaItem.GetMediaStream();

            try
            {
                using (var fileStream = File.Create(_wallpaperDirectory + "\\" + mediaItem.DisplayName + "." + mediaItem.Extension))
                {
                    mediaStream.Seek(0, SeekOrigin.Begin);
                    mediaStream.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error(ex.Message, this);
                throw ex;
            }

        }

        public void DeleteFile(Wallpaper wallpaper)
        {
            var fileToDelete = GetFile(wallpaper.itemId);

            Assert.IsNotNull(fileToDelete, "Wallpaper not found");

            try
            {
                fileToDelete.Delete();
            }
            catch (IOException ioException)
            {
                throw ioException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DirectoryInfo GetWallpaperDirectory()
        {
            var directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(@"\") + _wallpaperDirectory);
            return directory;
        }
    }
}