using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using E2tima.Network;
using System.Xml;
namespace Client
{
    public partial class Form1 : Form
    {
        UDPClient client = null;
        Static.CommandID State = Static.CommandID.Idle;

        int Count = -1;
        Data Selected = new Data();
        char split = (char)13;
        string Data = null;
        public Form1()
        {
            InitializeComponent();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            State = Static.CommandID.Remove;
            client.SendTo(Static.ServerIP, Static.Command[(int)Static.CommandID.Remove] + $"{split}" + $"{listBoxMain.SelectedIndex}");
            while (Data != "OK")
            {
                Application.DoEvents();
            }
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new UDPClient();
            client.DataAvaliable += Client_DataAvaliable;
            Refresh();
        }

        private void Client_DataAvaliable()
        {
            System.Net.IPEndPoint ip = null;
            Data = "";
            string data = client.ReceiveFrom(ref ip);
            /*Data += (char)(int.Parse(data[0]));
           for (int i = 1; i < data.Length - 1; i++)
               Data += (char)((int.Parse(data[i])) + (int.Parse(data[0])));*/
            for (int i = 0; i < data.Length; i++)
                Data += (char)(data[i] + 1);
            //Data = client.ReceiveFrom(ref ip);
            ServerStatus.Text = $"Сервер:{Data}";
            if (Data == "OK")
            {
               // State = Static.CommandID.Idle;
                return;
            }
            if ( State == Static.CommandID.Count)
            {
                Count = int.Parse(Data);
            }
            else if ( State == Static.CommandID.Get)
            {
                if ( Data == "Error")
                {
                    return;
                }
                Selected.FromString(Data);
            }
            State = Static.CommandID.Idle;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Selected = new Data();
            listBoxMain.SelectedIndex = -1;
            textBoxName.Text = "Тест";
            textBoxJob.Text = "Программист";
            textBoxTeam.Text = "E2tima";
            textBoxSurname.Text = "Тестов";
            textBoxAge.Text = "23";

        }

        private new void Refresh()
        {
            State = Static.CommandID.Count;
            client.SendTo(Static.ServerIP, Static.Command[(int)Static.CommandID.Count]);
            while ( State != Static.CommandID.Idle) { Application.DoEvents(); }
            listBoxMain.Items.Clear();
            for ( int i = 0; i < Count; i++)
            {
                State = Static.CommandID.Get;
                client.SendTo(Static.ServerIP, Static.Command[(int)Static.CommandID.Get] + split + i);
                while (State != Static.CommandID.Idle)
                {
                    Application.DoEvents();
                    
                }
              
                textBoxAge.Text = Selected.Age.ToString();
                textBoxJob.Text = Selected.Job;
                textBoxName.Text = Selected.Name;
                textBoxSurname.Text = Selected.Surname;
                textBoxTeam.Text = Selected.TeamName;

                string data = $"{Selected.Surname} {Selected.Name} Возраст{Selected.Age.ToString()}";

                listBoxMain.Items.Add(data);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if ( listBoxMain.SelectedIndex == -1)
            {
                State = Static.CommandID.Add;
                Selected.Age = int.Parse(textBoxAge.Text);
                Selected.Job = textBoxJob.Text;
                Selected.Name = textBoxName.Text;
                Selected.Surname = textBoxSurname.Text;
                Selected.TeamName = textBoxTeam.Text;
                client.SendTo(Static.ServerIP, Static.Command[(int)Static.CommandID.Add] + $"{split}" + Selected.ToString());
                Refresh();
            }
            else
            {
                State = Static.CommandID.Set;
                Selected.Age = int.Parse(textBoxAge.Text);
                Selected.Job = textBoxJob.Text;
                Selected.Name = textBoxName.Text;
                Selected.Surname = textBoxSurname.Text;
                Selected.TeamName = textBoxTeam.Text;
                client.SendTo(Static.ServerIP, Static.Command[(int)Static.CommandID.Set] + $"{split}" +  $"{listBoxMain.SelectedIndex}" + $"{split}" + Selected.ToString());
                Refresh();
            }
        }

        private void listBoxMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            State = Static.CommandID.Get;
            client.SendTo(Static.ServerIP, Static.Command[(int)Static.CommandID.Get] + split + listBoxMain.SelectedIndex);
            while (State != Static.CommandID.Idle)
            {
                Application.DoEvents();

            }
            
            textBoxAge.Text = Selected.Age.ToString();
            textBoxJob.Text = Selected.Job;
            textBoxName.Text = Selected.Name;
            textBoxSurname.Text = Selected.Surname;
            textBoxTeam.Text = Selected.TeamName;

         

           
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("users");
            xmlDoc.AppendChild(rootNode);
            for (int i = 0; i < Count; i++)
            {
                XmlNode userNode = xmlDoc.CreateElement("user");
                State = Static.CommandID.Get;
                client.SendTo(Static.ServerIP, Static.Command[(int)Static.CommandID.Get] + split + i);
                while (State != Static.CommandID.Idle)
                {
                    Application.DoEvents();
                }
                userNode.InnerText = Data.ToString();
                rootNode.AppendChild(userNode);
            } 
            xmlDoc.InsertBefore(xmlDoc.CreateXmlDeclaration("1.1",string.Empty,string.Empty), xmlDoc.DocumentElement);
            xmlDoc.Save("test-doc.xml");
        }
    }
}
