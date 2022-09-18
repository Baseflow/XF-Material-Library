using System;
using Microsoft.Maui;

namespace XF.Material.Maui.UI.Internals
{
    /// <inheritdoc />
    /// <summary>
    /// Used for rendering the <see cref="T:Microsoft.Maui.Controls.Entry" /> control in <see cref="T:XF.Material.Maui.UI.MaterialTextField" />.
    /// </summary>
    public class MaterialDatePicker : DatePicker
    {
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(MaterialDatePicker), Material.Color.Secondary);
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(nameof(NullableDate), typeof(DateTime?), typeof(MaterialDatePicker));
        public static readonly BindableProperty IgnoreCancelProperty = BindableProperty.Create(nameof(IgnoreCancel), typeof(bool), typeof(MaterialDatePicker), true);

        private Color? _color;
        public new EventHandler<NullableDateChangedEventArgs> DateSelected;

        /// <summary>
        /// Public constructor required for xamarin hot reload
        /// </summary>
        public MaterialDatePicker()
        {
            base.DateSelected += (sender, args) =>
            {
                if (!IgnoreCancel)
                    TriggerDateSelected(args.NewDate);
            };
        }

        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        public bool IgnoreCancel
        {
            get => (bool)GetValue(IgnoreCancelProperty);
            set => SetValue(IgnoreCancelProperty, value);
        }

        public DateTime? NullableDate
        {
            get => (DateTime?)GetValue(NullableDateProperty);

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
                if (_color == null)
                {
                    TextColor = _color;
                    _color = null;
                }

                Date = NullableDate.Value;
            }
            else
            {
                _color = TextColor;
                TextColor = Colors.Transparent;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsFocused) && !IsFocused && IgnoreCancel)
            {
                //Called even if cancel is selected. Except if IgnoreCancel is false.
                TriggerDateSelected(Date);
            }
        }

        private void TriggerDateSelected(DateTime date)
        {
            if (date != NullableDate)
            {
                var old = NullableDate;
                NullableDate = date;
                DateSelected?.Invoke(this, new NullableDateChangedEventArgs(old, date));
            }
        }
    }
}
