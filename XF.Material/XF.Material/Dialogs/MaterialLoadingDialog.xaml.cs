using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace XF.Material.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MaterialLoadingDialog : BaseMaterialModalPage
	{
		internal MaterialLoadingDialog ()
		{
            InitializeComponent ();
		}

        internal MaterialLoadingDialog(string message)
        {
            InitializeComponent();
            this.Message.Text = message;
        }

        internal static async Task<IMaterialModalPage> Loading(string message)
        {
            var dialog = new MaterialLoadingDialog(message);
            await dialog.ShowAsync();

            return dialog;
        }

        internal void Hide()
        {
            this.Dispose();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadingImage.Play();
        }
    }
}