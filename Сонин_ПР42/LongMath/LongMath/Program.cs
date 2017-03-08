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




namespace LongMath
{
    public class LongMath
    {
        public  List<long> number = null;
       // public  int rank;
        public  int rate;
        
        public LongMath(int Rate = 100000)
        {
            
            rate = Rate;
            number = new List<long>();
            
        }

       /* public LongMath(long _number, int Rate = 100000, int Rank = 10)
        {
            rank = Rank;
            rate = Rate;
            number = new List<long>(rank);
            for (int i = 0; i < rank; i++)
                number[i] = 0;
            number[rank - 1] = _number;
        }*/

        public static LongMath operator * (LongMath _Lnumber,long _number)
        {
            long reg = 0, _temp = 0;
            int i = 0;
            while (_temp >= 0)
            {
                if (i == _Lnumber.number.Count)
                {
                    _Lnumber.number.Capacity = _Lnumber.number.Capacity + 1;
                    _Lnumber.number.Add(0);
                }
                _temp = _Lnumber.number[i] * _number + reg;
                _Lnumber.number[i] = _temp % _Lnumber.rate;
                reg = _temp / _Lnumber.rate;

               if (_Lnumber.number[i] == 0 && i == _Lnumber.number.Count-1)
                {
                    _Lnumber.number.RemoveAt(_Lnumber.number.Count-1);
                    break;
                }

                i++;
            }


            return _Lnumber;
        }

        public static LongMath operator +(LongMath _Lnumber, long _number)
        {
            long reg = 1, _temp = 1;
            int i = 0;
            while (reg != 0 && _temp != 0 && _number != 0)
            {
                reg = 0; 
                if (i == _Lnumber.number.Count)
                {
                    _Lnumber.number.Capacity = _Lnumber.number.Capacity + 1;
                    _Lnumber.number.Add(0);
                }
                _temp = _Lnumber.number[i] + _number + reg;
                _number /= (_Lnumber.rate);
               /* if (_temp.ToString().Length <= _Lnumber.rate.ToString().Length)
                {
                    _Lnumber.number[i] = _temp;
                    reg = 0;
                }
                else
                {*/
                    _Lnumber.number[i] = _temp % _Lnumber.rate;
                    reg = _temp / _Lnumber.rate;
               // }
             

                if (_Lnumber.number[i] == 0 && i == _Lnumber.number.Count - 1 && reg == 0)
                {
                    _Lnumber.number.RemoveAt(_Lnumber.number.Count - 1);
                    break;
                }

                i++;
            }

            return _Lnumber;
        }

        public static LongMath operator +(LongMath _Lnumber, LongMath _Rnumber)
        {
            long reg = 1, _temp = 1;
            int i = 0;
            while (reg != 0 && _temp != 0)
            {
                reg = 0;
                if (i == _Lnumber.number.Count )
                {
                    _Lnumber.number.Capacity = _Lnumber.number.Capacity + 1;
                    _Lnumber.number.Add(0);
                }
                _temp = _Lnumber.number[i] + _Rnumber.number[i] + reg;
              //  _number /= (_Lnumber.rate + 1);
                if (_temp.ToString().Length <= _Lnumber.rate.ToString().Length)
                {
                    _Lnumber.number[i] = _temp;
                    reg = 0;
                }
                else
                {
                    _Lnumber.number[i] = _temp % _Lnumber.rate;
                    reg = _temp / _Lnumber.rate;
                }


                if (_Lnumber.number[i] == 0 && i == _Lnumber.number.Count - 1 && reg == 0)
                {
                    _Lnumber.number.RemoveAt(_Lnumber.number.Count - 1);
                    break;
                }

                i++;
            }

            return _Lnumber;
        }

