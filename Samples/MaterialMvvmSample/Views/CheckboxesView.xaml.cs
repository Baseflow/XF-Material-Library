using System;
using System.Collections.Generic;
using System.ComponentModel;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public partial class CheckboxesView : ContentPage
    {
        public CheckboxesView()
        {
            InitializeComponent();
            BindingContext = new CheckboxesViewModel();

        }
    }
}

