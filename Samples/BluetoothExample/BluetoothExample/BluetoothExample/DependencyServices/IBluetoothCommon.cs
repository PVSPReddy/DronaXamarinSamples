using System;
using System.Threading.Tasks;

namespace BluetoothExample
{
    public interface IBluetoothCommon
    {
        Task<bool> StartLEBluetooth();

        Task<bool> StopLEBluetooth();

        Task<bool> ConnectDevice();

        Task<bool> DisconnectDevice();

        Task<bool> PairSelectedDevice(string deviceName, string deviceAddress);

        Task<bool> UnPairSelectedDevice(string deviceName, string deviceAddress);

        //event EventHandler DiscoveredDevice;

        event EventHandler<IDeviceInfoEventArgs> DiscoveredDevice;
    }

    /*
    public interface IDeviceInfoEventArgs
    {
        string DeviceName { get; set; }
        string DeviceAddress { get; set; }
    }
    */

}
