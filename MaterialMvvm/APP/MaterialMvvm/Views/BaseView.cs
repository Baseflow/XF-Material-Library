using CommonServiceLocator;
using MaterialMvvm.Helpers;
using MaterialMvvm.ViewModels;
using Xamarin.Forms;

namespace MaterialMvvm.Views
{
    public class BaseView<TViewModel> : ContentPage, ICleanUp where TViewModel : BaseViewModel
    {
        private bool _viewModelInitialized;

        public BaseView()
        {
            this.BindingContext = this.ViewModel = ServiceLocator.Current.GetInstance<TViewModel>();
        }

        /// <summary>
        /// Gets the object that is currently bound as this view's <see cref="BindableObject.BindingContext"/>.
        /// </summary>
        protected TViewModel ViewModel { get; private set; }


        /// <summary>
        /// Method to clean up objects in this view by setting its <see cref="BaseView{TViewModel}.ViewModel"/>, <see cref="BindableObject.BindingContext"/> and <see cref="ContentPage.Content"/> to <see cref="null"/>.
        /// </summary>
        public virtual void CleanUp()
        {
            this.ViewModel.CleanUp();
            this.BindingContext = null;
            this.ViewModel = null;
            this.Content = null;
        }

        protected override void OnAppearing()
        {
            if(!_viewModelInitialized && this.BindingContext == null)
            {
                this.BindingContext = this.ViewModel;
                _viewModelInitialized = true;
            }

            base.OnAppearing();
        }
    }
}