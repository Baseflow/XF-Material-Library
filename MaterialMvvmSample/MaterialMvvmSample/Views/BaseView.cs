using MaterialMvvmSample.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public abstract class BaseView<TViewModel> : ContentPage where TViewModel : BaseViewModel
    {
        protected TViewModel ViewModel { get; }

        private bool _bindContextSet = false;

        protected BaseView()
        {
            this.ViewModel = CommonServiceLocator.ServiceLocator.Current.GetInstance<TViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.BindingContext == null && !_bindContextSet)
            {
                this.BindingContext = this.ViewModel;
                _bindContextSet = true;
            }
        }
    }
}
