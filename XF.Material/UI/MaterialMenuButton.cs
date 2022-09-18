using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Maui;
using XF.Material.Maui.Models;
using XF.Material.Maui.UI.Dialogs;
using XF.Material.Maui.UI.Dialogs.Configurations;

namespace XF.Material.Maui.UI
{
    /// <summary>
    /// A control that allow users to select a single choice on a temporary surface.
    /// </summary>
    public class MaterialMenuButton : MaterialIconButton
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="Choices"/>.
        /// </summary>
        public static readonly BindableProperty ChoicesProperty = BindableProperty.Create(nameof(Choices), typeof(IList), typeof(MaterialMenuButton));

        /// <summary>
        /// Backing field for the bindable property <see cref="MenuBackgroundColor"/>.
        /// </summary>
        public static readonly BindableProperty MenuBackgroundColorProperty = BindableProperty.Create(nameof(MenuBackgroundColor), typeof(Color), typeof(MaterialMenuButton), Material.Color.Surface);

        /// <summary>
        /// Backing field for the bindable property <see cref="MenuCornerRadius"/>.
        /// </summary>
        public static readonly BindableProperty MenuCornerRadiusProperty = BindableProperty.Create(nameof(MenuCornerRadius), typeof(float), typeof(MaterialMenuButton), 4f);

        /// <summary>
        /// Backing field for the bindable property <see cref="CommandParameter"/>.
        /// </summary>
        public static new readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialMenuButton));

        /// <summary>
        /// Backing field for the bindable property <see cref="Command"/>.
        /// </summary>
        public static new readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command<MaterialMenuResult>), typeof(MaterialMenuButton));

        /// <summary>
        /// Backing field for the bindable property <see cref="MenuTextColor"/>.
        /// </summary>
        public static readonly BindableProperty MenuTextColorProperty = BindableProperty.Create(nameof(MenuTextColor), typeof(Color), typeof(MaterialMenuButton), Material.Color.OnSurface);

        /// <summary>
        /// Backing field for the bindable property <see cref="MenuTextFontFamily"/>.
        /// </summary>
        public static readonly BindableProperty MenuTextFontFamilyProperty = BindableProperty.Create(nameof(MenuTextFontFamily), typeof(string), typeof(MaterialMenuButton));

        /// <summary>
        /// Raised when a menu item was selected.
        /// </summary>
        public event EventHandler<MenuSelectedEventArgs> MenuSelected;

        /// <summary>
        /// Gets or sets the list of items from which the user will choose from.
        /// </summary>
        public IList Choices
        {
            get => (IList)GetValue(ChoicesProperty);
            set => SetValue(ChoicesProperty, value);
        }

        /// <summary>
        /// Gets or sets the background color of the menu.
        /// </summary>
        public Color MenuBackgroundColor
        {
            get => (Color)GetValue(MenuBackgroundColorProperty);
            set => SetValue(MenuBackgroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the corner radius of the menu.
        /// </summary>
        public float MenuCornerRadius
        {
            get => (float)GetValue(MenuCornerRadiusProperty);
            set => SetValue(MenuCornerRadiusProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will execute when a menu item was selected.
        /// </summary>
        public new Command<MaterialMenuResult> Command
        {
            get => (Command<MaterialMenuResult>)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter to pass in <see cref="Command"/> when executed.
        /// </summary>
        public new object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the text color of the menu items.
        /// </summary>
        public Color MenuTextColor
        {
            get => (Color)GetValue(MenuTextColorProperty);
            set => SetValue(MenuTextColorProperty, value);
        }

        public string MenuTextFontFamily
        {
            get => (string)GetValue(MenuTextFontFamilyProperty);
            set => SetValue(MenuTextFontFamilyProperty, value);
        }

        protected override void OnButtonClicked(bool handled)
        {
            base.OnButtonClicked(false);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void OnViewTouch(double x, double y)
        {
            if (Choices == null || Choices?.Count == 0)
            {
                throw new InvalidOperationException("Cannot show menu, property Choices is null or has no items");
            }

            var dimension = new MaterialMenuDimension(x, y, Width, Height);

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await MaterialMenuDialog.ShowAsync(CreateMenuItems(), dimension, new MaterialMenuConfiguration
                {
                    CornerRadius = MenuCornerRadius,
                    BackgroundColor = MenuBackgroundColor,
                    TextColor = MenuTextColor,
                    TextFontFamily = MenuTextFontFamily
                });

                if (result >= 0)
                {
                    OnMenuSelected(new MaterialMenuResult(result, CommandParameter));
                }
                else
                {
                    OnMenuSelected(new MaterialMenuResult(-1, null));
                }
            });
        }

        /// <summary>
        /// Called when a menu item was selected.
        /// </summary>
        /// <param name="result">The result of the selection.</param>
        protected virtual void OnMenuSelected(MaterialMenuResult result)
        {
            Command?.Execute(result);
            MenuSelected?.Invoke(this, new MenuSelectedEventArgs(result));
        }

        private List<MaterialMenuItem> CreateMenuItems()
        {
            var items = new List<MaterialMenuItem>();
            var collectionType = Choices.Cast<object>().FirstOrDefault()?.GetType();

            if (collectionType == typeof(MaterialMenuItem))
            {
                if (!(Choices is IList<MaterialMenuItem> result))
                {
                    return default(List<MaterialMenuItem>);
                }

                items.AddRange(result);

                foreach (var item in items)
                {
                    item.Index = items.IndexOf(item);
                }
            }
            else
            {
                items.AddRange(from object item in Choices select new MaterialMenuItem { Text = item.ToString(), Index = Choices.IndexOf(item) });
            }

            return items;
        }
    }
}
