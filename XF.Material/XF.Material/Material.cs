using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XF.Material.Resources;
using XF.Material.Resources.Typography;

namespace XF.Material
{
    public sealed class Material
    {
        private readonly Application _app;
        private readonly MaterialResource _materialResource;
        private readonly ResourceDictionary _res;

        internal Material(Application app, MaterialResource materialResource) : this(app)
        {
            _materialResource = materialResource;
        }

        internal Material(Application app, string key) : this(app)
        {
            app.Resources.TryGetValue(key, out object value);

            if(value is MaterialResource materialResource)
            {
                _materialResource = materialResource;
            }

            else if(value != null)
            {
                throw new InvalidCastException($"The resource retrieved was not of the type {typeof(MaterialResource)}");
            }

            else
            {
                throw new KeyNotFoundException($"The resource with key {key} of the type {typeof(MaterialResource)} was not found");
            }
        }

        internal Material(Application app)
        {
            _app = app;
            _app.PropertyChanged += this.CurrentApp_PropertyChanged;
            _res = app.Resources;
        }

        /// <summary>
        /// Gets a resource of the specified type from the current ResourceDictionary.
        /// </summary>
        /// <typeparam name="T">The type of the resource object to be retrieved.</typeparam>
        /// <param name="key">The key of the resource object. For a list of Material resource keys, see the <see cref="MaterialConstants"/> class.</param>
        /// <returns></returns>
        public static T GetMaterialResource<T>(string key)
        {
            Application.Current.Resources.TryGetValue(key ?? throw new ArgumentNullException(nameof(key)), out object value);

            if (value is T resource)
            {
                return resource;
            }

            else if (value != null)
            {
                throw new InvalidCastException($"The resource retrieved was not of the type {typeof(T)}");
            }

            throw new KeyNotFoundException($"The resource with key {key} of the type {typeof(T)} was not found");
        }

        /// <summary>
        /// Initializes a new <see cref="Material"/> object from which we will get the values defined either from the app's resource dictionary or by using a <see cref="MaterialResource"/> object.
        /// This will automatically create new resources in the current app.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <param name="materialResource">The object that contains the values to be used for resource creation.</param>
        public static void Init(Application app, MaterialResource materialResource)
        {
            var material = new Material(app ?? throw new ArgumentNullException(nameof(app)), materialResource ?? throw new ArgumentNullException(nameof(materialResource)));
            material.SetupMaterialResources();
        }

        /// <summary>
        /// Initializes a new <see cref="Material"/> object from which we will get the values defined either from the app's resource dictionary or by using a <see cref="MaterialResource"/> object.
        /// This will automatically create new resources in the current app.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <param name="key">The key of the <see cref="MaterialResource"/> object in the current app's resource dictionary.</param>
        public static void Init(Application app, string key)
        {
            var material = new Material(app ?? throw new ArgumentNullException(nameof(app)), key ?? throw new ArgumentNullException(nameof(key)));
            material.SetupMaterialResources();
        }

        private void CurrentApp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Application.MainPage) && _app.MainPage is NavigationPage navigationPage)
            {
                navigationPage.SetDynamicResource(NavigationPage.BarBackgroundColorProperty, MaterialConstants.MATERIAL_COLOR_PRIMARY);
                navigationPage.SetDynamicResource(NavigationPage.BarTextColorProperty, MaterialConstants.MATERIAL_COLOR_ONPRIMARY);
                navigationPage.SetDynamicResource(NavigationPage.BackgroundColorProperty, MaterialConstants.MATERIAL_COLOR_BACKGROUND);
            }
        }

        private void GetMaterialResource()
        {
            try
            {
                SetupFonts(_materialResource);
                SetupColors(_materialResource);
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void MergeMaterialDictionaries()
        {
            _res.MergedDictionaries.Add(new MaterialSizes());
            _res.MergedDictionaries.Add(new MaterialStyles());
            _res.MergedDictionaries.Add(new MaterialTypography());
        }

        private void SetupColors(MaterialResource materialResource)
        {
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_PRIMARY, materialResource.Color.Primary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONPRIMARY, materialResource.Color.OnPrimary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_SECONDARY, materialResource.Color.Secondary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONSECONDARY, materialResource.Color.OnSecondary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_BACKGROUND, materialResource.Color.Background);
        }

        private void SetupFonts(MaterialResource materialResource)
        {
            this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_REGULAR, materialResource.FontFamily.Regular);
            this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_MEDIUM, materialResource.FontFamily.Medium);
            this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_BOLD, materialResource.FontFamily.Bold);
        }

        private void SetupMaterialResources()
        {
            this.MergeMaterialDictionaries();
            this.GetMaterialResource();
        }

        private void TryAddColorResource(string key, Color color)
        {
            if(key == null || color.IsDefault)
            {
                return;
            }

            _res.Add(key, color);
        }

        private void TryAddStringResource(string key, string value)
        {
            if(string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                return;
            }

            _res.Add(key, value);
        }
    }
}
