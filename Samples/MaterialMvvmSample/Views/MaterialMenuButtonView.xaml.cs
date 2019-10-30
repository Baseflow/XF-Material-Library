using System;
using System.Collections.Generic;
using MaterialMvvmSample.ViewModels;
using Xamarin.Forms;
using XF.Material.Forms.Models;

namespace MaterialMvvmSample.Views
{
    public partial class MaterialMenuButtonView : ContentPage
    {
        public MaterialMenuButtonView()
        {
            InitializeComponent();
            BindingContext = new MaterialMenuButtonViewModel();
        }
    }
}
