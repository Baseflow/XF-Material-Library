using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace XF.Material.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MaterialLoadingDialog : BaseMaterialModalPage
	{
		public MaterialLoadingDialog ()
		{
			InitializeComponent ();
		}

        public MaterialLoadingDialog(string message)
        {
            InitializeComponent();
            this.Message.Text = message;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadingImage.Play();
        }

        public static async Task<MaterialLoadingDialog> Loading(string message)
        {
            var dialog = new MaterialLoadingDialog(message);
            await PopupNavigation.Instance.PushAsync(dialog);

            return dialog;
        }

        public void Hide()
        {
            this.Dispose();
        }
	}
}