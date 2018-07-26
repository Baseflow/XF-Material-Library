using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XF.Material.Resources;
using XF.Material.Resources.Typography;
using XF.Material.Utilities;

namespace XF.Material
{
    public sealed class Material
    {
        private readonly ResourceDictionary _res;

        internal Material(Application app, MaterialConfiguration materialResource) : this(app)
        {
            Resource = materialResource;
        }

        internal Material(Application app, string key) : this(app)
        {
            app.Resources.TryGetValue(key, out object value);

            if (value is MaterialConfiguration materialResource)
            {
                Resource = materialResource;
            }

            else if (value != null)
            {
                throw new InvalidCastException($"The resource retrieved with key {key} was not of the type {typeof(MaterialConfiguration)}");
            }

            else
            {
                throw new KeyNotFoundException($"The resource with key {key} of the type {typeof(MaterialConfiguration)} was not found");
            }
        }

        internal Material(Application app)
        {
            _res = app.Resources;
            PlatformConfiguration = DependencyService.Get<IMaterialUtility>();
        }

        public static IMaterialUtility PlatformConfiguration { get; private set; }

        public static MaterialConfiguration Resource { get; private set; }

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
        /// Initializes a new <see cref="Material"/> object from which we will get the values defined either from the app's resource dictionary or by using a <see cref="Resource"/> object.
        /// This will automatically create new resources in the current app.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <param name="materialResource">The object that contains the values to be used for resource creation.</param>
        public static void Init(Application app, MaterialConfiguration materialResource)
        {
            var material = new Material(app ?? throw new ArgumentNullException(nameof(app)), materialResource ?? throw new ArgumentNullException(nameof(materialResource)));
            material.MergeMaterialDictionaries();
        }

        /// <summary>
        /// Initializes a new <see cref="Material"/> object from which we will get the values defined either from the app's resource dictionary or by using a <see cref="Resource"/> object.
        /// This will automatically create new resources in the current app.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <param name="key">The key of the <see cref="Resource"/> object in the current app's resource dictionary.</param>
        public static void Init(Application app, string key)
        {
            var material = new Material(app ?? throw new ArgumentNullException(nameof(app)), key ?? throw new ArgumentNullException(nameof(key)));
            material.MergeMaterialDictionaries();
        }

        public static void Init(Application app)
        {
            var material = new Material(app ?? throw new ArgumentNullException(nameof(app)));
            material.MergeMaterialDictionaries();
        }

        private void MergeMaterialDictionaries()
        {
            _res.MergedDictionaries.Add(new MaterialColors(Resource?.ColorConfiguration));
            _res.MergedDictionaries.Add(new MaterialTypography(Resource?.FontConfiguration));
            _res.MergedDictionaries.Add(new MaterialSizes());
            _res.MergedDictionaries.Add(new MaterialStyles());
        }
    }
}
