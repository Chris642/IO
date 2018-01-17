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
    class TAPclientserver
    {
        public void start()
        {
            Server s = new Server();
            s.Run();
            Client c1 = new Client();
            Client c2 = new Client();
            Client c3 = new Client();
            c1.run();
            c2.run();
            c3.run();
            CancellationTokenSource ct1 = new CancellationTokenSource();
            CancellationTokenSource ct2 = new CancellationTokenSource();
            CancellationTokenSource ct3 = new CancellationTokenSource();
            var client1T = c1.keepPinging("message", ct1.Token);
            var client2T = c2.keepPinging("message", ct2.Token);
            var client3T = c3.keepPinging("message", ct3.Token);
            ct1.CancelAfter(2000);
            ct2.CancelAfter(3000);
            ct3.CancelAfter(4000);
            Task.WaitAll(new Task[] { client1T, client2T, client3T });
            s.Stop();







            //Server s = new Server();
            //s.Run();
            //try
            //{
            //    s.Port = 660;
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Console.WriteLine("Błąd");

            //}

            //Client klient = new Client();

            //klient.run();
            //klient.run();
            //klient.run();
            //Task.WaitAll(s.ServerTask);
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
                running = true;
                Console.WriteLine("Client Start");
                //Task.WaitAll(clientTask());
                client.Connect(IPAddress.Parse("127.0.0.1"), 2048);
            }
            public void Stop()
            {
                running = false;
                client.Close();
            }
            //async Task ClientTask()
            //{
            //    byte[] buffer = new byte[1024];

            //    client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            //    byte[] message = new ASCIIEncoding().GetBytes("Hello");

            //    client.GetStream().Write(message, 0, message.Length);

            //}
            public async Task<string> Ping(string message)
            {
                byte[] buffer = new ASCIIEncoding().GetBytes(message);
                await client.GetStream().WriteAsync(buffer, 0, buffer.Length);
                byte[] plsrespond = new byte[1024];
                var t = await client.GetStream().ReadAsync(plsrespond, 0, plsrespond.Length);
                Console.WriteLine(Encoding.UTF8.GetString(plsrespond, 0, t));
                return Encoding.UTF8.GetString(plsrespond, 0, t);
            }
            public async Task<IEnumerable<string>> keepPinging(string message, CancellationToken token)
            {
                List<string> messages = new List<string>();
                while (running)
                {
                    if (token.IsCancellationRequested)
                        running = false;
                    Thread.Sleep(500);
                    messages.Add(await Ping(message));
                }
                return messages;
            }
        }


        class Server
        {
            TcpListener server;
            int port;
            IPAddress address;
            bool running = false;
            Task serverTask;
            CancellationTokenSource cancelToken = new CancellationTokenSource();

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
                server.Start();
                running = true;
                serverTask = runAsync();
            }

            public void Stop()
            {
                cancelToken.Cancel();
                server.Stop();
                running = false;
            }

            async Task runAsync()
            {
                //try
                //{
                //    server.Start();
                //    running = true;
                //}
                //catch
                //{
                //    throw new Exception("todo");
                //}
                CancellationToken token = cancelToken.Token;
                while (!token.IsCancellationRequested)
                {
                        TcpClient client = await server.AcceptTcpClientAsync();
                        byte[] buffer = new byte[1024];
                        await client.GetStream().ReadAsync(buffer, 0, buffer.Length).ContinueWith(
                            async (t) =>
                            {
                                int i = t.Result;
                                while (true)
                                {
                                    await client.GetStream().WriteAsync(buffer, 0, i);
                                    try
                                    {
                                        i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                                    }
                                    catch
                                    {
                                        break;
                                    }
                                    Console.WriteLine("server");
                                    Console.WriteLine(Encoding.ASCII.GetString(buffer));                                   
                                }
                            });
                    
                }
            }
        }
    }
}
// NOTATKI / WNIOSKI
//