using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Dialogs.Configurations;

namespace XF.Material.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDialog : BaseMaterialModalPage
    {
        internal static MaterialAlertDialogConfiguration GlobalConfiguration { get; set; }

        internal MaterialDialog(string message, string title, string action1Text, string action2Text, Action action, MaterialAlertDialogConfiguration configuration)
        {
            this.InitializeComponent();
            this.Configure(configuration);

            Message.Text = message;
            DialogTitle.Text = title;
            PositiveButton.Text = action1Text;
            PositiveButton.Command = new Command(() => this.HideDialog(action));
            NegativeButton.Text = action2Text;
            NegativeButton.Command = new Command(() => this.HideDialog());
        }

        internal static async Task AlertAsync(string message, string acknowledgementText = "Ok", MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialDialog(message, null, acknowledgementText, null, null, configuration);
            await dialog.ShowAsync();
        }

        internal static async Task AlertAsync(string message, string title, string acknowledgementText = "Ok", MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialDialog(message, title, acknowledgementText, null, null, configuration);
            await dialog.ShowAsync();
        }

        internal static async Task AlertAsync(string message, string confirmingText, Action confirmingAction, string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialDialog(message, null, confirmingText, dismissiveText, confirmingAction, configuration);
            await dialog.ShowAsync();
        }

        internal static async Task AlertAsync(string message, string title, string confirmingText, Action confirmingAction, string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialDialog(message, title, confirmingText, dismissiveText, confirmingAction, configuration);
            await dialog.ShowAsync();
        }

        private void Configure(MaterialAlertDialogConfiguration configuration)
        {
            var preferredConfig = configuration ?? GlobalConfiguration;

            if (preferredConfig != null)
            {
                this.BackgroundColor = preferredConfig.ScrimColor;
                Container.CornerRadius = preferredConfig.CornerRadius;
                Container.BackgroundColor = preferredConfig.BackgroundColor;
                DialogTitle.TextColor = preferredConfig.TitleTextColor;
                DialogTitle.FontFamily = preferredConfig.TitleFontFamily;
                Message.TextColor = preferredConfig.MessageTextColor;
                Message.FontFamily = preferredConfig.MessageFontFamily;
                PositiveButton.TextColor = NegativeButton.TextColor = preferredConfig.TintColor;
                PositiveButton.AllCaps = NegativeButton.AllCaps = preferredConfig.ButtonAllCaps;
                PositiveButton.FontFamily = NegativeButton.FontFamily = preferredConfig.ButtonFontFamily;
            }
        }

        private void HideDialog(Action action = null)
        {
            action?.Invoke();
            this.Dispose();
        }
    }
}