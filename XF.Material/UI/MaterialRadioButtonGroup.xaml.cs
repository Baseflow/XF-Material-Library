using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Xaml;
using XF.Material.Maui.UI.Internals;

namespace XF.Material.Maui.UI
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
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialRadioButtonGroup"/>.
        /// </summary>
        /// <param name="choices">The list of string which the user will choose from.</param>
        public MaterialRadioButtonGroup(IList<string> choices)
        {
            InitializeComponent();
            Choices = choices;
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
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that wil run if there is a change in the control's selected index.
        /// </summary>
        public Command<int> SelectedIndexChangedCommand
        {
            get => (Command<int>)GetValue(SelectedIndexChangedCommandProperty);
            set => SetValue(SelectedIndexChangedCommandProperty, value);
        }

        internal override ObservableCollection<MaterialSelectionControlModel> Models => selectionList.GetValue(BindableLayout.ItemsSourceProperty) as ObservableCollection<MaterialSelectionControlModel>;

        protected override void CreateChoices()
        {
            var models = new ObservableCollection<MaterialSelectionControlModel>();

            for (var i = 0; i < Choices.Count; i++)
            {
                var model = new MaterialSelectionControlModel
                {
                    Index = i,
                    Text = Choices[i]
                };
                model.SelectedChangeCommand = new Command<bool>((isSelected) => RadioButtonSelected(isSelected, model));

                models.Add(model);
            }

            selectionList.SetValue(BindableLayout.ItemsSourceProperty, models);
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (Math.Abs(widthConstraint * heightConstraint) > float.MinValue && SelectedIndex >= 0)
            {
                _selectedModel = Models[SelectedIndex];
            }

            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName != nameof(SelectedIndex))
            {
                return;
            }

            if (SelectedIndex >= 0 && Models?.Any() == true)
            {
                for (var i = 0; i < Models.Count; i++)
                {
                    Models[i].IsSelected = i == SelectedIndex;
                }
            }

            OnSelectedIndexChanged(SelectedIndex);
        }

        /// <summary>
        /// Called when there is a change in the collection of selected indices.
        /// </summary>
        /// <param name="selectedIndex">The new selected index.</param>
        protected virtual void OnSelectedIndexChanged(int selectedIndex)
        {
            SelectedIndexChangedCommand?.Execute(selectedIndex);
            SelectedIndexChanged?.Invoke(this, new SelectedIndexChangedEventArgs(selectedIndex));
        }

        private void RadioButtonSelected(bool isSelected, MaterialSelectionControlModel model)
        {
            if (isSelected)
            {
                SelectedIndex = model.Index;

                if (_selectedModel == model)
                {
                    return;
                }

                if (_selectedModel != null)
                {
                    _selectedModel.IsSelected = false;
                }

                _selectedModel = model;
            }
            else if (_selectedModel.Index == model.Index)
            {
                _selectedModel.IsSelected = true;
            }
        }
    }
}
