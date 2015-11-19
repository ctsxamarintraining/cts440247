using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using CoreGraphics;
using System.Collections;
using System.Drawing;

namespace RSSFeedApp
{
	public partial class ViewController : UIViewController
	{
		public NSIndexPath path;
		List<Hashtable> dbItems;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			dbItems = new List<Hashtable>();

			//Get the data from the DB
			Sqlite.GetConnection();

			//Subscribed Feed links tableView
//			table.Source = new TableView(this, dbItems);
			table.Layer.BorderColor = UIColor.FromRGB (3, 151, 215).CGColor;
			table.Layer.BorderWidth = 1;
			table.ContentInset = UIEdgeInsets.Zero;//(0, -15, 0, 0);

			//Subscribe to more links
			subscribeButton.Layer.CornerRadius = 5;
			subscribeButton.TouchUpInside += (object sender, EventArgs e) => {
				PerformSegue("subscribeSegue", this);
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			dbItems = Sqlite.getData (true);
			Console.WriteLine ("frame: " + table.Frame.Size.Height + " constraint: " + tableHeight.Constant);
			tableHeight.Constant = (table.Frame.Size.Height - (dbItems.Count * 55) > 0) ?  (table.Frame.Size.Height - (dbItems.Count * 55)) + 25 : tableHeight.Constant;
			Console.WriteLine ("frame: " + table.Frame.Size.Height + "constraint: " + tableHeight.Constant);
			table.Source = new TableView(this, dbItems);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "feedSegue") {
				Console.WriteLine (segue.Description);
				FeedViewController f = segue.DestinationViewController as FeedViewController;
				f._url = TableView.getValue(dbItems[path.Row]);	
			}
		}

	}

	public partial class TableView: UITableViewSource
	{
		protected string cellIdentifier = "Cell";
		protected List<Hashtable> _list;

		private ViewController _controller;
		//Cosntructor
		public TableView (ViewController controller, List<Hashtable> list) { 
			_controller = controller;
			_list = list;
		}

		public static string getValue (Hashtable h) {
			foreach (DictionaryEntry entry in h) {
				if (entry.Key.Equals("link")) {
					return entry.Value.ToString();
				}
			}
			return "";
		}

		#region UItableView DataSource and Delegates

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _list.Count;//rssLinks.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, cellIdentifier);
			cell.TextLabel.Text = "LinkFeed " + (indexPath.Row + 1);
			cell.DetailTextLabel.Text = getValue( _list[indexPath.Row]);
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{

			UIAlertView alert = new UIAlertView () { 
				Title = "Feed subscribed", Message = getValue(_list[indexPath.Row])
			};
			alert.AddButton("OK");
			alert.Show ();
			_controller.path = indexPath;
			_controller.PerformSegue ("feedSegue", _controller);

		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			UIView headerView = new UIView (new CGRect (20, 0, tableView.Frame.Size.Width - 20, 45));
			headerView.BackgroundColor = UIColor.FromRGB (3, 151, 215);
			UILabel label = new UILabel (new CGRect(10,0,headerView.Frame.Size.Width - 10, headerView.Frame.Size.Height));
			label.Text = "Following feeds";
			label.TextColor = UIColor.White;
			headerView.AddSubview (label);
			return headerView;
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			return 45;
		}


		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 55;
		}
		#endregion 

	}
}

