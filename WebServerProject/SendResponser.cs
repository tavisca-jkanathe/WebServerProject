using EO.Internal;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WebServerProject
{
   public  class SendResponser
    {
        static int iTotBytes = 0;
        static string sResponse = "";
        static string sMyWebServerRoot = "C:\\Users\\jkanathe\\source\\repos\\WebServerProject\\";
        static String sPhysicalFilePath = "";


        public static void SendResponse(string path,Socket mySocket)
        {
            sPhysicalFilePath = sMyWebServerRoot+path;
            Console.WriteLine("TestPath: "+sPhysicalFilePath);
            FileStream fs = new FileStream(sPhysicalFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader reader = new BinaryReader(fs);
            string lines = File.ReadAllText(sPhysicalFilePath, Encoding.UTF8);
            byte[] bytes = new byte[fs.Length];
            int read;
            while ((read = reader.Read(bytes, 0, bytes.Length)) != 0)
            {
                sResponse = sResponse + Encoding.ASCII.GetString(bytes, 0, read);
                iTotBytes = iTotBytes + read;
            }
            reader.Close();
            fs.Close();
            SendHeader(lines, "HTTP/1.1", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3", iTotBytes, " 200 OK", ref mySocket);
           SendToBrowser(bytes, ref mySocket);
        }
        public static void SendHeader(string lines, string sHttpVersion, string sMIMEHeader, int iTotBytes, string sStatusCode, ref Socket mySocket)
        {
            String sBuffer = "";
            
            sBuffer = sBuffer + sHttpVersion + sStatusCode + "\r\n";
            sBuffer = sBuffer + "Server: cx1193719-b\r\n";
            //sBuffer = sBuffer + "Content-Type: " + sMIMEHeader + "\r\n";
            sBuffer = sBuffer + "Accept: " + sMIMEHeader + "\r\n";
            //  sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
            //  sBuffer = sBuffer + "Content-Length: " + iTotBytes + "\r\n\r\n";
            sBuffer = sBuffer  + "\r\n";
          //  sBuffer += lines; 
           // Console.WriteLine("\n " + sBuffer);

            Byte[] bSendData = Encoding.ASCII.GetBytes(sBuffer);
            SendToBrowser(bSendData, ref mySocket);
            Console.WriteLine("Total Bytes : " + iTotBytes.ToString());
            
        }

       
        public static void SendToBrowser(Byte[] bSendData, ref Socket mySocket)
        {
            int numBytes = 0;
            try
            {
                if (mySocket.Connected)
                {
                     mySocket.Send(bSendData) ;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occurred : {0} ", e);
            }
        }
    }
   

}
