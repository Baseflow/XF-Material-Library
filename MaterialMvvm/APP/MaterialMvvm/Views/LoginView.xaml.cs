using MaterialMvvm.ViewModels;
using Xamarin.Forms.Xaml;

namespace MaterialMvvm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : BaseLoginView
	{
		public LoginView ()
		{
			this.InitializeComponent ();
		}
	}

    public class BaseLoginView : BaseView<LoginViewModel> { }
}