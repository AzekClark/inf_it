using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    class Program
    {
        static int TSort(List<int> list, bool Max = true)
        {
            list.Sort();
            if (Max)
                return list[list.Count - 1];
            else
                return list[0];
        }

        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            while (true)
            {
                string _s = Console.ReadLine();
                if (_s == "") break;

                int temp = int.Parse(_s);
                if (list.Count == list.Capacity - 1) list.Capacity++;
               
                list.Add(temp);
            }
            Console.WriteLine($"Максимальный:{TSort(list)}\r\nМинимальный:{TSort(list,false)}");
            Console.ReadLine();
        }
    }
}
