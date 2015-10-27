using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MikeRobbins.WallpaperManager.Contracts;

namespace MikeRobbins.WallpaperManager
{
    public class DataAccess : IDataAccess
    {
        public MediaItem GetMediaItem(string id)
        {
            var item = Database.GetDatabase("master").GetItem(new ID(id));

            MediaItem mediaItem = new MediaItem(item);

            return mediaItem;
        }
    }
}