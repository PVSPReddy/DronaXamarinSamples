using System;

using Xamarin.Forms;
using BluetoothExample.Droid;
using System.Threading.Tasks;
using Android.Bluetooth;
using Android.Content.PM;
using Android.Content;
using Android.OS;
using Android.Bluetooth.LE;
using Android.Widget;
using Android.App;
using Android.Support.V4.Content;

[assembly: Dependency(typeof(BluetoothServices))]
namespace BluetoothExample.Droid
{
    [Activity(Label = "BluetoothServices")]
    public class BluetoothServices : BluetoothAdapter.ILeScanCallback, IBluetooth
    {
        public BluetoothServices()
        {
            receiver = new SampleReceiver();
        }

        public IntPtr Handle
        {
            get;
            //{
            //throw new NotImplementedException();
            //}
        }


        private BluetoothManager bluetoothManager;
        private BluetoothAdapter bluetoothAdapter;
        Activity blueActivity;
        private Activity CurrentActivity;

        SampleReceiver receiver;
        bool isBlueToothRunning, isStartedBluetooth;


        public async Task<bool> StartBluetoothService(bool shallStartBluetooth)
        {
            try
            {
                //blueActivity = new Activity();
                //Intent blueToothActivity = new Intent(Forms.Context, typeof(SampleActivity));
                //blueActivity.StartActivityForResult(blueToothActivity, 1);
                //BluetoothAdapter.ILeScanCallback lescanback;


                var appContext = Android.App.Application.Context;
                bluetoothManager = (BluetoothManager)appContext.GetSystemService("bluetooth");
                bluetoothAdapter = bluetoothManager.Adapter;
                var bluetoothState = bluetoothAdapter.State;
                //if (bluetoothState == Android.Bluetooth.BluetoothAdapter.State.Off)
                //{

                //    bluetoothAdapter.Enable();
                //    bluetoothAdapter.StartDiscovery();
                //}
                //else
                //{
                //    //BluetoothAdapter.DefaultAdapter.BluetoothLeScanner.StartSca;
                //    bluetoothAdapter.StartDiscovery();
                //}
                /*
                var scanner = bluetoothAdapter.StartLeScan(this);
                var pairedDevices = bluetoothAdapter.BondedDevices;
                */

                //bluetoothAdapter.BluetoothLeScanner.


                //receiver = new SampleReceiver();
                IntentFilter filter = new IntentFilter(BluetoothDevice.ActionFound);
                CurrentActivity = (Activity)Forms.Context;
                //IntentFilter filter = new IntentFilter("testing");
                filter.AddAction(BluetoothAdapter.ActionDiscoveryFinished);
                filter.AddAction(BluetoothAdapter.ActionDiscoveryStarted);
                //filter.AddAction(BluetoothAdapter.ActionDiscoveryStarted);
                filter.AddAction(BluetoothAdapter.ActionDiscoveryFinished);
                //LocalBroadcastManager.GetInstance(this).RegisterReceiver(receiver, filter);
                //LocalBroadcastManager.GetInstance(this).RegisterReceiver(receiver, filter);
                CurrentActivity.RegisterReceiver(receiver, filter);




                //Android.Bluetooth.LE.ScanCallback callback = new Android.Bluetooth.LE.ScanCallback();
                //scanner.StartScan(callback);
                //BluetoothGattCallback callBack = new BluetoothGattCallback(this);
                //if (Build.VERSION.SdkInt >= Build.VERSION_CODES.JellyBeanMr2)
                //{
                //    bluetoothAdapter.Enable();
                //    //var data = bluetoothAdapter.StartLeScan(this);
                //}
                //else
                //{
                //    bluetoothAdapter.Enable();
                //}
                //this._gattCallback = new GattCallback(this);

                isStartedBluetooth = true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                isStartedBluetooth = false;
            }
            return isStartedBluetooth;
        }

