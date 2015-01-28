require.config({
    paths: {
        entityService: "/sitecore/shell/client/Services/Assets/lib/entityservice"
    }
});

define(["sitecore", "jquery", "underscore", "entityService"], function (Sitecore, $, _, entityService) {
    var WallpaperManager = Sitecore.Definitions.App.extend({

        filesUploaded: [],

        initialized: function () {
            this.GetWallpapers();
        },

        initialize: function () {
            this.on("upload-fileUploaded", this.FileUploaded, this);
        },

        GetWallpapers: function () {
            var datasource = this.DataSource;

            var wallpaperService = new entityService({
                url: "/sitecore/api/ssc/MikeRobbins-WallpaperManager-Controllers/Wallpaper"
            });

            var result = wallpaperService.fetchEntities().execute().then(function (wallpapers) {
                datasource.viewModel.items(wallpapers);
            });

            this.MessageBar.viewModel.hide();
        },

        FileUploaded: function (model) {
            this.filesUploaded.push(model.itemId);

            this.upFiles.viewModel.refreshNumberFiles();

            if (this.upFiles.viewModel.globalPercentage() == 100) {
                this.uploadDialog.viewModel.hide();
                this.SaveWallpapers();
            }
        },

        SaveWallpapers: function () {

            var wallpaperService = new entityService({
                url: "/sitecore/api/ssc/MikeRobbins-WallpaperManager-Controllers/Wallpaper"
            });

            for (var i = 0; i < this.filesUploaded.length; i++) {
                var wallpaperMediaId = this.filesUploaded[i];

                var wallpaper = {
                    itemId: wallpaperMediaId
                };

                var result = wallpaperService.create(wallpaper).execute().then(function (newWallpaper) {
                    var test = "";
                });
            }

            this.MessageBar.viewModel.show();
        },

        DeleteWallpaper: function () {

            var wallpaperService = new entityService({
                url: "/sitecore/api/ssc/MikeRobbins-WallpaperManager-Controllers/Wallpaper"
            });

            var successMessage = this.MessageBar;

            var itemId = this.MediaResultsListControl.viewModel.selectedItemId();

            var result = wallpaperService.fetchEntity(itemId).execute().then(function (wallpaper) {
                wallpaper.destroy().then(function () {
                    successMessage.addMessage("Notification", "Wallpaper has been delete. Please refresh to load new data");
                    successMessage.viewModel.show();
                });
            });
        }

    });

    return WallpaperManager;
});