using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using E2tima.Graphics;
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace Task
{
    class Program
    {
        
        [STAThread]
        static void Main(string[] args)
        {
            Queue queue = new Queue();
            bool working = true;

            while (working)
            {
                Console.WriteLine("Список доступных команд:");
                Console.WriteLine("1->Заливка области из файла");
                Console.WriteLine("2->Заливка области из bmp");
                Console.Write("Ваш выбор:");

                switch (Console.ReadLine().ToLower())
                {
                    
                    case "1":
                    case "заливка области из файла":
                        working = false;
                        Console.Clear();
                        TaskwithFile();
                        break;
                    case "2":
                    case "заливка области из bmp":
                        working = false;
                        Console.Clear();
                        TaskwithBMP();
                        break;
                    
                }

               
                if (working)
                    Console.Write("Неверный ввод! ");
                else
                    working = true;
                
                Console.WriteLine("Для выхода нажмите ESC...");
                
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    working = false;

                Fill();
            }
        }


        static void Fill()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            for (int i = 0; i < Console.WindowHeight; i++)
                for (int j = 0; j < Console.BufferWidth; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write("\u2588");
                    Thread.Sleep(1);
                }
            Console.Clear();
            Console.CursorVisible = true;
        }

        static void TaskwithBMP()
        {

        }
            
        static string GetFile(string Caption, string extension = "")
        {
            bool working = true;
            string _out = null;
            while (working)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine(Caption);
                Console.WriteLine(" Варианты пути:");
                Console.WriteLine("     Полный:C:\\folder\\file.file_extention");
                Console.WriteLine("     Из папки с программой:\\files\\file.file_extention");
                Console.WriteLine("     Открыть файловый диалог, написав 'dialog'");
                Console.Write("->");
                Console.CursorVisible = true;
                _out = Console.ReadLine();
                if (_out == "dialog" )
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Multiselect = false;
                    dialog.SupportMultiDottedExtensions = true;
                    dialog.CheckFileExists = true;
                    dialog.Title = "Выбор файла";
                    if (extension != "")
                    {
                        dialog.Filter = extension;
                    }
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        _out = dialog.FileName;
                        break;
                    }
                    
                }
               
                if (File.Exists(_out)) break ;
                else
                {
                    _out = null;
                    Console.CursorVisible = false;
                    Console.WriteLine("Неверно введен путь! Для выхода нажмите ESC...");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        break;

                }
            }
            return _out;
        }

        static void TaskwithFile()
        {
            string file = GetFile("Введите путь до файла с 'матрицей':");


        }
    }
}
