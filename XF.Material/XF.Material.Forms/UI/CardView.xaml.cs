using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Forms.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty(nameof(MainContent))]
    public partial class CardView : ContentPage
    {
        public static readonly BindableProperty OffsetYProperty
         = BindableProperty.Create(nameof(OffsetY), typeof(double), typeof(DropShadowView), 2.0);

        public double OffsetY
        {
            get { return (double)GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

        public static readonly BindableProperty OffsetXProperty
            = BindableProperty.Create(nameof(OffsetX), typeof(double), typeof(DropShadowView), 0.0);

        public double OffsetX
        {
            get { return (double)GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        public static readonly BindableProperty BlurRadiusProperty
            = BindableProperty.Create(nameof(BlurRadius), typeof(double), typeof(DropShadowView), 36.0);

        public double BlurRadius
        {
            get { return (double)GetValue(BlurRadiusProperty); }
            set { SetValue(BlurRadiusProperty, value); }
        }

        public static readonly BindableProperty CornerRadiusProperty
            = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(CardView), 4.0);

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty MainContentProperty
            = BindableProperty.Create(nameof(MainContent), typeof(View), typeof(CardView), default(View));

        public View MainContent
        {
            get { return (View)GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }

        public static readonly BindableProperty SurfaceColorProperty
            = BindableProperty.Create(nameof(SurfaceColor), typeof(Color), typeof(CardView), default(Color));

        public Color SurfaceColor
        {
            get { return (Color)GetValue(SurfaceColorProperty); }
            set { SetValue(SurfaceColorProperty, value); }
        }

        public static readonly BindableProperty ContentPaddingProperty
            = BindableProperty.Create(nameof(ContentPadding), typeof(Thickness), typeof(CardView), default(Thickness));

        public Thickness ContentPadding
        {
            get { return (Thickness)GetValue(ContentPaddingProperty); }
            set { SetValue(ContentPaddingProperty, value); }
        }

        public CardView()
        {
            InitializeComponent();

            dropShadow.CornerRadius = this.CornerRadius;
            dropShadow.OffsetX = this.OffsetX;
            dropShadow.OffsetY = this.OffsetY;
            dropShadow.BlurRadius = this.BlurRadius;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.MainContent))
            {
                mainContent.Content = this.MainContent;
            }

            if (propertyName == nameof(this.SurfaceColor))
            {
                dropShadow.SurfaceColor = this.SurfaceColor;
            }

            if (propertyName == nameof(this.CornerRadius))
            {
                dropShadow.CornerRadius = this.CornerRadius;
            }

            if (propertyName == nameof(this.OffsetX))
            {
                dropShadow.OffsetX = this.OffsetX;
            }

            if (propertyName == nameof(this.OffsetY))
            {
                dropShadow.OffsetY = this.OffsetY;
            }

            if (propertyName == nameof(this.BlurRadius))
            {
                dropShadow.BlurRadius = this.BlurRadius;
            }

            if (propertyName == nameof(this.ContentPadding))
            {
                mainContent.Padding = this.ContentPadding;
            }
        }
    }
}