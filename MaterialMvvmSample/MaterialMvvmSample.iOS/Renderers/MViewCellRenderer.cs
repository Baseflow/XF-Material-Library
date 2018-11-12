using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MaterialMvvmSample.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

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