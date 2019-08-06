using System;
using Xamarin.Forms;

namespace XF.Material.Forms.UI.Internals
{
    /// <inheritdoc />
    /// <summary>
    /// Used for rendering the <see cref="T:Xamarin.Forms.Entry" /> control in <see cref="T:XF.Material.Forms.UI.MaterialTextField" />.
    /// </summary>
    public class MaterialDatePicker : DatePicker
    {
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialDatePicker), Material.Color.Secondary);

        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(nameof(NullableDate), typeof(DateTime?), typeof(MaterialDatePicker), null);

        internal MaterialDatePicker()
        {
        }

        public Color TintColor
        {
            get
            {
                return (Color)this.GetValue(TintColorProperty);
            }

            set
            {
                this.SetValue(TintColorProperty, value);
            }
        }

        private Color? _color = null;

        public DateTime? NullableDate
        {
            get { return (DateTime?)this.GetValue(NullableDateProperty); }

            set
            {
                if (this.NullableDate != value)
                {
                    this.SetValue(NullableDateProperty, value);
                    this.UpdateDate();
                }
            }
        }

        private void UpdateDate()
        {
            if (this.NullableDate.HasValue)
            {
                if (this._color.HasValue)
                {
                    this.TextColor = this._color.Value;
                    this._color = null;
                }

                this.Date = this.NullableDate.Value;
            }
            else
            {
                this._color = this.TextColor;
                this.TextColor = Color.Transparent;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.UpdateDate();
        }

        public new EventHandler<NullableDateChangedEventArgs> DateSelected;

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);         

            if (propertyName == nameof(this.IsFocused) && !this.IsFocused && this.Date != this.NullableDate)
            {
                DateTime? old = this.NullableDate;
                this.NullableDate = this.Date;
                this.DateSelected(this, new NullableDateChangedEventArgs(old, this.NullableDate));
            }
        }
    }
}