        public async Task<bool> StopBluetoothService(bool shallStartBluetooth)
        {
            try
            {
                //if (bluetoothAdapter.State == State.On || bluetoothAdapter.State == State.TurningOff)
                //{
                //    bluetoothAdapter.Disable();
                //}
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return true;
        }

        /*
        public void OnLeScan(BluetoothDevice bleDevice, int rssi, byte[] scanRecord)
        {
            /*
            var deviceId = Device.DeviceIdFromAddress(bleDevice.Address);
            if (DiscoveredDevices.All(x => x.Id != deviceId))
            {
                var device = new Device(bleDevice, null, null, rssi, scanRecord);
                DiscoveredDevices.Add(device);
                DeviceDiscovered(this, new DeviceDiscoveredEventArgs(device));
            }
            *\/
        }
        */

        public async Task<BluethoothDevicesInfo> GetListOfBluetoohDevices()
        {
            try
            {
                //var ctx = Application.Context;
                var ctx = (Forms.Context);
                if (!ctx.PackageManager.HasSystemFeature(PackageManager.FeatureBluetoothLe))
                {
                    return null;
                }

                //var statusChangeReceiver = new BluetoothStatusBroadcastReceiver(UpdateState);
                //ctx.RegisterReceiver(statusChangeReceiver, new IntentFilter(BluetoothAdapter.ActionStateChanged));

                //_bluetoothManager = (BluetoothManager)ctx.GetSystemService(Context.BluetoothService);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return null;
        }


        public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
        {
            try
            {

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            //throw new NotImplementedException();
        }

        public void Dispose()
        {
            try
            {

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            //throw new NotImplementedException();
        }


        /*
        public class Api18BleScanCallback : Object, BluetoothAdapter.ILeScanCallback
        {
            private readonly Adapter _adapter;

            public Api18BleScanCallback(Adapter adapter)
            {
                _adapter = adapter;
            }

            public IntPtr Handle
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public void OnLeScan(BluetoothDevice bleDevice, int rssi, byte[] scanRecord)
            {
                //Trace.Message("Adapter.LeScanCallback: " + bleDevice.Name);

                _adapter.HandleDiscoveredDevice(new Device(_adapter, bleDevice, null, rssi, scanRecord));
            }
        }

        public class Api21BleScanCallback : ScanCallback
        {
            private readonly Adapter _adapter;
            public Api21BleScanCallback(Adapter adapter)
            {
                _adapter = adapter;
            }

            public override void OnScanFailed(ScanFailure errorCode)
            {
                Trace.Message("Adapter: Scan failed with code {0}", errorCode);
                base.OnScanFailed(errorCode);
            }

            public override void OnScanResult(ScanCallbackType callbackType, ScanResult result)
            {
                base.OnScanResult(callbackType, result);

                /* Might want to transition to parsing the API21+ ScanResult, but sort of a pain for now 
                List<AdvertisementRecord> records = new List<AdvertisementRecord>();
                records.Add(new AdvertisementRecord(AdvertisementRecordType.Flags, BitConverter.GetBytes(result.ScanRecord.AdvertiseFlags)));
                if (!string.IsNullOrEmpty(result.ScanRecord.DeviceName))
                {
                    records.Add(new AdvertisementRecord(AdvertisementRecordType.CompleteLocalName, Encoding.UTF8.GetBytes(result.ScanRecord.DeviceName)));
                }
                for (int i = 0; i < result.ScanRecord.ManufacturerSpecificData.Size(); i++)
                {
                    int key = result.ScanRecord.ManufacturerSpecificData.KeyAt(i);
                    var arr = result.ScanRecord.GetManufacturerSpecificData(key);
                    byte[] data = new byte[arr.Length + 2];
                    BitConverter.GetBytes((ushort)key).CopyTo(data,0);
                    arr.CopyTo(data, 2);
                    records.Add(new AdvertisementRecord(AdvertisementRecordType.ManufacturerSpecificData, data));
                }
                foreach(var uuid in result.ScanRecord.ServiceUuids)
                {
                    records.Add(new AdvertisementRecord(AdvertisementRecordType.UuidsIncomplete128Bit, uuid.Uuid.));
                }
                foreach(var key in result.ScanRecord.ServiceData.Keys)
                {
                    records.Add(new AdvertisementRecord(AdvertisementRecordType.ServiceData, result.ScanRecord.ServiceData));
                }*\/

                var device = new Device(_adapter, result.Device, null, result.Rssi, result.ScanRecord.GetBytes());

                //Device device;
                //if (result.ScanRecord.ManufacturerSpecificData.Size() > 0)
                //{
                //    int key = result.ScanRecord.ManufacturerSpecificData.KeyAt(0);
                //    byte[] mdata = result.ScanRecord.GetManufacturerSpecificData(key);
                //    byte[] mdataWithKey = new byte[mdata.Length + 2];
                //    BitConverter.GetBytes((ushort)key).CopyTo(mdataWithKey, 0);
                //    mdata.CopyTo(mdataWithKey, 2);
                //    device = new Device(result.Device, null, null, result.Rssi, mdataWithKey);
                //}
                //else
                //{
                //    device = new Device(result.Device, null, null, result.Rssi, new byte[0]);
                //}

                _adapter.HandleDiscoveredDevice(device);

            }
        }*/
    }

    //[BroadcastReceiver(Enabled = true, Exported = false)]
    //[IntentFilter(new string[] { "testing" })]
    public class SampleReceiver : BroadcastReceiver
    {
        //public SampleReceiver() { }

        public override void OnReceive(Context context, Intent intent)
        {
            // Do stuff here.

            string action = intent.Action;

            if (action == BluetoothDevice.ActionFound)
            {
                // Get device
                BluetoothDevice newDevice = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

                // now you could do your job with newDevice
                // etc. check if newDevice is not already in a list and then use it in a ListView
            }

            //String value = intent.GetStringExtra("key");
        }
    }

    [Activity(Label = "SampleActivity")]
    public class SampleActivity : Activity
    {
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {

            }
            else if (requestCode == 2)
            {

            }
            else if (requestCode == 3)
            {

            }
            else if (requestCode == 4)
            {

            }
            else
            {

            }
        }
    }
}

