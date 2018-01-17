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
        int wielkosc = 1000;
        public void start()
        {
            System.Console.WriteLine("Wynik dla 100:");
            System.Console.WriteLine(sumator(wielkosc,100));
        }
        static Int64 sumator(int ile,int fragsize)
        {
            
            Int64 wynik = 0;
            Random rnd = new Random();
            List<int> tablica = new List<int>();
            List<AutoResetEvent> events = new List<AutoResetEvent>();

            for (int i = 0; i < ile; i++)
            {
                tablica.Add(rnd.Next(100));
            }


            var list = new List<List<int>>();
            for (int i = 0; i < tablica.Count; i += fragsize)
                list.Add(tablica.GetRange(i, Math.Min(fragsize, tablica.Count - i)));

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
//Ilość waithandles musi być mniejsza lub równa 64. Jeżeli podzielimy tablice na zbyt dużo wątków przekroczymy limit. Lepszym rozwiązaniem byłoby wykorzystanie Tasków.