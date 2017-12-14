using System;
using BluetoothExample.Droid;
using Xamarin.Forms;
using Android.Bluetooth;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Bluetooth.LE;
using System.Linq;

[assembly: Dependency(typeof(BluetoothLEServices))]
namespace BluetoothExample.Droid
{
    public class BluetoothLEServices : IBluetoothLE
    {
        //BluetoothLEManager lemanager;
        BluetoothLeManagers bluetoothLEManagers;

        public event EventHandler<IDeviceInfoEventArgs> DiscoveredDevice;

        //public event EventHandler DiscoveredDevice;

        public BluetoothLEServices()
        {
            //lemanager = BluetoothLEManager.Current;
            bluetoothLEManagers = new BluetoothLeManagers();
            MessagingCenter.Subscribe<BluetoothLeManagers>(this, "above18", (obj) =>
            {
                var deviceInfo = obj.deviceInfoEventArgs;
                DeviceInfoEventArgs deviceInfoEventArgs = new DeviceInfoEventArgs()
                {
                    DeviceName = deviceInfo.DeviceName,
                    DeviceAddress = deviceInfo.DeviceAddress
                };
                DiscoveredDevice(null, deviceInfoEventArgs);
            });
        }


        public async Task<bool> StartLEBluetooth()
        {
            try
            {
                //await lemanager.BeginScanningForDevices();
                await bluetoothLEManagers.StartBlueTooth();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine("From StartScan Error : \n" + ex.StackTrace);
                System.Console.WriteLine("From StartScan Error : \n" + ex.StackTrace);
            }
            return true;
        }

        public async Task<bool> StopLEBluetooth()
        {
            try
            {
                await bluetoothLEManagers.StopBlueTooth();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine("From StopScan Error : \n" + ex.StackTrace);
                System.Console.WriteLine("From StopScan Error : \n" + ex.StackTrace);
            }
            return true;
        }

        public async Task<bool> ConnectDevice()
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

        public async Task<bool> DisconnectDevice()
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
    }

    public class DeviceInfoEventArgs : EventArgs, IDeviceInfoEventArgs
    {
        public string DeviceAddress { get; set; }
        public string DeviceName { get; set; }
    }

    public class BluetoothDevicesCollected
    {
        public BluetoothDevice BluetoothDeviceDiscovered { get; set; }
        public bool IsPaired { get; set; }
    }

    public class BluetoothLeManagers : Java.Lang.Object, BluetoothAdapter.ILeScanCallback
    {
        public static BluetoothLeManagers bluetoothLEManager;
        private BluetoothManager bluetoothManager;
        private BluetoothAdapter bluetoothAdapter;
        private BluetoothLeScanner bluetoothLEScanner;
        List<BluetoothDevicesCollected> bluetoothDevicesCollected;
        public DeviceInfoEventArgs deviceInfoEventArgs;

        public BluetoothLeManagers()
        {
            var appContext = Android.App.Application.Context;
            bluetoothManager = (BluetoothManager)appContext.GetSystemService("bluetooth");
            //bluetoothManager = (BluetoothManager)appContext.GetSystemService(Context.BluetoothService);

            bluetoothAdapter = bluetoothManager.Adapter;
            //bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            bluetoothLEManager = this;

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

                if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Lollipop)
                {
                    bluetoothAdapter.StartDiscovery();
                    //bluetoothScanCallBack = new BluetoothScanCallBack();
                    //bluetoothLEScanner = bluetoothAdapter.BluetoothLeScanner;
                    //bluetoothLEScanner.StartScan(bluetoothScanCallBack);
                }
                else if (Build.VERSION.SdkInt >= Build.VERSION_CODES.JellyBeanMr2)
                {
                    bluetoothAdapter.StartDiscovery();
                    bluetoothAdapter.StartLeScan(this);
                    //bluetoothCallBack = new BluetoothCallBackEvents();
                    //bluetoothAdapter.StartLeScan(bluetoothCallBack);
                }
                else
                {
                    bluetoothAdapter.StartDiscovery();
                }

                await Task.Delay(15000);
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
                if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Lollipop)
                {
                    bluetoothAdapter.CancelDiscovery();
                    //bluetoothLEScanner.StartScan(bluetoothScanCallBack);
                }
                else if (Build.VERSION.SdkInt >= Build.VERSION_CODES.JellyBeanMr2)
                {
                    bluetoothAdapter.CancelDiscovery();
                    bluetoothAdapter.StopLeScan(this);
                    //bluetoothAdapter.StopLeScan(bluetoothCallBack);
                }
                else
                {
                    bluetoothAdapter.CancelDiscovery();
                }
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
                    MessagingCenter.Send(this, "above18", deviceInfoEventArgs);
                    System.Diagnostics.Debug.WriteLine("From Scanned Results : \n" + device.Name + device.Address);
                    System.Console.WriteLine("From Scanned Results : \n" + device.Name + device.Address);
                }
                else
                {
                    var isDeviceAlreadyAdded = bluetoothDevicesCollected.Where(X => X.BluetoothDeviceDiscovered == device).Any();
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
                System.Diagnostics.Debug.WriteLine("From Scanned Results Error : \n" + ex.StackTrace);
                System.Console.WriteLine("From Scanned Results Error : \n" + ex.StackTrace);
            }
        }
    }
}