        public static LongMath operator -(LongMath _Lnumber, LongMath _Rnumber)
        {
            long reg = 1, _temp = 1;
            int i = 0;
            while (reg != 0 && _temp != 0)
            {
                reg = 0;
                if (i == _Lnumber.number.Count)
                {
                    _Lnumber.number.Capacity = _Lnumber.number.Capacity + 1;
                    _Lnumber.number.Add(0);
                }
                _temp = _Lnumber.number[i] + _Rnumber.number[i] + reg;
                //  _number /= (_Lnumber.rate + 1);
                if (_temp.ToString().Length <= _Lnumber.rate.ToString().Length)
                {
                    _Lnumber.number[i] = _temp;
                    reg = 0;
                }
                else
                {
                    _Lnumber.number[i] = _temp % _Lnumber.rate;
                    reg = _temp / _Lnumber.rate;
                }


                if (_Lnumber.number[i] == 0 && i == _Lnumber.number.Count - 1 && reg == 0)
                {
                    _Lnumber.number.RemoveAt(_Lnumber.number.Count - 1);
                    break;
                }

                i++;
            }

            return _Lnumber;
        }
        #region Comparing

        public static bool operator >(LongMath Lnumber, LongMath Rnumber)
        {
            if (String.Compare(Lnumber.ToString(), Rnumber.ToString() )> 0)
                return true;
            else return false; 
        }

        public static bool operator <(LongMath Lnumber, LongMath Rnumber)
        {
            if (String.Compare(Lnumber.ToString(), Rnumber.ToString()) < 0)
                return true;
            else return false;
        }

        public static bool operator >=(LongMath Lnumber, LongMath Rnumber)
        {
            if (String.Compare(Lnumber.ToString(), Rnumber.ToString()) >= 0)
                return true;
            else return false;
        }

        public static bool operator <=(LongMath Lnumber, LongMath Rnumber)
        {
            if (String.Compare(Lnumber.ToString(), Rnumber.ToString()) <= 0)
                return true;
            else return false;
        }

        public static bool operator ==(LongMath Lnumber, LongMath Rnumber)
        {
            if (Lnumber.ToString() == Rnumber.ToString())
                return true;
            else return false;
        }

        public static bool operator !=(LongMath Lnumber, LongMath Rnumber)
        {
            if (Lnumber.ToString() != Rnumber.ToString())
                return true;
            else return false;
        }


        #endregion

        public static implicit operator LongMath(long v)
        {
            LongMath _number = new LongMath();

            while (v > 0)
            {
                _number.number.Add(v % (_number.rate * 10));
                v /= (_number.rate * 10);
            }
            return _number;
        }

        public static implicit operator LongMath(string v)
        {
            LongMath _number = 0;

            while (v.Length >= _number.rate.ToString().Length )
            {
                _number += Convert.ToInt64(v.Substring(v.Length - _number.rate.ToString().Length - 1, _number.rate.ToString().Length));
                _number *= _number.rate;
                v = v.Remove(v.Length - _number.rate.ToString().Length - 1);
            }
            if ( v != "")
            {
                _number *= _number.rate;
                _number += long.Parse(v);
            }
          
            return _number;
        }

        public override string ToString()
        {
            string _out = $"{number[number.Count-1]}";
            int index = number.Count - 2;
            
            while ( index > -1)
            {
                _out += $"{number[index]:000000.#}";
                index--;
            }

            return _out;

        }
    } 


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

      
      
        static void main()
        {
            Console.Title = "E2tima.Tests";
          
           Thread.Sleep(new Random(2000).Next(450, 1200));
            loading = false;
            
            while (!Ready)
                Application.DoEvents();

            if (Ready)
            {
                Console.Title = "E2tima.Tests - LongMath";

                Console.CursorVisible = false;
                LongMath number = 1;
                number = "1298345793459346539876026879607849306749368749687549";
                number *= -1;

                LongMath n1 = long.MaxValue, n2 = long.MaxValue;
                if ( n1 <= n2)
                {
                    Console.WriteLine("work");
                }
                LongMath n3 = n1 + n2;
                for (int i = 99999; i <= 10000000; i++)
                {
                    Console.Write($"{number}+");
                    number += i;
                    Console.Write($"{i}={number}");
                    Console.WriteLine($" Длинна числа: {number.ToString().Length}");
                }
                 number = "1298345793459346539876026879607849306749368749687549";
                for (int i = 2; i <= 1000; i++)
                {
                    number *= i;
                    Console.Write($"{i}!={number}");
                    Console.WriteLine($" Длинна числа: {number.ToString().Length}");
                }
                Console.CursorVisible =false;
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
