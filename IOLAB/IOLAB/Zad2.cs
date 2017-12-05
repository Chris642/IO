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
    class Zad2
    {
        public void start()
        {
            ThreadPool.QueueUserWorkItem(serverinos);

            ThreadPool.QueueUserWorkItem(klient);
            ThreadPool.QueueUserWorkItem(klient);

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
            Console.WriteLine("Wysyłam: " + wiad);

        }



        static void serverinos(Object stateinfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                byte[] buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, 1024);
                client.GetStream().Write(buffer, 0, buffer.Length);
                client.Close();
                Console.WriteLine("Tu server");
                Console.WriteLine("odebralem: " + new ASCIIEncoding().GetString(buffer));

            }

            



        }
    }
}
// NOTATKI / WNIOSKI
// Server jest w stanie obsługiwać tylko jednego klienta w danym momencie, co oznacza, że reszta albo musi czekac albo nigdy nie zostanie obsłużona.S