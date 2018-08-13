using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BluetoothExample
{
    public partial class BluetoothAccessCommonPage : ContentPage
    {
        IBluetoothCommon bluetoothCommonServices = DependencyService.Get<IBluetoothCommon>();
        public static BluetoothAccessCommonPage bluetoothAccessCommonPage;
        List<AllBluetoothDevices> allDevices;

        public BluetoothAccessCommonPage()
        {
            InitializeComponent();
            bluetoothAccessCommonPage = this;
            allDevices = new List<AllBluetoothDevices>();


            bluetoothCommonServices.DiscoveredDevice += async (object sender, IDeviceInfoEventArgs e) =>
            {
                try
                {
                    //labelDataFill.Text += e.DeviceName + "\n" + e.DeviceAddress;
                    allDevices.Add(new AllBluetoothDevices()
                    {
                        DeviceAddress = e.DeviceAddress ?? "No Name",
                        DeviceName = e.DeviceName ?? "No Defined Address"
                    });
                    await FillDataToList((e.DeviceName ?? "No Name"), (e.DeviceAddress ?? "No Defined Address"));
                    //await FillDataToList();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            };

            //lvDisplay.ItemSelected += SelectedBluetoothDevice;
        }

        void StartBluetoothClicked(object sender, EventArgs e)
        {
            try
            {
                bluetoothCommonServices.StartLEBluetooth();
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
                bluetoothCommonServices.StopLEBluetooth();
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
                Device.BeginInvokeOnMainThread(() =>
                {
                    ////lvDisplay.BeginRefresh();
                    //lvDisplay.ItemsSource = allDevices;
                });
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine("From Fill Data To List1 Error : \n" + ex.StackTrace);
            }
            //lvDisplay.EndRefresh();
            return true;
        }


        public async Task<bool> FillDataToList(string v1, string v2)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Label labelDeviceName = new Label()
                    {
                        Text = v1,
                    };
                    Label labelDeviceAddress = new Label()
                    {
                        Text = v2,
                    };
                    StackLayout stackDeviceDataHolder = new StackLayout()
                    {
                        Children = { labelDeviceName, labelDeviceAddress },
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Center
                    };
                    TapGestureRecognizer deviceSelected = new TapGestureRecognizer();
                    deviceSelected.Tapped += (object sender, EventArgs e) =>
                    {
                        try
                        {
                            bluetoothCommonServices.PairSelectedDevice(v1, v2);
                        }
                        catch (Exception ex)
                        {
                            var msg = ex.Message;
                        }
                    };
                    stackDeviceDataHolder.GestureRecognizers.Add(deviceSelected);
                    stackDynamicCellHolder.Children.Add(stackDeviceDataHolder);
                });
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                System.Diagnostics.Debug.WriteLine("From Fill Data To List2 Error : \n" + ex.StackTrace);
            }
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
                    bluetoothCommonServices.PairSelectedDevice(deviceSelected.DeviceName, deviceSelected.DeviceAddress);
                }
                ((ListView)sender).SelectedItem = null;
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

    /*
    public class AllBluetoothDevices
    {
        public string DeviceName { get; set; }
        public string DeviceAddress { get; set; }
    }
    */

}
