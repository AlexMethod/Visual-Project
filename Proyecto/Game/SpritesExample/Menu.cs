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
    public delegate void delegado();
    
    public partial class Menu : Form
    {
        Juego nivel1;
        Ayuda ayuda;
        Creditos creditos;
        //otra forma para nivel dos
        //nive 3 y nivel 4, y una forma para la introduccion.
        //La forma para la introduccion es como el personaje se va a mover a traves del videojuego 
        // Y la introduccio tambien posera una historia con imagenes y letras para que pueda el jugador
        //entender la historia del juego, con imagenes y cosas asi xD
        public Menu()
        {
            
            InitializeComponent();
            nivel1 = new Juego();
            ayuda = new Ayuda();
            creditos = new Creditos();
            nivel1.getRef(this);
            ayuda.getRef(this);
            creditos.getRef(this);
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            nivel1.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ayuda.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            creditos.Show();
            this.Hide();
        }

        
    }
}
