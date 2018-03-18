using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillerRabinTest
{
    class Test
    {
        private int n = 0; // liczba sprawdzana
        private int a = 0; // losowa liczba 1<a<n
        private int r = 0; // potęga 2
        private int d = 0; // reszta
        private int k = 3; // określa dokładonść testu

        public Test()
        {

        }
        //konstruktor z liczbą która jest sprawdzana czy jest pierwszą
        public Test(int n)
        {
            this.n = n;
            Console.WriteLine("Liczba: " + this.n);
            Console.WriteLine("Dokładność: " + this.k);
        }

        public Test(int n, int k)
        {
            this.n = n;
            this.k = k;
            Console.WriteLine("Liczba: " + this.n);
            Console.WriteLine("Dokładność: " + this.k);
        }

        //wyznaczanie największej potęgi 2 i wyniku dzielenia s = 2^s *d
        public void CountPower()
        {
            double tmp = 0;

            while (true)
            {
                this.r++;
                //Console.WriteLine(this.r);
                this.d = (int)tmp;
                tmp = (double)(this.n - 1) / Math.Pow(2, this.r);

                if (tmp % 1 != 0 && r > 1)
                {
                    Console.WriteLine("d = " + this.d);
                    this.r--;
                    break;
                }
            }
            Console.WriteLine("r = " + this.r);
        }

        /* funkcja naiwnego potęgowania modulo - potrzebna bo przy zwykłym potęgowaniu z biblioteki 
         * Math liczby wychodzą poza zakres i nie działa
         */
        private static int PowerModulo(int a, int b, int m)
        {
            long result = 1;
            long x = a % m;

            for (int i = 1; i <= b; i <<= 1)
            {
                x %= m;
                if ((b & i) != 0)
                {
                    result *= x;
                    result %= m;
                }
                x *= x;
            }

            return (int)result;
        }

        public void CheckNumber()
        {
            float x = 0;

            int prime = 0;
            bool checkBreak = false;
            Random rand = new Random();


            for (int i = 0; i < this.k; i++)
            {
                Console.WriteLine();
                this.a = rand.Next(2, n);
                Console.WriteLine("a = " + this.a);

                x = (float) PowerModulo(this.a, this.d, this.n) % this.n;
                Console.WriteLine("X = " + x);

                if (x == 1 || x == n-1)
                {
                    //Console.WriteLine("CONTINUE");
                    continue;
                }

                for (int j = 0; j < this.r; j++)
                {
                    x = (float) Math.Pow(x, 2) % n;
                    Console.WriteLine("==> X = " + x);
                    if (x == 1)
                    {
                        Console.WriteLine("Liczba złożona.");
                        return;
                    }
                    else if (x == n - 1)
                    {
                        //Console.WriteLine("BREAK");
                        checkBreak = true;
                        break;
                    }
                }
                if (!checkBreak)
                {
                    Console.WriteLine("Liczba złożona. - poza pętlą");
                    return;
                }
            }
            Console.WriteLine("Prawdopodobnie liczba pierwsza - poza pętlą.");
            return;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int n = 0, k = 0;

            Console.WriteLine("Podaj liczbę:");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("Podaj dokładność sprawdzenia:");
            k = int.Parse(Console.ReadLine());

            Test test = new Test(n, k);
            test.CountPower();
            test.CheckNumber();

            Console.ReadKey();
        }
    }
}
