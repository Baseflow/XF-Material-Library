using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace XF.Material.Forms.UI.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialInputDialog : BaseMaterialModalPage, IMaterialAwaitableDialog<string>
    {
        internal MaterialInputDialog(string title = null, string message = null, string inputText = null, string inputPlaceholder = "Enter input", string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialInputDialogConfiguration configuration = null) : this(configuration)
        {
            InputTaskCompletionSource = new TaskCompletionSource<string>();
            Message.Text = message;
            DialogTitle.Text = title;
            TextField.Placeholder = inputPlaceholder;
            TextField.Text = inputText;
            PositiveButton.Text = confirmingText;
            NegativeButton.Text = dismissiveText;
            PositiveButton.Command = new Command(() =>
            {
                this.InputTaskCompletionSource?.SetResult(TextField.Text);
                this.Dismiss();
            });
            NegativeButton.Command = new Command(() =>
            {
                this.InputTaskCompletionSource?.SetResult(string.Empty);
                this.Dismiss();
            });
        }

        internal MaterialInputDialog(MaterialInputDialogConfiguration configuration = null)
        {
            this.InitializeComponent();
            this.Configure(configuration);
        }

        public TaskCompletionSource<string> InputTaskCompletionSource { get; set; }

        internal static MaterialInputDialogConfiguration GlobalConfiguration { get; set; }

        public static async Task<string> Show(string title = null, string message = null, string inputText = null, string inputPlaceholder = "Enter input", string confirmingText = "Ok", string dismissiveText = "Cancel", MaterialInputDialogConfiguration configuration = null)
        {
            var dialog = new MaterialInputDialog(title, message, inputText, inputPlaceholder, confirmingText, dismissiveText, configuration);
            dialog.PositiveButton.IsEnabled = false;

            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            TextField.TextChanged += this.TextField_TextChanged;
        }

        protected override bool OnBackButtonPressed()
        {
            this.InputTaskCompletionSource?.SetResult(string.Empty);

            return base.OnBackButtonPressed();
        }

        protected override bool OnBackgroundClicked()
        {
            this.InputTaskCompletionSource?.SetResult(string.Empty);

            return base.OnBackgroundClicked();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            TextField.TextChanged -= this.TextField_TextChanged;
        }

        private void Configure(MaterialInputDialogConfiguration configuration)
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
                PositiveButton.TextColor = NegativeButton.TextColor = TextField.TintColor = preferredConfig.TintColor;
                PositiveButton.AllCaps = NegativeButton.AllCaps = preferredConfig.ButtonAllCaps;
                PositiveButton.FontFamily = NegativeButton.FontFamily = preferredConfig.ButtonFontFamily;
                TextField.PlaceholderColor = preferredConfig.InputPlaceholderColor;
                TextField.TextColor = preferredConfig.InputTextColor;
                TextField.TextFontFamily = preferredConfig.InputTextFontFamily;
                TextField.PlaceholderFontFamily = preferredConfig.InputPlaceholderFontFamily;
                TextField.InputType = preferredConfig.InputType;
                TextField.MaxLength = preferredConfig.InputMaxLength;
            }
        }

        private void TextField_TextChanged(object sender, TextChangedEventArgs e)
        {
            PositiveButton.IsEnabled = !string.IsNullOrEmpty(e.NewTextValue);
        }
    }
}