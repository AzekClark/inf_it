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
using Excel = Microsoft.Office.Interop.Excel;

namespace LongMath
{
    public class LongMath
    {
        public static List<long> number = null;
        static int rank = 10;
        static int rate = 100000;
        static bool inited = false;
        public static void Init(int Rate, int Rank)
        {
            rank = Rank;
            rate = Rate;
            number = new List<long>(rank);
            for (int i = 0; i < rank; i++)
                number[i] = 0;
        }

        public static LongMath operator * (LongMath _Lnumber,long _number)
        {
            long reg = 0, _temp = 0;
            LongMath _out = _Lnumber;
            for ( int i=0; i < rank; i++)
            {
                _temp =  * _number + reg;

            }


            return _out;
        }

        public static LongMath operator +(LongMath _Lnumber, long _number)
        {
            long reg = 0, _temp = 0;
            LongMath _out = new LongMath();



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

        static int _number = -1;
        static int i = 1;
        static long Cycle = -1;
        static long Erot = -1;
        #region Excel variables

        private static Excel.Application    excelapp;
        private static Excel.Sheets         excelsheets;
        private static Excel.Worksheet      excelworksheet;
        private static Excel.Range          excelcells;
        private static Excel.Workbooks      excelappworkbooks;
        private static Excel.Workbook       excelappworkbook;
        #endregion
        static void main()
        {
            Console.Title = "E2tima.Tests";
            excelapp = new Excel.Application();
           // Thread.Sleep(new Random(2000).Next(450, 1200));
            loading = false;
            
            while (!Ready)
                Application.DoEvents();

            if (Ready)
            {
                Console.Title = "E2tima.Tests - Eratosphen";
                int dec = 0;
                bool UseExcel = true;
                Console.WriteLine("Тест алгоритмов.\r\nИспользовать стандартные значения?");
                bool standart = getFromConsole(Console.ReadLine());
                if (standart)
                {
                    _number = 1000000;
                    dec = 10000;

                }
                else {
                    Console.WriteLine("Введите до какого числа нужно найти простые числа:");
                    _number = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"Укажите шаг от 1 до {_number}:");
                    dec = Convert.ToInt32(Console.ReadLine()); ;
                    Console.WriteLine("Экспортировать данные в Excel [true/false]? ");
                    UseExcel = getFromConsole(Console.ReadLine());
                }
                #region Excel postinit
                const int c_row = 2, c_coll = 2;
                int row = c_row, coll = c_coll;
                if (UseExcel)
                {
                    MessageBox.Show("В окне Excel ничего не трогать во время работы программы!! \r\n Можно просто смотреть на эту красоту :)", 
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // postinit of excel
                    excelapp.Visible = true;
                    excelapp.SheetsInNewWorkbook = 2;
                    excelapp.Workbooks.Add(Type.Missing);
                    excelappworkbooks = excelapp.Workbooks;
                    excelappworkbook = excelappworkbooks[1];
                    excelsheets = excelappworkbook.Worksheets;
                    excelworksheet = (Excel.Worksheet)excelsheets.get_Item(1);

                    //var values
                    excelcells = excelworksheet.get_Range
                          ($"{getColloumnName(coll)}{row}",
                          $"{getColloumnName(coll + 1)}{row}");
                    excelcells.Merge();
                    excelcells.ColumnWidth = 24;
                    excelcells.Borders.ColorIndex = 1;
                    excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells.Value2 = "Начальные значения";
                    row++;
                    excelcells = excelworksheet.get_Range
                        ($"{getColloumnName(coll)}{row}",
                        Type.Missing);
                    excelcells.ColumnWidth = 14;
                    excelcells.Borders.ColorIndex = 1;
                    excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells.Value2 = "Количество(n):";
                    excelcells = excelworksheet.get_Range
                       ($"{getColloumnName(coll + 1)}{row}",
                       Type.Missing);
                    excelcells.ColumnWidth = 12;
                    excelcells.Borders.ColorIndex = 1;
                    excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells.Value2 = $"{_number}";
                    row++;
                    excelcells = excelworksheet.get_Range
                       ($"{getColloumnName(coll)}{row}",
                       Type.Missing);
                    excelcells.ColumnWidth = 14;
                    excelcells.Borders.ColorIndex = 1;
                    excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells.Value2 = "Шаг(k):";
                    
                    excelcells = excelworksheet.get_Range
                       ($"{getColloumnName(coll + 1)}{row}",
                       Type.Missing);
                    excelcells.ColumnWidth = 12;
                    excelcells.Borders.ColorIndex = 1;
                    excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells.Value2 = $"{dec}";
                    row++;
                    excelcells = excelworksheet.get_Range
                       ($"{getColloumnName(coll)}{row}",
                       Type.Missing);
                    excelcells.ColumnWidth = 14;
                    excelcells.Borders.ColorIndex = 1;
                    excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells.Value2 = "Процессор:";
                    excelcells = excelworksheet.get_Range
                       ($"{getColloumnName(coll + 1)}{row}",
                       Type.Missing);
                    excelcells.ColumnWidth = 40;
                    excelcells.Borders.ColorIndex = 1;
                    excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells.Value2 = $"{GetHardwareInfo("Win32_Processor", "Name")[0]}";
                    row++;
                    excelcells = excelworksheet.get_Range
                    ($"{getColloumnName(coll)}{row}",
                    Type.Missing);
                    excelcells.ColumnWidth = 20;
                    excelcells.Borders.ColorIndex = 1;
                    excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells.Value2 = $"Количество ядер:";
                  
                    excelcells = excelworksheet.get_Range
                       ($"{getColloumnName(coll + 1)}{row}",
                       Type.Missing);
                    excelcells.ColumnWidth = 40;
                    excelcells.Borders.ColorIndex = 1;
                    excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells.Value2 = $"{GetHardwareInfo("Win32_Processor", "NumberOfCores")[0]}";
                    row++;
                    coll += 3;
                    row = c_row;
                    // header of table 
                    excelcells = excelworksheet.get_Range
                            ($"{getColloumnName(coll)}{row}",
                            Type.Missing);
                    excelcells.ColumnWidth = 12;
                    excelcells.Borders.ColorIndex = 1;
                    excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells.Value2 = "Тесты, №";
                    excelcells = excelworksheet.get_Range
                            ($"{getColloumnName(coll + 1)}{row}",
                            Type.Missing);
                    excelcells.ColumnWidth = 14;
                    excelcells.Borders.ColorIndex = 1;
                    excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells.Value2 = "Линейный, мс";
                    excelcells = excelworksheet.get_Range
                             ($"{getColloumnName(coll + 2)}{row}",
                             Type.Missing);
                    excelcells.ColumnWidth = 14;
                    excelcells.Borders.ColorIndex = 1;
                    excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells.Value2 = "Эратосфен, мс";
                    row++;
                }
                else excelapp.Quit();
                #endregion
                Console.CursorVisible = false;
                while ( i <= _number)
                {

                    Thread test = new Thread(Test);

                    //Thread Erot = new Thread(TErot);
                    //Cycle.Start();
                    //Erot.Start();
                    Console.Write("В процессе");
                    
                    test.Start();
                    char[] anims = new char[] { '-', '/', '|', '\\' };
                    int animsIndex = 0;
                    while (test.ThreadState == System.Threading.ThreadState.Running)
                       // Console.Write(".");               
                    {
                        Console.Write(anims[animsIndex]);
                        Thread.Sleep(60);
                        try
                        {
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                        catch { }
                        animsIndex++;
                        if (animsIndex >= anims.Length)
                            animsIndex = 0;

                    }
                    try
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                    }
                    catch { }
                    //Console.WriteLine();
                    Console.WriteLine($"{i/dec + 1}) Эратосфен:{Erot} мс Линейный:{Cycle} мс");

                    // List<int> simple1 = Simple.Eratospen(100);
                    // List<int> simple2 = Simple.Cycle(100);
                    #region Excel Table
                    if ( UseExcel )
                    {
                      //  Console.WriteLine($"Debug info: {coll} - {getColloumnName(coll)}\r\n{coll+1} - {getColloumnName(coll + 1)}");
                        excelcells = excelworksheet.get_Range
                            ($"{getColloumnName(coll)}{row}", 
                           Type.Missing);
                        //excelcells.Merge();
                        excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                        excelcells.Borders.ColorIndex = 1;
                        excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                        excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                        excelcells.Value2 = $"{i / dec + 1}";
                        excelcells = excelworksheet.get_Range
                            ($"{getColloumnName(coll+1)}{row}",
                           Type.Missing);
                        //excelcells.Merge();
                        excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                        excelcells.Borders.ColorIndex = 1;
                        excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                        excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                        excelcells.Value2 = $"{Cycle}";
                        excelcells = excelworksheet.get_Range
                            ($"{getColloumnName(coll+2)}{row}",
                           Type.Missing);
                        //excelcells.Merge();
                        excelcells.HorizontalAlignment = Excel.Constants.xlCenter;
                        excelcells.Borders.ColorIndex = 1;
                        excelcells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        excelcells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                        excelcells.VerticalAlignment = Excel.Constants.xlCenter;
                        excelcells.Value2 = $"{Erot}";
                        row++;
                        
                    }
                    #endregion
                    i += dec;
                    test.Interrupt();
                }
                
                #region Excel Chart
                Excel.ChartObjects chartsobjrcts =
                (Excel.ChartObjects)excelworksheet.ChartObjects(Type.Missing);
               
                Excel.ChartObject chartsobjrct = chartsobjrcts.Add(700, 10, 500, 300);
                /*excelcells = excelworksheet.get_Range($"{getColloumnName(coll)}{c_row+1}",
                    $"{coll+1}{row}");*/
                excelcells = excelworksheet.get_Range
                          ($"{getColloumnName(coll+1)}{c_row+1}",
                          $"{getColloumnName(coll + 2)}{row-1}");
                
                Excel.Chart excelchart = chartsobjrct.Chart;
              
                excelchart.SetSourceData(excelcells, Type.Missing);
               
                excelchart.ChartType = Excel.XlChartType.xlXYScatterSmooth;
                excelchart.HasTitle = true;
                excelchart.ChartTitle.Text = "График производительности";
                excelchart.ChartTitle.Font.Size = 14;
                excelchart.ChartTitle.Font.Color = 1;
                excelchart.ChartTitle.Shadow = false;
                //excelchart.ChartTitle.Border.LineStyle = Excel.Constants.xlSolid;
                ((Excel.Axis)(excelchart.Axes(Excel.XlAxisType.xlCategory,
                              Excel.XlAxisGroup.xlPrimary)))
                                   .HasTitle = true;
                ((Excel.Axis)excelchart.Axes(Excel.XlAxisType.xlCategory,
                  Excel.XlAxisGroup.xlPrimary)).HasTitle = true;
                ((Excel.Axis)excelchart.Axes(Excel.XlAxisType.xlCategory,
                  Excel.XlAxisGroup.xlPrimary)).AxisTitle.Text = "Тест";
                // ((Excel.Axis)excelchart.Axes(Excel.XlAxisType.xlSeriesAxis,
                 //  Excel.XlAxisGroup.xlPrimary)).HasTitle = false;
               //  ((Excel.Axis)excelchart.Axes(Excel.XlAxisType.xlValue,
                 //  Excel.XlAxisGroup.xlPrimary)).HasTitle = true;
                 //((Excel.Axis)excelchart.Axes(Excel.XlAxisType.xlValue,
                  // Excel.XlAxisGroup.xlPrimary)).AxisTitle.Text = "Линейный/Эратосфен";
                 //((Excel.Axis)excelchart.Axes(Excel.XlAxisType.xlCategory,
                  // Excel.XlAxisGroup.xlPrimary)).HasMajorGridlines = true;
                 //((Excel.Axis)excelchart.Axes(Excel.XlAxisType.xlCategory,
                   //Excel.XlAxisGroup.xlPrimary)).HasMinorGridlines = false;
                 //((Excel.Axis)excelchart.Axes(Excel.XlAxisType.xlSeriesAxis,
                   //Excel.XlAxisGroup.xlPrimary)).HasMajorGridlines = true;
                 //((Excel.Axis)excelchart.Axes(Excel.XlAxisType.xlSeriesAxis,
                 //  Excel.XlAxisGroup.xlPrimary)).HasMinorGridlines = false;
                 ((Excel.Axis)excelchart.Axes(Excel.XlAxisType.xlValue,
                   Excel.XlAxisGroup.xlPrimary)).HasMinorGridlines = false;
                 ((Excel.Axis)excelchart.Axes(Excel.XlAxisType.xlValue,
                   Excel.XlAxisGroup.xlPrimary)).HasMajorGridlines = false;
                excelchart.HasLegend = true;
                excelchart.Legend.Position = Excel.XlLegendPosition.xlLegendPositionTop;
                ((Excel.LegendEntry)excelchart.Legend.LegendEntries(1)).Font.Size = 12;
                ((Excel.LegendEntry)excelchart.Legend.LegendEntries(2)).Font.Size = 12;
                Excel.SeriesCollection seriesCollection =
                 (Excel.SeriesCollection)excelchart.SeriesCollection(Type.Missing);
                Excel.Series series = seriesCollection.Item(1);
                series.Name = "Линейный, мс";
                
                series = seriesCollection.Item(2);
                series.Name = "Эратосфен, мс";
                MessageBox.Show("Теперь можно изменять в окне Excel всё что угодно :)",
                    "Информация", MessageBoxButtons.OK,MessageBoxIcon.Information);
                #endregion
                Console.CursorVisible =!false;
                Console.ReadLine();
            }
        }
        #region Excel
        private static string getColloumnName(int coll)
        {
            string _out = "";
            if ( coll % 26 == 0)
            {
                /* for (int i = 1; i < coll / 26; i++)
                     _out += "A";*/
                if ((char)('A' - 1) + (coll / 26 - 1) >= 'A')
                    _out += (char)(('A' - 1) + (coll / 26 - 1));
                _out += "Z";
            }
            else
            {
                while ( coll > 0)
                {
                    int prev = coll;
                    coll /= 26;
                    _out = $"{(char)(('A'-1) + (prev - coll*26))}" + _out;
                }
               // _out = $"{}" + _out;
            }

            return _out;
        }
        #endregion
        private static bool getFromConsole(string Data)
        {
            Data = Data.ToLower();
            if (Data == "" || Data == "y" || Data == "д" || Data == "yes" || Data == "да" || Data == "1")
                return true;
            else  return false;       
        }
        private static void Test()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List<int> simple2 = Simple.Cycle(i);
            stopWatch.Stop();
             Cycle = stopWatch.ElapsedMilliseconds;
            stopWatch.Reset();

            stopWatch.Start();
            List<int> simple1 = Simple.Eratospen(i);
            stopWatch.Stop();
             Erot = stopWatch.ElapsedMilliseconds;
            stopWatch.Reset();

        }


    }
}
