using System;
using System.Threading.Tasks;
namespace BluetoothExample
{
    public interface IBluetoothLE
    {
        Task<bool> StartLEBluetooth();

        Task<bool> StopLEBluetooth();

        Task<bool> ConnectDevice();

        Task<bool> DisconnectDevice();

    }
}
