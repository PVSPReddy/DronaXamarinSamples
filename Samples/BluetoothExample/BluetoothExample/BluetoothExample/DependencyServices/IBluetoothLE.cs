﻿using System;
using System.Threading.Tasks;
namespace BluetoothExample
{
    public interface IBluetoothLE
    {
        Task<bool> StartLEBluetooth();

        Task<bool> StopLEBluetooth();

        Task<bool> ConnectDevice();

        Task<bool> DisconnectDevice();

        Task<bool> PairSelectedDevice();

        Task<bool> UnPairSelectedDevice();

        //event EventHandler DiscoveredDevice;

        event EventHandler<IDeviceInfoEventArgs> DiscoveredDevice;
    }

    public interface IDeviceInfoEventArgs
    {
        string DeviceName { get; set; }
        string DeviceAddress { get; set; }
    }
}
