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
        List<AllBluetoothDevices> allDevices;

        public BluetoothAccessPage()
        {
            InitializeComponent();
            bluetoothAccessPage = this;
            bluetoothServices = DependencyService.Get<IBluetooth>();
            allDevices = new List<AllBluetoothDevices>();
            bluetoothLEServices.DiscoveredDevice += async (object sender, IDeviceInfoEventArgs e) =>
            {
                allDevices.Add(new AllBluetoothDevices()
                {
                    DeviceAddress = e.DeviceAddress,
                    DeviceName = e.DeviceName
                });
                await FillDataToList();
            };

            lvDisplay.ItemSelected += SelectedBluetoothDevice;
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

        public async Task<bool> FillDataToList()
        {
            try
            {
                lvDisplay.BeginRefresh();
                lvDisplay.ItemsSource = allDevices;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine("From Scanned Results Error : \n" + ex.StackTrace);
            }
            lvDisplay.EndRefresh();
            return true;
        }

        void SelectedBluetoothDevice(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var deviceSelected = ((ListView)sender).SelectedItem as AllBluetoothDevices;
                if (deviceSelected == null)
                {
                    return;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine("From Scanned Results Error : \n" + ex.StackTrace);
            }
        }

        public async Task<bool> DispayMessages(string message)
        {
            await DisplayAlert("BlueTooth", message, "Ok");
            return true;
        }
    }

    public class AllBluetoothDevices
    {
        public string DeviceName { get; set; }
        public string DeviceAddress { get; set; }
    }
}
