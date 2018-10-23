using System.Threading.Tasks;

namespace MaterialMvvmSample.Utilities
{
    public interface INavigationService
    {
        void SetRootView(string rootViewName, object parameter = null);

        Task PushAsync(string viewName, object parameter = null);

        Task PopAsync();

        Task PushModalAsync(string viewName, object parameter = null);

        Task PopModalAsync();
    }
}
