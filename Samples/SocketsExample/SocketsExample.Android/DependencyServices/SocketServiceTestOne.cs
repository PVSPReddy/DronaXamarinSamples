using System;
using System.Threading.Tasks;
using SocketsExample.Droid;
using Xamarin.Forms;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

[assembly : Dependency(typeof(SocketServiceTestOne))]
namespace SocketsExample.Droid
{
    public class SocketServiceTestOne : ISocketTestOne
    {
        public SocketServiceTestOne() { }

        string reply = "";

        Socket socket;

        int i;
        TcpClient client; // Creates a TCP Client
        NetworkStream stream; //Creats a NetworkStream (used for sending and receiving data)
        byte[] datalength = new byte[4]; // creates a new byte with length 4 ( used for receivng data's lenght)


        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        // The response from the remote device.  
        private static string response = String.Empty;

        public async Task<bool> InitalizeSocket()
        {
            try
            {
                var address = "nodetestchat.herokuapp.com";//https://nodetestchat.herokuapp.com
                var port = 443;//11000;//3000;

                IPHostEntry ipHostInfo = Dns.GetHostEntry(address);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);


                // Create a TCP/IP socket.  
                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), socket);

                connectDone.WaitOne();

                // Send test data to the remote device.  
                Send(socket, "This is a test<EOF>");
                sendDone.WaitOne();

                // Receive the response from the remote device.  
                Receive(socket);
                receiveDone.WaitOne();

                // Write the response to the console.  
                Console.WriteLine("Response received : {0}", response);

                // Release the socket.  
                socket.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception ex)
            {
                var msg = ex.Message + "\n" + ex.StackTrace;
            }
            return true;
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket   
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
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
    // State object for receiving data from remote device.  
    public class StateObject
    {
        // Client socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 256;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }
}
