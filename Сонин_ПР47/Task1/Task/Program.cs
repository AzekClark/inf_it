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
            List<int> list = new List<int>(100);
            while (true)
            {
                string _s = Console.ReadLine();
                if (_s == "") break;
                int temp = int.Parse(_s);
                if (list.Count == list.Capacity - 1) list.Capacity++;
                list.Add(temp);
            }
            list.Sort();
            for (int i = 0; i < list.Count; i++)
                Console.Write(list[i] + " ");
            
            Console.ReadLine();
        }
    }
}
