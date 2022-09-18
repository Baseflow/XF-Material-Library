﻿using Microsoft.Maui;

namespace XF.Material.Maui.UI
{
    /// <summary>
    /// A view that shows an image icon that can be tinted.
    /// </summary>
    public class MaterialIcon : Image, IMaterialTintableControl
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="TintColor"/>.
        /// </summary>
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialIcon), null);

        /// <summary>
        /// Gets or sets the tint color of the image icon.
        /// </summary>
        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }
    }
}
