
using System;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using UIKit;
using System.Drawing;
using CoreGraphics;

namespace RSSFeedApp
{
	public partial class SubscribeToLinksViewController : UIViewController
	{
		List<Hashtable> dbItems;

		public SubscribeToLinksViewController () : base ("SubscribeToLinksViewController", null)
		{
		}

		public SubscribeToLinksViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			dbItems = new List<Hashtable>();
			//Get the data from the DB
			dbItems = Sqlite.getData(false);

			//Subscribed Feed links tableView
			linksTableView.Source = new SubscribeTableView (dbItems);
			linksTableView.Layer.BorderColor = UIColor.FromRGB (3, 151, 215).CGColor;
			linksTableView.Layer.BorderWidth = 1;
			linksTableView.ContentInset = UIEdgeInsets.Zero;//(0, -15, 0, 0);

		}
	}


	public partial class SubscribeTableView: UITableViewSource
	{
		protected string identifier = "SubscribeCell";
		protected List<Hashtable> list;

		//Cosntructor
		public SubscribeTableView (List<Hashtable> list) { 
			this.list = list;
		}

		public static string getValue (Hashtable h) {
			foreach (DictionaryEntry entry in h) {
				if (entry.Key.Equals("link")) {
					return entry.Value.ToString();
				}
			}
			return "";
		}

		public static string getSubscribtionStatus (Hashtable h) {
			foreach (DictionaryEntry entry in h) {
				if (entry.Key.Equals("subscription")) {
					return entry.Value.ToString();
				}
			}
			return "";
		}

		#region UItableView DataSource and Delegates

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return this.list.Count;//rssLinks.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UIButton button = new UIButton();
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell (identifier);
			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, identifier);
				//Add a button
				button = new UIButton (new CGRect (cell.Frame.Size.Width - 60, 10, 90, 30));
				button.BackgroundColor = UIColor.Clear;
				button.SetTitleColor (UIColor.FromRGB (3, 151, 215), UIControlState.Normal);
				button.Layer.CornerRadius = 5;
				button.Layer.BorderColor = UIColor.FromRGB (3, 151, 215).CGColor;
				button.Layer.BorderWidth = 1;
				button.Tag = indexPath.Row;
				button.TouchUpInside += (object sender, EventArgs e) => {
					int n = Int32.Parse(((sender as UIButton).Tag).ToString());
					if (getSubscribtionStatus(list[n]) == "0"){
						Sqlite.updateFeed(getValue(list[n]), 1);
						button.SetTitle ("Following", UIControlState.Normal);
					} else{
						Sqlite.updateFeed(getValue(list[n]), 0);
						button.SetTitle ("Follow", UIControlState.Normal);
					}

					list = Sqlite.getData(false);
				};
				cell.AddSubview (button);

			} else {
				Array subviews = cell.Subviews;
				foreach(UIView v in subviews) {
					if (v.GetType() == typeof(UIButton)) {
						button = v as UIButton;
						button.SetTitle ("", UIControlState.Normal);
						button.Tag = indexPath.Row;
					}
				}
			}
			cell.TextLabel.Text = "LinkFeed " + (indexPath.Row + 1);
			cell.DetailTextLabel.Text = getValue( this.list[indexPath.Row]);

			if(getSubscribtionStatus(list[indexPath.Row]) == "1") {
				button.SetTitle ("Following", UIControlState.Normal);
			} else{
				button.SetTitle ("Follow", UIControlState.Normal);
			}

				
			return cell;
		}
			
		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			UIView headerView = new UIView (new CGRect (20, 0, tableView.Frame.Size.Width - 20, 45));
			headerView.BackgroundColor = UIColor.FromRGB (3, 151, 215);
			UILabel label = new UILabel (new CGRect(10,0,headerView.Frame.Size.Width - 10, headerView.Frame.Size.Height));
			label.Text = "Feeds";
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
			return 70;
		}
		#endregion 

	}
}
		