using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOLAB
{
    class Zad3
    {
        public void start()
        {
            ThreadPool.QueueUserWorkItem(klient);
            ThreadPool.QueueUserWorkItem(serverclienthandlerino);

            Console.WriteLine("main");
            Thread.Sleep(5000);

        }
        static void klient(Object stateinfo)
        {
            String wiad = "Wiad klienta";
            byte[] message = new ASCIIEncoding().GetBytes(wiad);
            byte[] echo = new ASCIIEncoding().GetBytes("echo");
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            NetworkStream stream = client.GetStream();
            client.GetStream().Write(message, 0, message.Length);
            stream.Read(echo, 0, echo.Length);
            Console.WriteLine("Tu klient");
            writeConsoleMessage("Wysyłam: " + wiad, ConsoleColor.Green);

        }
        static void serverinos(Object stateinfo)
        {


            byte[] buffer = new byte[1024];
            TcpClient client = (TcpClient)stateinfo;
            client.GetStream().Read(buffer, 0, 1024);
            client.GetStream().Write(buffer, 0, buffer.Length);
            client.Close();
            Console.WriteLine("Tu server");
            writeConsoleMessage("Odebralem: " + new ASCIIEncoding().GetString(buffer), ConsoleColor.Red);



        }
        static void serverclienthandlerino(Object stateinfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(serverinos, client);

            }
        }



        static void writeConsoleMessage(string message, ConsoleColor color)
        {

                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();

        }

    }
}
// NOTATKI / WNIOSKI
// Następuje problem z kolejnością i kolorami wypisywanych wiadomości, ponieważ wątki w nie zoorganizowany sposób korzystają ze zmiany koloru tekstu i wypisywania.