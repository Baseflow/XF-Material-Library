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
    public partial class MaterialRadioButtonGroup : BaseMaterialSelectionControlGroup
    {
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(MaterialRadioButtonGroup), -1);

        public static readonly BindableProperty SelectedIndexChangedCommandProperty = BindableProperty.Create(nameof(SelectedIndexChangedCommand), typeof(Command<int>), typeof(MaterialRadioButtonGroup));

        internal override ObservableCollection<MaterialSelectionControlModel> Models => selectionList.ItemsSource as ObservableCollection<MaterialSelectionControlModel>;
        private MaterialSelectionControlModel _selectedModel;

        public MaterialRadioButtonGroup()
        {
            this.InitializeComponent();
        }

        public MaterialRadioButtonGroup(IList<string> choices)
        {
            this.InitializeComponent();
            this.Choices = choices;
        }

        public event EventHandler<SelectedIndexChangedEventArgs> SelectedIndexChanged;

        public int SelectedIndex
        {
            get => (int)this.GetValue(SelectedIndexProperty);
            set => this.SetValue(SelectedIndexProperty, value);
        }

        public Command<int> SelectedIndexChangedCommand
        {
            get => (Command<int>)this.GetValue(SelectedIndexChangedCommandProperty);
            set => this.SetValue(SelectedIndexChangedCommandProperty, value);
        }

        protected override void CreateChoices()
        {
            var models = new ObservableCollection<MaterialSelectionControlModel>();

            foreach (var choice in this.Choices)
            {
                var index = this.Choices.IndexOf(choice);
                var model = new MaterialSelectionControlModel
                {
                    Index = index,
                    Text = choice
                };
                model.SelectedChangeCommand = new Command<bool>((isSelected) =>
                {
                    this.RadioButtonSelected(isSelected, model);
                });

                models.Add(model);
            }

            selectionList.ItemsSource = models;
            selectionList.HeightRequest = (this.Choices.Count * 48) + 2;
        }

        private void RadioButtonSelected(bool isSelected, MaterialSelectionControlModel model)
        {
            if (isSelected)
            {
                if (_selectedModel == model)
                {
                    return;
                }

                if (_selectedModel != null)
                {
                    _selectedModel.IsSelected = false;
                }

                _selectedModel = model;
                this.SelectedIndex = _selectedModel.Index;
            }

            else if (_selectedModel == model)
            {
                _selectedModel = null;
                this.SelectedIndex = -1;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.SelectedIndex))
            {
                if (this.SelectedIndex >= 0 && this.Models != null && this.Models.Any())
                {
                    _selectedModel = this.Models[this.SelectedIndex];
                    _selectedModel.IsSelected = true;
                }

                this.SelectedIndexChangedCommand?.Execute(this.SelectedIndex);
                this.SelectedIndexChanged?.Invoke(this, new SelectedIndexChangedEventArgs(this.SelectedIndex));
            }
        }
    }
}
