using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MaterialSnackbar : BaseMaterialModalPage
	{
        public const int DURATION_INDEFINITE = -1;
        private Action _hideAction;
        private bool _primaryActionRunning;
        private TapGestureRecognizer _primaryActionTap;
        private Command _primaryActionTapCommand;

        public MaterialSnackbar()
        {
            InitializeComponent();
        }

        public MaterialSnackbar(string message, int msDuration)
        {
            InitializeComponent();
            Message.Text = message;
            this.Duration = msDuration;
            ActionButton.IsVisible = false;
        }

        public MaterialSnackbar(string message, int msDuration, Action hideAction)
        {
            InitializeComponent();
            Message.Text = message;
            this.Duration = msDuration;
            ActionButton.IsVisible = false;
            _hideAction = hideAction;
        }

        public MaterialSnackbar(string message, string actionButtonText, Action primaryAction = null, Action hideAction = null, int msDuration = 3000)
        {
            InitializeComponent();
            Message.Text = message;
            this.Duration = msDuration;
            ActionButtonLabel.Text = actionButtonText.ToUpper();
            _primaryActionTapCommand = new Command(() => this.RunPrimaryAction(primaryAction), () => !_primaryActionRunning);
            _primaryActionTap = new TapGestureRecognizer { Command = _primaryActionTapCommand, NumberOfTapsRequired = 1 };
            ActionButton.GestureRecognizers.Add(_primaryActionTap);
            _hideAction = hideAction;
        }

        public int Duration { get; set; }

        public static async Task<IMaterialModalPage> Loading(string message)
        {
            var snackbar = new MaterialSnackbar(message, DURATION_INDEFINITE);
            await snackbar.ShowAsync();

            return snackbar;
        }

        public static async Task ShowAsync(string message, int msDuration = 3000)
        {
            var snackbar = new MaterialSnackbar(message, msDuration);
            await snackbar.ShowAsync();
        }

        public static async Task ShowAsync(string message, string actionButtonText, Action primaryAction = null, Action hideAction = null, int msDuration = 3000)
        {
            var snackbar = new MaterialSnackbar(message, actionButtonText, primaryAction, hideAction, msDuration);
            await snackbar.ShowAsync();
        }

        public override void Dispose()
        {
            ActionButton.GestureRecognizers.Clear();
            base.Dispose();
        }

        protected override async void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();

            if (this.Duration > 0)
            {
                await BeginDuration();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async Task BeginDuration()
        {
            await Task.Delay(this.Duration);

            if (!_primaryActionRunning)
            {
                _hideAction?.Invoke();
                this.Dispose();
            }
        }

        private void RunPrimaryAction(Action action = null)
        {
            _primaryActionRunning = true;
            action?.Invoke();
            this.Dispose();
        }
    }
}