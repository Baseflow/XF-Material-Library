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
        internal MaterialDialog(MaterialDialogConfiguration configuration)
        {
            this.InitializeComponent();

            if (configuration != null)
            {
                this.Configure(configuration);
            }
        }

        internal MaterialDialog(string message, string title, string positiveButtonText, string negativeButtonText, Action positiveAction, MaterialDialogConfiguration configuration) : this(configuration)
        {
            Message.Text = message;
            Title.Text = title;
            PositiveButton.Text = positiveButtonText;
            PositiveButton.Command = new Command(() => this.HideDialog(positiveAction));
            NegativeButton.Text = negativeButtonText;
            NegativeButton.Command = new Command(() => this.HideDialog());
        }

        internal static async Task AlertAsync(string message, MaterialDialogConfiguration configuration = null)
        {
            var dialog = new MaterialDialog(message, null, "Ok", null, null, configuration);
            await dialog.ShowAsync();
        }

        internal static async Task AlertAsync(string message, string title, MaterialDialogConfiguration configuration = null)
        {
            var dialog = new MaterialDialog(message, title, "Ok", null, null, configuration);
            await dialog.ShowAsync();
        }

        //internal static async Task AlertAsync(string message, string title, string positiveButtonText, Action positiveAction, string negativeButtonText = "CANCEL", Action negativeAction = null)
        //{
        //    var dialog = new MaterialDialog(message, title, positiveButtonText, positiveAction, negativeButtonText, negativeAction);

        //    await dialog.ShowAsync();
        //}

        //internal MaterialDialog(string message, string title = "Alert", MaterialDialogConfiguration configuration = null) : this(configuration)
        //{
        //    Message.Text = message;
        //    Title.Text = title;
        //    PositiveButton.Command = new Command(() => this.HideDialog());
        //    NegativeButton.IsVisible = false;
        //}

        //internal MaterialDialog(string message, string title, string positiveButtonText, Action positiveAction, string negativeButtonText = "CANCEL", Action negativeAction = null, MaterialDialogConfiguration configuration = null) : this(configuration)
        //{
        //    Message.Text = message;
        //    Title.Text = title;
        //    PositiveButton.Text = positiveButtonText.ToUpper();
        //    PositiveButton.Command = new Command(() => this.HideDialog(positiveAction));
        //    NegativeButton.Text = negativeButtonText.ToUpper();
        //    NegativeButton.Command = new Command(() => this.HideDialog(negativeAction));
        //}

        private void Configure(MaterialDialogConfiguration configuration)
        {
            this.BackgroundColor = configuration.ScrimColor;
            Container.CornerRadius = configuration.CornerRadius;
            Container.BackgroundColor = configuration.BackgroundColor;
            Title.TextColor = configuration.TitleTextColor;
            Title.FontFamily = configuration.TitleFontFamily;
            Message.TextColor = configuration.MessageTextColor;
            Message.FontFamily = configuration.MessageFontFamily;
            PositiveButton.TextColor = NegativeButton.TextColor = configuration.AccentColor;
            PositiveButton.AllCaps = NegativeButton.AllCaps = configuration.ButtonAllCaps;
            PositiveButton.FontFamily = NegativeButton.FontFamily = configuration.ButtonFontFamily;
        }

        private void HideDialog(Action action = null)
        {
            action?.Invoke();
            this.Dispose();
        }
    }

    public enum DialogAction
    {
        Primary,
        Secondary
    }
}