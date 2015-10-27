using System;
using System.IO;
using System.Linq;
using MikeRobbins.WallpaperManager.Models;
using System.Web;
using MikeRobbins.WallpaperManager.Contracts;
using MikeRobbins.WallpaperManager.IoC;
using StructureMap;

namespace MikeRobbins.WallpaperManager.Repository
{
    public class WallpaperRepository : Sitecore.Services.Core.IRepository<Wallpaper>
    {
        private const string webPath = @"\sitecore\shell\Themes\Backgrounds\";
  
        private readonly IFileAccess _iFileAccess;

        public WallpaperRepository(IFileAccess iFileAccess)
        {
            _iFileAccess = iFileAccess;
        }

        public IQueryable<Wallpaper> GetAll()
        {
            var files = _iFileAccess.GetFiles();

            var wallpapers = files.Select(x => new Wallpaper() { Path = webPath + x.Name, Name = x.Name.Replace(x.Extension, ""), Id = x.Name, itemId = x.Name });

            return wallpapers.AsQueryable();
        }

        public Wallpaper FindById(string id)
        {
            var file = _iFileAccess.GetFile(id);

            return new Wallpaper() { Path = webPath + file.Name, Name = file.Name.Replace(file.Extension, ""), Id = file.Name, itemId = file.Name };
        }

        public void Add(Wallpaper entity)
        {
            _iFileAccess.CreateFile(entity);
        }

        public bool Exists(Wallpaper entity)
        {
            return _iFileAccess.GetFiles().Any(x => x.Name == entity.Id);
        }

        public void Update(Wallpaper entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Wallpaper entity)
        {
            _iFileAccess.DeleteFile(entity);
        }
    }
}