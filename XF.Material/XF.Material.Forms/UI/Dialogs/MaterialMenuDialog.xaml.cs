using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs.Configurations;
using XF.Material.Forms.UI.Dialogs.Internals;
using XF.Material.Forms.Models;

namespace XF.Material.Forms.UI.Dialogs
{
    internal struct MaterialMenuDimension
    {
        public MaterialMenuDimension(double rawX, double rawY, double width, double height)
        {
            this.RawX = rawX;
            this.RawY = rawY;
            this.Width = width;
            this.Height = height;
        }

        public double Width { get; }

        public double Height { get; }

        public double RawX { get; }

        public double RawY { get; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialMenuDialog : BaseMaterialModalPage, IMaterialAwaitableDialog<int>
    {
        internal static readonly BindableProperty ParameterProperty = BindableProperty.CreateAttached("Parameter", typeof(object), typeof(MaterialMenuDialog), null);

        private const int _rowHeight = 48;
        private readonly MaterialMenuDimension _dimension;
        private int _itemChecker;
        private int _itemCount;
        private double _maxWidth;
        private List<MaterialMenuItem> _choices;

        internal static void SetPrarameterProperty(BindableObject view, object value)
        {
            view.SetValue(ParameterProperty, value);
        }

        internal static object GetParameterProperty(BindableObject view)
        {
            return view.GetValue(ParameterProperty);
        }

        internal MaterialMenuDialog(List<MaterialMenuItem> choices, MaterialMenuDimension dimension, MaterialMenuConfiguration configuration)
        {
            _dimension = dimension;
            _choices = choices;
            this.InitializeComponent();
            this.CreateActions(configuration);
            this.InputTaskCompletionSource = new TaskCompletionSource<int>();

            Container.CornerRadius = configuration.CornerRadius;
            Container.BackgroundColor = configuration.BackgroundColor;
        }

        public TaskCompletionSource<int> InputTaskCompletionSource { get; set; }

        internal static async Task<int> ShowAsync(List<MaterialMenuItem> choices, MaterialMenuDimension dimension, MaterialMenuConfiguration configuration)
        {
            var dialog = new MaterialMenuDialog(choices, dimension, configuration);
  
            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            DeviceDisplay.ScreenMetricsChanged += this.DeviceDisplay_ScreenMetricsChanged;
        }

        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();

            var newX = _dimension.RawX;
            var newY = (_dimension.RawY);

            DialogActionList.WidthRequest = _maxWidth <= 112 ? 112 : _maxWidth;
            DialogActionList.WidthRequest = _maxWidth > 280 ? 280 : DialogActionList.WidthRequest;

            if (newX + Container.Width >= this.Width)
            {
                newX -= Container.Width;
            }
            else if (newX + Container.Width < this.Width)
            {
                newX += _dimension.Width;
            }

            if (newY + Container.Height + 16 >= this.Height)
            {
                newY -= Container.Height;
            }

            Container.TranslationX = newX;
            Container.TranslationY = newY;
        }

        protected override bool OnBackButtonPressed()
        {
            this.InputTaskCompletionSource.SetResult(-1);

            return base.OnBackButtonPressed();
        }

        protected override bool OnBackgroundClicked()
        {
            this.InputTaskCompletionSource.SetResult(-1);

            return base.OnBackgroundClicked();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            DeviceDisplay.ScreenMetricsChanged -= this.DeviceDisplay_ScreenMetricsChanged;
        }

        private void CreateActions(MaterialMenuConfiguration configuration)
        {
            if (_choices == null || _choices.Count <= 0)
            {
                throw new ArgumentException("Parameter actions should not be null or empty");
            }

            var actionModels = new List<ActionModel>();
            _choices.ForEach(a =>
            {
                var actionModel = new ActionModel { Text = a.Text, Image = a.Image, Index = a.Index };
                actionModel.TextColor = configuration.TextColor;
                actionModel.FontFamily = configuration.TextFontFamily;
                actionModel.SelectedCommand = new Command<int>((position) =>
                {
                    if (this.InputTaskCompletionSource?.Task.Status == TaskStatus.WaitingForActivation)
                    {
                        actionModel.IsSelected = true;
                        this.InputTaskCompletionSource?.SetResult(position);
                        this.Dismiss();
                    }
                });

                actionModels.Add(actionModel);
                actionModel.Index = actionModels.IndexOf(actionModel);
            });

            this.SetList(actionModels);
        }

        private void SetList(IList<ActionModel> actionModels)
        {
            DialogActionList.RowHeight = _rowHeight;
            DialogActionList.HeightRequest = (_rowHeight * actionModels.Count) + 2;
            DialogActionList.ItemsSource = actionModels;
            _itemCount = actionModels.Count;
        }

        private void DeviceDisplay_ScreenMetricsChanged(object sender, ScreenMetricsChangedEventArgs e)
        {
            this.Dismiss();
        }

        private void Label_SizeChanged(object sender, EventArgs e)
        {
            if (_itemChecker != _itemCount)
            {
                var view = sender as View;
                var index = (int) view.GetValue(ParameterProperty);
                _maxWidth = string.IsNullOrEmpty(_choices[index].Image) ? view.Width : view.Width + 40;
                _itemChecker++;

                if (_itemChecker == _itemCount)
                {
                    _maxWidth += 32;
                }
            }
        }
    }
}