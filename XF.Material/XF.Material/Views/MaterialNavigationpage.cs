using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XF.Material.Views
{
    public class MaterialNavigationPage : NavigationPage
    {
        public MaterialNavigationPage(Page rootPage) : base(rootPage)
        {
            this.SetDynamicResource(BarBackgroundColorProperty, "Material.Color.Primary");
        }
    }
}
