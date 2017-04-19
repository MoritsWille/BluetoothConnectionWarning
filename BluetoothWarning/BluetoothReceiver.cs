using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Bluetooth;

namespace BluetoothWarning
{
    class BluetoothReceiver : BroadcastReceiver
    {
        public List<BluetoothDevice> Devices = new List<BluetoothDevice>();
        Context appContext;

        public BluetoothReceiver(Context context)
        {
            appContext = context;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;

            if (action == BluetoothAdapter.ActionDiscoveryStarted)
            {
                Toast.MakeText(appContext, "Started searching", ToastLength.Short).Show();
            }
            else if (action == BluetoothAdapter.ActionDiscoveryFinished)
            {
                Toast.MakeText(appContext, "Finished searching", ToastLength.Short).Show();
            }
            else if (action == BluetoothDevice.ActionFound)
            {
                //bluetooth device found
                BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

                Toast.MakeText(appContext, device.Name, ToastLength.Long).Show();
                
                Devices.Add(device);
            }
        }
    }
}