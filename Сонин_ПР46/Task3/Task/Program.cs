using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(1);
            int count = int.Parse(Console.ReadLine()), i =2;
            while ( list.Count <= count )
            {
                if (list.Count == list.Capacity - 1)
                    list.Capacity++;
                list.Add(list[i - 1] + list[i - 2]);
                i++;
            }
            for (int j = 0; j < list.Count; j++)
                Console.Write(list[j] + " ");
            Console.ReadLine();
        }
    }
}
