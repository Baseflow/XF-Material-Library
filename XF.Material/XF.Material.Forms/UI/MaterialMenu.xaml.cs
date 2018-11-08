using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Models;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace XF.Material.Forms.UI
{
    /// <summary>
    /// A control that allow users to select a single choice on a temporary surface.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialMenu : ContentView
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="Choices"/>.
        /// </summary>
        public static readonly BindableProperty ChoicesProperty = BindableProperty.Create(nameof(Choices), typeof(IList<object>), typeof(MaterialMenu));

        /// <summary>
        /// Backing field for the bindable property <see cref="MenuBackgroundColor"/>.
        /// </summary>
        public static readonly BindableProperty MenuBackgroundColorProperty = BindableProperty.Create(nameof(MenuBackgroundColor), typeof(Color), typeof(MaterialMenu), Material.Color.Surface);

        /// <summary>
        /// Backing field for the bindable property <see cref="MenuCornerRadius"/>.
        /// </summary>
        public static readonly BindableProperty MenuCornerRadiusProperty = BindableProperty.Create(nameof(MenuCornerRadius), typeof(float), typeof(MaterialMenu), 4f);

        /// <summary>
        /// Backing field for the bindable property <see cref="MenuSelectedCommandParameter"/>.
        /// </summary>
        public static readonly BindableProperty MenuSelectedCommandParameterProperty = BindableProperty.Create(nameof(MenuSelectedCommandParameter), typeof(object), typeof(MaterialMenu));

        /// <summary>
        /// Backing field for the bindable property <see cref="MenuSelectedCommand"/>.
        /// </summary>
        public static readonly BindableProperty MenuSelectedCommandProperty = BindableProperty.Create(nameof(MenuSelectedCommand), typeof(Command<MaterialMenuResult>), typeof(MaterialMenu));

        /// <summary>
        /// Backing field for the bindable property <see cref="MenuTextColor"/>.
        /// </summary>
        public static readonly BindableProperty MenuTextColorProperty = BindableProperty.Create(nameof(MenuTextColor), typeof(Color), typeof(MaterialMenu), Material.Color.OnSurface);

        /// <summary>
        /// Backing field for the bindable property <see cref="MenuTextFontFamily"/>.
        /// </summary>
        public static readonly BindableProperty MenuTextFontFamilyProperty = BindableProperty.Create(nameof(MenuTextFontFamily), typeof(string), typeof(MaterialMenu));

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialMenu"/>.
        /// </summary>
        public MaterialMenu()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Raised when a menu item was selected.
        /// </summary>
        public event EventHandler<MenuSelectedEventArgs> MenuSelected;

        /// <summary>
        /// Gets or sets the list of items from which the user will choose from.
        /// </summary>
        public IList<object> Choices
        {
            get => (IList<object>)this.GetValue(ChoicesProperty);
            set => this.SetValue(ChoicesProperty, value);
        }

        /// <summary>
        /// Gets or sets the background color of the menu.
        /// </summary>
        public Color MenuBackgroundColor
        {
            get => (Color)this.GetValue(MenuBackgroundColorProperty);
            set => this.SetValue(MenuBackgroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the corner radius of the menu.
        /// </summary>
        public float MenuCornerRadius
        {
            get => (float)this.GetValue(MenuCornerRadiusProperty);
            set => this.SetValue(MenuCornerRadiusProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will execute when a menu item was selected.
        /// </summary>
        public Command<MaterialMenuResult> MenuSelectedCommand
        {
            get => (Command<MaterialMenuResult>)this.GetValue(MenuSelectedCommandProperty);
            set => this.SetValue(MenuSelectedCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter to pass in <see cref="MenuSelectedCommand"/> when executed.
        /// </summary>
        public object MenuSelectedCommandParameter
        {
            get => this.GetValue(MenuSelectedCommandParameterProperty);
            set => this.SetValue(MenuSelectedCommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the text color of the menu items.
        /// </summary>
        public Color MenuTextColor
        {
            get => (Color)this.GetValue(MenuTextColorProperty);
            set => this.SetValue(MenuTextColorProperty, value);
        }

        public string MenuTextFontFamily
        {
            get => (string)this.GetValue(MenuTextFontFamilyProperty);
            set => this.SetValue(MenuTextFontFamilyProperty, value);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void OnViewTouch(double x, double y)
        {
            if (this.Choices == null || this.Choices?.Count == 0)
            {
                throw new InvalidOperationException("Cannot show menu, property Choices is null or has no items");
            }

            var dimension = new MaterialMenuDimension(x, y);

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await MaterialMenuDialog.ShowAsync(this.CreateMenuItems(), dimension, new MaterialMenuConfiguration
                {
                    CornerRadius = this.MenuCornerRadius,
                    BackgroundColor = this.MenuBackgroundColor,
                    TextColor = this.MenuTextColor,
                    TextFontFamily = this.MenuTextFontFamily
                });

                if (result >= 0)
                {
                    this.OnMenuSelected(new MaterialMenuResult(result, this.MenuSelectedCommandParameter));
                }
            });
        }

        /// <summary>
        /// Called when a menu item was selected.
        /// </summary>
        /// <param name="result">The result of the selection.</param>
        protected virtual void OnMenuSelected(MaterialMenuResult result)
        {
            this.MenuSelectedCommand?.Execute(result);
            this.MenuSelected?.Invoke(this, new MenuSelectedEventArgs(result));
        }

        private List<MaterialMenuItem> CreateMenuItems()
        {
            var items = new List<MaterialMenuItem>();
            var collectionType = this.Choices.FirstOrDefault()?.GetType();

            if (collectionType == typeof(string))
            {
                foreach (var item in this.Choices as IList<string>)
                {
                    items.Add(new MaterialMenuItem
                    {
                        Text = item,
                        Index = this.Choices.IndexOf(item)
                    });
                }
            }
            else if (collectionType == typeof(MaterialMenuItem))
            {
                items.AddRange(this.Choices as IList<MaterialMenuItem>);

                foreach (var item in items)
                {
                    item.Index = items.IndexOf(item);
                }
            }
            else
            {
                throw new InvalidOperationException("The property 'Choices' has invalid item types. Please use either a collection of 'System.String' or 'XF.Material.Forms.Models.MaterialMenuItem'.");
            }

            return items;
        }
    }
}