using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class Acceso
    {
        /*
         * Esta clase es usada para acceder a variables "globales" desde cualquier otra clase
         * Ya que sus atributos son estaticos, no es necesario crear un objeto de tipo Acces
         * Images guarda todas las imagenes de los recursos del proyecto mediante el tipo Dictionary
         * KeyPressed es usada para saber que tecla fue presionada
         * KeyUP es usada para saber que tecla fue soltada
         * PaintEventArgs es usada para dibujar todo.
         * GameSize es usada para conocer el tamaño de la forma.
         * El constructor es privado ya que no se ocupa ninguna instancia de esta clase.
         * */
        public static Dictionary<string, Bitmap> Images = new Dictionary<string, Bitmap>();
        public static KeyEventArgs KeyPressed;
        public static KeyEventArgs KeyUp;
        public static PaintEventArgs PaintEventArgs;
        public static Size GameSize;
        public static float Speed;
        public static float SpeedMisil;
        public static int MisilDamage;
        public static float Gravity;
        private Acceso() { }
    }
}
