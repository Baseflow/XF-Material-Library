using MaterialMvvmSample.Utilities;
using MaterialMvvmSample.ViewModels;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.Views;

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
            await this.Navigation.PopAsync(true);
        }

        public async Task PushViewAsync(string rootViewName, object parameter = null)
        {
            _currentNavigationParameter = parameter;
            var view = ViewFactory.GetView(rootViewName);
            await this.Navigation.PushAsync(view, true);
        }

        public async Task PushModalAsync(string rootViewName, object parameter = null)
        {
            _currentNavigationParameter = parameter;
            var view = ViewFactory.GetView(rootViewName);
            await this.Navigation.PushModalAsync(view, true);
        }

        public async Task PopModalAsync()
        {
            await this.Navigation.PopModalAsync(true);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.RootPage) && this.RootPage != null)
            {
                this.RootPage.Appearing += this.AppearingHandler;
            }
        }

        protected override void OnPagePushed(Page page)
        {
            if (page.BindingContext is BaseViewModel viewModel)
            {
                viewModel?.OnViewPushed(_currentNavigationParameter);
                System.Diagnostics.Debug.WriteLine("Page pushed");
                _currentNavigationParameter = null;
            }
        }

        protected override void OnPagePopped(Page page)
        {
            if (page.BindingContext is BaseViewModel viewModel)
            {
                viewModel.OnViewPopped();
            }
        }

        private void AppearingHandler(object sender, EventArgs e)
        {
            var viewModel = this.RootPage.BindingContext as BaseViewModel;
            viewModel?.OnViewPushed(_currentNavigationParameter);
            _currentNavigationParameter = null;
            this.RootPage.Appearing -= this.AppearingHandler;
        }
    }
}
