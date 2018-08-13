using System;
using System.Threading.Tasks;
using SocketsExample.Droid;
using Xamarin.Forms;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

//[assembly : Dependency(typeof(ISocketServiceTestOne))]
namespace SocketsExample.Droid
{
    public class TcpClientTestOne : ISocketTestOne
    {
        public TcpClientTestOne() { }

        string reply = "";

        Socket socket;
        TcpClient tcpClient;

        int i;
        TcpClient client; // Creates a TCP Client
        NetworkStream stream; //Creats a NetworkStream (used for sending and receiving data)
        byte[] datalength = new byte[4]; // creates a new byte with length 4 ( used for receivng data's lenght)

        public async Task<bool> InitalizeSocket()
        {
            try
            {
                var address = "nodetestchat.herokuapp.com";//https://nodetestchat.herokuapp.com
                var port = 443;//11000;//3000;
                client = new TcpClient(address, port);
                //hostname : "nodetestchat.herokuapp.com"
                //path : "/socket.io"
                //port : "443"
                //client = new TcpClient("YOUR COMPUTER IP FINDING BY IPCONFIG ", port); //Trys to Connect
                clientReceive(); //Starts Receiving When Connected

                /*
                var address = "127.0.0.1";
                var port = 11000;
                var r = new Random();

                var client = new TcpSocketClient();
                await client.ConnectAsync(address, port);

                // we're connected!
                for (int i = 0; i < 5; i++)
                {
                    // write to the 'WriteStream' property of the socket client to send data
                    var nextByte = (byte)r.Next(0, 254);
                    client.WriteStream.WriteByte(nextByte);
                    await client.WriteStream.FlushAsync();

                    // wait a little before sending the next bit of data
                    await Task.Delay(TimeSpan.FromMilliseconds(500));
                }

                await client.DisconnectAsync();
                */


                /*
                var port = 11000;//11000;//3000;
                // Establish the remote endpoint for the socket.
                // The name of the
                // remote device is "host.contoso.com".
                IPHostEntry ipHostInfo = Dns.GetHostEntry("host.contoso.com");
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);


                // Create a TCP/IP socket.  
                socket = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                //socket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), socket);
                //connectDone.WaitOne();
                */
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
            return true;
        }

        public async Task<bool> StopSocket()
        {
            try
            {
                client.GetStream().Close();
                client.Close();
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
            return true;
        }

        private void clientReceive()
        {
            try
            {
                stream = client.GetStream(); //Gets The Stream of The Connection
                new Thread(() => // Thread (like Timer)
                {
                    while ((i = stream.Read(datalength, 0, 4)) != 0)//Keeps Trying to Receive the Size of the Message or Data
                    {
                        // how to make a byte E.X byte[] examlpe = new byte[the size of the byte here] , i used BitConverter.ToInt32(datalength,0) cuz i received the length of the data in byte called datalength :D
                        byte[] data = new byte[BitConverter.ToInt32(datalength, 0)]; // Creates a Byte for the data to be Received On
                        stream.Read(data, 0, data.Length); //Receives The Real Data not the Size
                        //this.RunOnUiThread(() => this.textReceive.Text = textReceive.Text + (Encoding.ASCII.GetString(data)) + System.Environment.NewLine);
                        reply = Encoding.ASCII.GetString(data);
                    }
                }).Start(); // Start the Thread
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
        }

        public async Task<bool> SendMessage(string message)
        {
            try
            {
                if (client.Connected) // if the client is connected
                {
                    stream = client.GetStream(); //Gets The Stream of The Connection
                    byte[] data; // creates a new byte without mentioning the size of it cuz its a byte used for sending
                    data = Encoding.Default.GetBytes(message); // put the msg in the byte ( it automaticly uses the size of the msg )
                    int length = data.Length; // Gets the length of the byte data
                    byte[] datalength = new byte[4]; // Creates a new byte with length of 4
                    datalength = BitConverter.GetBytes(length); //put the length in a byte to send it
                    stream.Write(datalength, 0, 4); // sends the data's length
                    stream.Write(data, 0, data.Length); //Sends the real data
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
            return true;
        }

        public void clientSend(string msg)
        {
            //try
            //{
            //    stream = client.GetStream(); //Gets The Stream of The Connection
            //    byte[] data; // creates a new byte without mentioning the size of it cuz its a byte used for sending
            //    data = Encoding.Default.GetBytes(msg); // put the msg in the byte ( it automaticly uses the size of the msg )
            //    int length = data.Length; // Gets the length of the byte data
            //    byte[] datalength = new byte[4]; // Creates a new byte with length of 4
            //    datalength = BitConverter.GetBytes(length); //put the length in a byte to send it
            //    stream.Write(datalength, 0, 4); // sends the data's length
            //    stream.Write(data, 0, data.Length); //Sends the real data
            //}
            //catch (Exception ex)
            //{
            //    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            //}
        }
    }
}

