using CommonServiceLocator;
using Xamarin.Forms;

namespace MaterialMvvmSample.Utilities
{
    public static class ViewFactory
    {
        public static Page GetView(string viewName)
        {
            return ServiceLocator.Current.GetInstance<Page>(viewName);
        }

        public static TView GetView<TView>() where TView : ContentPage
        {
            return ServiceLocator.Current.GetInstance<TView>();
        }
    }
}
