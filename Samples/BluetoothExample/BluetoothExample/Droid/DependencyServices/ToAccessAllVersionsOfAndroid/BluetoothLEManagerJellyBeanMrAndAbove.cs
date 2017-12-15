using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.OS;
using Xamarin.Forms;
using Android.Content;

namespace BluetoothExample.Droid
{
    public class BluetoothLEManagerJellyBeanMrAndAbove : Java.Lang.Object, BluetoothAdapter.ILeScanCallback
    {
        public static BluetoothLEManagerJellyBeanMrAndAbove bluetoothLEManagerJellyBeanMrAndAbove;
        private BluetoothManager bluetoothManager;
        private BluetoothAdapter bluetoothAdapter;
        private BluetoothLeScanner bluetoothLEScanner;
        List<BluetoothDevicesCollected> bluetoothDevicesCollected;
        public DeviceInfoEventArgs deviceInfoEventArgs;

        public BluetoothLEManagerJellyBeanMrAndAbove()
        {
            var appContext = Android.App.Application.Context;
            bluetoothManager = (BluetoothManager)appContext.GetSystemService("bluetooth");
            //bluetoothManager = (BluetoothManager)appContext.GetSystemService(Context.BluetoothService);

            bluetoothAdapter = bluetoothManager.Adapter;
            //bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            bluetoothLEManagerJellyBeanMrAndAbove = this;

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
                bluetoothAdapter.StartLeScan(this);

                await Task.Delay(15000);
                //await GetPairedDevices();
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
                bluetoothAdapter.StopLeScan(this);

                await GetPairedDevices();
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
                    AddDeviceToList(bluetoothDevice);
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
                    /*
                    int intValue = 1234;
                    byte[] intBytes = BitConverter.GetBytes(intValue);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(intBytes);
                    }
                    byte[] result = intBytes;
                    deviceToBePaired.SetPin(result);

                    deviceToBePaired.SetPairingConfirmation(true);

                    deviceToBePaired.CreateBond();

                    //deviceToBePaired.Class.GetMethod("");
                    */


                    int intValue = 1234;
                    byte[] intBytes = BitConverter.GetBytes(intValue);
                    BluetoothPairingService pairingService = new BluetoothPairingService();
                    IntentFilter pairingIntent = new IntentFilter(BluetoothDevice.ActionBondStateChanged);
                    pairingIntent.AddAction(BluetoothDevice.ActionPairingRequest);
                    //pairingIntent.Priority
                    Forms.Context.RegisterReceiver(pairingService, pairingIntent);
                    deviceToBePaired.CreateBond();
                    MessagingCenter.Subscribe<BluetoothPairingService>(this, "Start Pairing", (obj) =>
                    {
                        try
                        {
                            //var method = deviceToBePaired.Class.GetMethod("SetPin", byte[(intBytes.Length)]());
                            //m.invoke(device, pinBytes);
                        }
                        catch (Exception ex)
                        {
                            var msg = ex.Message;
                        }
                    });


                    //deviceToBePaired.CreateBond();
                    //deviceToBePaired.SetPairingConfirmation(true);
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
                await StopScanningDevices();
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


        public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
        {
            try
            {
                AddDeviceToList(device);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine("From Scanned Results Error : \n" + ex.StackTrace);
                System.Console.WriteLine("From Scanned Results Error : \n" + ex.StackTrace);
            }
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
                    MessagingCenter.Send<BluetoothLEManagerJellyBeanMrAndAbove, DeviceInfoEventArgs>(this, "above18", deviceInfoEventArgs);
                    System.Diagnostics.Debug.WriteLine("From Scanned Results : \n" + device.Name + device.Address);
                    System.Console.WriteLine("From Scanned Results : \n" + device.Name + device.Address);
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
                        System.Diagnostics.Debug.WriteLine("From Scanned Results : \n" + device.Name + device.Address);
                        System.Console.WriteLine("From Scanned Results : \n" + device.Name + device.Address);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("From Scanned Results : \n" + device.Name + device.Address);
                        System.Console.WriteLine("From Scanned Results : \n" + device.Name + device.Address);
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

