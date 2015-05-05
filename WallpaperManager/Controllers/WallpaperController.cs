using System.Web.Mvc;
using MikeRobbins.WallpaperManager.IoC;
using MikeRobbins.WallpaperManager.Models;
using MikeRobbins.WallpaperManager.Repository;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Sitecore.Services;
using StructureMap;

namespace MikeRobbins.WallpaperManager.Controllers
{
    [ValidateAntiForgeryToken]
    [ServicesController]
    public class WallpaperController : EntityService<Wallpaper>
    {
        private Container _container;

        public static Container Container
        {
            get
            {
                return new Container(new IoCRegistry());
            }
        }

        public WallpaperController(IRepository<Wallpaper> repository)
            : base(repository)
        {
        }

        public WallpaperController()
            : this(Container.GetInstance<IRepository<Wallpaper>>())
        {
        }
    }
}