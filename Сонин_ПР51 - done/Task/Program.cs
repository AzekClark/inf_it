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
                data = data.Remove(data.IndexOf(" "), 1);
            }
            Stack<char> stack = new Stack<char>();
            while (data != "")
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
                    while (temp != '(')
                    {
                        _out += temp + " ";
                        temp = stack.Pop();
                    }
                }
                else
                {
                    while (data != "" && Char.IsDigit(data[0]))
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


        static bool CheckBrackets (string data)
        {
            var braceStack = new Stack<char>();

            foreach (var chr in data)
            {
                if (chr == '(' || chr == '{' || chr == '[')
                {
                    braceStack.Push(chr);
                    continue;
                }

                if (chr != ')' && chr != '}' && chr != ']') continue;

                char brace;

                if (braceStack.Count > 0)
                    brace = braceStack.Pop();
                else
                    return false;

                switch (brace)
                {
                    case '(':
                        if (chr != ')') return false;
                        break;
                    case '{':
                        if (chr != '}') return false;
                        break;
                    case '[':
                        if (chr != ']') return false;
                        break;
                }
            }

            return braceStack.Count == 0;
        }


        static void Main(string[] args)
        {
            bool work = true;
            while (work)
            {

                Stack<int> stack = new Stack<int>();

                Console.WriteLine("Введите выражение в постфиксной форме, разделяя пробелами, или в инфиксной форме:");
                Console.Write("->");
                try
                {
                    string input = Console.ReadLine();
                    bool postfix = false;
                    if ((input.EndsWith("/") || input.EndsWith("+") || input.EndsWith("-") || input.EndsWith("*"))
                          && !(input.Contains("(") || input.Contains(")")))
                        postfix = true;
                    if (!postfix)
                    {
                        
                        Console.Write("Проверка скобок...");
                        if (CheckBrackets(input))
                            Console.WriteLine("пройдена успешно");
                        else
                        {
                            Console.WriteLine("провалена!!");
                            throw new Exception();
                        }
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
                Console.WriteLine("Для продолжения работы нажмите Enter, а для выхода введите 'exit' и нажмите Enter:"); Console.Write("->");
              
                if (Console.ReadLine().ToLower() == "exit") work = false;
                Console.Clear();
            }
        }
    }
}
