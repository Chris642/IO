using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOLAB
{
    class Zad1
    {
        public void start()
        {

            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] { 100 });
            Thread.Sleep(1000);

        }



        static void ThreadProc(Object stateInfo)
        {
            var time = ((object[])stateInfo)[0];
            Thread.Sleep((int)time);
            Console.WriteLine("I HAVE WAITED FOR " + (int)time +" ms");
        }
    }
}
// NOTATKI / WNIOSKI
// Wykorzystywanie Sleep jest złą pratkyką z powodu nie przewidywalności. Powinno się wykorzystywać przystosowane do tego narzędzia synhcronizacyjne.