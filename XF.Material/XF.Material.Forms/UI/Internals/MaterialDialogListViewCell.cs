using System.Threading.Tasks;
using Xamarin.Forms;

namespace XF.Material.Forms.UI.Internals
{
    public class MaterialDialogListViewCell : ViewCell
    {
        public bool ItemFades { get; set; }

        internal MaterialDialogListViewCell() { }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(this.ItemFades)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(150);
                    await Task.WhenAll(this.View.FadeTo(1, 150, Easing.SinOut), this.View.TranslateTo(0, 0, 150, Easing.SinOut));
                });
            }
        }
    }
}
