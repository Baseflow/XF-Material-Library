using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.DeviceOrientation.Abstractions;
using Xamarin.Forms;


namespace XF.Material.Forms.UI.Internals
{
    public partial class CalendarHeaderView : CorneredContentView
    {
        private const string DATE_FORMAT = "ddd, MMM dd";

        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(CalendarHeaderView), DateTime.Now, BindingMode.OneWay, null, DatePropertyChanged);

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CalendarHeaderView), string.Empty, BindingMode.OneWay);

        /// <summary>
        /// Backing field for the bindable property <see cref="HeaderColor"/>.
        /// </summary>
        public static readonly BindableProperty HeaderColorProperty = BindableProperty.Create(nameof(HeaderColor), typeof(Color), typeof(CalendarHeaderView), Material.Color.Primary);

        /// <summary>
        /// Gets or sets the color of the selection control image if it was selected.
        /// </summary>
        public Color HeaderColor
        {
            get => (Color)this.GetValue(HeaderColorProperty);
            set => this.SetValue(HeaderColorProperty, value);
        }

        public string Title
        {
            get => (string)this.GetValue(TitleProperty);
            set => this.SetValue(TitleProperty, value);
        }

        private static void DatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            CalendarHeaderView control = (CalendarHeaderView)bindable;
            DateTime newDate = (DateTime)newValue;
            control.dateLabel.Text = newDate.ToString(DATE_FORMAT);
        }

        public DateTime Date
        {
            get => (DateTime)this.GetValue(DateProperty);
            set => this.SetValue(DateProperty, value);
        }

        public CalendarHeaderView()
        {
            this.InitializeComponent();
            
        }

        public void OnOrientationChanged(DeviceOrientations displayOrientation)
        {
            if (displayOrientation == DeviceOrientations.Portrait || displayOrientation == DeviceOrientations.PortraitFlipped)
            {
                //this.dateLabel.HeightRequest = 56;
                this.HeightRequest = 120;
                
            }
            else if (displayOrientation == DeviceOrientations.Landscape || displayOrientation == DeviceOrientations.LandscapeFlipped)
            {
                this.dateLabel.HeightRequest = 120;
                this.dateLabel.WidthRequest = 50;
                this.HeightRequest = 328;
            }
        }
    }
}