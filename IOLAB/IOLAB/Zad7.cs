using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOLAB
{
    class Zad7
    {
        public void start()
        {
            FileStream fs = new FileStream("c://text/text.txt", System.IO.FileMode.Open);
            byte[] buffer = new byte[1024];
            var evt = new AutoResetEvent(false);
            var magic = Tuple.Create(fs, buffer, evt);
            IAsyncResult result = fs.BeginRead(buffer, 0, buffer.Length, null, magic);
            fs.EndRead(result);
            Tuple<FileStream, byte[], AutoResetEvent> wynik = (Tuple<FileStream, byte[], AutoResetEvent>)result.AsyncState;

            byte[] bufor = wynik.Item2;


            Console.WriteLine(System.Text.Encoding.UTF8.GetString(bufor));
            FileStream fswynik = wynik.Item1;
            fs.Close();
            evt.Set();

        }


       

    }
}
// NOTATKI / WNIOSKI
// Wątek czeka, jeżeli operacja czytania potrwa dłuższy czas wciąż możemy wykonywac operacje do czasu uzyskania wyniku, co jest lepszym rozwiązaniem niż robienie wszystkiego po kolei.