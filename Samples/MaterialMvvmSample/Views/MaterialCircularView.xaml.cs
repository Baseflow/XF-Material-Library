using System;
using System.Collections.Generic;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialCircularView : ContentPage
    {
        public MaterialCircularView()
        {
            InitializeComponent();
            BindingContext = new MaterialCircularViewModel();
        }
    }
}
