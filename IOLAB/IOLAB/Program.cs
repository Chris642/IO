using IOLaboratorium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOLAB
{
    class Program
    {

        static void mainmenu()
        {
            Console.Clear();
            DateTime dat = DateTime.Now;
            Console.WriteLine("\nToday is {0:d} at {0:T}.", dat);
            Console.WriteLine("===================================================");
            Console.WriteLine("=    ====    =========  ===========  =====      ===");
            Console.WriteLine("==  ====  ==  ========  ==========    ====  ===  ==");
            Console.WriteLine("==  ===  ====  =======  =========  ==  ===  ====  =");
            Console.WriteLine("==  ===  ====  =======  ========  ====  ==  ===  ==");
            Console.WriteLine("==  ===  ====  =======  ========        ==  ===  ==");
            Console.WriteLine("==  ===  ====  =======  ========  ====  ==  ====  =");
            Console.WriteLine("==  ====  ==  ========  ========  ====  ==  ===  ==");
            Console.WriteLine("=    ====    =========        ==  ====  ==      ===");
            Console.WriteLine("===================================================");

            Console.WriteLine("IO lab zadania.");
            Console.WriteLine("Kod znajduje się w osobnych klasach, które można znależć w drzewie projektu.");
            Console.WriteLine("Wybierz numer od 1-15 aby uruchomić odpowiadające zadanie.");
            Console.WriteLine("LAB1 \"Pula wątków\"   => zad 1-5");
            Console.WriteLine("LAB2       APM       => zad 6-8");
            Console.WriteLine("LAB3       EAP       => zad 9-11");
            Console.WriteLine("LAB4       TAP       => zad 12-15");


            int wyb = Convert.ToInt32(Console.ReadLine());
            switch (wyb)
            {
                case 1:
                    Console.WriteLine("Zad 1");
                    Zad1 zad1 = new Zad1();
                    zad1.start();
                    break;
                case 2:
                    Console.WriteLine("Zad 2");
                    Zad2 zad2 = new Zad2();
                    zad2.start();
                    break;
                case 3:
                    Console.WriteLine("Zad 3");
                    Zad3 zad3 = new Zad3();
                    zad3.start();
                    break;
                case 4:
                    Console.WriteLine("Zad 4");
                    Zad4 zad4 = new Zad4();
                    zad4.start();
                    break;
                case 5:
                    Console.WriteLine("Zad 5");
                    Zad5 zad5 = new Zad5();
                    zad5.start();
                    break;
                case 6:
                    Console.WriteLine("Zad 6");
                    Zad6 zad6 = new Zad6();
                    zad6.start();
                    break;
                case 7:
                    Console.WriteLine("Zad 7");
                    Zad7 zad7 = new Zad7();
                    zad7.start();
                    break;
                case 8:
                    Console.WriteLine("Zad 8");
                    Zad8 zad8 = new Zad8();
                    zad8.start();
                    break;
                case 12:
                case 13:
                case 14:
                    Console.WriteLine("Zad 12-14 TAP");
                    ZadaniaTAP TAP = new ZadaniaTAP();
                    TAP.start();
                    break;
                case 15:
                    Console.WriteLine("Zad z LAB4");
                    TAPclientserver lab4 = new TAPclientserver();
                    lab4.start();
                    break;
                default:
                    Console.WriteLine("Zadanie nie zaimplementowane lub błędny wybór.");
                    break;
            }
            Console.Write("\nPress any key to continue... ");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            while (true) {
                mainmenu();
                
            }
        }
    }
}
