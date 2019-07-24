using System;
using System.Collections;
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
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MaterialRadioButtonGroup"/>.
        /// </summary>
        /// <param name="choices">The list of string which the user will choose from.</param>
        public MaterialCheckboxGroup(IList choices)
        {
            this.InitializeComponent();
            this.Choices = choices;
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
            get => (IList<int>)this.GetValue(SelectedIndicesProperty);
            set => this.SetValue(SelectedIndicesProperty, value);
        }

        /// <summary>
        /// Gets or sets the command that will execute when there is a change in the collection of selected indices.
        /// </summary>
        public Command<int[]> SelectedIndicesChangedCommand
        {
            get => (Command<int[]>)this.GetValue(SelectedIndicesChangedCommandProperty);
            set => this.SetValue(SelectedIndicesChangedCommandProperty, value);
        }

        protected override void CreateChoices()
        {
            var models = new ObservableCollection<MaterialSelectionControlModel>();
            var listType = this.Choices[0].GetType();

            for (var i = 0; i < this.Choices.Count; i++)
            {
                var i1 = i;

                var choiceString = "";
                if (!string.IsNullOrEmpty(this.ChoicesBindingName))
                {
                    var propInfo = listType.GetProperty(this.ChoicesBindingName);

                    if (propInfo == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Property {this.ChoicesBindingName} was not found for item in {this.Choices}.");
                        choiceString = (string)this.Choices[i];
                    }
                    else
                    {
                        var propValue = propInfo.GetValue(this.Choices[i]);
                        choiceString = propValue.ToString();
                    }
                }
                else
                {
                    choiceString = (string)this.Choices[i];
                }

                var model = new MaterialSelectionControlModel
                {
                    SelectedChangeCommand = new Command<bool>((isSelected) => this.CheckboxSelected(isSelected, i1)),
                    Text = choiceString,
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

            selectionList.SetValue(BindableLayout.ItemsSourceProperty, models);
        }

        /// <summary>
        /// Called when there is a change in the collection of selected indices.
        /// </summary>
        /// <param name="selectedIndices">The collection of new selected indices.</param>
        protected virtual void OnSelectedIndicesChanged(IList<int> selectedIndices)
        {
            this.SelectedIndicesChangedCommand?.Execute(selectedIndices.ToArray());
            this.SelectedIndicesChanged?.Invoke(this, new SelectedIndicesChangedEventArgs(selectedIndices.ToArray()));
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(this.SelectedIndices))
            {
                this.OnSelectedIndicesChanged();
            }
        }

        private void OnSelectedIndicesChanged()
        {
            switch (this.SelectedIndices)
            {
                case null:
                    throw new InvalidOperationException("The property 'SelectedIndices' was assigned with a null value.");
                case Array _:
                    throw new InvalidOperationException("The property 'SelectedIndices' is 'System.Array', please use a collection that has no fixed size");
                default:
                {
                    if (!this.SelectedIndices.Any())
                    {
                        foreach (var model in this.Models)
                        {
                            model.IsSelected = false;
                        }
                    }

                    else
                    {
                        foreach (var index in this.SelectedIndices)
                        {
                            var model = this.Models.ElementAt(index);
                            model.IsSelected = true;
                        }
                    }

                    break;
                }
            }

            this.OnSelectedIndicesChanged(this.SelectedIndices);
        }

        private void CheckboxSelected(bool isSelected, int index)
        {
            try
            {
                if (isSelected && this.SelectedIndices.All(s => s != index))
                {
                    this.SelectedIndices.Add(index);
                    this.OnSelectedIndicesChanged(this.SelectedIndices);
                }

                else if (!isSelected && this.SelectedIndices.Any(s => s == index))
                {
                    this.SelectedIndices.Remove(index);
                    this.OnSelectedIndicesChanged(this.SelectedIndices);
                }
            }

            catch (NotSupportedException)
            {
                throw new NotSupportedException("Please use a collection type that has no fixed size for the property SelectedIndices");
            }
        }
    }
}
