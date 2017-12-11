using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BluetoothExample.iOS
{
    public class BluetoothServices : IBluetooth
    {
        public BluetoothServices() { }

        bool isBlueToothRunning, isStartedBluetooth;

        public async Task<bool> StartBluetoothService(bool shallStartBluetooth)
        {
            try
            {
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

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return true;
        }

        public async Task<BluethoothDevicesInfo> GetListOfBluetoohDevices()
        {
            try
            {

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return null;
        }
    }
}

