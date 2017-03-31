using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task.Graphics
{
    public partial class GraphicForm : Form
    {
      
        public GraphicForm(Bitmap picture)
        {
            InitializeComponent();
            pictureBox1.Image = picture;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Parametrs.Point = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
            Parametrs.Ready = true;
            Close();
        }
    }
}
