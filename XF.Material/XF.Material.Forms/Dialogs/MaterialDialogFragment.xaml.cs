using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Forms.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDialogFragment : BaseMaterialModalPage, IMaterialAwaitableDialog<bool>
    {
        internal MaterialDialogFragment()
        {
            this.InitializeComponent();
            PositiveButton.Command = new Command(() =>
            {
                this.InputTaskCompletionSource?.SetResult(true);
                this.Dispose();
            });
            NegativeButton.Command = new Command(() =>
            {
                this.InputTaskCompletionSource?.SetResult(false);
                this.Dispose();
            });
        }

        public TaskCompletionSource<bool> InputTaskCompletionSource { get; set; }

        public static async Task<bool> Show(View content, string title = null, string confirmingText = "Ok", string dismissiveText = "Cancel")
        {
            var dialog = new MaterialDialogFragment()
            {
                InputTaskCompletionSource = new TaskCompletionSource<bool>()
            };

            dialog.DialogTitle.Text = title;
            dialog.ContentContainer.Content = content;
            dialog.PositiveButton.Text = confirmingText;
            dialog.NegativeButton.Text = dismissiveText;

            await dialog.ShowAsync();

            return await dialog.InputTaskCompletionSource.Task;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            ContentContainer.Content = null;
        }

        protected override bool OnBackButtonPressed()
        {
            this.InputTaskCompletionSource?.SetResult(false);

            return base.OnBackButtonPressed();
        }

        protected override bool OnBackgroundClicked()
        {
            this.InputTaskCompletionSource?.SetResult(false);

            return base.OnBackgroundClicked();
        }
    }
}