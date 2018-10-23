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

        protected BaseView()
        {
            this.ViewModel = CommonServiceLocator.ServiceLocator.Current.GetInstance<TViewModel>();
            this.BindingContext = this.ViewModel;
        }
    }
}
