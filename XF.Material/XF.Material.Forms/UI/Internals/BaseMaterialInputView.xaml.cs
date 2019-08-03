using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Forms.UI.Internals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseMaterialInputView : ContentView
    {
        public BaseMaterialInputView()
        {
            InitializeComponent();
        }
    }
}