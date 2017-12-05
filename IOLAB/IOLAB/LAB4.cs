using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IOLAB
{
    class LAB4
    {
        public void start()
        {
            Server s = new Server();
            s.Run();
            try
            {
                //s.Port = 120;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Client klient = new Client();

            klient.run();
            klient.run();
            klient.run();
            Task.WaitAll(s.ServerTask);
        }
        class Client
        {
            private TcpClient client;
            int port;
            IPAddress address;
            bool running = false;
            

            //public Task ClientTask { get => ClientTask; }
            public bool Running { get => running; }
            public IPAddress Address { get => address; set { if (!running) address = value; else throw new Exception("Proba zmiany adresu IP w trakcie dzialania serwera."); } }

            public int Port { get => port; set { if (!running) port = value; else throw new Exception("Proba zmiany portu w trakcie dzialania serwera."); } }


            public Client()
            {
                client = new TcpClient();
            }

            public void run()
            {
                Console.WriteLine("Client Start");
                //Task.WaitAll(clientTask());
            }
            async Task ClientTask()
            {
                byte[] buffer = new byte[1024];

                client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
                byte[] message = new ASCIIEncoding().GetBytes("Hello");

                client.GetStream().Write(message, 0, message.Length);

            }

        }


        class Server
        {
            TcpListener server;
            int port;
            IPAddress address;
            bool running = false;
            Task serverTask;

            public Task ServerTask { get => serverTask; }
            public bool Running { get => running; }
            public IPAddress Address { get => address; set { if (!running) address = value; else throw new Exception("Proba zmiany adresu IP w trakcie dzialania serwera."); } }

            public int Port { get => port; set { if (!running) port = value; else throw new Exception("Proba zmiany portu w trakcie dzialania serwera."); } }

            #region konstruktory

            public Server(int port = 2048, string address = "127.0.0.1")
            {
                this.Address = IPAddress.Parse(address);
                this.Port = port;
                server = new TcpListener(this.Address, port);
            }

            public Server(IPAddress address, int port = 2048)
            {
                this.Port = port;
                this.Address = address;
                server = new TcpListener(this.Address, port);
            }

            #endregion
            public void Run()
            {

                serverTask = runAsync();
            }

            async Task runAsync()
            {
                try
                {
                    server.Start();
                    running = true;
                }
                catch
                {
                    throw new Exception("todo");
                }
                while (true)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    byte[] buffer = new byte[1024];
                    await client.GetStream().ReadAsync(buffer, 0, buffer.Length).ContinueWith(
                        async (t) =>
                        {
                            int i = t.Result;
                            while (true)
                            {
                                Console.WriteLine("server");
                                Console.WriteLine(Encoding.ASCII.GetString(buffer));
                                client.GetStream().WriteAsync(buffer, 0, i);
                                i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                            }
                        });
                }
            }
        }
    }
}
// NOTATKI / WNIOSKI
//TO DO