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
namespace Client
{
    public partial class GetIP : Form
    {
        public GetIP()
        {
            InitializeComponent();
            if (File.Exists("config.dat"))
            {
                try
                {
                    StreamReader stream = new StreamReader("config.dat");
                    string[] data = stream.ReadLine().Split(new char[] { ' ' });
                    textBox1.Text += (char)(int.Parse(data[0]));

                    for (int i = 1; i < data.Length - 1; i++)
                        textBox1.Text += (char)((int.Parse(data[i])) + (int.Parse(data[0])));
                    //textBox1.Text = Convert.ToString(data);
                    stream.Close();
                }
                catch { textBox1.TabIndex = 0; button1.TabIndex = 1; }
             }
            else { textBox1.TabIndex = 0; button1.TabIndex = 1; }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Введите адрес сервера!", "Внимание", MessageBoxButtons.OK);
            try
            {
                string[] split = textBox1.Text.Split(new char[] { '.' });
                if (split.Length != 4)
                    throw new FormatException();
                Static.ServerIP = textBox1.Text;
                StreamWriter stream = new StreamWriter("config.dat");
                byte[] data = Encoding.Default.GetBytes(textBox1.Text);
                stream.Write(data[0] + " ");
                for (int i = 1; i < data.Length; i++)
                    stream.Write((data[i] - data[0]).ToString() + " ");
               
                stream.Close();
                Static.IPCorrect = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Введен неыерный адрес сервера!", "Внимание", MessageBoxButtons.OK);
                
            }
           
        }
    }
}
