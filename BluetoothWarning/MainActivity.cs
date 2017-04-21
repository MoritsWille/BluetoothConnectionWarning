using Android.App;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;
using Android.Content;
using Android.Views;
using Android;
using Java.Lang;
using System.Threading;
using System.Threading.Tasks;

namespace BluetoothWarning
{
    [Activity(Label = "BluetoothWarning", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string deviceName;
        BluetoothAdapter bluetoothAdapter;
        BluetoothReceiver receiver;
        public string deviceName = "";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            IntentFilter filter = new IntentFilter();
            filter.AddAction(BluetoothDevice.ActionFound);
            filter.AddAction(BluetoothAdapter.ActionDiscoveryStarted);
            filter.AddAction(BluetoothAdapter.ActionDiscoveryFinished);

            receiver = new BluetoothReceiver();

            RegisterReceiver(receiver, filter);

            bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            
            TextView dataView = FindViewById<TextView>(Resource.Id.text);
            EditText nameField = FindViewById<EditText>(Resource.Id.name);

<<<<<<< HEAD
            Button refreshButton = FindViewById<Button>(Resource.Id.button);
            refreshButton.Click += (o, e) => 
=======
            refreshButton = FindViewById<Button>(Resource.Id.button);
            Button submitButton = FindViewById<Button>(Resource.Id.submitButton);
            EditText nameView = FindViewById<EditText>(Resource.Id.name);
            dataView = FindViewById<TextView>(Resource.Id.text);

            submitButton.Click += (o, e) =>
            {
                deviceName = nameView.Text;
                
                receiver.deviceName = deviceName;

                bluetoothAdapter.StartDiscovery();
            };

            refreshButton.Click += (o, e) =>
>>>>>>> 054a962f5e3a2710265478f2efdfe7cb9d9272be
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

<<<<<<< HEAD
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
=======
            StartUpdate();
>>>>>>> 054a962f5e3a2710265478f2efdfe7cb9d9272be
        }

        protected override void OnDestroy()
        {
            UnregisterReceiver(receiver);
            StopUpdate();

            base.OnDestroy();
        }

        private CancellationTokenSource cts;
        public void StartUpdate()
        {
            if (cts != null) cts.Cancel();
            cts = new CancellationTokenSource();
            var ignore = UpdaterAsync(cts.Token);
        }

        public void StopUpdate()
        {
            if (cts != null) cts.Cancel();
            cts = null;
        }

        public async Task UpdaterAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                if (!bluetoothAdapter.IsDiscovering)
                {
                    bluetoothAdapter.StartDiscovery();
                }
                await Task.Delay(10000, ct);
            }
        }
    }

}

