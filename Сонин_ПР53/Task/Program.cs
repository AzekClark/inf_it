using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Task
{
    class Program
    {
        static Tree Custom ()
        {
            Tree tree = new Tree();
            tree.Data = "*";
            tree.Left = new Tree();
            tree.Left.Left = new Tree();
            tree.Left.Right = new Tree();
            tree.Right = new Tree();
            tree.Right.Left = new Tree();
            tree.Right.Right = new Tree();
            tree.Left.Data = "+";
            tree.Left.Left.Data = "1";
            tree.Left.Right.Data = "4";
            tree.Right.Data = "-";
            tree.Right.Left.Data = "9";
            tree.Right.Right.Data = "5";
            return tree;
        }

        static void Fill()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            for (int i = 0; i < Console.WindowHeight; i++)
                for (int j = 0; j < Console.BufferWidth; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write($"{(char)(new Random().Next(15,40))}");
                    Thread.Sleep(1);
                }
            Console.Clear();
            Console.CursorVisible = true;
        }

        static void Main(string[] args)
        {
            bool working = true;

            while (working)
            {
                Console.Write("Введите выражение:");

                Tree tree = null;
                try
                {
                    tree = TreeWorker.MakeTree(Console.ReadLine());
                    Console.WriteLine($"Результат:{TreeWorker.Calculate(tree)}");
                    Console.WriteLine($"Префиксная форма:{TreeWorker.ToPreficsForm(tree)}");
                    Console.WriteLine($"Постфиксная форма:{TreeWorker.ToPostficsForm(tree)}");
                    Console.WriteLine($"Обход в ширину:{TreeWorker.ToWidhtForm(tree)}");
                }
                catch (Exception exp)
                {
                    Console.WriteLine("Ошибка!");
                    Console.WriteLine("Причина:{0} и мои кривые руки ;)", exp.Message);
                    Console.WriteLine("Подробности:");
                    Console.WriteLine(exp.StackTrace);
                }

                Console.WriteLine("Для выхода нажмите ESC...");

                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    working = false;

                Fill();
            }
        }
    }
}
