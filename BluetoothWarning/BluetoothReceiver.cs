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
        public string deviceName = "";
        bool foundDevice = false;

        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;

            if (action == BluetoothAdapter.ActionDiscoveryStarted)
            {
                Toast.MakeText(context, "Started searching", ToastLength.Short).Show();
            }
            else if (action == BluetoothAdapter.ActionDiscoveryFinished)
            {
                Toast.MakeText(context, "Finished searching", ToastLength.Short).Show();

                if (deviceName == "")
                {
                    foundDevice = true;
                }

                if (!foundDevice)
                {
                    // Instantiate the builder and set notification elements:
                    Notification.Builder builder = new Notification.Builder(context)
                        .SetContentTitle("Device lost")
                        .SetContentText("Can no longer find device " + deviceName + "!")
                        .SetSmallIcon(Resource.Drawable.ic_hearing_black_24dp)
                        .SetVibrate(new long[] { 1000, 1000, 1000, 1000, 1000 });

                    // Build the notification:
                    Notification notification = builder.Build();

                    // Get the notification manager:
                    NotificationManager notificationManager =
                        context.GetSystemService(Context.NotificationService) as NotificationManager;

                    // Publish the notification:
                    const int notificationId = 0;
                    notificationManager.Notify(notificationId, notification);
                }

                foundDevice = false;
            }
            else if (action == BluetoothDevice.ActionFound)
            {
                //bluetooth device found
                BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

                Toast.MakeText(context, device.Name, ToastLength.Long).Show();
                
                Devices.Add(device);

                if(device.Name == deviceName)
                {
                    foundDevice = true;
                }
            }
        }
    }
}