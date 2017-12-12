using System;
using System.Threading.Tasks;
using BluetoothExample.iOS;
using CoreBluetooth;
using Xamarin.Forms;
using CoreFoundation;
using System.Collections.Generic;

[assembly: Dependency(typeof(BluetoothLEServices))]
namespace BluetoothExample.iOS
{
    public class BluetoothLEServices : CBCentralManagerDelegate, IBluetoothLE
    {
        public BluetoothLEServices() { }


        CBCentralManager centralManager;
        bool isScanning;
        public List<CBPeripheral> devicesList = new List<CBPeripheral>();

        public override void UpdatedState(CBCentralManager central)
        {
            try
            {

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            //throw new NotImplementedException();
        }

        public async Task<bool> StartLEBluetooth()
        {
            try
            {
                centralManager = new CBCentralManager(DispatchQueue.CurrentQueue);

                centralManager.DiscoveredPeripheral += async (object sender, CBDiscoveredPeripheralEventArgs e) =>
                {
                    var discovered = e.Peripheral.Name;
                    await BluetoothAccessPage.bluetoothAccessPage.DispayMessages(discovered);
                };

                centralManager.UpdatedState += async (object sender, EventArgs e) =>
                {
                    try
                    {
                        var status = e.ToString();
                        await BluetoothAccessPage.bluetoothAccessPage.DispayMessages(status);
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.Message;
                        await BluetoothAccessPage.bluetoothAccessPage.DispayMessages(msg);
                    }
                };

                centralManager.ConnectedPeripheral += async (object sender, CBPeripheralEventArgs e) =>
                {
                    var connected = e.Peripheral.Name;
                    await BluetoothAccessPage.bluetoothAccessPage.DispayMessages(connected);
                };

                centralManager.DisconnectedPeripheral += async (object sender, CBPeripheralErrorEventArgs e) =>
                {
                    var disConnected = e.Peripheral.Name;
                    await BluetoothAccessPage.bluetoothAccessPage.DispayMessages(disConnected);
                };

                centralManager.ScanForPeripherals((CBUUID[])null);
                isScanning = true;

                await Task.Delay(10000);

                if (isScanning)
                {
                    centralManager.StopScan();
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return true;
        }

        public async Task<bool> StopLEBluetooth()
        {
            try
            {
                if (centralManager != null && isScanning == true)
                {
                    centralManager.StopScan();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
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
}

