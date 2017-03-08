using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    static class Simple
    {
        public static bool isPrime(int number)
        {
            int k = 2;
            while (k * k <= number)
            {
                if (number % k == 0)
                    return false;
                k++;
            }
            return true;
        }
        public static List<int> Cycle(int number)
        {
            List<int> _simple = new List<int>();
            for (int i = 2; i <= number; i++)
                if (isPrime(i))
                    _simple.Add(i);
            return _simple;
        }
        public static List<int> Eratospen(int number)
        {

            List<bool> ArSimple = new List<bool>(number);
            for (int j = 0; j < number + 1; j++)
                ArSimple.Add(true);
            int k = 2, i = 0;
            while (k * k <= number)
            {
                if (ArSimple[k])
                {
                    i = k * k;
                    while (i <= number)
                    {
                        ArSimple[i] = false;
                        i += k;
                    }
                }
                k++;
            }
            List<int> _simple = new List<int>(ArSimple.Count);
            for (int j = 2; j < ArSimple.Count; j++)
                if (ArSimple[j])
                    _simple.Add(j);
            return _simple;

        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = Simple.Eratospen(int.Parse(Console.ReadLine()));
            for ( int i =0; i < list.Count; i++)
            {
                Console.Write(list[i] + " ");
            }
            Console.ReadLine();
        }
    }
}
