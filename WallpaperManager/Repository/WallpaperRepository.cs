using System;
using System.IO;
using System.Linq;
using MikeRobbins.WallpaperManager.Models;
using System.Web;

namespace MikeRobbins.WallpaperManager.Repository
{
    public class WallpaperRepository : Sitecore.Services.Core.IRepository<Wallpaper>
    {
        public IQueryable<Wallpaper> GetAll()
        {
            var diSource = new DirectoryInfo(HttpContext.Current.Server.MapPath(@"\") + @"sitecore\shell\Themes\Backgrounds");

            var files = diSource.GetFiles();
            var webPath = @"\sitecore\shell\Themes\Backgrounds\";

            var wallpapers = files.Select(x => new Wallpaper() { Path = webPath + x.Name, Name = x.Name.Replace(x.Extension, ""), Id = x.Name.Replace(x.Extension, ""), itemId = x.Name.Replace(x.Extension, "") });

            return wallpapers.AsQueryable();
        }

        public Wallpaper FindById(string id)
        {
            throw new NotImplementedException();
        }

        public void Add(Wallpaper entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Wallpaper entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Wallpaper entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Wallpaper entity)
        {
            throw new NotImplementedException();
        }
    }
}