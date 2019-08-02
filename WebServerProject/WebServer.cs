using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WebServerProject
{

    public class WebServer
    {
        public static TcpListener myListener;
        private int port = 10000;
        public WebServer()
        {
           
                try
                {
                    myListener = new TcpListener(port);
                    myListener.Start();
                    Console.WriteLine("Web Server Started... ");
            
                    Thread th = new Thread(new ThreadStart(CallClient.CallingClient));
                    th.Start();
                
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            
        }
    }
}
