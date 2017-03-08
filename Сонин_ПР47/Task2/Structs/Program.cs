using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using E2tima;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Windows.Shell;




namespace Structs
{
   
    class Program
    {


        static bool loading = true;
        static Thread thread = null;
        static bool Ready = false;

        static void Load()
        {
            Console.CursorVisible = false;



            var maxLength = Logo.Mainlogo.Aggregate(0, (max, line) => Math.Max(max, line.Length));
            var x = Console.BufferWidth / 2 - maxLength / 2;
            for (int y = -Logo.Mainlogo.Length; 4 * y < Console.WindowHeight + Logo.Mainlogo.Length; y++)
            {
                Animation.ConsoleDraw(Logo.Mainlogo, x, y);
                Thread.Sleep(80);
            }

            Animation.ConsoleDraw(Logo.Mainlogo, x, 9);
            Thread.Sleep(500);
            Animation.ConsoleDraw(Logo.LogoHi, x, 9);
            Thread.Sleep(500);
            Animation.ConsoleDraw(Logo.LogoLoading, x, 9);
            thread.Start();
            char[] anims = new char[] { '-', '/', '|', '\\' };
            int animsIndex = 0;
            while (loading)
            {
                Console.Write(anims[animsIndex]);
                Thread.Sleep(60);
                try
                {
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    taskBarState = (TaskbarItemProgressState)animsIndex;
                }
                catch { }
                animsIndex++;
                if (animsIndex >= anims.Length)
                    animsIndex = 0;

            }

            Console.CursorVisible = true;
            Console.Clear();
            Console.WriteLine("Система готова!");
            Thread.Sleep(150);
            Console.WriteLine(SayHi("login"));
            Ready = true;

        }
        static string SayHi(string file)
        {
            string User = Environment.UserName;
            string[] Names = null;

            if (File.Exists(file))
                Names = File.ReadAllLines(file);
           
            bool UsverExists = false;
            if ( Names != null)
                for (int i = 0; i < Names.Count(); i++)
                    if (User == Names[i])
                      UsverExists = true;
            if (UsverExists)
                return $"Рада снова видеть вас, {User}!";
            else
            {
                if (!File.Exists(file))
                    File.Create("login");
                try
                {
                    File.AppendAllText(file, User);
                }
                catch { }
                return $"Добро пожалвать, {User}!";
            }
        }

       

        private static List<string> GetHardwareInfo(string WIN32_Class, string ClassItemField)
        {
            List<string> result = new List<string>();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + WIN32_Class);

            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    result.Add(obj[ClassItemField].ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        internal enum ProgramState
        {
            Working,
            Error,
            Pause,
            Done
        }

        private static int Progress = 50;
        private static ProgramState programState = ProgramState.Done;
        private static TaskbarItemProgressState taskBarState = TaskbarItemProgressState.None;

        static void Main(string[] args)
        {
            TaskbarItemInfo Info = new TaskbarItemInfo();
            Console.Title = "E2tima";
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Blue;
            thread = new Thread(main);
            Load();
            Info.ProgressState = TaskbarItemProgressState.Normal;
            Info.ProgressValue = (double)Progress / 100;
            
            while ( true)
            {
                Info.ProgressState = taskBarState;
                Info.ProgressValue = (double)Progress / 100;
                Application.DoEvents();
            }
            // Console.ReadLine();
        }

         struct Student : IComparable<Student>
        {
            public string Surname;
            public string Name;
            public byte alg;
            public byte rus;
            public byte phys;
            public byte hyst;
            
            public Student(string Line)
            {
                string[] _temp = Line.Split(new char[] { ',' });
                 Surname = _temp[0];
                Name = _temp[1];
                alg = byte.Parse(_temp[2]);
                rus = byte.Parse(_temp[3]);
                phys = byte.Parse(_temp[4]);
                hyst = byte.Parse(_temp[5]);
            }

            public int CompareTo(Student other) => (-1) * string.Compare($"{other.Surname} {other.Name}", $"{this.Surname} {this.Name}");
            public override string ToString() => $"{this.Surname} {this.Name}";



        }
      
        static void main()
        {
            Console.Title = "E2tima.Tests";
          
           Thread.Sleep(new Random(2000).Next(450, 1200));
            loading = false;
            
            while (!Ready)
                Application.DoEvents();

            if (Ready)
            {
                Console.Title = "E2tima.Tests - Structs";
                List<Student> Students = new List<Student>(1000);
                StreamReader reader = new StreamReader("marks.csv", Encoding.Default);

                float avg_alg = 0f, avg_rus = 0f, avg_phys = 0f, avg_hyst = 0f;
                List<Student> bestStudents = new List<Student>(1000);
                int BestMarks = 0;
                int badStudentscount = 0;
                if ( reader != null)
                {
                 
                    while ( !reader.EndOfStream )
                    {
                        Student student = new Student(reader.ReadLine());
                        if (BestMarks < student.alg + student.hyst + student.phys + student.rus)
                            BestMarks = student.alg + student.hyst + student.phys + student.rus;
                        avg_alg += student.alg;
                        avg_rus += student.rus;
                        avg_phys += student.phys;
                        avg_hyst += student.hyst;
                        if (student.hyst == 2 || student.alg == 2 || student.phys == 2 || student.rus == 2)
                            badStudentscount++;


                        Students.Add(student);
                    }

                    reader.Close();
                }

                avg_alg /= (float)Students.Count;
                avg_hyst /= (float)Students.Count;
                avg_phys /= (float)Students.Count;
                avg_rus /= (float)Students.Count;

                

                Console.WriteLine($"Средние баллы: \r\nРусский:{avg_rus:#.0000}\r\nАлгебра:{avg_alg:#.0000}\r\nИстория:{avg_hyst:#.0000}\r\nФизика:{avg_phys:#.0000}");
                Console.WriteLine($"Максимальный балл:{BestMarks}");
                Students.Sort();
                Console.WriteLine($"Список учеников с максмимальным баллом:");
                int i = 1;
                foreach (Student student in Students)
                {
                    if (student.alg + student.hyst + student.phys + student.rus == BestMarks)
                     Console.WriteLine($"   {i++}){student}");
                }
                Console.WriteLine($"Учащиеся, получившие двойку:{badStudentscount}");
                Console.ReadLine();
               
            }
        }
    
        private static bool getFromConsole(string Data)
        {
            Data = Data.ToLower();
            if (Data == "" || Data == "y" || Data == "д" || Data == "yes" || Data == "да" || Data == "1")
                return true;
            else  return false;       
        }
   


    }
}
