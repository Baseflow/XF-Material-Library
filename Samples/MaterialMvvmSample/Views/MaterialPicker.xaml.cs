using System;
using System.Collections.Generic;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialPicker : ContentPage
    {
        public MaterialPicker()
        {
            InitializeComponent();
            BindingContext = new MaterialPickerViewModel();

        }
    }
}
