using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDialog : BaseMaterialModalPage
    {
        private Command _hideCommand => new Command(() => this.HideDialog());

        internal MaterialDialog()
        {
            InitializeComponent();
        }

        public static async Task AlertAsync(string message, string title = "Alert")
        {
            var dialog = new MaterialDialog(message, title);

            await dialog.ShowAsync();
        }

        public static async Task AlertAsync(string message, string title, string positiveButtonText, Action positiveAction, string negativeButtonText = "CANCEL", Action negativeAction = null)
        {
            var dialog = new MaterialDialog(message, title, positiveButtonText, positiveAction, negativeButtonText, negativeAction);

            await dialog.ShowAsync();
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
                Command = new Command(() => this.HideDialog(positiveAction))
            });
            this.NegativeButtonLabel.Text = negativeButtonText.ToUpper();
            this.NegativeButton.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => this.HideDialog(negativeAction))
            });
        }

        private void HideDialog(Action action = null)
        {
            action?.Invoke();
            this.Dispose();
        }


        public override void Dispose()
        {
            this.PositiveButton.GestureRecognizers.Clear();
            this.NegativeButton.GestureRecognizers.Clear();

            base.Dispose();
        }
    }
}