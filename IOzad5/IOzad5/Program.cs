using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace IOzad5
{
    class Program
    {

        
        static Int64 sumator(int ile)
        {

            Int64 wynik = 0;

            Random rnd = new Random();

            List<int> tablica = new List<int>();
            for (int i=0;i< ile;i++)
            {
                tablica.Add(rnd.Next(100));
            }


            List<AutoResetEvent> events = new List<AutoResetEvent>();

            int size = 10;

            var list = new List<List<int>>();
            for (int i = 0; i < tablica.Count; i += size)
                list.Add(tablica.GetRange(i, Math.Min(size, tablica.Count - i)));



            foreach (List<int> smalllist in list)
            {

                var evt = new AutoResetEvent(false);
                events.Add(evt);
                ThreadPool.QueueUserWorkItem(delegate
                {
                    // do work
                    wynik+=smalllist.Sum();

                    evt.Set();
                });


            }

         
            WaitHandle.WaitAll(events.ToArray());



            return wynik;
        }


     


        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World");
            System.Console.WriteLine(sumator(100));
           


        }

    }
}
