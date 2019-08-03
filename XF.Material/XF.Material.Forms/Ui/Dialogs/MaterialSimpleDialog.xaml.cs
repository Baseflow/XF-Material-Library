﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs.Configurations;
using XF.Material.Forms.UI.Dialogs.Internals;

namespace XF.Material.Forms.UI.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialSimpleDialog : BaseMaterialModalPage, IMaterialAwaitableDialog<int>
    {
        internal MaterialSimpleDialog(MaterialSimpleDialogConfiguration configuration)
        {
            this.InitializeComponent();
            this.Configure(configuration);
        }

        public TaskCompletionSource<int> InputTaskCompletionSource { get; set; }

        internal static MaterialSimpleDialogConfiguration GlobalConfiguration { get; set; }

        internal static async Task<int> ShowAsync(string title, IEnumerable<string> actions, MaterialSimpleDialogConfiguration configuration = null)
        {
            var dialog = new MaterialSimpleDialog(configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<int>(), DialogTitle = {Text = title}
            };
            dialog.CreateActions(actions.ToList(), configuration);

            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        private void Configure(MaterialSimpleDialogConfiguration configuration)
        {
            var preferredConfig = configuration ?? GlobalConfiguration;

            if (preferredConfig == null) return;
            this.BackgroundColor = preferredConfig.ScrimColor;
            Container.CornerRadius = preferredConfig.CornerRadius;
            Container.BackgroundColor = preferredConfig.BackgroundColor;
            DialogTitle.TextColor = preferredConfig.TitleTextColor;
            DialogTitle.FontFamily = preferredConfig.TitleFontFamily;
            Container.Margin = preferredConfig.Margin == default ? Material.GetResource<Thickness>("Material.Dialog.Margin") : preferredConfig.Margin;
        }

        private void CreateActions(List<string> actions, MaterialSimpleDialogConfiguration configuration)
        {
            if (actions == null || actions.Count <= 0)
            {
                throw new ArgumentException("Parameter actions should not be null or empty");
            }

            var actionModels = new List<ActionModel>();
            actions.ForEach(a =>
            {
                var preferredConfig = configuration ?? GlobalConfiguration;
                var actionModel = new ActionModel
                {
                    Text = a,
                    TextColor = preferredConfig?.TextColor ?? Color.FromHex("#DE000000"),
                    FontFamily = preferredConfig != null
                        ? preferredConfig.TextFontFamily
                        : Material.FontFamily.Body1
                };
                actionModel.SelectedCommand = new Command<int>(async(position) =>
                {
                    if (this.InputTaskCompletionSource?.Task.Status != TaskStatus.WaitingForActivation) return;
                    actionModel.IsSelected = true;
                    await this.DismissAsync();
                    this.InputTaskCompletionSource?.SetResult(position);
                });

                actionModels.Add(actionModel);
                actionModel.Index = actionModels.IndexOf(actionModel);
            });

            DialogActionList.SetValue(BindableLayout.ItemsSourceProperty, actionModels);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.ChangeLayout();
        }

        protected override void OnOrientationChanged(DisplayOrientation orientation)
        {
            base.OnOrientationChanged(orientation);

            this.ChangeLayout();
        }

        private void ChangeLayout()
        {
            switch (this.DisplayOrientation)
            {
                case DisplayOrientation.Landscape when Device.Idiom == TargetIdiom.Phone:
                    Container.WidthRequest = 560;
                    Container.HorizontalOptions = LayoutOptions.Center;
                    break;
                case DisplayOrientation.Portrait when Device.Idiom == TargetIdiom.Phone:
                    Container.WidthRequest = -1;
                    Container.HorizontalOptions = LayoutOptions.FillAndExpand;
                    break;
            }
        }

        protected override void OnBackButtonDismissed()
        {
            this.InputTaskCompletionSource.SetResult(-1);
        }

        protected override bool OnBackgroundClicked()
        {
            this.InputTaskCompletionSource.SetResult(-1);

            return base.OnBackgroundClicked();
        }
    }
}