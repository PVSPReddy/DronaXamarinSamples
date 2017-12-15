using System;
using Android.Bluetooth;
using Xamarin.Forms;

namespace BluetoothExample.Droid
{
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
}

