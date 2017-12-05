using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOLAB
{
    class Zad5
    {
        public void start()
        {
            System.Console.WriteLine("Wynik dla 100:");
            System.Console.WriteLine(sumator(100));

        }
        static Int64 sumator(int ile)
        {

            Int64 wynik = 0;
            Random rnd = new Random();
            List<int> tablica = new List<int>();
            List<AutoResetEvent> events = new List<AutoResetEvent>();

            for (int i = 0; i < ile; i++)
            {
                tablica.Add(rnd.Next(100));
            }


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

                    wynik += smalllist.Sum();

                    evt.Set();
                });

            }
      
            WaitHandle.WaitAll(events.ToArray());

            return wynik;
        }
    }
}
// NOTATKI / WNIOSKI
//TO DO