using System.Web.Mvc;
using MikeRobbins.WallpaperManager.Models;
using MikeRobbins.WallpaperManager.Repository;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Sitecore.Services;

namespace MikeRobbins.WallpaperManager.Controllers
{
    [ValidateAntiForgeryToken]
    [ServicesController]
    public class WallpaperController : EntityService<Wallpaper>
    {
        public WallpaperController(IRepository<Wallpaper> repository)
            : base(repository)
        {
        }

        public WallpaperController()
            : this(new WallpaperRepository())
        {
        }

    }
}