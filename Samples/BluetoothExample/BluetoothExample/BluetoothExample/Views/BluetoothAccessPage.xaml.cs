using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace BluetoothExample
{
    public partial class BluetoothAccessPage : ContentPage
    {
        IBluetooth bluetoothServices;
        IBluetoothLE bluetoothLEServices = DependencyService.Get<IBluetoothLE>();
        public static BluetoothAccessPage bluetoothAccessPage;

        public BluetoothAccessPage()
        {
            InitializeComponent();
            bluetoothAccessPage = this;
            bluetoothServices = DependencyService.Get<IBluetooth>();
        }

        void StartCoreBluetoothClicked(object sender, EventArgs e)
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

        void StopCoreBluetoothClicked(object sender, EventArgs e)
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
        void StartLEBluetoothClicked(object sender, EventArgs e)
        {
            try
            {
                bluetoothLEServices.StartLEBluetooth();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        void StopLEBluetoothClicked(object sender, EventArgs e)
        {
            try
            {
                bluetoothLEServices.StopLEBluetooth();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async Task<bool> DispayMessages(string message)
        {
            await DisplayAlert("BlueTooth", message, "Ok");
            return true;
        }
    }
}
