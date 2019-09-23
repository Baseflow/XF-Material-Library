using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using Xamarin.Forms;

namespace XF.Material.Forms.UI.Internals
{
    public partial class BaseMaterialCalendarView : ContentView//, IMaterialElementConfiguration
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

        public DateTime? SelectedDate
        {
            get => this._selectedDate;

            set
            {
                if (this._selectedDate != value)
                {
                    this._selectedDate = value;

                    base.OnPropertyChanged(nameof(this.SelectedDate));
                }
            }
        }

        private List<ContentView> _dayViews { get; set; } = new List<ContentView>(31);

        //public DateTime SelectedMonth { get; private set; }
        public DateTime CurrentMonth
        {
            get => this._currentMonth;

            set
            {
                if (this._currentMonth != value)
                {
                    this._currentMonth = value;
                    base.OnPropertyChanged(nameof(this.CurrentMonth));
                }
            }
        }

        public string Title
        {
            get => this._title;
            set
            {
                this._title = value;
                this.CalendarHeader.Title = value;
            }
        }

        //private const string MONTH_FORMAT = "MMMM yyyy";

        private DateTime? _selectedDate = DateTime.Now;
        private DateTime _currentMonth;
        private ICommand _monthChangedCommand;
        private string _title;

        private ContentView selectedView { get; set; }

        public delegate void SelectionChangedHandler(object sender, CalendarSelectionChangedEventArgs newSelection);

        public event SelectionChangedHandler SelectionChanged;

        public ICommand MonthChangedCommand
        {
            get => this._monthChangedCommand;

            set
            {
                this._monthChangedCommand = value;
                this.OnPropertyChanged(nameof(this.MonthChangedCommand));
            }
        }

        protected DeviceOrientations DisplayOrientation { get; private set; }

        public double FontSize { get; internal set; } = 12;

        public BaseMaterialCalendarView()
        {
            SelectionChanged += this.OnSelectionChanged;

            this.InitializeComponent();
            this.SelectedDate = DateTime.Now;
            this.CurrentMonth = new DateTime(this.SelectedDate.Value.Year, this.SelectedDate.Value.Month, 1);
            this.MonthChangedCommand = new Command(() =>
            {
                this.OnMonthChanged();
                //Nothing
            });

            this.FillCalendarDays();
            this.PositionCalendarDays();
        }

        private void OnMonthChanged()
        {
            this.ClearCalendar();
            this.PositionCalendarDays();
        }

        private void ClearCalendar()
        {
            if (this.DaysGrid == null)
                return;

            List<View> children = this.DaysGrid.Children.ToList();
            for (int i = 1; i < 7; i++)
            {
                foreach (View child in children.Where(child => Grid.GetRow(child) == i))
                {
                    this.DaysGrid.Children.Remove(child);
                }
            }
        }


