using System;
using System.Threading.Tasks;
using BluetoothExample.Droid;
using Xamarin.Forms;
using Android.OS;

[assembly: Dependency(typeof(BluetoothCommonServices))]
namespace BluetoothExample.Droid
{
    public class BluetoothCommonServices : IBluetoothCommon
    {
        //BluetoothLEManager lemanager;
        BluetoothLeManagers bluetoothLEManagers;
        BluetoothLEManagerJellyBeanMrAndAbove bluetoothLEManagerJellyBeanMrAndAbove;
        BluetoothLEManagerLollipopAndAbove bluetoothLEManagerLollipopAndAbove;

        public event EventHandler<IDeviceInfoEventArgs> DiscoveredDevice;

        //public event EventHandler DiscoveredDevice;

        public BluetoothCommonServices()
        {
            //lemanager = BluetoothLEManager.Current;
            //bluetoothLEManagers = new BluetoothLeManagers();
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                bluetoothLEManagerLollipopAndAbove = new BluetoothLEManagerLollipopAndAbove();
                MessagingCenter.Subscribe<BluetoothLEManagerLollipopAndAbove, DeviceInfoEventArgs>(this, "above18", (sender, arg) =>
                {
                    var deviceInfo = arg;
                    DeviceInfoEventArgs deviceInfoEventArgs = new DeviceInfoEventArgs()
                    {
                        DeviceName = deviceInfo.DeviceName,
                        DeviceAddress = deviceInfo.DeviceAddress
                    };
                    DiscoveredDevice(null, deviceInfoEventArgs);
                });
            }
            else if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                bluetoothLEManagerJellyBeanMrAndAbove = new BluetoothLEManagerJellyBeanMrAndAbove();
                MessagingCenter.Subscribe<BluetoothLEManagerJellyBeanMrAndAbove, DeviceInfoEventArgs>(this, "above18", (sender, arg) =>
                {
                    var deviceInfo = arg;
                    DeviceInfoEventArgs deviceInfoEventArgs = new DeviceInfoEventArgs()
                    {
                        DeviceName = deviceInfo.DeviceName,
                        DeviceAddress = deviceInfo.DeviceAddress
                    };
                    DiscoveredDevice(null, deviceInfoEventArgs);
                });
            }
            else
            {
                MessagingCenter.Subscribe<BluetoothLeManagers, DeviceInfoEventArgs>(this, "above18", (sender, arg) =>
                {
                    var deviceInfo = arg;
                    DeviceInfoEventArgs deviceInfoEventArgs = new DeviceInfoEventArgs()
                    {
                        DeviceName = deviceInfo.DeviceName,
                        DeviceAddress = deviceInfo.DeviceAddress
                    };
                    DiscoveredDevice(null, deviceInfoEventArgs);
                });
            }
        }

        public async Task<bool> StartLEBluetooth()
        {
            try
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    bluetoothLEManagerLollipopAndAbove.StartBlueTooth();
                }
                else if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
                {
                    bluetoothLEManagerJellyBeanMrAndAbove.StartBlueTooth();
                }
                else
                {

                }
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

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    bluetoothLEManagerLollipopAndAbove.StopBlueTooth();
                }
                else if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
                {
                    bluetoothLEManagerJellyBeanMrAndAbove.StopBlueTooth();
                }
                else
                {

                }
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

        public async Task<bool> PairSelectedDevice(string deviceName, string deviceAddress)
        {

            try
            {

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    bluetoothLEManagerLollipopAndAbove.PairSelectedDevice(deviceAddress);
                }
                else if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
                {
                    bluetoothLEManagerJellyBeanMrAndAbove.PairSelectedDevice(deviceAddress);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine("From StopScan Error : \n" + ex.StackTrace);
                System.Console.WriteLine("From StopScan Error : \n" + ex.StackTrace);
            }
            return true;
        }

        public async Task<bool> UnPairSelectedDevice(string deviceName, string deviceAddress)
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
}

