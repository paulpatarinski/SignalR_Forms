//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using RadiusNetworks.IBeaconAndroid;

//namespace AndroidBeacon.Services
//{
//  public class BeaconService : IBeaconConsumer
//  {
//    public BeaconService()
//    {
//      beaconMgr = IBeaconManager.GetInstanceForApplication(this);

//    }

//    public void Dispose()
//    {
//      throw new NotImplementedException();
//    }

//    public IntPtr Handle { get; private set; }
//    public bool BindService(Intent intent, IServiceConnection serviceConnection, Bind flags)
//    {
//      throw new NotImplementedException();
//    }

//    public void OnIBeaconServiceConnect()
//    {
//      throw new NotImplementedException();
//    }

//    public Context ApplicationContext { get; private set; }

//    public void UnbindService(IServiceConnection p0)
//    {
//      throw new NotImplementedException();
//    }
//  }
//}