using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace WebServerProject
{
    public class CallClient
    {
        public static void CallingClient()
        {
            for(int t=0;t<2;t++)
            {
                Socket mySocket = WebServerProject.WebServer.myListener.AcceptSocket();
                Console.WriteLine("Socket Type " + mySocket.SocketType);
            
                if (mySocket.Connected)
                {

                    Console.WriteLine("\n{0} Client is now Connected\n", mySocket.RemoteEndPoint);
                    Byte[] bReceive = new Byte[1024];
                    int i = mySocket.Receive(bReceive, bReceive.Length, 0);
                    string sBuffer = Encoding.ASCII.GetString(bReceive);
                    Console.WriteLine(sBuffer);
                    string path = "";
                    int j;
                    for (j = 5; sBuffer[j] != ' '; j++)
                    {
                        path += sBuffer[j];
                    }
                    SendResponser.SendResponse(path, mySocket);
                    mySocket.Close();
                }
            }
        }
    }
}

