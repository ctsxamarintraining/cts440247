
using System;

using Foundation;
using UIKit;

namespace RSSFeedApp
{
	public partial class FeedViewController : UIViewController
	{
		public string _url;
		public FeedViewController () : base ("FeedViewController", null)
		{
		}

		public FeedViewController (IntPtr handle) : base (handle)
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
			webView.LoadRequest(new NSUrlRequest(new NSUrl(_url)));
		}
	}

//	public partial class FeedWebView : UIWebView 
//	{
//		public FeedWebView () 
//		{
//		}
//	
//		public override void LoadRequest (NSUrlRequest r)
//		{
//
//		}
//
//	}
}

