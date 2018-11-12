using Xamarin.Forms;
using XF.Material.Forms.Utilities;

namespace XF.Material.Forms.UI.Internals
{
    internal class MaterialSelectionControlModel : PropertyChangeAware
    {
        private int _index;
        public int Index
        {
            get { return _index; }
            set { Set(ref _index, value); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { this.Set(ref _text, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { this.Set(ref _isSelected, value); }
        }

        private double _horizontalSpacing;
        public double HorizontalSpacing
        {
            get { return _horizontalSpacing; }
            set { this.Set(ref _horizontalSpacing, value); }
        }

        private Command<bool> _selectedChangeCommand;
        public Command<bool> SelectedChangeCommand
        {
            get { return _selectedChangeCommand; }
            set { this.Set(ref _selectedChangeCommand, value); }
        }

        private string _fontFamily;
        public string FontFamily
        {
            get => _fontFamily;
            set => this.Set(ref _fontFamily, value);
        }

        private double _fontSize;
        public double FontSize
        {
            get => _fontSize;
            set => this.Set(ref _fontSize, value);
        }

        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set => this.Set(ref _selectedColor, value);
        }

        private Color _unselectedColor;
        public Color UnselectedColor
        {
            get => _unselectedColor;
            set => this.Set(ref _unselectedColor, value);
        }

        private Color _textColor;
        public Color TextColor
        {
            get => _textColor;
            set => this.Set(ref _textColor, value);
        }

        private double _verticalSpacing;
        public double VerticalSpacing
        {
            get => _verticalSpacing;
            set => this.Set(ref _verticalSpacing, value);
        }

        private bool _canBeUnselected = true;
        public bool CanBeUnselected
        {
            get => _canBeUnselected;
            set => this.Set(ref _canBeUnselected, value);
        }
    }
}
