using MaterialMvvmSample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaterialMvvmSample.Views
{
    public partial class MainView : BaseMainView
    {
        public MainView()
        {
            this.InitializeComponent();
        }
    }

    public abstract class BaseMainView : BaseView<MainViewModel> { }
}
