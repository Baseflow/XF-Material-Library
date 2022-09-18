using Microsoft.Maui;
using Microsoft.Maui.Controls.Xaml;

namespace XF.Material.Maui.UI.Internals
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
