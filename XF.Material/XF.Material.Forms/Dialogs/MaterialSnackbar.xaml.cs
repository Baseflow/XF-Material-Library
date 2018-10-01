using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Dialogs.Configurations;

namespace XF.Material.Forms.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MaterialSnackbar : BaseMaterialModalPage, IMaterialAwaitableDialog<bool>
	{
        public const int DURATION_INDEFINITE = -1;
        public const int DURATION_LONG = 2750;
        public const int DURATION_SHORT = 1500;
        private int _duration;
        private Action _hideAction;
        private Command _primaryActionCommand;
        private bool _primaryActionRunning;

        internal MaterialSnackbar(string message, string actionButtonText, int msDuration = DURATION_LONG, MaterialSnackbarConfiguration configuration = null)
        {
            this.InitializeComponent();
            this.Configure(configuration);
            Message.Text = message;
            _duration = msDuration;
            ActionButton.Text = actionButtonText;
            _primaryActionCommand = new Command(() =>
            {
                _primaryActionRunning = true;
                this.InputTaskCompletionSource?.SetResult(true);
                this.Dispose();
            }, () => !_primaryActionRunning);
            ActionButton.Command = _primaryActionCommand;
            _hideAction = () => this.InputTaskCompletionSource?.SetResult(false);
        }

        internal static MaterialSnackbarConfiguration GlobalConfiguration { get; set; }
        public TaskCompletionSource<bool> InputTaskCompletionSource { get; set; }

        internal static async Task<MaterialSnackbar> Loading(string message, MaterialSnackbarConfiguration configuration = null)
        {
            var snackbar = new MaterialSnackbar(message, null, DURATION_INDEFINITE, configuration);
            await snackbar.ShowAsync();

            return snackbar;
        }

        internal static async Task ShowAsync(string message, int msDuration = 3000, MaterialSnackbarConfiguration configuration = null)
        {
            var snackbar = new MaterialSnackbar(message, null, msDuration, configuration);
            await snackbar.ShowAsync();
        }

        internal static async Task<bool> ShowAsync(string message, string actionButtonText, int msDuration = DURATION_LONG, MaterialSnackbarConfiguration configuration = null)
        {
            var snackbar = new MaterialSnackbar(message, actionButtonText, msDuration, configuration)
            {
                InputTaskCompletionSource = new TaskCompletionSource<bool>()
            };
            await snackbar.ShowAsync();

            return await snackbar.InputTaskCompletionSource.Task;
        }

        protected override async void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();

            if (_duration > 0)
            {
                await Task.Delay(_duration);

                if (!_primaryActionRunning)
                {
                    _hideAction?.Invoke();
                    this.Dispose();
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void Configure(MaterialSnackbarConfiguration configuration)
        {
            var preferredConfig = configuration ?? GlobalConfiguration;

            if (preferredConfig != null)
            {
                Message.FontFamily = preferredConfig.MessageFontFamily;
                Message.TextColor = preferredConfig.MessageTextColor;
                Container.BackgroundColor = preferredConfig.BackgroundColor;
                ActionButton.TextColor = preferredConfig.TintColor;
                ActionButton.FontFamily = preferredConfig.ButtonFontFamily;
                ActionButton.AllCaps = preferredConfig.ButtonAllCaps;
            }
        }
    }
}