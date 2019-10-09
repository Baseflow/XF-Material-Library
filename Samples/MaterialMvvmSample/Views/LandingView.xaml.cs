using System;
using System.Collections.Generic;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaterialMvvmSample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingView : BaseLandingView
    {
        public LandingView()
        {
            InitializeComponent();
        }
    }


    public abstract class BaseLandingView : BaseView<LandingViewModel> { }
}
