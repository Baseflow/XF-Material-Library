using System.Runtime.CompilerServices;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Xaml;

namespace XF.Material.Maui.UI
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

            dropShadow.CornerRadius = CornerRadius;
            dropShadow.OffsetX = OffsetX;
            dropShadow.OffsetY = OffsetY;
            dropShadow.BlurRadius = BlurRadius;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(MainContent))
            {
                mainContent.Content = MainContent;
            }

            if (propertyName == nameof(SurfaceColor))
            {
                dropShadow.SurfaceColor = SurfaceColor;
            }

            if (propertyName == nameof(CornerRadius))
            {
                dropShadow.CornerRadius = CornerRadius;
            }

            if (propertyName == nameof(OffsetX))
            {
                dropShadow.OffsetX = OffsetX;
            }

            if (propertyName == nameof(OffsetY))
            {
                dropShadow.OffsetY = OffsetY;
            }

            if (propertyName == nameof(BlurRadius))
            {
                dropShadow.BlurRadius = BlurRadius;
            }

            if (propertyName == nameof(ContentPadding))
            {
                mainContent.Padding = ContentPadding;
            }
        }
    }
}
