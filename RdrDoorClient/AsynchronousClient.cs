using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using PacketFormat;

namespace SerialLogger
{
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






    public  class AsynchronousClient
    {
        // The port number for the remote device.
        private const int port = 32626;

        private static IPAddress ipAddress;
        private static IPEndPoint remoteEP;
        private static Socket client;

        // ManualResetEvent instances signal completion.
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        // The response from the remote device.
        private static String response = String.Empty;



        public AsynchronousClient()
        {
            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.

                ipAddress = IPAddress.Parse("127.0.0.1");
                //ipAddress = IPAddress.Parse("135.254.167.145");
                remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();
            }
            catch (Exception e)
            {
                applog.logexcep("AsynchronousClient",e.ToString());
               
            }
        }

        public static bool pingserver()
        {
            RdrMsgFormat sndmsg = new RdrMsgFormat();
            try
            {
                sndmsg.msgid = message_id.ping;
                sndmsg.key = message_key.pingkey;
                SendMsgtoServer(sndmsg);
                // Send test data to the remote device.
                //Send("Hai how are you <EOF>");
                sendDone.WaitOne();
                //MessageBox.Show("Message Sent");
                // Receive the response from the remote device.
                Receive();
                receiveDone.WaitOne();

                
                // Write the response to the console.
                applog.loggen(string.Format("Response received : {0}", response));
                
                return true;
            }
            catch (Exception e)
            {
                applog.logexcep("pingserver", e.ToString());
                return false;
            }
        }


        ~AsynchronousClient()
        {
            try
            {

                // Release the socket.
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception e)
            {
                applog.logexcep("~AsynchronousClient", e.ToString());
            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket lclient = (Socket)ar.AsyncState;

                // Complete the connection.
                lclient.EndConnect(ar);

                applog.loggen(string.Format("Socket connected to {0}",
                    lclient.RemoteEndPoint.ToString()));

                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception e)
            {
                applog.logexcep("ConnectCallback", e.ToString());
            }
        }

        private static void ReceiveMsgfromServer()
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(RcvMsgHandler), state);
            }
            catch (Exception e)
            {
                applog.logexcep("Receive", e.ToString());
            }
        }

        private static void RcvMsgHandler(IAsyncResult ar)
        {
            try
            {
                String content = String.Empty;
                RdrMsgFormat rcvmsg = new RdrMsgFormat();
                // Retrieve the state object and the handler socket
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;

                // Read data from the client socket. 
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    rcvmsg = rcvmsg.GetPacketObject(state.buffer);

                    switch (rcvmsg.msgid)
                    {
                        case message_id.ping_rply:

                            if (rcvmsg.key == 0x11223344)
                                MessageBox.Show("Received ping sucessfully");
                            // Signal that all bytes have been received.
                            receiveDone.Set();
                            break;
                        case message_id.testmsg:

                            break;
                        default:

                            break;
                    }
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(RcvMsgHandler), state);
                }
            }
            catch (Exception e)
            {
                applog.logexcep("RcvMsgHandler", e.ToString());
            }

        }


        private static void Receive()
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(RcvMsgHandler), state);
            }
            catch (Exception e)
            {
                applog.logexcep("Receive", e.ToString());
            }
        }


        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                Socket lclient = state.workSocket;

                // Read data from the remote device.
                int bytesRead = lclient.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.
                    lclient.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
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
                applog.logexcep("ReceiveCallback", e.ToString());
            }
        }




        private static void SendMsgtoServer(RdrMsgFormat data)
        {


            byte[] byteData = data.GetPacketBytes();


            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void Send(String data)
        {
          try
            {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }
        catch (Exception e)
        {
            applog.logexcep("SendCallback", e.ToString());
        }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket lclient = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = lclient.EndSend(ar);
                //MessageBox.Show(string.Format("Sent {0} bytes to server.", bytesSent));
                applog.loggen(string.Format("Sent {0} bytes to server.", bytesSent));

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                applog.logexcep("SendCallback", e.ToString());
            }
        }

    }
}
