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
            _app = app;
            _res = app.Resources;
            Configuration = DependencyService.Get<IMaterialUtility>();
        }

        /// <summary>
        /// Gets the object used for changing the underlying Material theme.
        /// </summary>
        public static IMaterialUtility Configuration { get; private set; }

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
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_PRIMARY_VARIANT, materialResource.Color.PrimaryVariant);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONPRIMARY, materialResource.Color.OnPrimary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_SECONDARY, materialResource.Color.Secondary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_SECONDARY_VARIANT, materialResource.Color.SecondaryVariant);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONSECONDARY, materialResource.Color.OnSecondary);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_BACKGROUND, materialResource.Color.Background);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONBACKGROUND, materialResource.Color.OnBackground);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_SURFACE, materialResource.Color.Surface);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONSURFACE, materialResource.Color.OnSurface);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ERROR, materialResource.Color.Error);
            this.TryAddColorResource(MaterialConstants.MATERIAL_COLOR_ONERROR, materialResource.Color.OnError);

            Configuration.ChangeStatusBarColor(materialResource.Color.PrimaryVariant);
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
