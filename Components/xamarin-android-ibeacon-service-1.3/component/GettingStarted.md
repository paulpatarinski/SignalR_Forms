Xamarin.Android iBeacon Service
===============================

# Reference Application

For a working example of Monitoring and Ranging iBeacons with Xamarin.Android, check out the [Find The Monkey - Android](https://github.com/chrisriesgo/FindTheMonkey-Android) reference application.

## Monitoring Example Code

	public class MainActivity : Activity, IBeaconConsumer
	{
		private const string UUID = "e4C8A4FCF68B470D959F29382AF72CE7";
		private const string monkeyId = "Monkey";

		View _view;
		IBeaconManager _iBeaconManager;
		MonitorNotifier _monitorNotifier;
		Region _monitoringRegion;

		public MainActivity()
		{
			_iBeaconManager = IBeaconManager.GetInstanceForApplication(this);

			_monitorNotifier = new MonitorNotifier();
			_monitoringRegion = new Region(monkeyId, UUID, null, null);
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);			

			_view = FindViewById<RelativeLayout>(Resource.Id.findTheMonkeyView);
			_text = FindViewById<TextView>(Resource.Id.monkeyStatusLabel);

			_iBeaconManager.Bind(this);

			_monitorNotifier.EnterRegionComplete += EnteredRegion;
			_monitorNotifier.ExitRegionComplete += ExitedRegion;
		}
		
		public void OnIBeaconServiceConnect()
		{
			_iBeaconManager.SetMonitorNotifier(_monitorNotifier);
			_iBeaconManager.StartMonitoringBeaconsInRegion(_monitoringRegion);
		}

		void EnteredRegion(object sender, MonitorEventArgs e)
		{
			RunOnUiThread(() => Toast.MakeText(this, "Beacons are visible", ToastLength.Short).Show());
		}

		void ExitedRegion(object sender, MonitorEventArgs e)
		{
			RunOnUiThread(() => Toast.MakeText(this, "No beacons visible", ToastLength.Short).Show());
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			_monitorNotifier.EnterRegionComplete -= EnteredRegion;
			_monitorNotifier.ExitRegionComplete -= ExitedRegion;

			_iBeaconManager.StopMonitoringBeaconsInRegion(_monitoringRegion);
			_iBeaconManager.UnBind(this);
		}
	}



## Ranging Example Code

	public class MainActivity : Activity, IBeaconConsumer
	{
		private const string UUID = "e4C8A4FCF68B470D959F29382AF72CE7";
		private const string monkeyId = "Monkey";

		View _view;
		IBeaconManager _iBeaconManager;
		RangeNotifier _rangeNotifier;
		Region _rangingRegion;
		TextView _text;

		int _previousProximity;

		public MainActivity()
		{
			_iBeaconManager = IBeaconManager.GetInstanceForApplication(this);

			_rangeNotifier = new RangeNotifier();
			_rangingRegion = new Region(monkeyId, UUID, null, null);
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);

			_view = FindViewById<RelativeLayout>(Resource.Id.findTheMonkeyView);
			_text = FindViewById<TextView>(Resource.Id.monkeyStatusLabel);

			_iBeaconManager.Bind(this);

			_rangeNotifier.DidRangeBeaconsInRegionComplete += RangingBeaconsInRegion;
		}

		public void OnIBeaconServiceConnect()
		{
			_iBeaconManager.SetRangeNotifier(_rangeNotifier);
			_iBeaconManager.StartRangingBeaconsInRegion(_rangingRegion);
		}

		void RangingBeaconsInRegion(object sender, RangeEventArgs e)
		{
			if (e.Beacons.Count > 0)
			{
				var beacon = e.Beacons.FirstOrDefault();

				switch((ProximityType)beacon.Proximity)
				{
					case ProximityType.Immediate:
						RunOnUiThread(() => Toast.MakeText(this, "You found the monkey!", ToastLength.Short).Show());
						break;
					case ProximityType.Near:
					RunOnUiThread(() => Toast.MakeText(this, "You're getting warmer", ToastLength.Short).Show());
						break;
					case ProximityType.Far:
						RunOnUiThread(() => Toast.MakeText(this, "You're freezing cold", ToastLength.Short).Show());
						break;
					case ProximityType.Unknown:
						RunOnUiThread(() => Toast.MakeText(this, "I'm not sure how close you are to the monkey", ToastLength.Short).Show());
						break;
				}

				_previousProximity = beacon.Proximity;
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			_rangeNotifier.DidRangeBeaconsInRegionComplete -= RangingBeaconsInRegion;

			_iBeaconManager.StopRangingBeaconsInRegion(_rangingRegion);
			_iBeaconManager.UnBind(this);
		}
	}