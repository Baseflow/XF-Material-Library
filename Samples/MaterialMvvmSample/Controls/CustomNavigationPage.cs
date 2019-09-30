using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MaterialMvvmSample.Utilities;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace MaterialMvvmSample.Controls
{
    public sealed class CustomNavigationPage : MaterialNavigationPage
    {
        private object _currentNavigationParameter;

        public CustomNavigationPage(string rootViewName, object parameter = null) : base(ViewFactory.GetView(rootViewName))
        {
            _currentNavigationParameter = parameter;
        }

        public async Task PopViewAsync()
        {
            await Navigation.PopAsync(true);
        }

        public async Task PushViewAsync(string rootViewName, object parameter = null)
        {
            _currentNavigationParameter = parameter;
            var view = ViewFactory.GetView(rootViewName);
            await Navigation.PushAsync(view, true);
        }

        public async Task PushModalAsync(string rootViewName, object parameter = null)
        {
            _currentNavigationParameter = parameter;
            var view = ViewFactory.GetView(rootViewName);
            await Navigation.PushModalAsync(view, true);
        }

        public async Task PopModalAsync()
        {
            await Navigation.PopModalAsync(true);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(RootPage) && RootPage != null)
            {
                RootPage.Appearing += AppearingHandler;
            }
        }

        protected override void OnPagePush(Page page)
        {
            base.OnPagePush(page);

            if (!(page.BindingContext is BaseViewModel viewModel))
            {
                return;
            }

            viewModel?.OnViewPushed(_currentNavigationParameter);
            _currentNavigationParameter = null;
        }

        protected override void OnPagePop(Page previousPage, Page poppedPage)
        {
            base.OnPagePop(previousPage, poppedPage);

            if (previousPage.BindingContext is BaseViewModel viewModel)
            {
                viewModel.OnViewPopped();
            }
        }

        private void AppearingHandler(object sender, EventArgs e)
        {
            var viewModel = RootPage.BindingContext as BaseViewModel;
            viewModel?.OnViewPushed(_currentNavigationParameter);
            _currentNavigationParameter = null;
            RootPage.Appearing -= AppearingHandler;
        }
    }
}
