using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XF.Material.Resources;

namespace XF.Material
{
    public sealed class Material
    {
        private readonly Application _app;
        private readonly ResourceDictionary _res;

        internal Material(Application app, MaterialResource materialResource = null)
        {
            _app = app ?? throw new ArgumentNullException();
            _app.PropertyChanged += this.CurrentApp_PropertyChanged;
            _res = app.Resources;
            this.MergeMaterialDictionaries();

            if (materialResource == null)
            {
                SetupStyleFromResource();
            }

            else
            {
                SetupStyleFromObject(materialResource);
            }
        }

        /// <summary>
        /// Initializes a new <see cref="Material"/> object from which we will get the values defined either from the app's resource dictionary or by using a <see cref="MaterialResource"/> object.
        /// This will automatically create new resources in the current app.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <param name="materialResource">The object that contains the values to be used for resource creation.</param>
        public static void Init(Application app, MaterialResource materialResource = null)
        {
            var material = new Material(app, materialResource);
        }

        private void MergeMaterialDictionaries()
        {
            _res.MergedDictionaries.Add(new XF.Material.Resources.MaterialSizes());
            _res.MergedDictionaries.Add(new XF.Material.Resources.MaterialStyles());
            _res.MergedDictionaries.Add(new XF.Material.Resources.Typography.MaterialTypography());
        }

        private void CurrentApp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Application.MainPage) && _app.MainPage is NavigationPage navigationPage)
            {
                navigationPage.SetDynamicResource(NavigationPage.BarBackgroundColorProperty, MaterialConstants.MATERIAL_COLOR_PRIMARY);
                navigationPage.SetDynamicResource(NavigationPage.BarTextColorProperty, MaterialConstants.MATERIAL_COLOR_ONPRIMARY);
            }
        }

        private void SetupColors(MaterialResource materialResource)
        {
            _res.Add(MaterialConstants.MATERIAL_COLOR_PRIMARY, materialResource.Color.Primary);
            _res.Add(MaterialConstants.MATERIAL_COLOR_ONPRIMARY, materialResource.Color.OnPrimary);

            if (!materialResource.Color.Secondary.IsDefault)
            {
                _res.Add(MaterialConstants.MATERIAL_COLOR_SECONDARY, materialResource.Color.Secondary);
            }

            if (!materialResource.Color.OnSecondary.IsDefault)
            {
                _res.Add(MaterialConstants.MATERIAL_COLOR_ONSECONDARY, materialResource.Color.OnSecondary);
            }
        }

        private void SetupFonts(MaterialResource materialResource)
        {
            _res.Add(MaterialConstants.MATERIAL_FONTFAMILY_REGULAR, materialResource.FontFamily.Regular);
            _res.Add(MaterialConstants.MATERIAL_FONTFAMILY_MEDIUM, materialResource.FontFamily.Medium);
            _res.Add(MaterialConstants.MATERIAL_FONTFAMILY_BOLD, materialResource.FontFamily.Bold);
        }

        private void SetupStyleFromObject(MaterialResource materialResource)
        {
            try
            {
                SetupFonts(materialResource);
                SetupColors(materialResource);
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void SetupStyleFromResource()
        {
            _res.TryGetValue("Material.Style", out object value);

            try
            {
                if (value is MaterialResource materialResource)
                {
                    SetupFonts(materialResource);
                    SetupColors(materialResource);
                }
            }

            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
