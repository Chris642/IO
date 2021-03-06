﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOLAB
{
    class Zad8
    {
        delegate Int64 DelegateType(Int64 argumenty);
        static DelegateType delegateName;
        static Int64 Silniarek(Int64 argumenty)
        {
            if (argumenty < 1)
                return 1;
            else
                return argumenty * Silniarek(argumenty - 1);


        }
        static Int64 Silniaite(Int64 argumenty)
        {

            Int64 result = 1;
            for (Int64 i = 1; i <= argumenty; i++)
            {
                result *= i;
            }
            return result;
        }

        static Int64 Fiborek(Int64 argumenty)
        {

            if ((argumenty == 1) || (argumenty == 2))
                return 1;
            else
                return Fiborek(argumenty - 1) + Fiborek(argumenty - 2);
        }

        static Int64 Fiboite(Int64 argumenty)
        {

            Int64 A = 0;
            Int64 B = 1;

            Int64 wynik = 0;

            if (argumenty < 2) return argumenty;

            for (Int64 i = 2; i <= argumenty; i++)
            {
                wynik = A + B;
                A = B;
                B = wynik;
            }

            return wynik;


        }
        public void start()
        {
            Console.WriteLine("Silnia rekurencyjnie:");
           Int64 argu = 25;
            Stopwatch ticktock = new Stopwatch();
            ticktock.Start();

            delegateName = new DelegateType(Silniarek);
            IAsyncResult ar1 = delegateName.BeginInvoke(argu, null, null);
            Int64 wsilniarek = delegateName.EndInvoke(ar1);

            Console.WriteLine(ticktock.Elapsed);
            Console.WriteLine("Wynik:");
            Console.WriteLine(wsilniarek);

            Console.WriteLine("Silnia iteracyjnie:");
            delegateName = new DelegateType(Silniaite);
            IAsyncResult ar2 = delegateName.BeginInvoke(argu, null, null);
            Int64 wsilniaite = delegateName.EndInvoke(ar2);

            Console.WriteLine(ticktock.Elapsed);

            Console.WriteLine("Wynik:");
            Console.WriteLine(wsilniaite);

            Console.WriteLine("Fibonacci rekurencyjnie:");

            delegateName = new DelegateType(Fiborek);
            IAsyncResult ar3 = delegateName.BeginInvoke(argu, null, null);
            Int64 wfiborek = delegateName.EndInvoke(ar3);

            Console.WriteLine(ticktock.Elapsed);
            Console.WriteLine("Wynik:");
            Console.WriteLine(wfiborek);

            Console.WriteLine("Fibonacci iteracyjnie:");


            delegateName = new DelegateType(Fiboite);
            IAsyncResult ar4 = delegateName.BeginInvoke(argu, null, null);
            Int64 wfiboite = delegateName.EndInvoke(ar4);

            Console.WriteLine(ticktock.Elapsed);

            Console.WriteLine("Wynik:");

            Console.WriteLine(wfiboite);

        }




    }
}
