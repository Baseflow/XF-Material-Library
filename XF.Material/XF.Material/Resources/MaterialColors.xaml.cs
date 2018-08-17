
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialColors : ResourceDictionary
    {
        internal MaterialColors(MaterialColorConfiguration materialColor) : this()
        {
            if (materialColor == null)
            {
                materialColor = new MaterialColorConfiguration();
            }

            this.SetColors(materialColor);
        }

        internal MaterialColors()
        {
            InitializeComponent();
        }

        private void SetColors(MaterialColorConfiguration materialColor)
        {
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_PRIMARY, materialColor.Primary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_PRIMARY_VARIANT, materialColor.PrimaryVariant);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONPRIMARY, materialColor.OnPrimary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_SECONDARY, materialColor.Secondary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_SECONDARY_VARIANT, materialColor.SecondaryVariant);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONSECONDARY, materialColor.OnSecondary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_BACKGROUND, materialColor.Background);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONBACKGROUND, materialColor.OnBackground);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_SURFACE, materialColor.Surface);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONSURFACE, materialColor.OnSurface);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ERROR, materialColor.Error);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONERROR, materialColor.OnError);

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