using Android.App;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;
using Android.Content;
using Android.Views;
using Android;

namespace BluetoothWarning
{
    [Activity(Label = "BluetoothWarning", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string deviceName;
        BluetoothAdapter bluetoothAdapter;
        BluetoothReceiver receiver;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            IntentFilter filter = new IntentFilter();
            filter.AddAction(BluetoothDevice.ActionFound);
            filter.AddAction(BluetoothAdapter.ActionDiscoveryStarted);
            filter.AddAction(BluetoothAdapter.ActionDiscoveryFinished);

            receiver = new BluetoothReceiver(this);

            RegisterReceiver(receiver, filter);

            bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            
            TextView dataView = FindViewById<TextView>(Resource.Id.text);
            EditText nameField = FindViewById<EditText>(Resource.Id.name);

            Button refreshButton = FindViewById<Button>(Resource.Id.button);
            refreshButton.Click += (o, e) => 
            {
                if (receiver.Devices.Count == 0)
                {
                    dataView.Text = "No data yet";
                }
                else
                {
                    dataView.Text = "";
                    foreach (BluetoothDevice device in receiver.Devices)
                    {
                        dataView.Text += device.Name + "\n";
                    }
                }
            };

            Button submitButton = FindViewById<Button>(Resource.Id.submitButton);
            submitButton.Click += (o, e) =>
            {
                deviceName = nameField.Text;
                if(deviceName.Length > 0)
                {
                    //start background intent here
                }
                else
                {
                    Toast.MakeText(this, "Please write a device name", ToastLength.Short);
                }
            };
            
            bluetoothAdapter.StartDiscovery();
        }

        protected override void OnDestroy()
        {
            UnregisterReceiver(receiver);

            base.OnDestroy();
        }

        void DeviceLost(string deviceName)
        {
        }
    }

}

