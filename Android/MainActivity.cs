using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Core;
using Core.Pages;
using RadiusNetworks.IBeaconAndroid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace AndroidBeacon
{
	[Activity (Label = "Android Beacon", MainLauncher = true)]
	public class MainActivity : AndroidActivity, IBeaconConsumer
	{
		const string UUID = "2F234454-CF6D-4A0F-ADF2-F4911BA9FFA6";
		const string BEACON_ID = "iOSBeacon";
		IBeaconManager beaconMgr;
		MonitorNotifier monitorNotifier;
		RangeNotifier rangeNotifier;
		Region monitoringRegion;
		Region rangingRegion;
		TextView beaconStatusLabel;
	  private MainPage _mainPage;

	  public MainActivity ()
		{
			beaconMgr = IBeaconManager.GetInstanceForApplication (this);

			monitorNotifier = new MonitorNotifier ();
			monitoringRegion = new Region (BEACON_ID, UUID, null, null);

			rangeNotifier = new RangeNotifier ();
			rangingRegion = new Region (BEACON_ID, UUID, null, null);
		}

		public void OnIBeaconServiceConnect ()
		{
			beaconMgr.SetMonitorNotifier (monitorNotifier);
			beaconMgr.SetRangeNotifier (rangeNotifier);

			beaconMgr.StartMonitoringBeaconsInRegion (monitoringRegion);
			beaconMgr.StartRangingBeaconsInRegion (rangingRegion);
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

      Forms.Init(this, bundle);

		  _mainPage = (MainPage) App.GetMainPage();

      SetPage(_mainPage);

			beaconMgr.Bind (this);

			monitorNotifier.EnterRegionComplete += EnteredRegion;
			monitorNotifier.ExitRegionComplete += ExitedRegion;

			rangeNotifier.DidRangeBeaconsInRegionComplete += RangingBeaconsInRegion;
		}

		void EnteredRegion (object sender, MonitorEventArgs e)
		{
			ShowMessage ("Welcome back!");
		}

		void ExitedRegion (object sender, MonitorEventArgs e)
		{
			ShowMessage ("Thanks for shopping here!");
		}

		void RangingBeaconsInRegion (object sender, RangeEventArgs e)
		{
			if (e.Beacons.Count > 0) {
				IBeacon beacon = e.Beacons.FirstOrDefault ();

				switch ((ProximityType)beacon.Proximity) {
				case ProximityType.Immediate:
            ShowMessage("Proximity immediate ");
            break;
        case ProximityType.Near:
            ShowMessage("Proximity near " );
            break;
        case ProximityType.Far:
					ShowMessage ("Proximity far " );
					break;
				case ProximityType.Unknown:
					ShowMessage ("Beacon proximity unknown");
					break;
				}
			}
		}

		void ShowMessage(string message)
		{
		  _mainPage.ShowMessage(message);
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();

			monitorNotifier.EnterRegionComplete -= EnteredRegion;
			monitorNotifier.ExitRegionComplete -= ExitedRegion;

			rangeNotifier.DidRangeBeaconsInRegionComplete -= RangingBeaconsInRegion;

			beaconMgr.StopMonitoringBeaconsInRegion (monitoringRegion);
			beaconMgr.StopRangingBeaconsInRegion (rangingRegion);
			beaconMgr.UnBind (this);
		}
	}
}