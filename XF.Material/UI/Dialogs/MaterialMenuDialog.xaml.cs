using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Xaml;
using XF.Material.Maui.Models;
using XF.Material.Maui.UI.Dialogs.Configurations;
using XF.Material.Maui.UI.Dialogs.Internals;

namespace XF.Material.Maui.UI.Dialogs
{
    internal struct MaterialMenuDimension
    {
        public MaterialMenuDimension(double rawX, double rawY, double width, double height)
        {
            RawX = rawX;
            RawY = rawY;
            Width = width;
            Height = height;
        }

        public double Height { get; }
        public double RawX { get; }
        public double RawY { get; }
        public double Width { get; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialMenuDialog : BaseMaterialModalPage, IMaterialAwaitableDialog<int>
    {
        internal static readonly BindableProperty ParameterProperty = BindableProperty.CreateAttached("Parameter", typeof(object), typeof(MaterialMenuDialog), null);

        private const int RowHeight = 48;
        private readonly List<MaterialMenuItem> _choices;
        private readonly MaterialMenuDimension _dimension;
        private int _itemChecker;
        private int _itemCount;
        private double _maxWidth;

        internal MaterialMenuDialog(List<MaterialMenuItem> choices, MaterialMenuDimension dimension, MaterialMenuConfiguration configuration)
        {
            _dimension = dimension;
            _choices = choices;
            InitializeComponent();
            CreateActions(configuration);
            InputTaskCompletionSource = new TaskCompletionSource<int>();

            Container.CornerRadius = configuration.CornerRadius;
            Container.BackgroundColor = configuration.BackgroundColor;
        }

        public TaskCompletionSource<int> InputTaskCompletionSource { get; set; }

        internal static object GetParameterProperty(BindableObject view)
        {
            return view.GetValue(ParameterProperty);
        }

        internal static void SetPrarameterProperty(BindableObject view, object value)
        {
            view.SetValue(ParameterProperty, value);
        }

        internal static async Task<int> ShowAsync(List<MaterialMenuItem> choices, MaterialMenuDimension dimension, MaterialMenuConfiguration configuration)
        {
            var dialog = new MaterialMenuDialog(choices, dimension, configuration);

            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
        }

        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();

            var newX = _dimension.RawX;
            var newY = _dimension.RawY;
            var width = Width - 56;
            var height = Height - 56;

            _maxWidth += 32;
            DialogActionList.WidthRequest = _maxWidth <= 112 ? 112 : _maxWidth;
            DialogActionList.WidthRequest = _maxWidth > 280 ? 280 : DialogActionList.WidthRequest;

            if (newX + Container.Width >= width)
            {
                newX -= (Container.Width / 2) + (_dimension.Width / 2);
            }
            else if (newX + Container.Width < width)
            {
                newX += _dimension.Width / 2;
            }

            if (newY + Container.Height + 16 >= height)
            {
                newY -= Container.Height;
            }

            Container.TranslationX = newX;
            Container.TranslationY = newY;
        }

        protected override void OnBackButtonDismissed()
        {
            InputTaskCompletionSource.TrySetResult(-1);
        }

        protected override bool OnBackgroundClicked()
        {
            InputTaskCompletionSource.TrySetResult(-1);

            return base.OnBackgroundClicked();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            DeviceDisplay.MainDisplayInfoChanged -= DeviceDisplay_MainDisplayInfoChanged;
        }

        private async void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            await DismissAsync();
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
                var actionModel = new MenuActionModel
                {
                    Text = a.Text,
                    Image = a.Image,
                    Index = a.Index,
                    TextColor = configuration.TextColor,
                    FontFamily = configuration.TextFontFamily
                };
                actionModel.SelectedCommand = new Command<int>(async (position) =>
                {
                    if (InputTaskCompletionSource?.Task.Status != TaskStatus.WaitingForActivation)
                    {
                        return;
                    }

                    actionModel.IsSelected = true;
                    await DismissAsync();
                    InputTaskCompletionSource?.TrySetResult(position);
                });
                actionModel.SizeChangeCommand = new Command<Dictionary<string, object>>(LabelSizeChanged);

                actionModels.Add(actionModel);
                actionModel.Index = actionModels.IndexOf(actionModel);
            });

            SetList(actionModels);
        }

        private void LabelSizeChanged(Dictionary<string, object> param)
        {
            var width = Convert.ToDouble(param["width"]);
            var index = Convert.ToInt32(param["parameter"]);
            var maxWidth = (_choices[index].Image?.IsEmpty ?? true) ? width : width + 40;
            _itemChecker++;

            if (_maxWidth < maxWidth)
            {
                _maxWidth = maxWidth;
            }
        }

        private void SetList(ICollection<ActionModel> actionModels)
        {
            DialogActionList.HeightRequest = RowHeight * actionModels.Count;
            DialogActionList.SetValue(BindableLayout.ItemsSourceProperty, actionModels);
            _itemCount = actionModels.Count;
        }
    }
}
