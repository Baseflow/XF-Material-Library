using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDialog : BaseMaterialModalPage
    {
        internal MaterialDialog()
        {
            InitializeComponent();
        }

        internal static async Task AlertAsync(string message, string title = "Alert")
        {
            var dialog = new MaterialDialog(message, title);
            await dialog.ShowAsync();
        }

        internal static async Task AlertAsync(string message, string title, string positiveButtonText, Action positiveAction, string negativeButtonText = "CANCEL", Action negativeAction = null)
        {
            var dialog = new MaterialDialog(message, title, positiveButtonText, positiveAction, negativeButtonText, negativeAction);

            await dialog.ShowAsync();
        }

        internal MaterialDialog(string message, string title = "Alert")
        {
            InitializeComponent();
            this.Message.Text = message;
            this.Title.Text = title;
            this.PositiveButton.Command = new Command(() => this.HideDialog());
            this.NegativeButton.IsVisible = false;
        }

        internal MaterialDialog(string message, string title, string positiveButtonText, Action positiveAction, string negativeButtonText = "CANCEL", Action negativeAction = null)
        {
            InitializeComponent();
            this.Message.Text = message;
            this.Title.Text = title;
            this.PositiveButton.Text = positiveButtonText.ToUpper();
            this.PositiveButton.Command = new Command(() => this.HideDialog(positiveAction));
            this.NegativeButton.Text = negativeButtonText.ToUpper();
            this.NegativeButton.Command = new Command(() => this.HideDialog(negativeAction));
        }

        internal void AddAction(DialogAction dialogAction, string actionName, Action action)
        {
            switch(dialogAction)
            {
                case DialogAction.Primary:
                    this.PositiveButton.Text = actionName;
                    this.PositiveButton.Command = new Command(() => this.HideDialog(action));
                    break;
                case DialogAction.Secondary:
                    this.NegativeButton.IsVisible = true;
                    this.NegativeButton.Text = actionName;
                    this.NegativeButton.Command = new Command(() => this.HideDialog(action));
                    break;
            }
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