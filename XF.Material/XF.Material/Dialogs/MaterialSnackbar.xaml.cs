using System;
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
        private Command _primaryActionTapCommand;

        internal MaterialSnackbar()
        {
            InitializeComponent();
        }

        internal MaterialSnackbar(string message, int msDuration)
        {
            InitializeComponent();
            Message.Text = message;
            this.Duration = msDuration;
            ActionButton.IsVisible = false;
        }

        internal MaterialSnackbar(string message, int msDuration, Action hideAction)
        {
            InitializeComponent();
            Message.Text = message;
            this.Duration = msDuration;
            ActionButton.IsVisible = false;
            _hideAction = hideAction;
        }

        internal MaterialSnackbar(string message, string actionButtonText, Action primaryAction = null, Action hideAction = null, int msDuration = 3000)
        {
            InitializeComponent();
            Message.Text = message;
            this.Duration = msDuration;
            ActionButton.Text = actionButtonText;
            _primaryActionTapCommand = new Command(() => this.RunPrimaryAction(primaryAction), () => !_primaryActionRunning);
            ActionButton.Command = _primaryActionTapCommand;
            _hideAction = hideAction;
        }

        public int Duration { get; set; }

        public override void Dispose()
        {
            ActionButton.GestureRecognizers.Clear();
            base.Dispose();
        }

        internal static async Task<IMaterialModalPage> Loading(string message)
        {
            var snackbar = new MaterialSnackbar(message, DURATION_INDEFINITE);
            await snackbar.ShowAsync();

            return snackbar;
        }

        internal static async Task ShowAsync(string message, int msDuration = 3000)
        {
            var snackbar = new MaterialSnackbar(message, msDuration);
            await snackbar.ShowAsync();
        }

        internal static async Task ShowAsync(string message, string actionButtonText, Action primaryAction = null, Action hideAction = null, int msDuration = 3000)
        {
            var snackbar = new MaterialSnackbar(message, actionButtonText, primaryAction, hideAction, msDuration);
            await snackbar.ShowAsync();
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