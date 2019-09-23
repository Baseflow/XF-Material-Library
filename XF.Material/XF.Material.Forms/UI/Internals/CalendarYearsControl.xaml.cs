using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace XF.Material.Forms.UI.Internals
{
    public partial class CalendarYearsControl : ContentView
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="SelectionColor"/>.
        /// </summary>
        public static readonly BindableProperty SelectionColorProperty = BindableProperty.Create(nameof(SelectionColor), typeof(Color), typeof(CalendarHeaderView), Material.Color.Secondary);

        /// <summary>
        /// Gets or sets the color of the selection control image if it was selected.
        /// </summary>
        public Color SelectionColor
        {
            get => (Color)this.GetValue(SelectionColorProperty);
            set => this.SetValue(SelectionColorProperty, value);
        }

        private int _selectedYear;

        public int SelectedYear
        {
            get => this._selectedYear;
            set
            {
                this._selectedYear = value;
                base.OnPropertyChanged(nameof(this.SelectedYear));
            }
        }

        private ObservableCollection<int> _years = new ObservableCollection<int>();

        public ObservableCollection<int> Years
        {
            get => this._years;
            set
            {
                this._years = value;
                base.OnPropertyChanged(nameof(this.Years));
            }
        }
        public CalendarYearsControl()
        {
            this.InitializeComponent();

            for (int i = DateTime.Now.Year; i < DateTime.Now.Year + 100; i++)
            {
                this.Years.Add(i);
            }
        }

        public void OnSelectionChanged(object sender, Xamarin.Forms.SelectionChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine(sender);
        }
    }
}
