using System;
using BluetoothExample.Droid;
using Xamarin.Forms;
using Android.Bluetooth;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;

[assembly: Dependency(typeof(BluetoothLEServices))]
namespace BluetoothExample.Droid
{
    public class BluetoothLEServices : Java.Lang.Object, BluetoothAdapter.ILeScanCallback, IBluetoothLE
    {
        public BluetoothLEServices()
        {
            callBack = new GattCallback(this);
        }

        public IntPtr Handle
        {
            get;
            //{
            //    throw new NotImplementedException();
            //}
        }

        private BluetoothManager bluetoothManager;
        private BluetoothAdapter bluetoothAdapter;
        protected readonly GattCallback callBack;

        public async Task<bool> StartLEBluetooth()
        {
            try
            {
                var appContext = Android.App.Application.Context;
                bluetoothManager = (BluetoothManager)appContext.GetSystemService("bluetooth");
                bluetoothAdapter = bluetoothManager.Adapter;
                var bluetoothState = bluetoothAdapter.State;
                if (bluetoothState == State.Off)
                {

                    bluetoothAdapter.Enable();
                }
                if (Build.VERSION.SdkInt >= Build.VERSION_CODES.JellyBeanMr2)
                {
                    var data = bluetoothAdapter.StartLeScan(this);
                }
                else
                {
                    bluetoothAdapter.StartDiscovery();
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
            if (bluetoothAdapter != null)
            {
                if (bluetoothAdapter.IsEnabled)
                {
                    try
                    {
                        if (Build.VERSION.SdkInt >= Build.VERSION_CODES.JellyBeanMr2)
                        {
                            bluetoothAdapter.StopLeScan(this);
                        }
                        else
                        {
                            bluetoothAdapter.CancelDiscovery();
                        }
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.Message;
                    }
                    bluetoothAdapter.Disable();
                }
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

        public void Dispose()
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


        public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
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

        protected class GattCallback : BluetoothGattCallback
        {
            protected BluetoothLEServices _parent;

            public GattCallback(BluetoothLEServices parent)
            {
                this._parent = parent;
            }

            public override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState)
            {
                Console.WriteLine("OnConnectionStateChange: ");
                base.OnConnectionStateChange(gatt, status, newState);

                switch (newState)
                {
                    // disconnected
                    case ProfileState.Disconnected:
                        Console.WriteLine("disconnected");
                        ////TODO/BUG: Need to remove this, but can't remove the key (uncomment and see bug on disconnect)
                        ////                  if (this._parent._connectedDevices.ContainsKey (gatt.Device))
                        ////                      this._parent._connectedDevices.Remove (gatt.Device);
                        //this._parent.DeviceDisconnected(this, new DeviceConnectionEventArgs() { Device = gatt.Device });
                        break;
                    // connecting
                    case ProfileState.Connecting:
                        Console.WriteLine("Connecting");
                        break;
                    // connected
                    case ProfileState.Connected:
                        Console.WriteLine("Connected");
                        ////TODO/BUGBUG: need to remove this when disconnected
                        //this._parent._connectedDevices.Add(gatt.Device, gatt);
                        //this._parent.DeviceConnected(this, new DeviceConnectionEventArgs() { Device = gatt.Device });
                        break;
                    // disconnecting
                    case ProfileState.Disconnecting:
                        Console.WriteLine("Disconnecting");
                        break;
                }
            }

            public override void OnServicesDiscovered(BluetoothGatt gatt, GattStatus status)
            {
                base.OnServicesDiscovered(gatt, status);

                Console.WriteLine("OnServicesDiscovered: " + status.ToString());

                //TODO: somehow, we need to tie this directly to the device, rather than for all
                // google's API deisgners are children.

                ////TODO: not sure if this gets called after all services have been enumerated or not
                //if (!this._parent._services.ContainsKey(gatt.Device))
                //    this._parent.Services.Add(gatt.Device, this._parent._connectedDevices[gatt.Device].Services);
                //else
                //    this._parent._services[gatt.Device] = this._parent._connectedDevices[gatt.Device].Services;

                //this._parent.ServiceDiscovered(this, new ServiceDiscoveredEventArgs()
                //{
                //    Gatt = gatt
                //});
            }

        }
    }
}

