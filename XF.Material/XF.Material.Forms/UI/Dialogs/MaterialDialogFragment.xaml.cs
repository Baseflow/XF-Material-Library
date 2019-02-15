using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace XF.Material.Forms.UI.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDialogFragment : BaseMaterialModalPage, IMaterialAwaitableDialog<bool?>
    {
        internal MaterialDialogFragment(MaterialAlertDialogConfiguration configuration)
        {
            this.InitializeComponent();
            this.Configure(configuration);
            this.InputTaskCompletionSource = new TaskCompletionSource<bool?>();
        }

        public TaskCompletionSource<bool?> InputTaskCompletionSource { get; set; }

        public static async Task<bool?> ShowAsync(View view, string message, string title, string positiveButtonText, string negativeButtonText, MaterialAlertDialogConfiguration configuration)
        {
            var d = new MaterialDialogFragment(configuration);
            d.container.Content = view ?? throw new ArgumentNullException(nameof(view));
            d.DialogTitle.Text = title;
            d.Message.Text = message;
            d.PositiveButton.Text = positiveButtonText;
            d.NegativeButton.Text = negativeButtonText;

            await d.ShowAsync();

            return await d.InputTaskCompletionSource.Task;
        }

        public override void OnBackButtonDismissed()
        {
            this.InputTaskCompletionSource?.SetResult(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            PositiveButton.Clicked += this.PositiveButton_Clicked;
            NegativeButton.Clicked += this.NegativeButton_Clicked;

            this.ChangeLayout();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            PositiveButton.Clicked -= this.PositiveButton_Clicked;
            NegativeButton.Clicked -= this.NegativeButton_Clicked;
        }

        protected override void OnOrientationChanged(DisplayOrientation orientation)
        {
            base.OnOrientationChanged(orientation);

            this.ChangeLayout();
        }

        private void ChangeLayout()
        {
            if (this.DisplayOrientation == DisplayOrientation.Landscape && Device.Idiom == TargetIdiom.Phone)
            {
                Container.WidthRequest = 560;
            }
            else if (this.DisplayOrientation == DisplayOrientation.Portrait && Device.Idiom == TargetIdiom.Phone)
            {
                Container.WidthRequest = 280;
            }
        }

        internal static MaterialAlertDialogConfiguration GlobalConfiguration { get; set; }

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

        private async void NegativeButton_Clicked(object sender, System.EventArgs e)
        {
            await this.DismissAsync();
            this.InputTaskCompletionSource?.SetResult(false);
        }

        private async void PositiveButton_Clicked(object sender, System.EventArgs e)
        {
            await this.DismissAsync();
            this.InputTaskCompletionSource?.SetResult(true);
        }
    }
}