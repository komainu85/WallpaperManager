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

        },

        FileUploaded: function (model) {
            this.filesUploaded.push(model.itemId);

            this.upFiles.viewModel.refreshNumberFiles();

            if (this.upFiles.viewModel.globalPercentage() == 100) {

                if (this.upFiles.viewModel.totalFiles() == 1) {
                    this.uploadDialog.viewModel.hide();
                    this.SaveWallpapers();
                }
            }
        },

        SaveWallpapers: function () { }

    });

    return WallpaperManager;
});