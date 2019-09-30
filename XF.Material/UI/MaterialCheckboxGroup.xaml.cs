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
    /// <inheritdoc />
    /// <summary>
    /// A control that allow user to select one or more items from a set.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialCheckboxGroup : BaseMaterialSelectionControlGroup
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="SelectedIndices"/>.
        /// </summary>
        public static readonly BindableProperty SelectedIndicesProperty = BindableProperty.Create(nameof(SelectedIndices), typeof(IList<int>), typeof(MaterialCheckboxGroup), new List<int>(), BindingMode.TwoWay);

        /// <summary>
        /// Backing field for the bindable property <see cref="SelectedIndicesChangedCommand"/>.
        /// </summary>
        public static readonly BindableProperty SelectedIndicesChangedCommandProperty = BindableProperty.Create(nameof(SelectedIndicesChangedCommand), typeof(Command<int[]>), typeof(MaterialCheckboxGroup));

        internal override ObservableCollection<MaterialSelectionControlModel> Models => selectionList.GetValue(BindableLayout.ItemsSourceProperty) as ObservableCollection<MaterialSelectionControlModel>;

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialCheckboxGroup"/>.
        /// </summary>
        public MaterialCheckboxGroup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialCheckboxGroup"/>.
        /// </summary>
        /// <param name="choices">The list of string which the user will choose from.</param>
        public MaterialCheckboxGroup(IList<string> choices)
        {
            InitializeComponent();
            Choices = choices;
        }

        /// <summary>
        /// Raised when there is a change in the collection of selected indices.
        /// </summary>
        public event EventHandler<SelectedIndicesChangedEventArgs> SelectedIndicesChanged;

        /// <summary>
        /// Gets or sets the indices that are selected.
        /// </summary>
        public IList<int> SelectedIndices
        {
            get => (IList<int>)GetValue(SelectedIndicesProperty);
            set => SetValue(SelectedIndicesProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will execute when there is a change in the collection of selected indices.
        /// </summary>
        public Command<int[]> SelectedIndicesChangedCommand
        {
            get => (Command<int[]>)GetValue(SelectedIndicesChangedCommandProperty);
            set => SetValue(SelectedIndicesChangedCommandProperty, value);
        }

        protected override void CreateChoices()
        {
            var models = new ObservableCollection<MaterialSelectionControlModel>();

            for (var i = 0; i < Choices.Count; i++)
            {
                var i1 = i;
                var model = new MaterialSelectionControlModel
                {
                    SelectedChangeCommand = new Command<bool>((isSelected) => CheckboxSelected(isSelected, i1)),
                    Text = Choices[i],
                    HorizontalSpacing = HorizontalSpacing,
                    FontFamily = FontFamily,
                    FontSize = FontSize,
                    SelectedColor = SelectedColor,
                    UnselectedColor = UnselectedColor,
                    TextColor = TextColor,
                    VerticalSpacing = VerticalSpacing
                };

                models.Add(model);
            }

            selectionList.SetValue(BindableLayout.ItemsSourceProperty, models);
        }

        /// <summary>
        /// Called when there is a change in the collection of selected indices.
        /// </summary>
        /// <param name="selectedIndices">The collection of new selected indices.</param>
        protected virtual void OnSelectedIndicesChanged(IList<int> selectedIndices)
        {
            SelectedIndicesChangedCommand?.Execute(selectedIndices.ToArray());
            SelectedIndicesChanged?.Invoke(this, new SelectedIndicesChangedEventArgs(selectedIndices.ToArray()));
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(SelectedIndices))
            {
                OnSelectedIndicesChanged();
            }
        }

        private void OnSelectedIndicesChanged()
        {
            switch (SelectedIndices)
            {
                case null:
                    throw new InvalidOperationException("The property 'SelectedIndices' was assigned with a null value.");
                case Array _:
                    throw new InvalidOperationException("The property 'SelectedIndices' is 'System.Array', please use a collection that has no fixed size");
                default:
                    {
                        if (!SelectedIndices.Any())
                        {
                            foreach (var model in Models)
                            {
                                model.IsSelected = false;
                            }
                        }

                        else
                        {
                            foreach (var index in SelectedIndices)
                            {
                                var model = Models.ElementAt(index);
                                model.IsSelected = true;
                            }
                        }

                        break;
                    }
            }

            OnSelectedIndicesChanged(SelectedIndices);
        }

        private void CheckboxSelected(bool isSelected, int index)
        {
            try
            {
                if (isSelected && SelectedIndices.All(s => s != index))
                {
                    SelectedIndices.Add(index);
                    OnSelectedIndicesChanged(SelectedIndices);
                }

                else if (!isSelected && SelectedIndices.Any(s => s == index))
                {
                    SelectedIndices.Remove(index);
                    OnSelectedIndicesChanged(SelectedIndices);
                }
            }

            catch (NotSupportedException)
            {
                throw new NotSupportedException("Please use a collection type that has no fixed size for the property SelectedIndices");
            }
        }
    }
}
