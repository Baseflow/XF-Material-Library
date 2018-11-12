using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Forms.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialColors : ResourceDictionary
    {
        internal MaterialColors(MaterialColorConfiguration materialColor)
        {
            this.InitializeComponent();
            this.SetColors(materialColor);
        }

        private void SetColors(MaterialColorConfiguration materialColor)
        {
            this.TryAddColorResource(MaterialConstants.Color.PRIMARY, materialColor.Primary);
            this.TryAddColorResource(MaterialConstants.Color.PRIMARY_VARIANT, materialColor.PrimaryVariant);
            this.TryAddColorResource(MaterialConstants.Color.ON_PRIMARY, materialColor.OnPrimary);
            this.TryAddColorResource(MaterialConstants.Color.SECONDARY, materialColor.Secondary);
            this.TryAddColorResource(MaterialConstants.Color.SECONDARY_VARIANT, materialColor.SecondaryVariant);
            this.TryAddColorResource(MaterialConstants.Color.ON_SECONDARY, materialColor.OnSecondary);
            this.TryAddColorResource(MaterialConstants.Color.BACKGROUND, materialColor.Background);
            this.TryAddColorResource(MaterialConstants.Color.ON_BACKGROUND, materialColor.OnBackground);
            this.TryAddColorResource(MaterialConstants.Color.SURFACE, materialColor.Surface);
            this.TryAddColorResource(MaterialConstants.Color.ON_SURFACE, materialColor.OnSurface);
            this.TryAddColorResource(MaterialConstants.Color.ERROR, materialColor.Error);
            this.TryAddColorResource(MaterialConstants.Color.ON_ERROR, materialColor.OnError);
        }

        private void TryAddColorResource(string key, Color color)
        {
            if (key == null || color.IsDefault)
            {
                return;
            }

            this.Add(key, color);
        }
    }
}