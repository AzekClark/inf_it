using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    class Item : IComparable
    {
        public string Word { get; set; }
        public int Count { get; set; }


        int IComparable.CompareTo(object obj)
        {
            
        if (obj == null) return 1;

        Item item = obj as Item;
        if (item != null) 
            return this.Word.CompareTo(item.Word);
        else
           throw new ArgumentException("Object is not a Item");
        }
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
                if (List[i].Word == word) {
                    List[i].Count++; break;
                }
            }
            if ( List.Count == List.Capacity -1 )
                List.Capacity++;
            List.Add ( new Item() {  Count = 1,  Word = word});
        }

        public string Get(int index)
        {
            if (index >= List.Capacity - 1) throw new IndexOutOfRangeException();
            List.Sort();
            return string.Format("{0] {1}", List[index].Word, List[i].Count);
        }




    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
