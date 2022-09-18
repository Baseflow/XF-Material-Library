using Microsoft.Maui;
using Microsoft.Maui.Controls.Xaml;

namespace XF.Material.Maui.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialColors : ResourceDictionary
    {
        internal MaterialColors(MaterialColorConfiguration materialColor)
        {
            InitializeComponent();
            SetColors(materialColor);
        }

        private void SetColors(MaterialColorConfiguration materialColor)
        {
            TryAddColorResource(MaterialConstants.Color.PRIMARY, materialColor.Primary);
            TryAddColorResource(MaterialConstants.Color.PRIMARY_VARIANT, materialColor.PrimaryVariant);
            TryAddColorResource(MaterialConstants.Color.ON_PRIMARY, materialColor.OnPrimary);
            TryAddColorResource(MaterialConstants.Color.SECONDARY, materialColor.Secondary);
            TryAddColorResource(MaterialConstants.Color.SECONDARY_VARIANT, materialColor.SecondaryVariant);
            TryAddColorResource(MaterialConstants.Color.ON_SECONDARY, materialColor.OnSecondary);
            TryAddColorResource(MaterialConstants.Color.BACKGROUND, materialColor.Background);
            TryAddColorResource(MaterialConstants.Color.ON_BACKGROUND, materialColor.OnBackground);
            TryAddColorResource(MaterialConstants.Color.SURFACE, materialColor.Surface);
            TryAddColorResource(MaterialConstants.Color.ON_SURFACE, materialColor.OnSurface);
            TryAddColorResource(MaterialConstants.Color.ERROR, materialColor.Error);
            TryAddColorResource(MaterialConstants.Color.ON_ERROR, materialColor.OnError);
        }

        private void TryAddColorResource(string key, Color color)
        {
            if (key == null || color.IsDefault())
            {
                return;
            }

            Add(key, color);
        }
    }
}
