using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Dialogs.Configurations;
using XF.Material.Forms.Dialogs.Internals;

namespace XF.Material.Forms.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialSimpleDialog : BaseMaterialModalPage, IMaterialAwaitableDialog<int>
    {
        private const int _rowHeight = 48;

        internal MaterialSimpleDialog(MaterialSimpleDialogConfiguration configuration)
        {
            this.InitializeComponent();
            this.Configure(configuration);
        }

        public TaskCompletionSource<int> InputTaskCompletionSource { get; set; }

        internal static MaterialSimpleDialogConfiguration GlobalConfiguration { get; set; }

        internal static async Task<int> ShowAsync(string title, IList<string> actions, MaterialSimpleDialogConfiguration configuration = null)
        {
            var dialog = new MaterialSimpleDialog(configuration) { InputTaskCompletionSource = new TaskCompletionSource<int>() };
            dialog.DialogTitle.Text = title;
            dialog.CreateActions(actions.ToList(), configuration);

            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        private void Configure(MaterialSimpleDialogConfiguration configuration)
        {
            var preferredConfig = configuration ?? GlobalConfiguration;

            if (preferredConfig != null)
            {
                this.BackgroundColor = preferredConfig.ScrimColor;
                Container.CornerRadius = preferredConfig.CornerRadius;
                Container.BackgroundColor = preferredConfig.BackgroundColor;
                DialogTitle.TextColor = preferredConfig.TitleTextColor;
                DialogTitle.FontFamily = preferredConfig.TitleFontFamily;
            }
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
                var actionModel = new ActionModel { Text = a };
                actionModel.TextColor = preferredConfig != null ? preferredConfig.TextColor : Color.FromHex("#DE000000");
                actionModel.FontFamily = preferredConfig != null ? preferredConfig.TextFontFamily : Material.FontFamily.Body1;
                actionModel.SelectedCommand = new Command<int>((position) =>
                {
                    if (this.InputTaskCompletionSource?.Task.Status == TaskStatus.WaitingForActivation)
                    {
                        actionModel.IsSelected = true;
                        this.InputTaskCompletionSource?.SetResult(position);
                        this.Dismiss();
                    }
                });

                actionModels.Add(actionModel);
                actionModel.Index = actionModels.IndexOf(actionModel);
            });

            DialogActionList.RowHeight = _rowHeight;
            DialogActionList.HeightRequest = (_rowHeight * actionModels.Count) + 2;
            DialogActionList.ItemsSource = actionModels;
        }
    }
}