using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad8
{
    class Program
    {

        delegate Int64 DelegateType(Int64 arguments);
        static DelegateType delegateName;
        static Int64 Silniarek(Int64 arguments)
        {
            if (arguments < 1)
                return 1;
            else
                return arguments * Silniarek(arguments - 1);

            
        }
        static Int64 Silniaite(Int64 arguments)
        {

            Int64 result = 1;
            for (Int64 i = 1; i <= arguments; i++)
            {
                result *= i;
            }
            return result;
        }

        static Int64 Fiborek(Int64 arguments)
        {

            if ((arguments == 1) || (arguments == 2))
                return 1;
            else
                return Fiborek(arguments - 1) + Fiborek(arguments - 2);
        }

        static Int64 Fiboite(Int64 arguments)
        {

            Int64 A = 0;
            Int64 B = 1;

            Int64 wynik = 0; 

            if (arguments < 2) return arguments; 
            
            for (Int64 i = 2; i <= arguments; i++)
            {
                wynik = A + B; 
                A = B; 
                B = wynik;  
            }

            return wynik; 


        }
        static void Main(string[] args)
        {
            Int64 argu = 25;
            Stopwatch ticktock = new Stopwatch();
            ticktock.Start();

            delegateName = new DelegateType(Silniarek);
            IAsyncResult ar1 = delegateName.BeginInvoke(argu, null, null);
            Int64 wsilniarek = delegateName.EndInvoke(ar1);

            Console.WriteLine(ticktock.Elapsed);


            delegateName = new DelegateType(Silniaite);
            IAsyncResult ar2 = delegateName.BeginInvoke(argu, null, null);
            Int64 wsilniaite = delegateName.EndInvoke(ar2);

            Console.WriteLine(ticktock.Elapsed);

            delegateName = new DelegateType(Fiborek);
            IAsyncResult ar3 = delegateName.BeginInvoke(argu, null, null);
            Int64 wfiborek = delegateName.EndInvoke(ar3);

            Console.WriteLine(ticktock.Elapsed);


            delegateName = new DelegateType(Fiboite);
            IAsyncResult ar4 = delegateName.BeginInvoke(argu, null, null);
            Int64 wfiboite = delegateName.EndInvoke(ar4);

            Console.WriteLine(ticktock.Elapsed);


            Console.WriteLine(wsilniarek);
            Console.WriteLine(wsilniaite);
            Console.WriteLine(wfiborek);
            Console.WriteLine(wfiboite);


        }
    }
}
