using MikeRobbins.WallpaperManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MikeRobbins.WallpaperManager.Interfaces;
using Sitecore.Diagnostics;

namespace MikeRobbins.WallpaperManager
{
    public class FileAccess : IFileAccess
    {
        private readonly string _wallpaperDirectory = HttpContext.Current.Server.MapPath(@"\") + @"sitecore\shell\Themes\Backgrounds";

        public FileAccess()
        {
        }

        public FileInfo[] GetFiles()
        {
            var diSource = new DirectoryInfo(_wallpaperDirectory);

            return diSource.GetFiles();
        }

        public FileInfo GetFile(string fileName)
        {
            var diSource = new DirectoryInfo(_wallpaperDirectory);

            return diSource.GetFiles().FirstOrDefault(x => x.Name == fileName);
        }

        public void CreateFile(Wallpaper wallpaper)
        {
            var dataAccess = new DataAccess();

            var mediaItem = dataAccess.GetMediaItem(wallpaper.itemId);

            var mediaStream = mediaItem.GetMediaStream();

            using (var fileStream = File.Create(_wallpaperDirectory + "\\" + mediaItem.DisplayName + "." + mediaItem.Extension))
            {
                mediaStream.Seek(0, SeekOrigin.Begin);
                mediaStream.CopyTo(fileStream);
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
    }
}