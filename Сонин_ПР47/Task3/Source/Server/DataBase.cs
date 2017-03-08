using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
namespace Server
{
    #region Interfaces
    
    public abstract class Item
    {
       public int ID { get; set; }
       public override int GetHashCode() => ID;
    }
  
    public class stringItem : Item
    {
        public string Value { get; set; }
        public stringItem(string value)
        {
            Value = value;
        }
        public stringItem()
        {
            Value = "";
        }
    }

   
    public class longItem : Item
    {
        public long Value { get; set; }
        public longItem(long value)
        {
            Value = value;
        }
        public longItem()
        {
            Value = 0;
        }
    }
  
    public class boolItem : Item
    {
        public bool Value { get; set; }
        public boolItem(bool value)
        {
            Value = value;
        }
        public boolItem()
        {
            Value = false;
        }
    }
    
    public struct Row
    {
        
        public List<Item> Items;
       public int Count
        {
            get
            {
                return Items.Capacity;
            }

        }

       public Row(List<Item> Items)
        {
            this.Items = Items;  
        } 

    }

    #endregion
    
    public class DataBase
    {
        
        public List<Row> Columns;
        
        public Row Constructor { get; }

        public delegate Item Comparator();

        public DataBase(Row costructor, int count)
        {
            Constructor = costructor;
            Columns = new List<Row>(count);
            for (int i = 0; i < Columns.Capacity; i++)
                Columns.Add( costructor);
        }
        public DataBase()
        {
            Constructor = new Row(new List<Item>());
            Columns = new List<Row>();
            for (int i=0; i<Columns.Capacity; i++)
                Columns.Add(Constructor);
            
        }

        public void AddRow()
        {
            Columns.Capacity++;
            Columns.Add(Constructor);
        }

        public void DeleteRow(int index)
        {
            Columns.RemoveAt(index);
            Columns.Capacity--;
        }

        public Item Get(int x, int y) => Columns[x].Items[y];

        public void Set(Item item, int x, int y) => Columns[x].Items[y] = item;

        public void LoadXML(FileStream stream)
        {
            
            throw new NotImplementedException();
        }

        public void SaveXML(Comparator comp,FileStream stream)
        {
            if ( comp == null)
            throw new NotImplementedException();


        }

    }

        



  

  
    
}
