using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Views.Internals;

namespace XF.Material.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialCheckboxGroup : BaseMaterialSelectionControlGroup
    {
        public static readonly BindableProperty SelectedIndicesProperty = BindableProperty.Create(nameof(SelectedIndices), typeof(IList<int>), typeof(MaterialCheckboxGroup), new List<int>(), BindingMode.TwoWay);

        public static readonly BindableProperty SelectedIndicesChangedCommandProperty = BindableProperty.Create(nameof(SelectedIndicesChangedCommand), typeof(Command<int[]>), typeof(MaterialCheckboxGroup));

        internal override ObservableCollection<MaterialSelectionControlModel> Models => selectionList.ItemsSource as ObservableCollection<MaterialSelectionControlModel>;

        public MaterialCheckboxGroup()
        {
            this.InitializeComponent();
        }

        public MaterialCheckboxGroup(IList<string> choices)
        {
            this.InitializeComponent();
            this.Choices = choices;
        }

        public event EventHandler<SelectedIndicesChangedEventArgs> SelectedIndicesChanged;

        public IList<int> SelectedIndices
        {
            get => (IList<int>)this.GetValue(SelectedIndicesProperty);
            set => this.SetValue(SelectedIndicesProperty, value);
        }

        public Command<int[]> SelectedIndicesChangedCommand
        {
            get => (Command<int[]>)this.GetValue(SelectedIndicesChangedCommandProperty);
            set => this.SetValue(SelectedIndicesChangedCommandProperty, value);
        }

        protected override void CreateChoices()
        {
            var models = new ObservableCollection<MaterialSelectionControlModel>();

            foreach(var choice in this.Choices)
            {
                var model = new MaterialSelectionControlModel
                {
                    SelectedChangeCommand = new Command<bool>((isSelected) => this.CheckboxSelected(isSelected, this.Choices.IndexOf(choice))),
                    Text = choice,
                    HorizontalSpacing = this.HorizontalSpacing,
                    FontFamily = this.FontFamily,
                    FontSize = this.FontSize,
                    SelectedColor = this.SelectedColor,
                    UnselectedColor = this.UnselectedColor,
                    TextColor = this.TextColor,
                    VerticalSpacing = this.VerticalSpacing
                };

                models.Add(model);
            }

            selectionList.ItemsSource = models;
            selectionList.HeightRequest = (this.Choices.Count * 48) + 2;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.SelectedIndices) && this.SelectedIndices.Count > 0 && this.Models != null && this.Models.Any())
            {
                foreach (var index in this.SelectedIndices)
                {
                    this.Models[index].IsSelected = true;
                }

                this.SelectedIndicesChangedCommand?.Execute(this.SelectedIndices);
                this.SelectedIndicesChanged?.Invoke(this, new SelectedIndicesChangedEventArgs(this.SelectedIndices));
            }
        }

        private void CheckboxSelected(bool isSelected, int index)
        {
            try
            {
                if (isSelected)
                {
                    this.SelectedIndices.Add(index);
                }

                else
                {
                    this.SelectedIndices.Remove(index);
                }

                this.SelectedIndicesChangedCommand?.Execute(this.SelectedIndices.ToArray());
                this.SelectedIndicesChanged?.Invoke(this, new SelectedIndicesChangedEventArgs(this.SelectedIndices));
            }

            catch(NotSupportedException)
            {
                throw new NotSupportedException("Please use a collection type that has no fixed size for the property SelectedIndices");
            }
        }
    }
}
