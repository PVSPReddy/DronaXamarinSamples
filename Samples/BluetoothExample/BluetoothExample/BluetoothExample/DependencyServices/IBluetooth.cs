using System;
using System.Threading.Tasks;
namespace BluetoothExample
{
    public interface IBluetooth
    {
        Task<BluethoothDevicesInfo> GetListOfBluetoohDevices();

        Task<bool> StartBluetoothService(bool shallStartBluetooth);

        Task<bool> StopBluetoothService(bool shallStartBluetooth);
    }

    public class BluethoothDevicesInfo
    {
        public string DeviceName { get; set; }
    }
}
