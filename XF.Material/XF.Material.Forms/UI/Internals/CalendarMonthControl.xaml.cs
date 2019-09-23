using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace XF.Material.Forms.UI.Internals
{
    public partial class CalendarMonthControl : Grid
    {
        private const string MONTH_FORMAT = "MMMM yyyy";

        public static readonly BindableProperty CurrentMonthProperty = BindableProperty.Create(nameof(CurrentMonth), typeof(DateTime), typeof(CalendarMonthControl), DateTime.Now, BindingMode.TwoWay, null, CurrentMonthPropertyChanged);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CalendarMonthControl));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CalendarMonthControl), null);

        public ICommand Command
        {
            get { return (ICommand)base.GetValue(CommandProperty); }
            set { base.SetValue(CommandProperty, value); }
        }
        
        public object CommandParameter
        {
            get { return (object)base.GetValue(CommandParameterProperty); }
            set { base.SetValue(CommandParameterProperty, value); }
        }

        public ICommand NextMonthCommand
        {
            get
            {
                return new Command(() =>
                {
                    this.CurrentMonth = this.CurrentMonth.AddMonths(1);
                    if (this.Command != null)
                    {
                        this.Command.Execute(this.CommandParameter);
                    }
                });
            }
        }

        public ICommand PreviousMonthCommand
        {
            get
            {
                return new Command(() =>
                {
                    this.CurrentMonth = this.CurrentMonth.AddMonths(-1);
                    if (this.Command != null)
                    {
                        this.Command.Execute(this.CommandParameter);
                    }
                });
            }
        }

        public DateTime CurrentMonth
        {
            get => (DateTime)this.GetValue(CurrentMonthProperty);
            set => this.SetValue(CurrentMonthProperty, value);
        }

        private static void CurrentMonthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            CalendarMonthControl control = (CalendarMonthControl)bindable;
            DateTime newDate = (DateTime)newValue;
            control.monthLabel.Text = newDate.ToString(MONTH_FORMAT);
        }

        public CalendarMonthControl()
        {
            this.InitializeComponent();
        }
    }
}
