using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MikeRobbins.WallpaperManager.Contracts;
using MikeRobbins.WallpaperManager.Providers;
using Sitecore.Services.Core;
using StructureMap.Configuration.DSL;

namespace MikeRobbins.WallpaperManager.IoC
{
    public class Registry : StructureMap.Configuration.DSL.Registry
    {
        public Registry()
        {
            For<IFileAccess>().Use<FileAccess>();
            For<IDataAccess>().Use<DataAccess>();
            For<IImageResizer>().Use<ImageResizer>();
            For<ISettingsProvider>().Use<SettingsProvider>();
            For(typeof(IRepository<>)).Use(typeof(MikeRobbins.WallpaperManager.Repository.WallpaperRepository));
        }
    }
}