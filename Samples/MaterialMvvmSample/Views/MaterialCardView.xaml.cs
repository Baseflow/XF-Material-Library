using System;
using System.Collections.Generic;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialCardView : ContentPage
    {
        public MaterialCardView()
        {
            InitializeComponent();
            BindingContext = new MaterialCardViewModel();
        }
    }
}
