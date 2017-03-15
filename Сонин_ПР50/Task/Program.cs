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
            Stack<int> stack = new Stack<int>();
            Console.WriteLine("Введите выражение в постфиксной форме, разделяя пробелами:");
            try
            {
                string[] data = Console.ReadLine().Split(new char[] { ' ' });
                for ( int i =0; i < data.Length; i++)
                {
                    if (data[i] == "+")
                        stack.Push(stack.Pop() + stack.Pop());
                    else if (data[i] == "-")
                    {
                        int data1 = stack.Pop(), data2 = stack.Pop();
                        stack.Push(data2 - data1);
                    }
                    else if (data[i] == "*")
                        stack.Push(stack.Pop() * stack.Pop());
                    else if (data[i] == "/")
                    {
                        int data1 = stack.Pop(), data2 = stack.Pop();
                        stack.Push(data2 / data1);
                    }
                    else if (data[i] == "^")
                    {
                        int data1 = stack.Pop(), data2 = stack.Pop();
                        stack.Push(Convert.ToInt32(Math.Pow(data2, data1)));
                    }
                    
                    else
                        stack.Push(int.Parse(data[i]));
 
                }
                Console.WriteLine("Результат:{0}\r\nДля выхода нажмите любую кнопку...",stack.Pop());
                Console.ReadLine();
            }
            catch {
                Console.WriteLine("Данные введены некоректно!");
            }
        }
    }
}
