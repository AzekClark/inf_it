using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using E2tima.IData;
using E2tima.Network;
using E2tima.Crypto;
using System.Drawing;
using System.Security.Cryptography;
namespace Server
{
  
    public class Data : DataItem
    {
       
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string TeamName { get; set; }
        public string Job { get; set; }

        char split = (char)12;

        void DataItem.FromString(string data)
        {
            string[] split = data.Split(new char[] { (char)12 });
            Name = split[0];
            Surname = split[1];
            Age = int.Parse(split[2]);
            TeamName = split[3];
            Job = split[4];
        }

        string DataItem.ToString() => $"{Name}{split}{Surname}{split}{Age}{split}{TeamName}{split}{Job}";
    }


    class Program
    {

        static  UDPServer server = null;
        static DataTable Table = null;
        static char separator = (char)13;

        static void Main(string[] args)
        {

            server = new UDPServer();
            server.DataAvaliable += Server_DataAvaliable;

            Table = new DataTable(1);
            
                Console.Write("Loading db...");
                Console.WriteLine(Loaddb());
            bool working = true;
            while ( working)
            {
               string  input = Console.ReadLine();
                if (input == "save")
                    Console.WriteLine(Savedb());
                else if (input.ToLower() == "exit")
                    working = false;
            }
        }

        private static string Savedb()
        {
            using (StreamWriter stream = new StreamWriter("data.db",true, Encoding.Default))
            {
                if (stream == null)
                    return "File Error";
                int i = 0;
                while( i < Table.Count)
                {
                    try
                    {
                        stream.WriteLine(Table.Get(i++).ToString());
                    }
                    catch
                    {
                        return "File Error";
                    }
                    
                }
                return "OK";
            }               
        }

        private static string  Loaddb()
        {
            try
            {
                using (StreamReader stream = new StreamReader("data.db", Encoding.Default))
                {
                    if (stream == null)
                        return "File Error";
                    int i = 0;
                    while (!stream.EndOfStream)
                    {
                        string _temp = stream.ReadLine();
                        try
                        {
                            Data data = new Data();
                            ((DataItem)data).FromString(_temp);

                            if (i == 0)
                                Table.Set(data, i++);
                            Table.Add(data);
                        }
                        catch
                        {
                            return "File Error";
                        }
                    }
                    return "OK";
                }
            }
            catch
            {
                return "File Error";
            } 
        }

        private static void Server_DataAvaliable()
        {
            System.Net.IPEndPoint ip = null;
            string Data = server.ReceiveFrom(ref ip),
                answer = "";
            string[] _temp = Data.Split(new char[] { separator });
            _temp[0] = _temp[0].ToLower();
            Console.Write($"{ip.Address.ToString()} {_temp[0]}");
            if (_temp[0] == "db.save")
            {
                Console.Write($"{ip.Address} saving database");
                answer = Savedb();
            }
            else if  (_temp[0] == "db.add")
            {
                try
                {
                    Data data = new Data();
                    ((DataItem)data).FromString(_temp[1]);
                    Table.Add(data);
                    answer=  "OK";
                }
                catch
                {
                    answer = "Error";
                }
            }
            else if (_temp[0] == "db.get")
            {
                try
                {
                    answer = Table.Get(int.Parse(_temp[1])).ToString();
                }
                catch
                {
                    answer = "Error";
                }
            }
            else if ( _temp[0] == "db.set")
            {
                try
                {
                    Data data = new Data();
                    ((DataItem)data).FromString(_temp[2]);
                    Table.Set(data, int.Parse(_temp[1]));
                    answer = "OK";
                }
                catch
                {
                    answer = "Error";
                }
            }
            else if ( _temp[0] == "db.remove")
            {
                try
                {
                    Table.Remove(int.Parse(_temp[1]));
                    answer = "OK";
                }
                catch
                {
                    answer = "Error";
                }
            }
            else if (_temp[0] == "db.count")
            {
                try
                {
                    answer = Table.Count.ToString();
                }
                catch
                {
                    answer = "Error";
                }
            }
            else
            {
                answer = "Command Error";
            }
            Console.WriteLine("->" + answer);
            string tosend = "";
            for (int i = 0; i < answer.Length; i++)
                tosend += (char)(answer[i]-1);
            /*Encoding enc = Encoding.GetEncoding("Windows-1251");
            byte[] _data = enc.GetBytes(answer);
            tosend += (_data[0].ToString() + " ");
            int k = _data[0];
            for (int i = 1; i < _data.Length; i++)
                tosend += ((_data[i] - _data[0]).ToString() + " ");*/
            Console.WriteLine("crypted:" + tosend);
            server.SendTo(ip.Address.ToString(), tosend);
        }
    }
}
