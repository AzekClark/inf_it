using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Data 
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string TeamName { get; set; }
        public string Job { get; set; }

        char split = (char)12;

        public void FromString(string data)
        {
            string[] split = data.Split(new char[] { (char)12 });
            Name = split[0];
            Surname = split[1];
            Age = int.Parse(split[2]);
            TeamName = split[3];
            Job = split[4];
        }

        public override string ToString() => $"{Name}{split}{Surname}{split}{Age}{split}{TeamName}{split}{Job}";
    }

}
