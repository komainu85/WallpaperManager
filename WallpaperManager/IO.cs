using MikeRobbins.WallpaperManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MikeRobbins.WallpaperManager
{
    public class IO
    {
        public FileInfo[] GetFiles()
        {
            var diSource = new DirectoryInfo(HttpContext.Current.Server.MapPath(@"\") + @"sitecore\shell\Themes\Backgrounds");

            return diSource.GetFiles();
        }

        public void CreateFile(Wallpaper wallpaper)
        {
            var dataAccess = new DataAccess();

            var mediaItem = dataAccess.GetMediaItem(wallpaper.itemId);

            var mediaStream = mediaItem.GetMediaStream();

            using (var fileStream = File.Create(HttpContext.Current.Server.MapPath(@"\") + @"sitecore\shell\Themes\Backgrounds\" + mediaItem.DisplayName + "." + mediaItem.Extension))
            {
                mediaStream.Seek(0, SeekOrigin.Begin);
                mediaStream.CopyTo(fileStream);
            }
        }
    }
}