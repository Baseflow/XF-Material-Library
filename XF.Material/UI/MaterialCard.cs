using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.Resources;

namespace XF.Material.Forms.UI
{
    /// <summary>
    /// A container that display content and actions on a single topic.
    /// </summary>
    public class MaterialCard : Frame
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="ClickCommandParameter"/>.
        /// </summary>
        public static readonly BindableProperty ClickCommandParameterProperty = BindableProperty.Create(nameof(ClickCommandParameter), typeof(object), typeof(MaterialCard));

        /// <summary>
        /// Backing field for the bindable property <see cref="ClickCommand"/>.
        /// </summary>
        public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create(nameof(ClickCommand), typeof(ICommand), typeof(MaterialCard));

        /// <summary>
        /// Backing field for the bindable property <see cref="Elevation"/>.
        /// </summary>
        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(int), typeof(MaterialCard), 1);

        /// <summary>
        /// Backing field for the bindable property <see cref="IsClickable"/>.
        /// </summary>
        public static readonly BindableProperty IsClickableProperty = BindableProperty.Create(nameof(IsClickable), typeof(bool), typeof(MaterialCard), false);

        private TapGestureRecognizer _tapGestureRecognizer;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialCard"/>.
        /// </summary>
        public MaterialCard()
        {
            this.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.SURFACE);
        }

        /// <summary>
        /// When the property <see cref="IsClickable"/> is set to true, this event is raised when this card was clicked.
        /// </summary>
        public event EventHandler Clicked;

        /// <summary>
        /// When the property <see cref="IsClickable"/> is set to true, this command will be executed when this card was clicked.
        /// </summary>
        public ICommand ClickCommand
        {
            get => (ICommand)this.GetValue(ClickCommandProperty);
            set => this.SetValue(ClickCommandProperty, value);
        }

        /// <summary>
        /// The parameter to pass when <see cref="ClickCommand"/> executes.
        /// </summary>
        public object ClickCommandParameter
        {
            get => this.GetValue(ClickCommandParameterProperty);
            set => this.SetValue(ClickCommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the virtual distance along the z-axis for emphasis.
        /// </summary>
        public int Elevation
        {
            get => (int)this.GetValue(ElevationProperty);
            set => this.SetValue(ElevationProperty, value);
        }

        /// <summary>
        /// Gets or sets the flag indicating of the card is clickable with a ripple effect.
        /// </summary>
        public bool IsClickable
        {
            get => (bool)this.GetValue(IsClickableProperty);
            set => this.SetValue(IsClickableProperty, value);
        }

        protected virtual void OnClick()
        {
            this.Clicked?.Invoke(this, EventArgs.Empty);
            this.ClickCommand?.Execute(this.ClickCommandParameter);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(this.HasShadow))
            {
                return;
            }

            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.IsClickable))
            {
                this.OnIsClickableChanged(this.IsClickable);
            }
        }

        private void OnIsClickableChanged(bool isClickable)
        {
            if (isClickable)
            {
                if (_tapGestureRecognizer == null)
                {
                    _tapGestureRecognizer = new TapGestureRecognizer
                    {
                        Command = new Command(this.OnClick)
                    };
                }

                this.GestureRecognizers.Add(_tapGestureRecognizer);
            }
            else
            {
                this.GestureRecognizers.Remove(_tapGestureRecognizer);
            }
        }
    }
}