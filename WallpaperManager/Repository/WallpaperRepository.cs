using System;
using System.IO;
using System.Linq;
using MikeRobbins.WallpaperManager.Models;
using System.Web;

namespace MikeRobbins.WallpaperManager.Repository
{
    public class WallpaperRepository : Sitecore.Services.Core.IRepository<Wallpaper>
    {
        private string webPath = @"\sitecore\shell\Themes\Backgrounds\";

        public IQueryable<Wallpaper> GetAll()
        {
            var io = new IO();

            var files = io.GetFiles();

            var wallpapers = files.Select(x => new Wallpaper() { Path = webPath + x.Name, Name = x.Name.Replace(x.Extension, ""), Id = x.Name.Replace(x.Extension, ""), itemId = x.Name.Replace(x.Extension, "") });

            return wallpapers.AsQueryable();
        }

        public Wallpaper FindById(string id)
        {
            throw new NotImplementedException();
        }

        public void Add(Wallpaper entity)
        {
            var io = new IO();

            io.CreateFile(entity);
        }

        public bool Exists(Wallpaper entity)
        {
            var io = new IO();
            return io.GetFiles().Any(x => x.Name.Replace(x.Extension, "") == entity.Id);
        }

        public void Update(Wallpaper entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Wallpaper entity)
        {

        }
    }
}