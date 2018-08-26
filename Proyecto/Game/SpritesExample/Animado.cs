using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    abstract class Animado
    {
        /*
         * La clase Animado es una clase  abstracta, no se requieren instancias de esta clase
         * solo representa a las clases animadas.
         * Posee muchos atributos
         * El atributo muerto es para saber si la instancia debe ser eliminada o no
         * los atributos index1 hasta index4 son utilizados para iterar en las animaciones
         * El atributo ID e Id es para identificar a la instancia
         * Tiene dos atributos de tipo Vector2 para la posicion y la velocidad
         * El atributo speedX es para manipular la velocidad en el eje x del personaje
         * Los atributos frames1 hasta frames4 son utilizados para crear instancias de tipo Bitmap 
         * para los frames de la animacion.
         * Los atributos right,left,isRunning,isJumping,hasJumped,isSmash son de tipo bool 
         * y son utilizados para saber y manipular el estado del personaje. 
         * la bandera right mueve a la derecha, la bandera left mueve a la izquierda, isRunning 
         * si es true despliega la animacion de correr, isSmash es true despliega la animacion de golpe
         * Si isJumping es true entonces el personaje despliega la animacion de salto
         */
         public bool muerto = false; //Sirve para saber si el objeto ha muerto
         public int index1=0,index2 = 0, index3 = 0,index4 = 0, elapsedFrames=0,numFrames = 2; //Sirven para iterar entre los frames de las animaciones y el delay 
         public static int Id = 0; //Identifica al objeto
         public int ID = Id; //Identifica al objeto
         public Vector2 position; //Para la posicion del objeto
         public Vector2 speedV; //Para la velocidad del objeto
         public float speedX=Acceso.Speed, jumpHeight=30f, velocity=10f, gravity=Acceso.Gravity; //Son utilizados para algunas logicas del jugador
         protected List<Bitmap> frames1, frames2, frames3,frames4; // Estos atributos guardan cada frame de las diferentes animaciones
         public bool right = true,left = false,isRunning = false,isJumping = false, hasJumped = true, isSmash = false; //Son banderas para saber el estado del objeto

         public Animado(string nameFrame,int numFrames)
         {
             frames1 = new List<Bitmap>();
             position = new Vector2(0, 0);
             speedV = new Vector2(0, 0);
             string original = nameFrame;
             for(int i = 1;i<=numFrames;i++){
                 string str = i.ToString();
                 nameFrame+=str;
                 Bitmap bit = new Bitmap(Acceso.Images[nameFrame]);
                 frames1.Add(bit);
                 nameFrame = original;
             }
             Id++;
         }
        
         public Animado(string nameFrame1, string nameFrame2, string nameFrame3,string nameFrame4, int numFrame1, int numFrame2, int numFrame3,int numFrame4)
         {
             //Los string son las animaciones que el objeto puede tener para la animacion
             //Los int son el numero de frames para cada animacion
             //nameFrame1 corresponde con numFrame1 y asi sucesivamente
             frames1 = new List<Bitmap>();
             frames2 = new List<Bitmap>();
             frames3 = new List<Bitmap>();
             frames4 = new List<Bitmap>();
             position = new Vector2(0, 0);
             speedV = new Vector2(0, 0);
             string original = nameFrame1;
             for (int i = 1; i <= numFrame1; i++)
             {
                 string str = i.ToString();
                 nameFrame1 += str;
                 Bitmap bit = new Bitmap(Acceso.Images[nameFrame1]);
                 frames1.Add(bit);
                 nameFrame1 = original;
             }

             original = nameFrame2;
             for (int i = 1; i <= numFrame2; i++)
             {
                 string str = i.ToString();
                 nameFrame2 += str;
                 Bitmap bit = Acceso.Images[nameFrame2];
                 frames2.Add(bit);
                 nameFrame2 = original;
             }

             original = nameFrame3;
             for (int i = 1; i <= numFrame3; i++)
             {
                 string str = i.ToString();
                 nameFrame3 += str;
                 Bitmap bit = new Bitmap(Acceso.Images[nameFrame3]);
                 frames3.Add(bit);
                 nameFrame3 = original;
             }

             original = nameFrame4;
             for (int i = 1; i <= numFrame4; i++)
             {
                 string str = i.ToString();
                 nameFrame4 += str;
                 Bitmap bit = new Bitmap(Acceso.Images[nameFrame4]);
                 frames4.Add(bit);
                 nameFrame4 = original;
             }
         }
        /* El metodo Update es utilizado para la logica del juego
         * El metodo Draw dibuja la animacion
         * El metodo Move mueve al personaje
         * El metodo Flip gira los frames de la animacion de izquierda a derecha
         * IsCollision con parametro tipo Item detecta las colisiones hechas con objetos de tipo Item
         * IsCollision con parametro tipo Base detecta las colisiones hechas con objetos de tipo Base
         * StayOnLimits hace que el personaje se quede en los limites de la forma
         */
         public abstract void UpDate(); //Aquí va la lógica del objeto
         public abstract void Draw(); //Sirve para que se dibuje en objeto
         public abstract bool Move(); //Sirve para que se mueva 
         public abstract void Flip(); //Sirve para cambiar el sentido del frame izquierda a derecha
         public abstract bool IsCollision(Enemigo i); //Detecta las colisiones con los enemigos
         public abstract bool IsCollision(Base i); //Detecta las colisiones con las bases
         public abstract void StayOnLimits(); //Mantiene al jugador dentro del area visible
        //
    }
}
