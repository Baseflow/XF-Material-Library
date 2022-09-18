using MaterialMvvmSample.iOS.Renderers;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(ViewCell), typeof(MViewCellRenderer))]
namespace MaterialMvvmSample.iOS.Renderers
{
    public class MViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;

            return cell;
        }
    }
}
