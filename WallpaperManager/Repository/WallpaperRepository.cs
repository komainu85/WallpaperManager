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

            var wallpapers = files.Select(x => new Wallpaper() { Path = webPath + x.Name, Name = x.Name.Replace(x.Extension, ""), Id = x.Name, itemId = x.Name });

            return wallpapers.AsQueryable();
        }

        public Wallpaper FindById(string id)
        {
            var io = new IO();
            var file = io.GetFile(id);

            return new Wallpaper() { Path = webPath + file.Name, Name = file.Name.Replace(file.Extension, ""), Id = file.Name, itemId = file.Name };

        }

        public void Add(Wallpaper entity)
        {
            var io = new IO();

            io.CreateFile(entity);
        }

        public bool Exists(Wallpaper entity)
        {
            var io = new IO();
            return io.GetFiles().Any(x => x.Name == entity.Id);
        }

        public void Update(Wallpaper entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Wallpaper entity)
        {
            var io = new IO();

            io.DeleteFile(entity);
        }
    }
}