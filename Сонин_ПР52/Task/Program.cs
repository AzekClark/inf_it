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
using System.Reflection;

namespace Task
{
    class Program
    {
        
        [STAThread]
        static void Main(string[] args)
        {
            bool working = true;

            while (working)
            {
                Console.WriteLine("Список доступных команд:");
                Console.WriteLine("1->Заливка области из файла");
                Console.WriteLine("2->Заливка области из bmp");
                Console.Write("Ваш выбор:");

                try
                {
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

                }
                catch (Exception exp)
                {
                    Console.WriteLine("Ошибка!");
                    Console.WriteLine("Причина:{0} и мои кривые руки ;)", exp.Message);
                    Console.WriteLine("Подробности:");
                    Console.WriteLine(exp.StackTrace);
                    
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


        static int AskUser(string message, int minvalue, int maxvalue)
        {
            bool working = true;
            int value = 0;
            message = $"{message} [{minvalue}..{maxvalue}]:";
            while ( working)
            {
               
                Console.Write(message);
                value = int.Parse(Console.ReadLine());
                if (value < minvalue || value > maxvalue)
                {
                    Console.Write("Данные введены неверно! Повторить? [y/n]");
                    switch (Console.ReadLine().ToLower())
                    {
                        case "y":
                            Console.SetCursorPosition(0, Console.CursorTop - 2);
                            for (int i = 0; i < message.Length +8; i++)
                                Console.Write(" ");
                            Console.WriteLine();
                            Console.WriteLine("                                                  ");
                            Console.SetCursorPosition(0, Console.CursorTop - 2);
                            continue;
                        case "n":
                            throw new IndexOutOfRangeException("Входная строка имела неверный формат!");
                    }
                }
                else
                    working = false;
            }
            return value;
        }

        static bool AskUser(string message)
        {
            bool working = true;
            bool value = false;
            message = $"{message} [true/false]:";
            while (working)
            {
                try
                {
                    Console.Write(message);
                    value = Convert.ToBoolean(Console.ReadLine());
                    working = false;
                }
                catch
                {
                    Console.Write("Данные введены неверно! Повторить? [y/n]");
                    switch (Console.ReadLine().ToLower())
                    {
                        case "y":
                            Console.SetCursorPosition(0, Console.CursorTop - 2);
                            for (int i = 0; i < message.Length + 8; i++)
                                Console.Write(" ");
                            Console.WriteLine();
                            Console.WriteLine("                                                  ");
                            Console.SetCursorPosition(0, Console.CursorTop - 2);
                            continue;
                        case "n":
                            throw new IndexOutOfRangeException("Входная строка имела неверный формат!");
                    }
                }
            }
            return value;
        }


        static string SaveFile(string Caption, string extension = "TXT-file|*.txt")
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
                Console.WriteLine("     Из папки с программой:files\\file.file_extention");
                Console.WriteLine("     Открыть файловый диалог, написав 'dialog'");
                Console.Write("->");
                Console.CursorVisible = true;
                _out = Console.ReadLine();
                if (_out == "dialog")
                {
                    SaveFileDialog dialog = new SaveFileDialog();

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
                else working = false;

                /*if (File.Exists(_out)) break;
                else
                {
                    _out = null;
                    Console.CursorVisible = false;
                    Console.WriteLine("Неверно введен путь! Для выхода нажмите ESC...");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                        break;

                }*/
            }
            return _out;
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
                Console.WriteLine("     Из папки с программой:files\\file.file_extention");
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

        static Brush RandomBrush()
        {
            PropertyInfo[] brushInfo = typeof(Brushes).GetProperties();
            Brush[] brushList = new Brush[brushInfo.Length];
            for (int i = 0; i < brushInfo.Length; i++)
            {
                brushList[i] = (Brush)brushInfo[i].GetValue(null, null);
            }
            Random randomNumber = new Random();
            return brushList[randomNumber.Next(1, brushList.Length)];
        }

        static void TaskwithBMP()
        {
            //Application.Run(new Graphics.GraphicForm(new Bitmap(GetFile("Выберите файл", "BMP|*.bmp"))));
            Bitmap picture = null;

            if (AskUser("Сгенерировать картинку"))
            {
                picture = new Bitmap(500, 500);
                System.Drawing.Graphics graphics =  System.Drawing.Graphics.FromImage(picture);
                Random random = new Random();
                int count = random.Next(0, 10000);
                for (int i =0; i < 10000;i++)
                {
                    graphics.FillRectangle(RandomBrush(), random.Next(0, 500), random.Next(0, 500), random.Next(0, 500), random.Next(0, 500));
                }
            }
            else
                picture = new Bitmap(GetFile("Выберите файл с картинкой", "BMP|*.bmp"));

         

            Queue queue = new Queue();


            
            Console.WriteLine("Выберите на картинке область, котрую нужно залить новым цветом\r\nНажмите Enter для продолжения...");
            Console.ReadLine();
            Application.Run(new Graphics.GraphicForm(picture));
            if (!Parametrs.Ready)
                throw new ArgumentException();

            Color newColor;

            Console.Write("Теперь выберите цвет для замены...");
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                newColor = dialog.Color;
                Console.WriteLine("Готово");
            }
            else
                throw new ArgumentException();
            

            queue.Enqueue(Parametrs.Point);
            Color startColor = picture.GetPixel(Parametrs.Point.X, Parametrs.Point.Y);
            while (queue.Count > 0)
            {
                Point point = (Point)queue.Dequeue();
                if (picture.GetPixel(point.X, point.Y) == startColor)
                {
                    picture.SetPixel(point.X, point.Y, newColor);
                    if (point.X > 0)
                        queue.Enqueue(new Point(point.X - 1, point.Y));
                    if (point.Y > 0)
                        queue.Enqueue(new Point(point.X, point.Y - 1));
                    if (point.X < picture.Width - 1)
                        queue.Enqueue(new Point(point.X + 1, point.Y));
                    if (point.Y < picture.Height - 1)
                        queue.Enqueue(new Point(point.X, point.Y + 1));
                }
            }
            Application.Run(new Graphics.GraphicForm(picture));
            picture.Save("picture.bmp");
            if (AskUser("Сохранить в bmp-файл"))
            {
                Console.Write("Сохранение...");
                try
                {
                    picture.Save(SaveFile("Выберите путь для сохранения:", "BMP|*.bmp"));
                    Console.WriteLine("Успешно!");
                }
                catch (Exception exp)
                {

                    Console.WriteLine("Ошибка!");
                    Console.WriteLine("Причина:{0} и мои кривые руки ;)", exp.Message);
                    Console.WriteLine("Подробности:");
                    Console.WriteLine(exp.StackTrace);
                }
            }
            if (AskUser("Сохранить в файл-матрицу"))
            {
                Console.Write("Сохранение...");
                try
                {
                    using (StreamWriter writer = new StreamWriter(SaveFile("Выберите путь для сохранения"), false))
                    {
                        writer.WriteLine($"{picture.Width} {picture.Height}");
                        for (int i = 0; i < picture.Height; i++)
                        {
                            for (int j = 0; j < picture.Width; j++)
                            {
                                if (j == picture.Width - 1)
                                {
                                    writer.WriteLine($"{picture.GetPixel(j, i).B}");
                                    continue;
                                }
                                writer.Write($"{picture.GetPixel(j, i).B} ");
                            }
                        }
                    }
                    Console.WriteLine("Успешно!");

                }
                catch (Exception exp)
                {
                    Console.WriteLine("Ошибка!");
                    Console.WriteLine("Причина:{0} и мои кривые руки ;)", exp.Message);
                    Console.WriteLine("Подробности:");
                    Console.WriteLine(exp.StackTrace);
                }
            }
        }

        static void TaskwithFile()
        {
            Bitmap picture = null;
            string file = GetFile("Введите путь до файла с 'матрицей':");
            Console.Write("Конвертация в bmp...");
            try
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string temp = reader.ReadLine();
                    picture = new Bitmap(int.Parse(temp.Split(new char[] { ' ' })[0]), int.Parse(temp.Split(new char[] { ' ' })[1]));
                    int y = 0;

                    while (!reader.EndOfStream)
                    {
                        temp = reader.ReadLine();
                        string[] line = temp.Split(new char[] { ' ' });
                        for (int i = 0; i < line.Length; i++)
                            picture.SetPixel(i, y, Color.FromArgb(int.Parse(line[i]), int.Parse(line[i]), int.Parse(line[i])));
                        y++;
                    }

                    #if DEBUG
                        Application.Run(new Graphics.GraphicForm(picture));
                    #endif
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Ошибка!");
                Console.WriteLine("Причина:{0} и мои кривые руки ;)", exp.Message);
                Console.WriteLine("Подробности:");
                Console.WriteLine(exp.StackTrace);
                return;
            }
            Console.WriteLine("Успешно!");

            picture.Save("picture1.bmp");

            Queue queue = new Queue();

            
            int newColor = AskUser("Укажите необходимый цвет для замены", 0, 255);
            int x0 = AskUser("Введите начальную координату для поиска 'X'", 0, picture.Width);
            int y0 = AskUser("Введите начальную координату для поиска 'Y'", 0, picture.Height);

            queue.Enqueue(new Point(x0, y0));
            int startColor = picture.GetPixel(x0, y0).G;
            while (queue.Count >0)
            {
                Point point = (Point)queue.Dequeue();
                if (picture.GetPixel(point.X,point.Y).B == startColor)
                {
                    picture.SetPixel(point.X, point.Y, Color.FromArgb(newColor, newColor, newColor));
                    if (point.X > 0)
                        queue.Enqueue(new Point(point.X - 1, point.Y));
                    if (point.Y > 0)
                        queue.Enqueue(new Point(point.X, point.Y-1));
                    if (point.X < picture.Width-1)
                        queue.Enqueue(new Point(point.X+1, point.Y));
                    if (point.Y < picture.Height - 1)
                        queue.Enqueue(new Point(point.X, point.Y + 1));
                }
            }
            Application.Run(new Graphics.GraphicForm(picture));
            picture.Save("picture.bmp");
            if (AskUser("Сохранить в bmp-файл"))
            {
                Console.Write("Сохранение...");
                try
                {
                    picture.Save(SaveFile("Выберите путь для сохранения:","BMP|*.bmp"));
                    Console.WriteLine("Успешно!");
                }
                catch (Exception exp)
                {

                    Console.WriteLine("Ошибка!");
                    Console.WriteLine("Причина:{0} и мои кривые руки ;)", exp.Message);
                    Console.WriteLine("Подробности:");
                    Console.WriteLine(exp.StackTrace);
                }
            }
            if (AskUser("Сохранить в файл-матрицу"))
            {
                Console.Write("Сохранение...");
                try
                {
                    using (StreamWriter writer = new StreamWriter(SaveFile("Выберите путь для сохранения"), false))
                    {
                        writer.WriteLine($"{picture.Width} {picture.Height}");
                        for (int i = 0; i < picture.Height; i++)
                        {
                            for (int j = 0; j < picture.Width; j++)
                            {
                                if (j == picture.Width - 1)
                                {
                                    writer.WriteLine($"{picture.GetPixel(j, i).B}");
                                    continue;
                                }
                                writer.Write($"{picture.GetPixel(j, i).B} ");
                            }
                        }
                    }
                    Console.WriteLine("Успешно!");

                }
                catch (Exception exp)
                {
                    Console.WriteLine("Ошибка!");
                    Console.WriteLine("Причина:{0} и мои кривые руки ;)", exp.Message);
                    Console.WriteLine("Подробности:");
                    Console.WriteLine(exp.StackTrace);
                }
            }
        }
    }
}
