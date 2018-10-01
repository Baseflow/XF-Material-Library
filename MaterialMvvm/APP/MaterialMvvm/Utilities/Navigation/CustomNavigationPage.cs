using MaterialMvvm.Helpers;
using Xamarin.Forms;
using System.Linq;

namespace MaterialMvvm.Utilities.Navigation
{
    public class CustomNavigationPage : XF.Material.Forms.Views.MaterialNavigationPage, ICleanUp
    {
        public CustomNavigationPage(Page root) : base(root)
        {
            this.Popped += this.CustomNavigationPage_Popped;
        }

        /// <summary>
        /// Method to clean this <see cref="NavigationPage"/> and its <see cref="NavigationPage.RootPage"/>.
        /// </summary>
        public void CleanUp()
        {
            foreach (var view in this.Navigation.NavigationStack.OfType<ICleanUp>())
            {
                System.Diagnostics.Debug.WriteLine("Cleaning up: " + view);
                view.CleanUp();
            }

            this.Popped -= this.CustomNavigationPage_Popped;
        }

        private void CustomNavigationPage_Popped(object sender, NavigationEventArgs e)
        {
            if (e.Page is ICleanUp view)
            {
                System.Diagnostics.Debug.WriteLine("Cleaning up: " + view);
                view.CleanUp();
            }
        }
    }
}
