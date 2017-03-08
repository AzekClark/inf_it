using System;
using System.Collections.Generic;
using System.IO;
namespace E2tima.IData
{
    public class Null : DataItem
    {
        void DataItem.FromString(string data) { }

        string DataItem.ToString() => "";
       
    }

    public interface DataItem
    {
        string ToString();
        void FromString(string data);
    }
    
    public class DataTable
    {
        public  List<DataItem> Table;

        public DataTable()
        {
            Table = new List<DataItem>();
            Fill(new Null());
        }

        public DataTable(DataItem item)
        {
            Table = new List<DataItem>();
            Fill(item);
        }

        public DataTable(int capacity)
        {
            Table = new List<DataItem>(capacity);
            Fill(new Null());
        }

        public DataTable(DataItem item,int capacity)
        {
            Table = new List<DataItem>(capacity);
            Fill(item);
        }

        public DataItem Get(int index)
        {
            if (index >= Table.Capacity)
                throw new IndexOutOfRangeException();
            return Table[index];
        }

        public void Set(DataItem item, int index)
        {
            if (index >= Table.Capacity)
                throw new IndexOutOfRangeException();
            Table[index] = item;
        }
        public void Add(DataItem item)
        {
            if (Table.Capacity == Table.Capacity)
                Table.Capacity++;
            Table.Add(item);
        }

        public int Count
        {
            get
            {
                return Table.Count;
            }
        }

        public void Remove(int index)
        {
            if (index >= Table.Capacity)
                throw new IndexOutOfRangeException();
            Table.RemoveAt(index);
        }

        public void LoadXml(FileStream stream)
        {
            throw new NotImplementedException();
        }

        public void SaveXml(FileStream stream)
        {
            if (!stream.CanWrite)
                throw new FileLoadException();

        }

        private void Fill(DataItem item)
        {
            for (int i = 0; i < Table.Capacity; i++)
                Table.Add(item);
        }

    }
}
