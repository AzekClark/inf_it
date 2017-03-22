using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    class Program
    {
        static int Max(List<int> list) => list.Max();
        
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            while (true)
            {
                int temp = int.Parse(Console.ReadLine());
                if (list.Count == list.Capacity - 1) list.Capacity++;
                if (temp == 0) break;
                list.Add(temp);
            }
            Console.WriteLine(Max(list));
            Console.ReadLine();
        }
    }
}
