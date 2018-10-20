using MaterialMvvmSample.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaterialMvvmSample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand ButtonCommand => new Command(async () => await this.Navigate());

        private async Task Navigate()
        {
            await this.Navigation.PushAsync(ViewNames.MainView);
        }
    }
}
