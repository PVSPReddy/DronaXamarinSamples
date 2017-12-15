using System;

using Xamarin.Forms;
using Android.Content;

namespace BluetoothExample.Droid
{
    public class BluetoothPairingService : BroadcastReceiver
    {
        public BluetoothPairingService() { }

        public override void OnReceive(Context context, Intent intent)
        {
            try
            {

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}

