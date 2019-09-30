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
                return (Color)GetValue(TintColorProperty);
            }

            set
            {
                SetValue(TintColorProperty, value);
            }
        }

        private Color? _color = null;

        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }

            set
            {
                if (NullableDate != value)
                {
                    SetValue(NullableDateProperty, value);
                    UpdateDate();
                }
            }
        }

        private void UpdateDate()
        {
            if (NullableDate.HasValue)
            {
                if (_color.HasValue)
                {
                    TextColor = _color.Value;
                    _color = null;
                }

                Date = NullableDate.Value;
            }
            else
            {
                _color = TextColor;
                TextColor = Color.Transparent;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        public new EventHandler<NullableDateChangedEventArgs> DateSelected;

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsFocused) && !IsFocused && Date != NullableDate)
            {
                var old = NullableDate;
                NullableDate = Date;
                DateSelected(this, new NullableDateChangedEventArgs(old, NullableDate));
            }
        }
    }
}