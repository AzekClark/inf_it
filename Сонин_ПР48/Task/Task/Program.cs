using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace Task
{
    class Item : IComparable
    {
        public string Word { get; set; }
        public int Count { get; set; }
        public bool SortByCount { get; set; } = false;

        int IComparable.CompareTo(object obj)
        {
            
        if (obj == null) return 1;

        Item item = obj as Item;
            if (item != null)
            {
                if (!SortByCount)
                    return this.Word.CompareTo(item.Word);
                else
                    return Count.CompareTo(item.Count);
            }
            else
                throw new ArgumentException("Object is not a Item");
        }

        public override string ToString() => $"{Word} {Count}";
        
        
    }

    class WordDictionary
    {
        protected List<Item> List;

        public WordDictionary(int Capacity = 10)
        {
            List = new List<Item>(Capacity);
        }
        
        public void Add(string word)
        {
            if (word == null) throw new NullReferenceException();
            for ( int i=0; i < List.Count; i++)
            {
                if (List[i].Word == word)
                {
                    List[i].Count++; return;
                }
            }
            if ( List.Count == List.Capacity -1 )
                List.Capacity++;

            List.Add ( new Item() {  Count = 1,  Word = word});
        }

        public string Get(int index)
        {
            if (index >= List.Capacity - 1) throw new IndexOutOfRangeException();
            

            return List[index].ToString() ;
        }

        public int Count => List.Count;

        public void Sort(bool byCount = false)
        {
            if (byCount)
            {
                for (int i = 0; i < List.Count; i++)
                    List[i].SortByCount = byCount;
            }
            List.Sort(); 
        }


    }

    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            Console.WriteLine(@"Выберите файлы для анализа из папки 'txt'  [нужно ввести имя файла]: " );
            GetDirectory(Application.StartupPath + "\\txt");
            
            WordDictionary dictianory = new WordDictionary();
            string path = Console.ReadLine();
            StreamReader reader = new StreamReader(Application.StartupPath + "\\txt\\" + path, Encoding.Default);
            if ( reader == null )
            {
                Console.WriteLine("Невозможно открыть файл!");
                while (true) Console.ReadLine();
            }
            long count = 0, current = 0;
            while (!reader.EndOfStream)
            {
                reader.ReadLine();
                Console.SetCursorPosition(0, Console.CursorTop-1);
               Console.WriteLine($"{ count++} строк прочитано...");
            }
            reader.Close();

             reader = new StreamReader(Application.StartupPath + "\\txt\\" + path, Encoding.Default);
            
            while ( !reader.EndOfStream )
            {
                string _s = reader.ReadLine(), s = "";
                _s = _s.ToLower();
                int index = 0;
                if (_s != null || _s != "")
                {
                    while (index < _s.Length)
                    {
                        /*if (s[index] != ' ' && !Char.IsLetter(s[index]))
                            s = s.Remove(index, 1);
                        else if ( s[index] == '.' || s[index] == ')' || s[index] == '(' || s[index] == '[' ||
                             s[index] == ']' || s[index] == ',' || s[index] == ';' || s[index] == '-' ||
                              s[index] == '!' || s[index] == '?' || s[index] == '<' || s[index] == '>')
                            s = s.Remove(index, 1);
                        */
                        if (_s[index] == ' ' || Char.IsLetter(_s[index]))
                            s += _s[index];
                        index++;
                    }
                    try
                    {
                        string[] data = s.Split(new char[] { ' ' });
                        for (int i = 0; i < data.Length; i++)
                            dictianory.Add(data[i]);
                    }
                    catch
                    {
                        dictianory.Add(s);
                    }
                }
                Console.SetCursorPosition(0, Console.CursorTop -1 );
                Console.WriteLine($"{current++ + 1}/{count} строк обработано, пожалуйста подождите!");
            }
            Console.WriteLine("выберите способ сортировки[false для сортировки по слову, true для сортировки по кол-ву]:");

            dictianory.Sort(Convert.ToBoolean(Console.ReadLine()));

            Console.WriteLine("выберите способ сортировки[true - сортировка по убыванию, false - сортировка по возрастанию]:");
            bool type = Convert.ToBoolean(Console.ReadLine());
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = " *.txt";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            dialog.Title = "Выбкрите файл для сохранения алфовитно-частотного словаря";
            dialog.Filter = "Текстовые файлы (.txt) |*.txt | Все файлы| *.*";
            string file = "";
            if (dialog.ShowDialog() == DialogResult.OK)
                file = dialog.FileName;
          
            StreamWriter writer;
            try
            {
                writer = new StreamWriter(file);
            }
            catch
            {
                Console.WriteLine("выбран неверный файл!");
                while (true) { Console.ReadLine(); return; };
            }
            if (!type)
            {
                for (int i = 0; i < dictianory.Count; i++)
                {
                    Console.WriteLine(dictianory.Get(i));
                    writer.WriteLine(dictianory.Get(i));
                }
            }
            else
            {
                for (int i = dictianory.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine(dictianory.Get(i));
                    writer.WriteLine(dictianory.Get(i));
                }
            }
            reader.Close();
            writer.Close();
            Console.ReadLine();

        }

        static void GetDirectory(string directory, bool subdirectory = false)
        {
            try
            {
                string[] datas = Directory.GetFiles(directory);
                foreach (string data in datas)
                {
                    string[] work = data.Split(new char[] { '\\' });
                    string temp = work[work.Length - 1];
                    if (temp.EndsWith(".txt"))
                        Console.WriteLine($"->{temp}");
                }
            }
             catch
            {
                Console.WriteLine("->В папке файлов не найдено!");
            }
        }
    }
}
