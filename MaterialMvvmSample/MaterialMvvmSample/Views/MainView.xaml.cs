using MaterialMvvmSample.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms;
using XF.Material.Forms.UI;
using XF.Material.Forms.UI.Dialogs;

namespace MaterialMvvmSample.Views
{
    public partial class MainView : BaseMainView
    {
        public MainView()
        {
            this.InitializeComponent();
        }

        private void MaterialTextField_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as MaterialTextField).HasError = string.IsNullOrEmpty(e.NewTextValue);
        }

        private void MaterialTextField_ChoiceSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Debug.WriteLine($"Selected {e.SelectedItem}");
        }
    }

    public abstract class BaseMainView : BaseView<MainViewModel> { }
}
