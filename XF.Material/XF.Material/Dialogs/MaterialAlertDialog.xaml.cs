using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Dialogs.Configurations;

namespace XF.Material.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialAlertDialog : BaseMaterialModalPage
    {
        private EventHandler _backgroundClicked = null;
        private EventHandler _backButtonPressed = null;

        internal static MaterialAlertDialogConfiguration GlobalConfiguration { get; set; }

        internal MaterialAlertDialog(string message, string title, string action1Text, string action2Text, Action action1, Action action2 = null, MaterialAlertDialogConfiguration configuration = null) : this(configuration)
        {
            Message.Text = message;
            DialogTitle.Text = title;
            PositiveButton.Text = action1Text;
            PositiveButton.Command = new Command(() => this.HideDialog(action1));
            NegativeButton.Text = action2Text;
            NegativeButton.Command = new Command(() => this.HideDialog(action2));
        }

        internal MaterialAlertDialog(MaterialAlertDialogConfiguration configuration)
        {
            this.InitializeComponent();
            this.Configure(configuration);
        }

        internal static async Task AlertAsync(string message, string acknowledgementText = "Ok", MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialAlertDialog(message, null, acknowledgementText, null, null, configuration: configuration);
            await dialog.ShowAsync();
        }

        internal static async Task AlertAsync(string message, string title, string acknowledgementText = "Ok", MaterialAlertDialogConfiguration configuration = null)
        {
            var dialog = new MaterialAlertDialog(message, title, acknowledgementText, null, null, configuration: configuration);
            await dialog.ShowAsync();
        }

        internal static async Task<bool> ConfirmAsync(string message, string confirmingText, string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null)
        {
            var tcs = new TaskCompletionSource<bool>();
            var dialog = new MaterialAlertDialog(message, null, confirmingText, dismissiveText, () => tcs.SetResult(true), () => tcs.SetResult(false), configuration)
            {
                _backgroundClicked = (s, e) =>
                {
                    tcs.SetResult(false);
                },
                _backButtonPressed = (s, e) =>
                {
                    tcs.SetResult(false);
                }
            };

            dialog.BackgroundClicked += dialog._backgroundClicked;
            dialog.BackButtonPressed += dialog._backButtonPressed;

            await dialog.ShowAsync();

            return await tcs.Task;
        }

        internal static async Task<bool> ConfirmAsync(string message, string title, string confirmingText, string dismissiveText = "Cancel", MaterialAlertDialogConfiguration configuration = null)
        {
            var tcs = new TaskCompletionSource<bool>();
            var dialog = new MaterialAlertDialog(message, title, confirmingText, dismissiveText, () => tcs.SetResult(true), () => tcs.SetResult(false), configuration)
            {
                _backgroundClicked = (s, e) =>
                {
                    tcs.SetResult(false);
                },
                _backButtonPressed = (s, e) =>
                {
                    tcs.SetResult(false);
                }
            };

            dialog.BackgroundClicked += dialog._backgroundClicked;
            dialog.BackButtonPressed += dialog._backButtonPressed;

            await dialog.ShowAsync();

            return await tcs.Task;
        }

        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();

            this.BackButtonPressed -= _backButtonPressed;
            this.BackgroundClicked -= _backgroundClicked;
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