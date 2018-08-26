using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Ayuda : Form
    {
        public Menu menu;
        public Ayuda()
        {
            InitializeComponent();
        }

        public void getRef(Menu m)
        {
            menu = m;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu.Show();
        }
    }
}
