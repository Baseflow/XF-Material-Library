using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XF.Material.Effects;
using XF.Material.Resources;
using XF.Material.Resources.Typography;

namespace XF.Material.Views
{
    public class MaterialButton : Button
    {
        public const string MaterialButtonColorChanged = "BackgroundColorChanged";
        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialButton), Material.GetMaterialResource<Color>(MaterialConstants.MATERIAL_COLOR_SECONDARY));
        public static readonly BindableProperty AllCapsProperty = BindableProperty.Create(nameof(AllCaps), typeof(bool), typeof(MaterialButton), true);
        public static readonly BindableProperty ButtonTypeProperty = BindableProperty.Create(nameof(ButtonType), typeof(MaterialButtonType), typeof(MaterialButton), MaterialButtonType.Elevated, propertyChanged: ButtonTypeChanged);

        private static void ButtonTypeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MaterialButton materialButton)
            {
                switch (materialButton.ButtonType)
                {
                    case MaterialButtonType.Text:
                        materialButton.RemoveDynamicResource(TextColorProperty);
                        materialButton.SetDynamicResource(TextColorProperty, MaterialConstants.MATERIAL_COLOR_SECONDARY);
                        break;
                    case MaterialButtonType.Outlined:

                        if (materialButton.BorderColor == (Color)BorderColorProperty.DefaultValue)
                        {
                            materialButton.SetDynamicResource(BorderColorProperty, MaterialConstants.MATERIAL_BUTTON_OUTLINED_BORDERCOLOR);
                        }

                        if (materialButton.BorderWidth == (double)BorderWidthProperty.DefaultValue)
                        {
                            materialButton.SetDynamicResource(BorderWidthProperty, MaterialConstants.MATERIAL_BUTTON_OUTLINED_BORDERWIDTH);
                        }

                        materialButton.RemoveDynamicResource(TextColorProperty);
                        materialButton.SetDynamicResource(TextColorProperty, MaterialConstants.MATERIAL_COLOR_SECONDARY);

                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the text of this button should be capitalized. The default value is true.
        /// </summary>
        public bool AllCaps
        {
            get => (bool)GetValue(AllCapsProperty);
            set => SetValue(AllCapsProperty, value);
        }

        /// <summary>
        /// Gets or sets the type of this button. The default value is <see cref="MaterialButtonType.Elevated"/>
        /// </summary>
        public MaterialButtonType ButtonType
        {
            get => (MaterialButtonType)GetValue(ButtonTypeProperty);
            set => SetValue(ButtonTypeProperty, value);
        }

        /// <summary>
        /// Gets or sets the background color. The default value is based on the Color value of <see cref="MaterialColor.Secondary"/> if you are using a Material resource, otherwise the default value is <see cref="Color.Accent"/>
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public MaterialButton()
        {
            this.SetValue(MaterialEffectsUtil.LetterSpacingProperty, MaterialTypeScale.Button);
            this.SetDynamicResource(FontFamilyProperty, MaterialConstants.MATERIAL_FONTFAMILY_MEDIUM);
            this.SetDynamicResource(FontSizeProperty, MaterialConstants.MATERIAL_FONTSIZE_BUTTON);
            this.SetDynamicResource(CornerRadiusProperty, MaterialConstants.MATERIAL_BUTTON_CORNERRADIUS);
            this.SetDynamicResource(BackgroundColorProperty, MaterialConstants.MATERIAL_COLOR_SECONDARY);
            this.SetDynamicResource(TextColorProperty, MaterialConstants.MATERIAL_COLOR_ONSECONDARY);
            this.SetDynamicResource(HeightRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(BackgroundColor))
            {
                base.OnPropertyChanged(MaterialButton.MaterialButtonColorChanged);
            }

            else
            {
                base.OnPropertyChanged(propertyName);

                if (propertyName == nameof(this.Style))
                {
                    this.Style.Setters.ForEach(s => this.SetValue(s.Property, s.Value));
                }
            }
        }

        public enum MaterialButtonType
        {
            /// <summary>
            /// This button will cast a shadow.
            /// </summary>
            Elevated,

            /// <summary>
            /// This button will not cast a shadow.
            /// </summary>
            Flat,

            /// <summary>
            /// This button will have a transparent background with a border.
            /// </summary>
            Outlined,

            /// <summary>
            /// This button will have a transparent background and no border.
            /// </summary>
            Text
        }
    }
}
