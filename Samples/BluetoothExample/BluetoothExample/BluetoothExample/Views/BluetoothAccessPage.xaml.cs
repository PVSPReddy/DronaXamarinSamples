using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BluetoothExample
{
    public partial class BluetoothAccessPage : ContentPage
    {
        IBluetooth bluetoothServices;
        public BluetoothAccessPage()
        {
            InitializeComponent();
            bluetoothServices = DependencyService.Get<IBluetooth>();
        }

        void StartBluetoothClicked(object sender, EventArgs e)
        {
            try
            {
                bluetoothServices.StartBluetoothService(true);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        void StopBluetoothClicked(object sender, EventArgs e)
        {
            try
            {
                bluetoothServices.StopBluetoothService(true);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
