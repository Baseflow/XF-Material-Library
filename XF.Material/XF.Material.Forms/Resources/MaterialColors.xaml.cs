
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
            this.SetColors(materialColor ?? new MaterialColorConfiguration());
        }

        private void SetColors(MaterialColorConfiguration materialColor)
        {
            this.TryAddColorResource(MaterialConstants.Color.PRIMARY, materialColor.Primary);
            this.TryAddColorResource(MaterialConstants.Color.PRIMARY_VARIANT, materialColor.PrimaryVariant);
            this.TryAddColorResource(MaterialConstants.Color.ONPRIMARY, materialColor.OnPrimary);
            this.TryAddColorResource(MaterialConstants.Color.SECONDARY, materialColor.Secondary);
            this.TryAddColorResource(MaterialConstants.Color.SECONDARY_VARIANT, materialColor.SecondaryVariant);
            this.TryAddColorResource(MaterialConstants.Color.ONSECONDARY, materialColor.OnSecondary);
            this.TryAddColorResource(MaterialConstants.Color.BACKGROUND, materialColor.Background);
            this.TryAddColorResource(MaterialConstants.Color.ONBACKGROUND, materialColor.OnBackground);
            this.TryAddColorResource(MaterialConstants.Color.SURFACE, materialColor.Surface);
            this.TryAddColorResource(MaterialConstants.Color.ONSURFACE, materialColor.OnSurface);
            this.TryAddColorResource(MaterialConstants.Color.ERROR, materialColor.Error);
            this.TryAddColorResource(MaterialConstants.Color.ONERROR, materialColor.OnError);

            Material.PlatformConfiguration.ChangeStatusBarColor(materialColor.PrimaryVariant);
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