using MaterialMvvmSample.Utilities;
using MaterialMvvmSample.ViewModels;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaterialMvvmSample.Controls
{
    public sealed class CustomNavigationPage : NavigationPage
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.Pushed += this.Navigation_Pushed;
            this.Popped += this.NavigationPage_Popped;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this.Pushed -= this.Navigation_Pushed;
            this.Popped -= this.NavigationPage_Popped;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.RootPage) && this.RootPage != null)
            {
                EventHandler appearingHandler = null;
                appearingHandler = delegate
                {
                    var viewModel = this.RootPage.BindingContext as BaseViewModel;
                    viewModel?.OnViewPushed(_currentNavigationParameter);
                    this.RootPage.Appearing -= appearingHandler;
                };

                this.RootPage.Appearing += appearingHandler;
            }
        }

        private void Navigation_Pushed(object sender, NavigationEventArgs e)
        {
            if (e.Page.BindingContext is BaseViewModel viewModel)
            {
                viewModel?.OnViewPushed(_currentNavigationParameter);
                System.Diagnostics.Debug.WriteLine("Page pushed");
                _currentNavigationParameter = null;
            }
        }

        private void NavigationPage_Popped(object sender, NavigationEventArgs e)
        {
            if (e.Page.BindingContext is BaseViewModel viewModel)
            {
                viewModel.OnViewPopped();
            }
        }
    }
}
