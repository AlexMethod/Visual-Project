using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    class Base
    {
        public float X, Y; // Para la posicion de la base
        public bool outOfBounds=false; //Para saber si la base se ha salido del escenario
        public Bitmap bitmap; //El mapa de bits que seran dibujados para representar la clase 
        public int vida = 30;
        public bool muerto = false;
        public Base(Bitmap bit,int x, int y)
        {
            bitmap = new Bitmap(bit);
            X = x;
            Y = y;
        }

        public void Draw()
        {
            Acceso.PaintEventArgs.Graphics.DrawImage(bitmap, X, Y);
        }

        public void Move() 
        {
            X-=Acceso.Speed;
        }
    }
}
