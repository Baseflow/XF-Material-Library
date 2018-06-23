using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDialog : BaseMaterialModalPage
    {
        private Command _hideCommand => new Command(async () => await this.HideDialog());

        public MaterialDialog()
        {
            InitializeComponent();
        }

        public static void Alert(string message, string title = "Alert")
        {
            var dialog = new MaterialDialog(message, title);

            PopupNavigation.Instance.PushAsync(dialog, true);
        }

        public static async Task AlertAsync(string message, string title = "Alert")
        {
            var dialog = new MaterialDialog(message, title);

            await PopupNavigation.Instance.PushAsync(dialog, true);
        }

        public static async Task AlertAsync(string message, string title, string positiveButtonText, Action positiveAction, string negativeButtonText = "CANCEL", Action negativeAction = null)
        {
            var dialog = new MaterialDialog(message, title, positiveButtonText, positiveAction, negativeButtonText, negativeAction);

            await PopupNavigation.Instance.PushAsync(dialog, true);
        }

        public MaterialDialog(string message, string title = "Alert")
        {
            InitializeComponent();
            this.Message.Text = message;
            this.Title.Text = title;
            this.PositiveButton.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = _hideCommand
            });
            this.NegativeButton.IsVisible = false;
        }

        public MaterialDialog(string message, string title, string positiveButtonText, Action positiveAction, string negativeButtonText = "CANCEL", Action negativeAction = null)
        {
            InitializeComponent();
            this.Message.Text = message;
            this.Title.Text = title;
            this.PositiveButtonLabel.Text = positiveButtonText.ToUpper();
            this.PositiveButton.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () => await HideDialog(positiveAction))
            });
            this.NegativeButtonLabel.Text = negativeButtonText.ToUpper();
            this.NegativeButton.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () => await HideDialog(negativeAction))
            });
        }

        private async Task HideDialog(Action action = null)
        {
            await PopupNavigation.Instance.PopAsync(true);
            action?.Invoke();
        }

        protected override void OnDisappearingAnimationEnd()
        {
            this.PositiveButton.GestureRecognizers.Clear();
            this.NegativeButton.GestureRecognizers.Clear();

            base.OnDisappearingAnimationEnd();
        }
    }
}