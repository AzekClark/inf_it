using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    class Program
    {

        static string ToPostfics(string data)
        {
            string _out = ""; 
            while (data.Contains(" "))
            {
                data = data.Remove(data.IndexOf(" "),1);
            }
            Stack<char> stack = new Stack<char>();
            while ( data != "")
            {
                if (data.StartsWith("("))
                {
                    data = data.Remove(0, 1);
                    stack.Push('(');

                }
                if (data.StartsWith("+"))
                {
                    data = data.Remove(0, 1);
                    stack.Push('+');

                }
                if (data.StartsWith("-"))
                {
                    data = data.Remove(0, 1);
                    stack.Push('-');

                }
                if (data.StartsWith("*"))
                {
                    data = data.Remove(0, 1);
                    stack.Push('*');

                }
                if (data.StartsWith("/"))
                {
                    data = data.Remove(0, 1);
                    stack.Push('/');

                }
                if (data.StartsWith(")"))
                {
                    data = data.Remove(0, 1);
                    char temp = stack.Pop();
                    while(temp != '(')
                    {
                        _out += temp + " ";
                        temp = stack.Pop();
                    }
                }
                else
                {
                    while( data != "" && Char.IsDigit(data[0]))
                    {
                        _out += $"{data[0]}";
                        data = data.Remove(0, 1);
                    }
                    _out += " ";
                }
            }
            if (stack != null) _out += stack.Pop();
            return _out;
        }



        static void Main(string[] args)
        {
            bool work = true;
            while (work)
            {

                Stack<int> stack = new Stack<int>();

                Console.WriteLine("Введите выражение в постфиксной форме, разделяя пробелами, или инфиксной форме:");
                try
                {
                    string input = Console.ReadLine();
                    bool postfix = false;
                    if ((input.EndsWith("/") || input.EndsWith("+") || input.EndsWith("-") || input.EndsWith("*"))
                          && !(input.Contains("(") || input.Contains(")")))
                         postfix = true;
                    if (!postfix)
                    {
                        Console.WriteLine("Выражение было запсано в инфиксной форме, преобразование, подождите...");
                        input = ToPostfics(input);
                        Console.WriteLine($"Результат преобразования:{input}");
                    }
               
                    string[] data = input.Split(new char[] { ' ' });
                    for (int i = 0; i < data.Length; i++)
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
                    Console.WriteLine($"Результат подсчета:{stack.Pop()}");
                   
                }
                catch
                {
                    Console.WriteLine("Данные введены некоректно!");
                }
                Console.WriteLine("Для выхода нажмите Enter, а для продолжения введите что-нибудь и нажмите Enter:");
                if (Console.ReadLine() == "") work = false;
                Console.Clear();
            }
        }
    }
}
