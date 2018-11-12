using Xamarin.Forms;

namespace XF.Material.Forms.Resources
{
    /// <summary>
    /// Class that provides color theme configuration based on https://material.io/design/color.
    /// </summary>
    public sealed class MaterialColorConfiguration : BindableObject
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="Background"/>.
        /// </summary>
        public static readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Color), typeof(Color), Color.FromHex("#EAEAEA"));

        /// <summary>
        /// Backing field for the bindable property <see cref="Error"/>.
        /// </summary>
        public static readonly BindableProperty ErrorProperty = BindableProperty.Create(nameof(Error), typeof(Color), typeof(Color), Color.FromHex("#B00020"));

        /// <summary>
        /// Backing field for the bindable property <see cref="OnBackground"/>.
        /// </summary>
        public static readonly BindableProperty OnBackgroundProperty = BindableProperty.Create(nameof(OnBackground), typeof(Color), typeof(Color), Color.FromHex("#000000"));

        /// <summary>
        /// Backing field for the bindable property <see cref="OnError"/>.
        /// </summary>
        public static readonly BindableProperty OnErrorProperty = BindableProperty.Create(nameof(OnError), typeof(Color), typeof(Color), Color.FromHex("#FFFFFF"));

        /// <summary>
        /// Backing field for the bindable property <see cref="OnPrimary"/>.
        /// </summary>
        public static readonly BindableProperty OnPrimaryProperty = BindableProperty.Create(nameof(OnPrimary), typeof(Color), typeof(Color), Color.FromHex("#FFFFFF"));

        /// <summary>
        /// Backing field for the bindable property <see cref="OnSecondary"/>.
        /// </summary>
        public static readonly BindableProperty OnSecondaryProperty = BindableProperty.Create(nameof(OnSecondary), typeof(Color), typeof(Color), Color.FromHex("#FFFFFF"));

        /// <summary>
        /// Backing field for the bindable property <see cref="OnSurface"/>.
        /// </summary>
        public static readonly BindableProperty OnSurfaceProperty = BindableProperty.Create(nameof(OnSurface), typeof(Color), typeof(Color), Color.FromHex("#000000"));

        /// <summary>
        /// Backing field for the bindable property <see cref="Primary"/>.
        /// </summary>
        public static readonly BindableProperty PrimaryProperty = BindableProperty.Create(nameof(Primary), typeof(Color), typeof(Color), Color.FromHex("#6200EE"));

        /// <summary>
        /// Backing field for the bindable property <see cref="PrimaryVariant"/>.
        /// </summary>
        public static readonly BindableProperty PrimaryVariantProperty = BindableProperty.Create(nameof(PrimaryVariant), typeof(Color), typeof(Color), Color.FromHex("#6200EE"));

        /// <summary>
        /// Backing field for the bindable property <see cref="Secondary"/>.
        /// </summary>
        public static readonly BindableProperty SecondaryProperty = BindableProperty.Create(nameof(Secondary), typeof(Color), typeof(Color), default(Color));

        /// <summary>
        /// Backing field for the bindable property <see cref="SecondaryVariant"/>.
        /// </summary>
        public static readonly BindableProperty SecondaryVariantProperty = BindableProperty.Create(nameof(SecondaryVariant), typeof(Color), typeof(Color), Color.FromHex("#0400BA"));

        /// <summary>
        /// Backing field for the bindable property <see cref="Surface"/>.
        /// </summary>
        public static readonly BindableProperty SurfaceProperty = BindableProperty.Create(nameof(Surface), typeof(Color), typeof(Color), Color.FromHex("#FFFFFF"));

        /// <summary>
        /// The underlying color of an app’s content.
        /// Typically the background color of scrollable content.
        /// </summary>
        public Color Background
        {
            get => (Color)this.GetValue(BackgroundProperty);
            set => this.SetValue(BackgroundProperty, value);
        }

        /// <summary>
        /// The color used to indicate error status.
        /// </summary>
        public Color Error
        {
            get => (Color)this.GetValue(ErrorProperty);
            set => this.SetValue(ErrorProperty, value);
        }

        /// <summary>
        /// A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Background"/>.
        /// </summary>
        public Color OnBackground
        {
            get => (Color)this.GetValue(OnBackgroundProperty);
            set => this.SetValue(OnBackgroundProperty, value);
        }

        /// <summary>
        /// A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Error"/>.
        /// </summary>
        public Color OnError
        {
            get => (Color)this.GetValue(OnErrorProperty);
            set => this.SetValue(OnErrorProperty, value);
        }

        /// <summary>
        /// A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Primary"/>.
        /// </summary>
        public Color OnPrimary
        {
            get => (Color)this.GetValue(OnPrimaryProperty);
            set => this.SetValue(OnPrimaryProperty, value);
        }

        /// <summary>
        /// 	A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Secondary"/>.
        /// </summary>
        public Color OnSecondary
        {
            get
            {
                var color = (Color)this.GetValue(OnSecondaryProperty);

                return color.IsDefault ? this.OnPrimary : color;
            }

            set => this.SetValue(OnSecondaryProperty, value);
        }

        /// <summary>
        /// A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Surface"/>
        /// </summary>
        public Color OnSurface
        {
            get => (Color)this.GetValue(OnSurfaceProperty);
            set => this.SetValue(OnSurfaceProperty, value);
        }

        /// <summary>
        /// Displayed most frequently across your app.
        /// </summary>
        public Color Primary
        {
            get => (Color)this.GetValue(PrimaryProperty);
            set => this.SetValue(PrimaryProperty, value);
        }

        /// <summary>
        /// A tonal variation of <see cref="Primary"/>.
        /// </summary>
        public Color PrimaryVariant
        {
            get => (Color)this.GetValue(PrimaryVariantProperty);
            set => this.SetValue(PrimaryVariantProperty, value);
        }

        /// <summary>
        /// Accents select parts of your UI.
        /// If not provided, use <see cref="Primary"/>.
        /// </summary>
        public Color Secondary
        {
            get
            {
                var color = (Color)this.GetValue(SecondaryProperty);

                if(color.IsDefault && this.Primary.IsDefault)
                {
                    return Color.Accent;
                }

                else
                {
                    return color.IsDefault ? this.Primary : color;
                }
            }
            set => this.SetValue(SecondaryProperty, value);
        }

        /// <summary>
        /// A tonal variation of <see cref="Secondary"/>.
        /// </summary>
        public Color SecondaryVariant
        {
            get => (Color)this.GetValue(SecondaryVariantProperty);
            set => this.SetValue(SecondaryVariantProperty, value);
        }

        /// <summary>
        /// The color of surfaces such as cards, sheets, menus.
        /// </summary>
        public Color Surface
        {
            get => (Color)this.GetValue(SurfaceProperty);
            set => this.SetValue(SurfaceProperty, value);
        }
    }
}
