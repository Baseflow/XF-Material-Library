using System;
using System.Collections.Generic;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingView : ContentPage
    {
        public LandingView()
        {
            InitializeComponent();
            BindingContext = new LandingViewModel();

        }
    }


    //public abstract class BaseLandingView : BaseView<LandingViewModel> { }
}
