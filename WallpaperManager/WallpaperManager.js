require.config({
    paths: {
        entityService: "/sitecore/shell/client/Services/Assets/lib/entityservice"
    }
});

define(["sitecore", "jquery", "underscore", "entityService"], function (Sitecore, $, _, entityService) {
    var WallpaperManager = Sitecore.Definitions.App.extend({

        initialized: function () {
            this.GetWallpapers();
        },

        initialize: function () { },

        GetWallpapers: function () {
            var datasource = this.DataSource;

            var wallpaperService = new entityService({
                url: "/sitecore/api/ssc/MikeRobbins-WallpaperManager-Controllers/Wallpaper"
            });

            var result = wallpaperService.fetchEntities().execute().then(function (wallpapers) {
                datasource.viewModel.items(wallpapers);
            });

        }
    });

    return WallpaperManager;
});