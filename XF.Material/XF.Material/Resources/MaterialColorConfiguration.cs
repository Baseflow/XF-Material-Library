using Xamarin.Forms;

namespace XF.Material.Resources
{
    /// <summary>
    /// App color theme configuration based on https://material.io/design/color values.
    /// </summary>
    public sealed class MaterialColorConfiguration : BindableObject
    {
        public static readonly BindableProperty PrimaryProperty = BindableProperty.Create(nameof(Primary), typeof(Color), typeof(Color), default(Color));
        public static readonly BindableProperty PrimaryVariantProperty = BindableProperty.Create(nameof(PrimaryVariant), typeof(Color), typeof(Color), default(Color));
        public static readonly BindableProperty SecondaryProperty = BindableProperty.Create(nameof(Secondary), typeof(Color), typeof(Color), default(Color));
        public static readonly BindableProperty SecondaryVariantProperty = BindableProperty.Create(nameof(SecondaryVariant), typeof(Color), typeof(Color), default(Color));
        public static readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Color), typeof(Color), Color.White);
        public static readonly BindableProperty ErrorProperty = BindableProperty.Create(nameof(Error), typeof(Color), typeof(Color), default(Color));
        public static readonly BindableProperty SurfaceProperty = BindableProperty.Create(nameof(Surface), typeof(Color), typeof(Color), default(Color));
        public static readonly BindableProperty OnPrimaryProperty = BindableProperty.Create(nameof(OnPrimary), typeof(Color), typeof(Color), default(Color));
        public static readonly BindableProperty OnSecondaryProperty = BindableProperty.Create(nameof(OnSecondary), typeof(Color), typeof(Color), default(Color));
        public static readonly BindableProperty OnBackgroundProperty = BindableProperty.Create(nameof(OnBackground), typeof(Color), typeof(Color), default(Color));
        public static readonly BindableProperty OnErrorProperty = BindableProperty.Create(nameof(OnError), typeof(Color), typeof(Color), default(Color));
        public static readonly BindableProperty OnSurfaceProperty = BindableProperty.Create(nameof(OnSurface), typeof(Color), typeof(Color), default(Color));

        /// <summary>
        /// Displayed most frequently across your app.
        /// </summary>
        public Color Primary
        {
            get => (Color)GetValue(PrimaryProperty);
            set => SetValue(PrimaryProperty, value);
        }

        /// <summary>
        /// A tonal variation of <see cref="MaterialColorConfiguration.Primary"/>.
        /// </summary>
        public Color PrimaryVariant
        {
            get => (Color)GetValue(PrimaryVariantProperty);
            set => SetValue(PrimaryVariantProperty, value);
        }

        /// <summary>
        /// Accents select parts of your UI.
        /// If not provided, use <see cref="MaterialColorConfiguration.Primary"/>.
        /// </summary>
        public Color Secondary
        {
            get
            {
                var color = (Color)GetValue(SecondaryProperty);

                if(color.IsDefault && this.Primary.IsDefault)
                {
                    return Color.Accent;
                }

                else
                {
                    return color.IsDefault ? this.Primary : color;
                }
            }
            set => SetValue(SecondaryProperty, value);
        }

        /// <summary>
        /// A tonal variation of <see cref="MaterialColorConfiguration.Secondary"/>.
        /// </summary>
        public Color SecondaryVariant
        {
            get => (Color)GetValue(SecondaryVariantProperty);
            set => SetValue(SecondaryVariantProperty, value);
        }

        /// <summary>
        /// The underlying color of an app’s content.
        /// Typically the background color of scrollable content.
        /// </summary>
        public Color Background
        {
            get => (Color)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        /// <summary>
        /// The color used to indicate error status.
        /// </summary>
        public Color Error
        {
            get => (Color)GetValue(ErrorProperty);
            set => SetValue(ErrorProperty, value);
        }

        /// <summary>
        /// The color of surfaces such as cards, sheets, menus.
        /// </summary>
        public Color Surface
        {
            get => (Color)GetValue(SurfaceProperty);
            set => SetValue(SurfaceProperty, value);
        }

        /// <summary>
        /// A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="MaterialColorConfiguration.Primary"/>.
        /// </summary>
        public Color OnPrimary
        {
            get => (Color)GetValue(OnPrimaryProperty);
            set => SetValue(OnPrimaryProperty, value);
        }

        /// <summary>
        /// 	A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="MaterialColorConfiguration.Secondary"/>.
        /// </summary>
        public Color OnSecondary
        {
            get
            {
                var color = (Color)GetValue(OnSecondaryProperty);

                return color.IsDefault ? this.OnPrimary : color;
            }

            set => SetValue(OnSecondaryProperty, value);
        }

        /// <summary>
        /// A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="MaterialColorConfiguration.Background"/>.
        /// </summary>
        public Color OnBackground
        {
            get => (Color)GetValue(OnBackgroundProperty);
            set => SetValue(OnBackgroundProperty, value);
        }

        /// <summary>
        /// A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="MaterialColorConfiguration.Error"/>.
        /// </summary>
        public Color OnError
        {
            get => (Color)GetValue(OnErrorProperty);
            set => SetValue(OnErrorProperty, value);
        }

        /// <summary>
        /// A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="MaterialColorConfiguration.Surface"/>
        /// </summary>
        public Color OnSurface
        {
            get => (Color)GetValue(OnSurfaceProperty);
            set => SetValue(OnSurfaceProperty, value);
        }
    }
}
