using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
        private readonly int _duration;
        private readonly Action _hideAction;
        private bool _primaryActionRunning;

        internal MaterialSnackbar(string message, string actionButtonText, int msDuration = DurationLong, MaterialSnackbarConfiguration configuration = null)
        {
            this.InitializeComponent();
            this.Configure(configuration);
            Message.Text = message;
            _duration = msDuration;
            ActionButton.Text = actionButtonText;
            var primaryActionCommand = new Command(async () =>
            {
                _primaryActionRunning = true;
                await this.DismissAsync();
                this.InputTaskCompletionSource?.SetResult(true);
            }, () => !_primaryActionRunning);
            ActionButton.Command = primaryActionCommand;
            _hideAction = () => this.InputTaskCompletionSource?.SetResult(false);
        }

        public TaskCompletionSource<bool> InputTaskCompletionSource { get; set; }

        public override string MessageText
        {
            get { return Message.Text; }
            set { Message.Text = value; }
        }

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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.ChangeLayout();
        }

        protected override async void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();

            if (_duration <= 0) return;
            await Task.Delay(_duration);

            if (_primaryActionRunning) return;
            _hideAction?.Invoke();
            await this.DismissAsync();
        }

        protected override void OnOrientationChanged(DisplayOrientation orientation)
        {
            base.OnOrientationChanged(orientation);

            this.ChangeLayout();
        }

        private void ChangeLayout()
        {
            switch (this.DisplayOrientation)
            {
                case DisplayOrientation.Landscape when Device.Idiom == TargetIdiom.Phone:
                    Container.WidthRequest = 344;
                    Container.HorizontalOptions = LayoutOptions.Center;
                    break;
                case DisplayOrientation.Portrait when Device.Idiom == TargetIdiom.Phone:
                    Container.WidthRequest = -1;
                    Container.HorizontalOptions = LayoutOptions.FillAndExpand;
                    break;
            }
        }

        private void Configure(MaterialSnackbarConfiguration configuration)
        {
            var preferredConfig = configuration ?? GlobalConfiguration;

            if (preferredConfig == null) return;
            Message.FontFamily = preferredConfig.MessageFontFamily;
            Message.TextColor = preferredConfig.MessageTextColor;
            Container.BackgroundColor = preferredConfig.BackgroundColor;
            ActionButton.TextColor = preferredConfig.TintColor;
            ActionButton.FontFamily = preferredConfig.ButtonFontFamily;
            ActionButton.AllCaps = preferredConfig.ButtonAllCaps;
            Container.Margin = preferredConfig.Margin == default ? Material.GetResource<Thickness>("Material.Snackbar.Margin") : preferredConfig.Margin;
        }

    }
}