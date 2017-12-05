using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOLAB
{
    class Zad6
    {
        public void start()
        {

            FileStream fs = new FileStream("c://text/text.txt", System.IO.FileMode.Open);
            byte[] buffer = new byte[1024];
            var evt = new AutoResetEvent(false);
            fs.BeginRead(buffer, 0, buffer.Length, myAsyncCallback, Tuple.Create(fs, buffer, evt));
            evt.WaitOne();

        }
        static void myAsyncCallback(IAsyncResult nazwa)
        {

            Tuple<FileStream, byte[], AutoResetEvent> state = (Tuple<FileStream, byte[], AutoResetEvent>)nazwa.AsyncState;
            byte[] bufor = state.Item2;

            Console.WriteLine(System.Text.Encoding.UTF8.GetString(bufor));
            FileStream fs = state.Item1;
            var evt = state.Item3;
            fs.Close();
            evt.Set();

        }



    }
}
// NOTATKI / WNIOSKI
// W tym wypadku czeka dzięki WaitOne() po zakończeniu operacji wywoływany jest Callback