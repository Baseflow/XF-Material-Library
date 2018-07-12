using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XF.Material.Resources;
using XF.Material.Resources.Typography;
using XF.Material.Utilities;

namespace XF.Material
{
    public sealed class Material
    {
        private readonly MaterialResource _materialResource;
        private readonly ResourceDictionary _res;

        internal Material(Application app, MaterialResource materialResource) : this(app)
        {
            _materialResource = materialResource;
        }

        internal Material(Application app, string key) : this(app)
        {
            app.Resources.TryGetValue(key, out object value);

            if (value is MaterialResource materialResource)
            {
                _materialResource = materialResource;
            }

            else if (value != null)
            {
                throw new InvalidCastException($"The resource retrieved with key {key} was not of the type {typeof(MaterialResource)}");
            }

            else
            {
                throw new KeyNotFoundException($"The resource with key {key} of the type {typeof(MaterialResource)} was not found");
            }
        }

        internal Material(Application app)
        {
            _res = app.Resources;
            PlatformConfiguration = DependencyService.Get<IMaterialUtility>();
        }

        public static IMaterialUtility PlatformConfiguration { get; private set; }

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

        public static void Init(Application app)
        {
            var material = new Material(app ?? throw new ArgumentNullException(nameof(app)));
            material.SetupMaterialResources();
        }

        private void GetMaterialResource()
        {
            if (_materialResource?.Color != null)
            {
                this.SetupColors();
            }

            if (_materialResource?.FontFamily != null)
            {
                this.SetupFonts();
            }

            if(_materialResource == null)
            {
                // Add fallback values to be used by Material.Views
                _res.Add(MaterialConstants.MATERIAL_COLOR_SECONDARY, Color.Accent);
                _res.Add(MaterialConstants.MATERIAL_COLOR_ONSURFACE, Color.White);
            }
        }

        private void MergeMaterialDictionaries()
        {
            _res.MergedDictionaries.Add(new MaterialSizes());
            _res.MergedDictionaries.Add(new MaterialStyles());
            _res.MergedDictionaries.Add(new MaterialTypography());
        }

        private void SetupColors()
        {
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_PRIMARY, _materialResource.Color.Primary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_PRIMARY_VARIANT, _materialResource.Color.PrimaryVariant);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONPRIMARY, _materialResource.Color.OnPrimary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_SECONDARY, _materialResource.Color.Secondary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_SECONDARY_VARIANT, _materialResource.Color.SecondaryVariant);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONSECONDARY, _materialResource.Color.OnSecondary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_BACKGROUND, _materialResource.Color.Background);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONBACKGROUND, _materialResource.Color.OnBackground);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_SURFACE, _materialResource.Color.Surface);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONSURFACE, _materialResource.Color.OnSurface);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ERROR, _materialResource.Color.Error);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONERROR, _materialResource.Color.OnError);

            PlatformConfiguration.ChangeStatusBarColor(_materialResource.Color.PrimaryVariant);
        }

        private void SetupFonts()
        {
            this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_REGULAR, _materialResource.FontFamily.Regular);
            this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_MEDIUM, _materialResource.FontFamily.Medium);
            this.TryAddStringResource(MaterialConstants.MATERIAL_FONTFAMILY_BOLD, _materialResource.FontFamily.Bold);
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
