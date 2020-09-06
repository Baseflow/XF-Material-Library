using System;
using Xamarin.Forms;
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
        private static readonly Lazy<IMaterialUtility> MaterialUtilityInstance = new Lazy<IMaterialUtility>(() => DependencyService.Get<IMaterialUtility>());
        private readonly MaterialConfiguration _config;
        private static ResourceDictionary _res;

        internal Material(Application app, MaterialConfiguration materialResource) : this(app)
        {
            _config = materialResource;
        }

        internal Material(Application app, string key) : this(app)
        {
            _config = GetResource<MaterialConfiguration>(key);
        }

        internal Material(Application app)
        {
            _res = app.Resources;
            _config = new MaterialConfiguration
            {
                ColorConfiguration = new MaterialColorConfiguration(),
                FontConfiguration = new MaterialFontConfiguration()
            };
        }

        /// <summary>
        /// A dependency service for configuring cross-platform UI customizations.
        /// </summary>
        public static IMaterialUtility PlatformConfiguration => MaterialUtilityInstance.Value;

        /// <summary>
        /// Gets a resource of the specified type from the current <see cref="Application.Resources"/>.
        /// </summary>
        /// <typeparam name="T">The type of the resource object to be retrieved.</typeparam>
        /// <param name="key">The key of the resource object. For a list of Material resource keys, see <see cref="MaterialConstants"/>.</param>
        /// <exception cref="InvalidCastException" />
        /// <exception cref="ArgumentNullException" />
        public static T GetResource<T>(string key)
        {
            _res.TryGetValue(key ?? throw new ArgumentNullException(nameof(key)), out var value);

            if (value is T resource)
            {
                return resource;
            }

            if (value != null)
            {
                throw new InvalidCastException($"The resource retrieved was not of the type {typeof(T)}. Use {value.GetType()} instead.");
            }

            return default;
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
            _res.MergedDictionaries.Add(new MaterialColors(_config.ColorConfiguration ?? new MaterialColorConfiguration()));
            _res.MergedDictionaries.Add(new MaterialTypography(_config.FontConfiguration ?? new MaterialFontConfiguration()));
            _res.MergedDictionaries.Add(new MaterialSizes());
        }

        /// <summary>
        /// Static class that contains the current Material color values.
        /// </summary>
        public static class Color
        {
            /// <summary>
            /// The underlying color of an app’s content.
            /// Typically the background color of scrollable content.
            /// </summary>
            public static Xamarin.Forms.Color Background => GetResource<Xamarin.Forms.Color>(MaterialConstants.Color.BACKGROUND);

            /// <summary>
            /// The color used to indicate error status.
            /// </summary>
            public static Xamarin.Forms.Color Error => GetResource<Xamarin.Forms.Color>(MaterialConstants.Color.ERROR);

            /// <summary>
            /// A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="MaterialColorConfiguration.Background"/>.
            /// </summary>
            public static Xamarin.Forms.Color OnBackground => GetResource<Xamarin.Forms.Color>(MaterialConstants.Color.ON_BACKGROUND);

            /// <summary>
            /// A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="MaterialColorConfiguration.Error"/>.
            /// </summary>
            public static Xamarin.Forms.Color OnError => GetResource<Xamarin.Forms.Color>(MaterialConstants.Color.ON_ERROR);

            /// <summary>
            /// A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="MaterialColorConfiguration.Primary"/>.
            /// </summary>
            public static Xamarin.Forms.Color OnPrimary => GetResource<Xamarin.Forms.Color>(MaterialConstants.Color.ON_PRIMARY);

            /// <summary>
            /// 	A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="MaterialColorConfiguration.Secondary"/>.
            /// </summary>
            public static Xamarin.Forms.Color OnSecondary => GetResource<Xamarin.Forms.Color>(MaterialConstants.Color.ON_SECONDARY);

            /// <summary>
            /// A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="MaterialColorConfiguration.Surface"/>
            /// </summary>
            public static Xamarin.Forms.Color OnSurface => GetResource<Xamarin.Forms.Color>(MaterialConstants.Color.ON_SURFACE);

            /// <summary>
            /// Displayed most frequently across your app.
            /// </summary>
            public static Xamarin.Forms.Color Primary => GetResource<Xamarin.Forms.Color>(MaterialConstants.Color.PRIMARY);

            /// <summary>
            /// A tonal variation of <see cref="MaterialColorConfiguration.Primary"/>.
            /// </summary>
            public static Xamarin.Forms.Color PrimaryVariant => GetResource<Xamarin.Forms.Color>(MaterialConstants.Color.PRIMARY_VARIANT);

            /// <summary>
            /// Accents select parts of your UI.
            /// If not provided, use <see cref="MaterialColorConfiguration.Primary"/>.
            /// </summary>
            public static Xamarin.Forms.Color Secondary => GetSecondaryColor();

            //TODO: Make configurations bindable.
            private static Xamarin.Forms.Color GetSecondaryColor()
            {
                var color = GetResource<Xamarin.Forms.Color>(MaterialConstants.Color.SECONDARY);

                return color.IsDefault ? Xamarin.Forms.Color.FromHex("#6200EE") : color;
            }

            /// <summary>
            /// A tonal variation of <see cref="MaterialColorConfiguration.Secondary"/>.
            /// </summary>
            public static Xamarin.Forms.Color SecondaryVariant => GetResource<Xamarin.Forms.Color>(MaterialConstants.Color.SECONDARY_VARIANT);

            /// <summary>
            /// The color of surfaces such as cards, sheets, menus.
            /// </summary>
            public static Xamarin.Forms.Color Surface => GetResource<Xamarin.Forms.Color>(MaterialConstants.Color.SURFACE);
        }

        /// <summary>
        /// Static class that contains the current Material font family values.
        /// </summary>
        public static class FontFamily
        {
            /// <summary>
            /// Body 1 font family, used for long-form writing and small text sizes.
            /// </summary>
            public static string Body1 => GetResource<string>(MaterialConstants.FontFamily.BODY1);

            /// <summary>
            /// Body 2 font family, used for long-form writing and small text sizes.
            /// </summary>
            public static string Body2 => GetResource<string>(MaterialConstants.FontFamily.BODY2);

            /// <summary>
            /// Button font family, used by different types of buttons.
            /// </summary>
            public static string Button => GetResource<string>(MaterialConstants.FontFamily.BUTTON);

            /// <summary>
            /// Caption font family, used for annotations or to introduce a headline text.
            /// </summary>
            public static string Caption => GetResource<string>(MaterialConstants.FontFamily.CAPTION);

            /// <summary>
            /// Headline 1 font family, used by large text on the screen.
            /// </summary>
            public static string H1 => GetResource<string>(MaterialConstants.FontFamily.H1);

            /// <summary>
            /// Headline 2 font family, used by large text on the screen.
            /// </summary>
            public static string H2 => GetResource<string>(MaterialConstants.FontFamily.H2);

            /// <summary>
            /// Headline 3 font family, used by large text on the screen.
            /// </summary>
            public static string H3 => GetResource<string>(MaterialConstants.FontFamily.H3);

            /// <summary>
            /// Headline 4 font family, used by large text on the screen.
            /// </summary>
            public static string H4 => GetResource<string>(MaterialConstants.FontFamily.H4);

            /// <summary>
            /// Headline 5 font family, used by large text on the screen.
            /// </summary>
            public static string H5 => GetResource<string>(MaterialConstants.FontFamily.H5);

            /// <summary>
            /// Headline 6 font family, used by large text on the screen.
            /// </summary>
            public static string H6 => GetResource<string>(MaterialConstants.FontFamily.H6);

            /// <summary>
            /// Overline font family, used for annotations or to introduce a headline text.
            /// </summary>
            public static string Overline => GetResource<string>(MaterialConstants.FontFamily.OVERLINE);

            /// <summary>
            /// Subtitle 1 font family, used by medium-emphasis text.
            /// </summary>
            public static string Subtitle1 => GetResource<string>(MaterialConstants.FontFamily.SUBTITLE1);

            /// <summary>
            /// Subtitle 2 font family, used by medium-emphasis text.
            /// </summary>
            public static string Subtitle2 => GetResource<string>(MaterialConstants.FontFamily.SUBTITLE2);
        }
    }
}
