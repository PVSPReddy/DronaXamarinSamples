using System;
using Android.Bluetooth;
using BluetoothExample.Droid;
using Xamarin.Forms;

//[assembly: Dependency(typeof(IBluetoothAccessService))]
namespace BluetoothExample.Droid
{
    public class IBluetoothAccessService : BluetoothAdapter.ILeScanCallback//,IBluetoothAccess
    {
        public IntPtr Handle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IBluetoothAccessService() { }

        public void EnablingBluetooth()
        {
            var appContext = Android.App.Application.Context;
            BluetoothManager bm = (BluetoothManager)appContext.GetSystemService("bluetooth");
            BluetoothAdapter ba = bm.Adapter;
            //BluetoothGattCallback callBack = new BluetoothGattCallback(this);
            ba.StartLeScan(this);

            //this._gattCallback = new GattCallback(this);
        }

        public void DisablingBluetooth()
        {

        }

        public void OnLeScan(BluetoothDevice device, int rssi, byte[] scanRecord)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

