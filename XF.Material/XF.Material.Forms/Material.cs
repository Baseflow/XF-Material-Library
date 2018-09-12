using System;
using Xamarin.Forms;
using XF.Material.Forms.Dialogs;
using XF.Material.Forms.Resources;
using XF.Material.Forms.Resources.Typography;
using XF.Material.Forms.Utilities;

namespace XF.Material.Forms
{
    /// <summary>
    /// Class that provides static methods and properties for configuring Material resources.
    /// </summary>
    public class Material
    {
        private readonly ResourceDictionary _res;

        internal Material(Application app, MaterialConfiguration materialResource) : this(app)
        {
            ColorConfiguration = materialResource.ColorConfiguration;
            FontConfiguration = materialResource.FontConfiguration;
        }

        internal Material(Application app, string key) : this(app)
        {
            var materialResource = GetResource<MaterialConfiguration>(key);
            ColorConfiguration = materialResource.ColorConfiguration;
            FontConfiguration = materialResource.FontConfiguration;
        }

        internal Material(Application app)
        {
            _res = app.Resources;
            PlatformConfiguration = DependencyService.Get<IMaterialUtility>();
            MaterialDialog.Instance = new MaterialDialog();
        }

        /// <summary>
        /// A dependency service for configuring cross-platform UI customizations.
        /// </summary>
        public static IMaterialUtility PlatformConfiguration { get; private set; }

        /// <summary>
        /// The current color configuration.
        /// </summary>
        public static MaterialColorConfiguration ColorConfiguration { get; private set; }

        /// <summary>
        /// The current font configuration.
        /// </summary>
        public static MaterialFontConfiguration FontConfiguration { get; private set; }

        /// <summary>
        /// Gets a resource of the specified type from the current <see cref="Application.Resources"/>.
        /// </summary>
        /// <typeparam name="T">The type of the resource object to be retrieved.</typeparam>
        /// <param name="key">The key of the resource object. For a list of Material resource keys, see <see cref="MaterialConstants"/>.</param>
        /// <exception cref="InvalidCastException" />
        /// <exception cref="ArgumentNullException" />
        public static T GetResource<T>(string key)
        {
            Application.Current.Resources.TryGetValue(key ?? throw new ArgumentNullException(nameof(key)), out object value);

            if (value is T resource)
            {
                return resource;
            }

            else if (value != null)
            {
                throw new InvalidCastException($"The resource retrieved was not of the type {typeof(T)}. Use {value.GetType()} instead.");
            }

            return default(T);
        }

        /// <summary>
        /// Configure's the current app's resources by merging pre-defined Material resources and creating new resources based on the <see cref="MaterialConfiguration"/>'s properties.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <param name="materialResource">The object containing the <see cref="MaterialColorConfiguration"/> and <see cref="MaterialFontConfiguration"/>.</param>
        /// <exception cref="ArgumentNullException" />
        public static void Init(Application app, MaterialConfiguration materialResource)
        {
            var material = new Material(app ?? throw new ArgumentNullException(nameof(app)), materialResource ?? throw new ArgumentNullException(nameof(materialResource)));
            material.MergeMaterialDictionaries();
        }

        /// <summary>
        /// Configure's the current app's resources by merging pre-defined Material resources and creating new resources based on the <see cref="MaterialConfiguration"/>'s properties.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <param name="key">The key of the <see cref="MaterialConfiguration"/> object in the current app's resource dictionary.</param>
        /// <exception cref="ArgumentNullException" />
        public static void Init(Application app, string key)
        {
            var material = new Material(app ?? throw new ArgumentNullException(nameof(app)), key ?? throw new ArgumentNullException(nameof(key)));
            material.MergeMaterialDictionaries();
        }

        /// <summary>
        /// Configure's the current app's resources by merging pre-defined Material resources.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <exception cref="ArgumentNullException" />
        public static void Init(Application app)
        {
            var material = new Material(app ?? throw new ArgumentNullException(nameof(app)));
            material.MergeMaterialDictionaries();
        }

        private void MergeMaterialDictionaries()
        {
            _res.MergedDictionaries.Add(new MaterialColors(ColorConfiguration));
            _res.MergedDictionaries.Add(new MaterialTypography(FontConfiguration));
            _res.MergedDictionaries.Add(new MaterialSizes());
            _res.MergedDictionaries.Add(new MaterialStyles());
        }
    }
}
