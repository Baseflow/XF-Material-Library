using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace XF.Material.Forms.UI.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialLoadingDialog : BaseMaterialModalPage
    {
        internal MaterialLoadingDialog(string message, MaterialLoadingDialogConfiguration configuration)
        {
            InitializeComponent();
            Configure(configuration);
            Message.Text = message;
        }

        internal MaterialLoadingDialog(string message, string lottieAnimation, MaterialLoadingDialogConfiguration configuration)
        {
            InitializeComponent();
            Configure(configuration);
            Message.Text = message;
            LoadingImage.Animation = lottieAnimation;
        }

        public override bool Dismissable => false;

        public override string MessageText
        {
            get { return Message.Text; }
            set { Message.Text = value; }
        }

        internal static MaterialLoadingDialogConfiguration GlobalConfiguration { get; set; }

        internal static async Task<IMaterialModalPage> Loading(string message, MaterialLoadingDialogConfiguration configuration = null)
        {
            var dialog = new MaterialLoadingDialog(message, configuration);
            await dialog.ShowAsync();

            return dialog;
        }

        internal static async Task<IMaterialModalPage> Loading(string message, string lottieAnimation, MaterialLoadingDialogConfiguration configuration = null)
        {
            var dialog = new MaterialLoadingDialog(message, lottieAnimation, configuration);
            await dialog.ShowAsync();

            return dialog;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadingImage.Play();

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

        private void Configure(MaterialLoadingDialogConfiguration configuration)
        {
            var preferredConfig = configuration ?? GlobalConfiguration;

            if (preferredConfig == null)
            {
                return;
            }

            BackgroundColor = preferredConfig.ScrimColor;
            Container.CornerRadius = preferredConfig.CornerRadius;
            Container.BackgroundColor = preferredConfig.BackgroundColor;
            Message.TextColor = preferredConfig.MessageTextColor;
            Message.FontFamily = preferredConfig.MessageFontFamily;
            LoadingImage.TintColor = preferredConfig.TintColor;
            Container.Margin = preferredConfig.Margin == default ? Material.GetResource<Thickness>("Material.Dialog.Margin") : preferredConfig.Margin;
        }
    }
}