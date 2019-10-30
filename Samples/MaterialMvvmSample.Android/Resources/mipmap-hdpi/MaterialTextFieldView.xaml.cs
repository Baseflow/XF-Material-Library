using System;
using System.Collections.Generic;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialTextFieldView : ContentPage
    {
        public MaterialTextFieldView()
        {
            InitializeComponent();
            BindingContext = new MaterialDialogsViewModel();

        }
    }
}
