using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Xaml;
using XF.Material.Maui.UI.Dialogs.Configurations;
using XF.Material.Maui.UI.Dialogs.Internals;

namespace XF.Material.Maui.UI.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialSimpleDialog : BaseMaterialModalPage, IMaterialAwaitableDialog<int>
    {
        private static SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);

        internal MaterialSimpleDialog(MaterialSimpleDialogConfiguration configuration)
        {
            InitializeComponent();
            Configure(configuration);
        }

        public TaskCompletionSource<int> InputTaskCompletionSource { get; set; }

        internal static MaterialSimpleDialogConfiguration GlobalConfiguration { get; set; }

        internal static async Task<int> ShowAsync(string title, IEnumerable<string> actions, MaterialSimpleDialogConfiguration configuration = null)
        {
            var dialog = new MaterialSimpleDialog(configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<int>(),
                DialogTitle = { Text = title }
            };
            dialog.CreateActions(actions.ToList(), configuration);

            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        private void Configure(MaterialSimpleDialogConfiguration configuration)
        {
            var preferredConfig = configuration ?? GlobalConfiguration;

            if (preferredConfig == null)
            {
                return;
            }

            BackgroundColor = preferredConfig.ScrimColor;
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
                    TextColor = preferredConfig?.TextColor ?? Color.FromArgb("#DE000000"),
                    FontFamily = preferredConfig != null
                        ? preferredConfig.TextFontFamily
                        : Material.FontFamily.Body1
                };
                actionModel.SelectedCommand = new Command<int>(async (position) =>
                {
                    // Prevent any parrallel execution when clicking fast on the element
                    await SemaphoreSlim.WaitAsync();

                    try
                    {
                        if (InputTaskCompletionSource?.Task.Status != TaskStatus.WaitingForActivation)
                        {
                            return;
                        }

                        actionModel.IsSelected = true;
                        await DismissAsync();
                        InputTaskCompletionSource?.SetResult(position);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        SemaphoreSlim.Release();
                    }
                });

                actionModels.Add(actionModel);
                actionModel.Index = actionModels.IndexOf(actionModel);
            });

            DialogActionList.SetValue(BindableLayout.ItemsSourceProperty, actionModels);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ChangeLayout();
        }

        protected override void OnOrientationChanged(DisplayOrientation orientation)
        {
            base.OnOrientationChanged(orientation);

            ChangeLayout();
        }

        private void ChangeLayout()
        {
            switch (DisplayOrientation)
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
            InputTaskCompletionSource.SetResult(-1);
        }

        protected override bool OnBackgroundClicked()
        {
            InputTaskCompletionSource.SetResult(-1);

            return base.OnBackgroundClicked();
        }
    }
}
