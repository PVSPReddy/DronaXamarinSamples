using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using Android.OS;
using Xamarin.Forms;

namespace BluetoothExample.Droid
{
    public class BluetoothLEManagerLollipopAndAbove : Android.Bluetooth.LE.ScanCallback
    {
        public static BluetoothLEManagerLollipopAndAbove bluetoothLEManagerLollipopAndAbove;
        private BluetoothManager bluetoothManager;
        private BluetoothAdapter bluetoothAdapter;
        private BluetoothLeScanner bluetoothLEScanner;
        List<BluetoothDevicesCollected> bluetoothDevicesCollected;
        public DeviceInfoEventArgs deviceInfoEventArgs;

        public BluetoothLEManagerLollipopAndAbove()
        {
            var appContext = Android.App.Application.Context;
            bluetoothManager = (BluetoothManager)appContext.GetSystemService("bluetooth");
            //bluetoothManager = (BluetoothManager)appContext.GetSystemService(Context.BluetoothService);

            bluetoothAdapter = bluetoothManager.Adapter;
            //bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            bluetoothLEManagerLollipopAndAbove = this;

            bluetoothDevicesCollected = new List<BluetoothDevicesCollected>();
        }

        public async Task<bool> StartBlueTooth()
        {
            try
            {
                if (!bluetoothAdapter.IsEnabled)
                {
                    bluetoothAdapter.Enable();
                }
                await StartScanningDevices();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return true;
        }

        public async Task<bool> StartScanningDevices()
        {
            try
            {
                var bluetoothState = bluetoothAdapter.State;
                if (bluetoothState == Android.Bluetooth.State.Off)
                {
                    bluetoothAdapter.Enable();
                    //await Task.Delay(2000);
                }
                bluetoothAdapter.StartDiscovery();
                bluetoothLEScanner = bluetoothAdapter.BluetoothLeScanner;
                bluetoothLEScanner.StartScan(this);

                await Task.Delay(15000);
                await GetPairedDevices();
                StopScanningDevices();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine("From StartScan Error : \n" + ex.StackTrace);
                System.Console.WriteLine("From StartScan Error : \n" + ex.StackTrace);
            }
            return true;
        }

        public async Task<bool> StopScanningDevices()
        {
            try
            {
                bluetoothAdapter.CancelDiscovery();
                bluetoothLEScanner.StartScan(this);
                GetPairedDevices();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine("From StopScan Error : \n" + ex.StackTrace);
                System.Console.WriteLine("From StopScan Error : \n" + ex.StackTrace);
            }
            return true;
        }

        public async Task<List<BluetoothDevice>> GetPairedDevices()
        {
            List<BluetoothDevice> listPairedDevices = new List<BluetoothDevice>();
            try
            {
                var bluetoothDevices = bluetoothAdapter.BondedDevices;
                foreach (var bluetoothDevice in bluetoothDevices)
                {
                    listPairedDevices.Add(bluetoothDevice);
                    await AddDeviceToList(bluetoothDevice);
                    System.Diagnostics.Debug.WriteLine("Paired/Bonded Device : \n" + bluetoothDevice.Name);
                    System.Console.WriteLine("Paired/Bonded Device : \n" + bluetoothDevice.Name);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return listPairedDevices;
        }

        public async Task<bool> PairSelectedDevice(string deviceAddress)
        {
            try
            {
                var _deviceToBePaired = bluetoothDevicesCollected.Where(X => X.BluetoothDeviceDiscovered.Address == deviceAddress).FirstOrDefault();
                var deviceToBePaired = ((BluetoothDevice)_deviceToBePaired.BluetoothDeviceDiscovered);
                if (deviceToBePaired.BondState == Bond.None)
                {
                    BluetoothPairingService pairingService = new BluetoothPairingService();
                    IntentFilter pairingIntent = new IntentFilter(BluetoothDevice.ActionBondStateChanged);
                    //pairingIntent.Priority
                    Forms.Context.RegisterReceiver(pairingService, pairingIntent);
                    deviceToBePaired.CreateBond();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return true;
        }

        public async Task<bool> UnPairSelectedDevice(string deviceAddress)
        {
            try
            {
                var _deviceToBePaired = bluetoothDevicesCollected.Where(X => X.BluetoothDeviceDiscovered.Address == deviceAddress).FirstOrDefault();
                var deviceToBePaired = ((BluetoothDevice)_deviceToBePaired.BluetoothDeviceDiscovered);
                if (deviceToBePaired.BondState != Bond.None)
                {
                    //deviceToBePaired.SetPairingConfirmation(true);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return true;
        }

        /*
        public async Task<bool> StartBlueTooth()
        {
            try
            {

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return true;
        }
        */

        public async Task<bool> StopBlueTooth()
        {
            try
            {
                bluetoothLEScanner.StopScan(this);
                if (bluetoothAdapter.IsEnabled)
                {
                    bluetoothAdapter.Disable();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return true;
        }


        public override void OnScanResult(Android.Bluetooth.LE.ScanCallbackType callbackType, Android.Bluetooth.LE.ScanResult result)
        {
            base.OnScanResult(callbackType, result);
            var device = result.Device;
            try
            {
                AddDeviceToList(device);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("From Lollipop Result : \n" + result.Device.Name);
                System.Console.WriteLine("From Lollipop Result : \n" + result.Device.Name);
            }

        }


        /*
        public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
        {
            try
            {
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine("From Scanned Results Error : \n" + ex.StackTrace);
                System.Console.WriteLine("From Scanned Results Error : \n" + ex.StackTrace);
            }
        }
        */


        public override void OnBatchScanResults(IList<Android.Bluetooth.LE.ScanResult> results)
        {
            base.OnBatchScanResults(results);
            try
            {
                foreach (var result in results)
                {
                    var bluetoothDevice = result.Device;
                    AddDeviceToList(bluetoothDevice);
                    System.Diagnostics.Debug.WriteLine("From Lollipop Results : \n" + bluetoothDevice.Name);
                    System.Console.WriteLine("From Lollipop Results : \n" + bluetoothDevice.Name);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine("From On Batch Scanned Results Error : \n" + ex.StackTrace);
                System.Console.WriteLine("From On Batch Scanned Results Error : \n" + ex.StackTrace);
            }
        }

        public override void OnScanFailed(Android.Bluetooth.LE.ScanFailure errorCode)
        {
            base.OnScanFailed(errorCode);

            System.Diagnostics.Debug.WriteLine("From Lollipop ScanFailed : \n" + errorCode);
            System.Console.WriteLine("From Lollipop ScanFailed : \n" + errorCode);
        }

        public async Task<bool> AddDeviceToList(BluetoothDevice device)
        {
            try
            {
                if (bluetoothDevicesCollected.Count <= 0)
                {
                    bluetoothDevicesCollected.Add(new BluetoothDevicesCollected()
                    {
                        BluetoothDeviceDiscovered = device,
                        IsPaired = false
                    });
                    deviceInfoEventArgs = new DeviceInfoEventArgs()
                    {
                        DeviceName = device.Name,
                        DeviceAddress = device.Address
                    };
                    MessagingCenter.Send<BluetoothLEManagerLollipopAndAbove, DeviceInfoEventArgs>(this, "above18", deviceInfoEventArgs);
                    System.Diagnostics.Debug.WriteLine("From Add to List Results : \n" + device.Name + device.Address);
                    System.Console.WriteLine("From Add To List Results : \n" + device.Name + device.Address);
                }
                else
                {
                    var isDeviceAlreadyAdded = bluetoothDevicesCollected.Where(X => X.BluetoothDeviceDiscovered.Address == device.Address).Any();
                    if (!isDeviceAlreadyAdded)
                    {
                        bluetoothDevicesCollected.Add(new BluetoothDevicesCollected()
                        {
                            BluetoothDeviceDiscovered = device,
                            IsPaired = false
                        });
                        deviceInfoEventArgs = new DeviceInfoEventArgs()
                        {
                            DeviceName = device.Name,
                            DeviceAddress = device.Address
                        };
                        MessagingCenter.Send(this, "above18", deviceInfoEventArgs);
                        System.Diagnostics.Debug.WriteLine("From Add to List Results : \n" + device.Name + device.Address);
                        System.Console.WriteLine("From Add to List Results : \n" + device.Name + device.Address);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("From Add to List Results : \n" + device.Name + device.Address);
                        System.Console.WriteLine("From Add to List Results : \n" + device.Name + device.Address);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return true;
        }
    }
}