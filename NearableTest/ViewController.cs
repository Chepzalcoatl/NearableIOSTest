using System;
using CoreLocation;
using Estimote;
using UIKit;

namespace EstimoteTest
{
    public partial class ViewController : UIViewController, INearableManagerDelegate
    {
        NearableManager manager;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			
			manager = new NearableManager();

			manager.RangedNearables += (sender, e) =>
			{
				//Create Alert
				var okAlertController = UIAlertController.Create("Nearables Found", "Just found: " + e.Nearables.Length + " nearables.", UIAlertControllerStyle.Alert);
				//Add Action
				okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
				// Present Alert
				PresentViewController(okAlertController, true, null);
			};


        }

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			manager.StartRangingForType(NearableType.All);
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);

			manager.StopRangingForType(NearableType.All);
		}

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void UIButton21_TouchUpInside(UIButton sender)
        {
            manager.StartMonitoringForType(NearableType.All);
        }
    }

	public class MyLocationDelegate : CLLocationManagerDelegate
	{
		public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
		{
			foreach (var loc in locations)
			{
				Console.WriteLine(loc);
			}
		}
	}
}
