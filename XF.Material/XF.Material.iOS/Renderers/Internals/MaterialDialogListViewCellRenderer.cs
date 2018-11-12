using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Material.Forms.UI.Internals;
using XF.Material.iOS.Renderers.Internals;

[assembly: ExportRenderer(typeof(MaterialDialogListViewCell), typeof(MaterialDialogListViewCellRenderer))]
namespace XF.Material.iOS.Renderers.Internals
{
    internal class MaterialDialogListViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectedBackgroundView = new UIView { BackgroundColor = UIColor.Clear, Bounds = cell.Bounds, Frame = cell.Frame };

            return cell;
        }
    }
}