using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
           static private Object thisLock = new Object();  

        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(klient);
            ThreadPool.QueueUserWorkItem(serverclienthandlerino);

            Console.WriteLine("main");
            Thread.Sleep(5000);



        }


        static void ThreadProc(Object stateinfo, int time)
        {
            Thread.Sleep(time);
            Console.WriteLine("I WAIT FOR" + time);
        }

        static void klient(Object stateinfo)
        {
            String wiad = "fdgdfgdf";
            byte[] message = new ASCIIEncoding().GetBytes(wiad);
            byte[] echo= new ASCIIEncoding().GetBytes("echo");
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            NetworkStream stream = client.GetStream();
            client.GetStream().Write(message, 0, message.Length);
            stream.Read(echo, 0, echo.Length);
            Console.WriteLine("klient");
            writeConsoleMessage("Wysyłam: "+ wiad,ConsoleColor.Green);

        }

        static void serverinos(Object stateinfo)
        {

           
                byte[] buffer = new byte[1024];
                TcpClient client = (TcpClient)stateinfo;
                client.GetStream().Read(buffer, 0, 1024);
                client.GetStream().Write(buffer, 0, buffer.Length);
                client.Close();
                Console.WriteLine("server");
                writeConsoleMessage("odebralem: " + new ASCIIEncoding().GetString(buffer), ConsoleColor.Red);



        }
        static void serverclienthandlerino(Object stateinfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(serverinos,client);
               
            }
        }



        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            lock (thisLock)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }

    }


}
