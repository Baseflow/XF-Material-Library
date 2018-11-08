using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Internals;

namespace XF.Material.Forms.UI
{
    /// <summary>
    /// A selection control group that allows the user to choose one from a set of choices.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialRadioButtonGroup : BaseMaterialSelectionControlGroup
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="SelectedIndexChangedCommand"/>.
        /// </summary>
        public static readonly BindableProperty SelectedIndexChangedCommandProperty = BindableProperty.Create(nameof(SelectedIndexChangedCommand), typeof(Command<int>), typeof(MaterialRadioButtonGroup));

        /// <summary>
        /// Backing field for the bindable property <see cref="SelectedIndex"/>.
        /// </summary>
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(MaterialRadioButtonGroup), -1, BindingMode.TwoWay);

        private MaterialSelectionControlModel _selectedModel;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialRadioButtonGroup"/>.
        /// </summary>
        public MaterialRadioButtonGroup()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialRadioButtonGroup"/>.
        /// </summary>
        /// <param name="choices">The list of string which the user will choose from.</param>
        public MaterialRadioButtonGroup(IList<string> choices)
        {
            this.InitializeComponent();
            this.Choices = choices;
        }

        /// <summary>
        /// Raised when there is a change in the control's selected index.
        /// </summary>
        public event EventHandler<SelectedIndexChangedEventArgs> SelectedIndexChanged;

        /// <summary>
        /// Gets or sets the index of the selected choice.
        /// </summary>
        public int SelectedIndex
        {
            get => (int)this.GetValue(SelectedIndexProperty);
            set => this.SetValue(SelectedIndexProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that wil run if there is a change in the control's selected index.
        /// </summary>
        public Command<int> SelectedIndexChangedCommand
        {
            get => (Command<int>)this.GetValue(SelectedIndexChangedCommandProperty);
            set => this.SetValue(SelectedIndexChangedCommandProperty, value);
        }

        internal override ObservableCollection<MaterialSelectionControlModel> Models => selectionList.ItemsSource as ObservableCollection<MaterialSelectionControlModel>;

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

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (widthConstraint * heightConstraint != 0 && this.SelectedIndex >= 0)
            {
                _selectedModel = this.Models[this.SelectedIndex];
            }

            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.SelectedIndex))
            {
                if (this.SelectedIndex >= 0 && this.Models != null && this.Models.Any())
                {
                    var model = this.Models[this.SelectedIndex];
                    model.IsSelected = true;
                }

                this.OnSelectedIndexChanged(this.SelectedIndex);
            }
        }

        /// <summary>
        /// Called when there is a change in the collection of selected indices.
        /// </summary>
        /// <param name="selectedIndex">The new selected index.</param>
        protected virtual void OnSelectedIndexChanged(int selectedIndex)
        {
            this.SelectedIndexChangedCommand?.Execute(selectedIndex);
            this.SelectedIndexChanged?.Invoke(this, new SelectedIndexChangedEventArgs(selectedIndex));
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
                _selectedModel.IsSelected = true;
            }
        }
    }
}