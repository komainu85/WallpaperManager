require.config({
    paths: {
        entityService: "/sitecore/shell/client/Services/Assets/lib/entityservice"
    }
});

define(["sitecore", "jquery", "underscore", "entityService"], function (Sitecore, $, _, entityService) {
    var WallpaperManager = Sitecore.Definitions.App.extend({

        initialized: function () {
            this.GetWorkflows();
        },

        initialize: function () { },

        ApplyWorkflow: function () {
            var messagePanel = this.miMessages;
            var progressIcon = this.pi;

            progressIcon.viewModel.show();

            var selectedWorkflow = this.workflow.viewModel.selectedValue();
            var selectedTemplates = this.tvTemplates.viewModel.checkedItemIds().split("|");;

            var templateService = new entityService({
                url: "/sitecore/api/ssc/MikeRobbins-BulkWorkflow-Controllers/template"
            });

            var updated = 0;

            for (var i = 0; i < selectedTemplates.length; i++) {
                var selectedTemplate = selectedTemplates[i];

                templateService.fetchEntity(selectedTemplate).execute().then(function (template) {
                    template.WorkflowID = selectedWorkflow;

                    template.save().then(function (savedTemplate) {                        updated++;                        if (updated == selectedTemplates.length) {
                            progressIcon.viewModel.hide();
                        }

                        messagePanel.addMessage("notification", { text: "Workflow applied successfully for " + savedTemplate.DisplayName, actions: [], closable: true, temporary: true });
                    });
                });
            }
        },

        GetWallpapers: function () {
            var datasource = this.JsonDS;

            var wallpaperService = new entityService({
                url: "/sitecore/api/ssc/MikeRobbins-WallpaperManager-Controllers/Wallpaper"
            });

            var result = wallpaperService.fetchEntities().execute().then(function (wallpapers) {
                for (var i = 0; i < wallpapers.length; i++) {
                    datasource.add(wallpapers[i]);
                }
            });

        }
    });

    return WallpaperManager;
});