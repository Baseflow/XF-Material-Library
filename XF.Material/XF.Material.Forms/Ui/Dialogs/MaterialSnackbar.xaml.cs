using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace XF.Material.Forms.UI.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialSnackbar : BaseMaterialModalPage, IMaterialAwaitableDialog<bool>
    {
        public const int DurationIndefinite = -1;
        public const int DurationLong = 2750;
        public const int DurationShort = 1500;
        private int _duration;
        private Action _hideAction;
        private Command _primaryActionCommand;
        private bool _primaryActionRunning;

        internal MaterialSnackbar(string message, string actionButtonText, int msDuration = DurationLong, MaterialSnackbarConfiguration configuration = null)
        {
            this.InitializeComponent();
            this.Configure(configuration);
            Message.Text = message;
            _duration = msDuration;
            ActionButton.Text = actionButtonText;
            _primaryActionCommand = new Command(async() =>
            {
                _primaryActionRunning = true;
                await this.DismissAsync();
                this.InputTaskCompletionSource?.SetResult(true);
            }, () => !_primaryActionRunning);
            ActionButton.Command = _primaryActionCommand;
            _hideAction = () => this.InputTaskCompletionSource?.SetResult(false);
        }

        public TaskCompletionSource<bool> InputTaskCompletionSource { get; set; }

        public override bool Dismissable => false;

        internal static MaterialSnackbarConfiguration GlobalConfiguration { get; set; }

        internal static async Task<IMaterialModalPage> Loading(string message, MaterialSnackbarConfiguration configuration = null)
        {
            var snackbar = new MaterialSnackbar(message, null, DurationIndefinite, configuration);
            await snackbar.ShowAsync();

            return snackbar;
        }

        internal static async Task ShowAsync(string message, int msDuration = 3000, MaterialSnackbarConfiguration configuration = null)
        {
            var snackbar = new MaterialSnackbar(message, null, msDuration, configuration);
            await snackbar.ShowAsync();
        }

        internal static async Task<bool> ShowAsync(string message, string actionButtonText, int msDuration = DurationLong, MaterialSnackbarConfiguration configuration = null)
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
                    await this.DismissAsync();
                }
            }
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
                Container.Margin = new Thickness(8, 0, 8, preferredConfig.BottomOffset);
            }
        }

        public override void SetMessageText(string text)
        {
            Message.Text = text;
        }

    }
}