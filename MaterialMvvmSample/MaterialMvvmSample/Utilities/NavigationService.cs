using MaterialMvvmSample.Controls;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaterialMvvmSample.Utilities
{
    public class NavigationService : INavigationService
    {
        private CustomNavigationPage _currentNavigationPage;

        public async Task PopAsync()
        {
            await _currentNavigationPage?.PopViewAsync();
        }

        public async Task PushAsync(string viewName, object parameter = null)
        {
            await _currentNavigationPage?.PushViewAsync(viewName, parameter);
        }

        public async Task PushModalAsync(string viewName, object parameter = null)
        {
            await _currentNavigationPage?.PushModalAsync(viewName, parameter);
        }

        public async Task PopModalAsync()
        {
            await _currentNavigationPage?.PopModalAsync();
        }

        public void SetRootView(string rootViewName, object parameter = null)
        {
            _currentNavigationPage = new CustomNavigationPage(rootViewName, parameter);
            Application.Current.MainPage = _currentNavigationPage;
        }
    }
}
