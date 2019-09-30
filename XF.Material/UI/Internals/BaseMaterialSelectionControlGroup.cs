using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XF.Material.Forms.Resources;

namespace XF.Material.Forms.UI.Internals
{
    /// <summary>
    /// Base class of selection control groups. Used by <see cref="MaterialRadioButtonGroup"/> and <see cref="MaterialCheckboxGroup"/>.
    /// </summary>
    public abstract class BaseMaterialSelectionControlGroup : ContentView
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="Choices"/>.
        /// </summary>
        public static readonly BindableProperty ChoicesProperty = BindableProperty.Create(nameof(Choices), typeof(IList<string>), typeof(BaseMaterialSelectionControlGroup), default(IList<string>));

        /// <summary>
        /// Backing field for the bindable property <see cref="FontFamily"/>.
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(BaseMaterialSelectionControlGroup), Material.FontFamily.Body1);

        /// <summary>
        /// Backing field for the bindable property <see cref="FontSize"/>.
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(BaseMaterialSelectionControlGroup), Material.GetResource<double>(MaterialConstants.MATERIAL_FONTSIZE_BODY1));

        /// <summary>
        /// Backing field for the bindable property <see cref="HorizontalSpacing"/>.
        /// </summary>
        public static readonly BindableProperty HorizontalSpacingProperty = BindableProperty.Create(nameof(HorizontalSpacing), typeof(double), typeof(BaseMaterialSelectionControlGroup), 0.0);

        /// <summary>
        /// Backing field for the bindable property <see cref="SelectedColor"/>.
        /// </summary>
        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(BaseMaterialSelectionControlGroup), Material.Color.Secondary);

        /// <summary>
        /// Backing field for the bindable property <see cref="TextColor"/>.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(BaseMaterialSelectionControlGroup), Color.FromHex("#DE000000"));

        /// <summary>
        /// Backing field for the bindable property <see cref="UnselectedColor"/>.
        /// </summary>
        public static readonly BindableProperty UnselectedColorProperty = BindableProperty.Create(nameof(UnselectedColor), typeof(Color), typeof(BaseMaterialSelectionControlGroup), Color.FromHex("#84000000"));

        /// <summary>
        /// Backing field for the bindable property <see cref="VerticalSpacing"/>.
        /// </summary>
        public static readonly BindableProperty VerticalSpacingProperty = BindableProperty.Create(nameof(VerticalSpacing), typeof(double), typeof(BaseMaterialSelectionControlGroup), 0.0);

        internal static readonly BindableProperty ShouldShowScrollbarProperty = BindableProperty.Create(nameof(ShouldShowScrollbar), typeof(bool), typeof(BaseMaterialSelectionControlGroup), false);

        internal bool ShouldShowScrollbar
        {
            get => (bool)GetValue(ShouldShowScrollbarProperty);
            set => SetValue(ShouldShowScrollbarProperty, value);
        }

        /// <summary>
        /// Gets or sets the list of string which the user will choose from.
        /// </summary>
        public IList<string> Choices
        {
            get => (IList<string>)GetValue(ChoicesProperty);
            set => SetValue(ChoicesProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family of the text of each selection controls.
        /// </summary>
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the font size of the text of each selection controls.
        /// </summary>
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the spacing between the selection control and its text.
        /// </summary>
        public double HorizontalSpacing
        {
            get => (double)GetValue(HorizontalSpacingProperty);
            set => SetValue(HorizontalSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the color that will be used to tint this control when selected.
        /// </summary>
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the text of each selection control.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color that will be used to tint this control when unselected.
        /// </summary>
        public Color UnselectedColor
        {
            get => (Color)GetValue(UnselectedColorProperty);
            set => SetValue(UnselectedColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the spacing between each selection control.
        /// </summary>
        public double VerticalSpacing
        {
            get => (double)GetValue(VerticalSpacingProperty);
            set => SetValue(VerticalSpacingProperty, value);
        }

        internal abstract ObservableCollection<MaterialSelectionControlModel> Models { get; }

        protected abstract void CreateChoices();

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Choices) when Choices != null && Choices.Any():
                    CreateChoices();
                    break;
                case nameof(IsEnabled):
                    Opacity = IsEnabled ? 1.0 : 0.38;
                    break;
            }
        }
    }
}
