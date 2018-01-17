using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;


namespace IOLaboratorium
{
    public class ZadaniaTAP
    {
        public void start()
        {
            Console.WriteLine("Hello World!");
            ZadaniaTAP test = new ZadaniaTAP();
            Task<XmlDocument> task = test.Zadanie3("http://www.feedforall.com/sample.xml");
            XmlDocument xmlDoc = task.Result;
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                xmlDoc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                Console.WriteLine(stringWriter.GetStringBuilder().ToString());  
            }
            


        }
        #region Zadanie 1(12)
        public struct TResultDataStructure
        {          
            public int A { get; set; }
            public int B { get; set; }
            public TResultDataStructure(int a, int b)
            {
                this.A = a;
                this.B = b;
            }
        }
        public Task<TResultDataStructure> AsyncMethod1(byte[] buffer)
        {
            TaskCompletionSource<TResultDataStructure> tcs = new TaskCompletionSource<TResultDataStructure>();
            Task.Run(() =>
            {
                tcs.SetResult(new TResultDataStructure(1,1));
            });
            return tcs.Task;
        }
        public TResultDataStructure Zadanie1()
        {
            var task = AsyncMethod1(null);
            task.Wait();
            return task.GetAwaiter().GetResult();
        }
        #endregion
        #region Zadanie 2(13)
        private bool zadanie2 = false;
        public bool Z2
        {
            get { return zadanie2; }
            set { zadanie2 = value; }
        }
        public void Zadanie2()
        {
            //ZADANIE 2. ODKOMENTUJ I POPRAW  

            Task.Run(() =>
            {
                Z2 = true;
               
            });
           
        }
        #endregion
        #region Zadanie 3(14)
        public async Task<XmlDocument> Zadanie3(string address)
        {
            WebClient webClient = new WebClient();
            string what = await webClient.DownloadStringTaskAsync(new Uri(address));
            XmlDocument exmldoc = new XmlDocument();
            exmldoc.LoadXml(what);
            return exmldoc;
        }
        #endregion
    }
}
// NOTATKI / WNIOSKI
// 