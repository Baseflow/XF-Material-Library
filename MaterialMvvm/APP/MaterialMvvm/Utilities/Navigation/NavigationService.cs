using CommonServiceLocator;
using MaterialMvvm.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaterialMvvm.Utilities.Navigation
{
    public sealed class NavigationService : INavigationService
    {
        #region Fields

        private static readonly Stack<CustomNavigationPage> _navigationPageStack = new Stack<CustomNavigationPage>();
        private EventHandler _onAppearing;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the current visible Page key
        /// </summary>
        public string CurrentPageKey { get; set; }

        private CustomNavigationPage CurrentNavigationPage => _navigationPageStack.Peek();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets the Root Page of a new Navigation Stack.
        /// </summary>
        /// <param name="rootPageKey">The key to identify the root page. See also <see cref="ViewNames"/>.</param>
        /// <param name="parameter">The parameter to be passed to the view model of the root page.</param>
        public void SetRootPage(string rootPageKey, object parameter = null)
        {
            var rootPage = GetPage(rootPageKey, parameter);
            var mainPage = new CustomNavigationPage(rootPage);

            if (_navigationPageStack.Count > 0)
            {
                this.CurrentNavigationPage.CleanUp();
            }

            _navigationPageStack.Clear();
            _navigationPageStack.Push(mainPage);

            this.CurrentPageKey = rootPageKey;

            Application.Current.MainPage = mainPage;
        }

        /// <summary>
        /// Navigate back to the most recent Page.
        /// </summary>
        public async Task GoBack()
        {
            var navigationStack = this.CurrentNavigationPage.Navigation;
            if (navigationStack.NavigationStack.Count > 1)
            {
                await this.CurrentNavigationPage.PopAsync();
                return;
            }

            if (_navigationPageStack.Count > 1)
            {
                _navigationPageStack.Pop();
                await this.CurrentNavigationPage.Navigation.PopModalAsync();
                return;
            }

            await this.CurrentNavigationPage.PopAsync();
        }

        /// <summary>
        /// Navigate to a Page displayed modally.
        /// </summary>
        /// <param name="pageKey">The key to identify the Page to navigate to.</param>
        /// <param name="animated">Sets whether the navigation will be animated.</param>
        public async Task NavigateModalAsync(string pageKey, bool animated = true)
        {
            await NavigateModalAsync(pageKey, null, animated);
        }

        /// <summary>
        /// Navigate to a Page displayed modally.
        /// </summary>
        /// <param name="pageKey">The key to identify the Page to navigate to.</param>
        /// <param name="parameter">The parameter to be passed to the view model of the Page that will be navigated to.</param>
        /// <param name="animated">Sets whether the navigation will be animated.</param>
        public async Task NavigateModalAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);

            await this.CurrentNavigationPage.Navigation.PushModalAsync(page, animated);

            this.CurrentPageKey = pageKey;
        }

        /// <summary>
        /// Navigate to a Page
        /// </summary>
        /// <param name="pageKey">The key to identify the Page to navigate to.</param>
        /// <param name="animated">Sets whether the navigation will be animated.</param>
        public async Task NavigateAsync(string pageKey, bool animated = true)
        {
            await NavigateAsync(pageKey, null, animated);
        }

        /// <summary>
        /// Navigate to a Page
        /// </summary>
        /// <param name="pageKey">The key to identify the Page to navigate to.</param>
        /// <param name="parameter">The parameter to be passed to the view model of the Page that will be navigated to.</param>
        /// <param name="animated">Sets whether the navigation will be animated.</param>
        public async Task NavigateAsync(string pageKey, object parameter, bool animated = true)
        {
            var page = GetPage(pageKey, parameter);

            await this.CurrentNavigationPage.Navigation.PushAsync(page, animated);

            this.CurrentPageKey = pageKey;
        }

        /// <summary>
        /// Initializes and retrieves a Page.
        /// </summary>
        /// <param name="pageKey">The key to identify the page to be retrieved.</param>
        /// <param name="parameter">The parameter to be passed to the view model of the Page that will be retrieved.</param>
        private Page GetPage(string pageKey, object parameter = null)
        {
            var page = ServiceLocator.Current.GetInstance<Page>(pageKey);

            this._onAppearing = (s, e) =>
            {
                if (page.BindingContext is BaseViewModel viewModel)
                {
                    viewModel.Init(parameter);
                }

                page.Appearing -= this._onAppearing;
            };

            page.Appearing += this._onAppearing;

            return page;
        }

        #endregion Methods
    }
}