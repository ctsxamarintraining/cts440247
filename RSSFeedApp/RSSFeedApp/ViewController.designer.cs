// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace RSSFeedApp
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton subscribeButton { get; set; }

		[Outlet]
		UIKit.UITableView table { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint tableHeight { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (subscribeButton != null) {
				subscribeButton.Dispose ();
				subscribeButton = null;
			}

			if (table != null) {
				table.Dispose ();
				table = null;
			}

			if (tableHeight != null) {
				tableHeight.Dispose ();
				tableHeight = null;
			}
		}
	}
}
