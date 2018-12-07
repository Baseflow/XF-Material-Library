using MaterialMvvmSample.ViewModels;
using System.Collections.Generic;

namespace MaterialMvvmSample.Views
{
    public partial class MainView : BaseMainView
    {
        public MainView()
        {
            this.InitializeComponent();
            _textField.Choices = new List<string>
            {
                "Ayala Corporation",
                "San Miguel Corporation",
                "YNGEN Holdings Inc.",
                "ERNI Development Center Philippines, Inc., Bern, Switzerland"
            };
            _textField.Text = "Empty";
            _textField2.Text = "Empty";
        }

        private void MaterialButton_Clicked(object sender, System.EventArgs e)
        {
            _textField.Text = string.Empty;
            _textField2.Text = string.Empty;
        }
    }

    public abstract class BaseMainView : BaseView<MainViewModel> { }
}