        public void FillCalendarDays()
        {
            //loop round the days
            for (int i = 1; i < 31 + 1; i++)
            {

                XF.Material.Forms.UI.MaterialLabel label = new XF.Material.Forms.UI.MaterialLabel
                {
                    Text = i.ToString(),
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    TextColor = Color.Gray,
                    BackgroundColor = Color.Transparent,
                };

                CorneredContentView corneredContentView = new CorneredContentView
                {
                    Margin = 2,
                    WidthRequest = 36,
                    HeightRequest = 36,
                    CornerRadius = 18,
                    BackgroundColor = Color.White,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    BorderColor = Color.White,
                    BorderThickness = 1,
                    Content = label,
                    HasShadow = false
                };

                corneredContentView.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => this.OnLabelClicked(corneredContentView))
                });

                this._dayViews.Add(corneredContentView);
            }
        }

        public void PositionCalendarDays()
        {
            if (this.DaysGrid == null)
                return;

            DateTime now = DateTime.Now;
            int rowIndex = 1;
            int dayColumnIndex = (int)this.CurrentMonth.DayOfWeek;
            int daysInMonth = DateTime.DaysInMonth(this.CurrentMonth.Year, this.CurrentMonth.Month);
            int? dayDate = this.SelectedDate?.Day;
            //loop round the days
            for (int i = 0; i < daysInMonth; i++, dayColumnIndex++)
            {
                if (dayColumnIndex > 6)
                {
                    dayColumnIndex = 0;
                    rowIndex++;
                }

                ContentView contentView = this._dayViews[i];
                this.DaysGrid.Children.Add(contentView);

                Grid.SetRow(contentView, rowIndex);
                Grid.SetColumn(contentView, dayColumnIndex);

                contentView.BackgroundColor = Color.White;
                ((CorneredContentView)contentView).BorderColor = Color.White;
                ((MaterialLabel)contentView.Content).TextColor = Color.Gray;


                if (i + 1 == now.Day)
                {
                    if (now.Month == this.CurrentMonth.Month && now.Year == this.CurrentMonth.Year)
                    {
                        ((CorneredContentView)contentView).BorderColor = this.SelectionColor;
                    }
                }

                if (i + 1 == dayDate)
                {
                    if (this.SelectedDate.HasValue && this.SelectedDate.Value.Month == this.CurrentMonth.Month && this.SelectedDate.Value.Year == this.CurrentMonth.Year)
                    {
                        SelectionChanged?.Invoke(this, new CalendarSelectionChangedEventArgs(this.selectedView, contentView));
                    }
                }
            }
        }

        public void OnOrientationChanged(DeviceOrientations displayOrientation)
        {
            if (displayOrientation == DeviceOrientations.Portrait || displayOrientation == DeviceOrientations.PortraitFlipped)
            {
                this.HeightRequest = 512;
                this.WidthRequest = 328;

                this.CalendarHeader.OnOrientationChanged(displayOrientation);

                RowDefinitionCollection rowDefs = new RowDefinitionCollection();

                rowDefs.Add(new RowDefinition { Height = 120 });
                rowDefs.Add(new RowDefinition { Height = 56 });
                rowDefs.Add(new RowDefinition { Height = 344 });

                this.CalendarContainer.RowDefinitions = rowDefs;

                ColumnDefinitionCollection columnDefs = new ColumnDefinitionCollection();

                //None

                this.CalendarContainer.ColumnDefinitions = columnDefs;

                Grid.SetRowSpan(this.CalendarHeader, 1);
                Grid.SetColumn(this.CalendarHeader, 0);
                Grid.SetColumn(this.MonthControl, 0);
                Grid.SetColumn(this.DaysGrid, 0);
                Grid.SetRow(this.CalendarHeader, 0);
                Grid.SetRow(this.MonthControl, 1);
                Grid.SetRow(this.DaysGrid, 2);
            }
            else if (displayOrientation == DeviceOrientations.Landscape || displayOrientation == DeviceOrientations.LandscapeFlipped)
            {
                this.HeightRequest = 328;
                this.WidthRequest = 512;

                this.CalendarHeader.OnOrientationChanged(displayOrientation);

                RowDefinitionCollection rowDefs = new RowDefinitionCollection();

                rowDefs.Add(new RowDefinition { Height = 56 });
                rowDefs.Add(new RowDefinition { Height = 272 });

                this.CalendarContainer.RowDefinitions = rowDefs;


                ColumnDefinitionCollection columnDefs = new ColumnDefinitionCollection();

                columnDefs.Add(new ColumnDefinition { Width = 180 });
                columnDefs.Add(new ColumnDefinition { Width = 332 });

                this.CalendarContainer.ColumnDefinitions = columnDefs;

                Grid.SetRowSpan(this.CalendarHeader, 2);
                Grid.SetColumn(this.CalendarHeader, 0);
                Grid.SetColumn(this.MonthControl, 1);
                Grid.SetColumn(this.DaysGrid, 1);
                Grid.SetRow(this.CalendarHeader, 0);
                Grid.SetRow(this.MonthControl, 0);
                Grid.SetRow(this.DaysGrid, 1);
            }
        }

        private void OnLabelClicked(object view)
        {
            CorneredContentView corneredContentView = (CorneredContentView)view;
            if (this.selectedView == corneredContentView)
            {
                return;
            }
            else
            {
                //Let's change the selection
            }

            SelectionChanged?.Invoke(this, new CalendarSelectionChangedEventArgs(this.selectedView, corneredContentView));
        }

        private void OnSelectionChanged(object sender, CalendarSelectionChangedEventArgs args)
        {
            XF.Material.Forms.UI.MaterialLabel label = null;
            if (args.NewSelection != null)
            {
                label = args.NewSelection.Content as XF.Material.Forms.UI.MaterialLabel;

                int dayDate = Convert.ToInt32(label.Text);
                this.SelectedDate = new DateTime(this.CurrentMonth.Year, this.CurrentMonth.Month, dayDate);
                //this.dateLabel.Text = this.SelectedDate.ToString(DATE_FORMAT);
            }

            if (args.PreviousSelection != null)
            {
                //Probably better to fire selection changed here and handle that passing both views
                XF.Material.Forms.UI.MaterialLabel previousLabel = (XF.Material.Forms.UI.MaterialLabel)args.PreviousSelection.Content;
                this.selectedView.BackgroundColor = Color.White;
                previousLabel.TextColor = Color.Gray;
            }

            if (args.NewSelection != null)
            {
                this.selectedView = args.NewSelection;
                this.selectedView.BackgroundColor = this.SelectionColor;
                label.TextColor = Color.White;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// For internal use only.
        /// </summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public void ElementChanged(bool created)
        //{
        //    if (created)
        //    {
        //        CrossDeviceOrientation.Current.OrientationChanged += this.CurrentOnOrientationChanged;
        //    }
        //    else
        //    {
        //        CrossDeviceOrientation.Current.OrientationChanged -= this.CurrentOnOrientationChanged;
        //    }
        //}
    }

    public class CalendarSelectionChangedEventArgs
    {
        public CalendarSelectionChangedEventArgs(ContentView previousSelection, ContentView newSelection)
        {
            this.PreviousSelection = previousSelection;
            this.NewSelection = newSelection;
        }

        public ContentView PreviousSelection { get; private set; }

        public ContentView NewSelection { get; private set; }
    }
}
