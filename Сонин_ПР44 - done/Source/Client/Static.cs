using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class Static
    {
        public static string ServerIP { get; set; }
        public static bool IPCorrect = false;
        public static string[] Command = new string[]
        {
            "db.count",
            "db.save",
            "db.add",
            "db.get",
            "db.set",
            "db.remove",
            
        };
        public enum CommandID
        {
            Count = 0,
            Save = 1,
            Add = 2,
            Get = 3,
            Set = 4,
            Remove = 5,
            Idle = 6
        }
    }
}
