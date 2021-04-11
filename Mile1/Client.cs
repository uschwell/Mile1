
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{

    class Client
    {

        // Main Method
        /*
        static void Main(string[] args)
        {
            ExecuteClient();
        }
        */

        // ExecuteClient() Method
        static void ExecuteClient()
        {

            try
            {

                // Establish the sockets endpoint  (Currently: port 11111 on local computer)
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

                // Create TCP/IP Socket via Constructor
                Socket sender = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

                try
                {

                    // Connect Socket to the endpoint with Connect()
                    sender.Connect(localEndPoint);

                    // We print that we are connected
                    Console.WriteLine("Socket connected to -> {0} ",
                                  sender.RemoteEndPoint.ToString());

                    // Creation message that we will send to Server
                    byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");
                    int byteSent = sender.Send(messageSent);

                    // Data buffer
                    byte[] messageReceived = new byte[1024];

                    // We receive the message via Receive(). 
                    //This returns the number of bytes
                    // received, that we'll use to 
                    // convert them to string
                    int byteRecv = sender.Receive(messageReceived);
                    Console.WriteLine("Message from Server -> {0}",
                          Encoding.ASCII.GetString(messageReceived,
                                                     0, byteRecv));

                    // Close Socket using 
                    // the method Close()
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }

                // Possible exceptions for Socket
                catch (ArgumentNullException ane)
                {

                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }

                catch (SocketException se)
                {

                    Console.WriteLine("SocketException : {0}", se.ToString());
                }

                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}