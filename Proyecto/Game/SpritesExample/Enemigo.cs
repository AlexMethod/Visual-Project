using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    class Enemigo : Animado
    {
        /*
         * Esta Clase hereda de Animado. Son los enemigos del personaje.
         * El constructor tiene 4 parametros, el parametro nameFrame es el nombre del recurso
         * el parametro numFrame es el numero de frames para la animacion, locationX posicion en X
         * y locationY posicion en Y. 
         */
        public int vida = 20;
        public Enemigo(string nameFrame, int numFrame, int locationX, int locationY) : base(nameFrame, numFrame) 
        { 
            position.X = locationX; 
            position.Y = locationY; 
            speedV.X = 4; 
        }
        public override void UpDate()
        {
            /*
             * Logica para la generar la animacion
            */
            elapsedFrames++;
            if (elapsedFrames == numFrames)
            {
                elapsedFrames = 0;
                index1++;
                if (index1 >= frames1.Count) index1 = 0;
            }
            else
            {
                return;
            }

        }

        public  Bitmap CurrentFrame
        {
            /*
             * Regresa el frame actual 
             */
            get
            {
                return frames1[index1];
            }
        }

        public override void Draw()
        {
            /*
             * Dibuja la animacion
            */
            Acceso.PaintEventArgs.Graphics.DrawImage(CurrentFrame, position.X, position.Y);
        }

        public override bool Move() 
        {
            /*
             * Logica para mover al objeto de izquierda a derecha
             */
            if(position.X<928) //920 sin el with 
            {
                position += speedV;
                if (position.X + CurrentFrame.Width >= Acceso.GameSize.Width)
                {
                    speedV.X *= -1;
                    Flip();
                }
                else if (position.X <= 0)
                {
                    speedV.X *= -1;
                    Flip();
                }
                return true;
            
            }
            else { return false; }   
        }

        public override void Flip()
        {
            /*
             * Gira de izquierda a derecha o derecha a izquierda todos los frames
             * del objeto
             * */
            foreach (Bitmap bit in frames1)
            {
                bit.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            
        }

        public override bool IsCollision(Enemigo i)
        {
            /*
             * Detecta las colisiones con objetos del tipo Item
            */
            if (position.X >= i.position.X && position.X <= i.position.X + i.CurrentFrame.Width && position.Y >= i.position.Y && position.Y <= i.position.Y+ i.CurrentFrame.Height)
            {
                speedV.X *= -1;
                Flip();
                return true;
            }
            //Punto C
            else if (position.X + CurrentFrame.Width >= i.position.X && position.X + CurrentFrame.Width <= i.position.X + i.CurrentFrame.Width && position.Y + CurrentFrame.Height >= i.position.Y && position.Y + CurrentFrame.Height <= i.position.Y + i.CurrentFrame.Height)
            {
                speedV.X*= -1;
                Flip();
                return true;
            }
            //Punto B
            if (position.X + CurrentFrame.Width >= i.position.X && position.X + CurrentFrame.Width <= i.position.X + i.CurrentFrame.Width && position.Y >= i.position.Y && position.Y <= i.position.Y + i.CurrentFrame.Height)
            {
                speedV.X *= -1;
                Flip();
                return true;
            }
            //Punto D
            else if (position.X >= i.position.X && position.X <= i.position.X + i.CurrentFrame.Width && position.Y + CurrentFrame.Height >= i.position.Y && position.Y + CurrentFrame.Height <= i.position.Y + i.CurrentFrame.Height)
            {
                speedV.X *= -1;
                Flip();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void StayOnLimits()
        {
            /*
             * Hace que la instancia se quede en los limites de la forma
            */
            if (position.X <= 0)
            {
                position.X = 0;
            }
            if (position.Y <= 0)
            {
                position.Y = 0;
            }
            if (position.X + CurrentFrame.Width >= Acceso.GameSize.Width)
            {
                position.X = Acceso.GameSize.Width - CurrentFrame.Width;
            }
            if (position.Y + CurrentFrame.Height >= Acceso.GameSize.Height)
            {
                position.Y = Acceso.GameSize.Height - CurrentFrame.Height;
            }
        }

        public override bool IsCollision(Base i)
        {
            /*
             * Detecta las colisiones con objetos del tipo Base
            */
            if (position.X+CurrentFrame.Width >= i.X && position.X+CurrentFrame.Width <= i.X + i.bitmap.Width && position.Y + CurrentFrame.Height/2 >= i.Y && position.Y + CurrentFrame.Height/2 <= i.Y + i.bitmap.Height)
            {
                speedV.X *= -1;
                Flip();
                return true;
            }
            //LEFT
            else if (position.X >= i.X && position.X <= i.X + i.bitmap.Width && position.Y + CurrentFrame.Height/2 >= i.Y && position.Y + CurrentFrame.Height/2 <= i.Y + i.bitmap.Height)
            {
                speedV.X *= -1;
                Flip();
                return true;
            }
            else { return false;}
        }

        public bool IsCollision(Misil i)
        {
            if (position.X + CurrentFrame.Width / 2 >= i.position.X && position.X + CurrentFrame.Width / 2 <= i.position.X + i.bitmap.Width && position.Y + CurrentFrame.Height - 10 >= i.position.Y && position.Y + CurrentFrame.Height - 10 <= i.position.Y + i.bitmap.Height)
            {
                vida-=Acceso.MisilDamage; if (vida <= 0) { muerto = true; } return true;
            }
            if (position.X + CurrentFrame.Width / 2 >= i.position.X && position.X + CurrentFrame.Width / 2 <= i.position.X + i.bitmap.Width && position.Y >= i.position.Y && position.Y <= i.position.Y + i.bitmap.Height)
            {
                vida -= Acceso.MisilDamage; if (vida <= 0) { muerto = true; } return true;
            }
            //RIGHT
            if (position.X + CurrentFrame.Width >= i.position.X && position.X + CurrentFrame.Width <= i.position.X + i.bitmap.Width && position.Y + CurrentFrame.Height / 2 >= i.position.Y && position.Y + CurrentFrame.Height / 2 <= i.position.Y + i.bitmap.Height)
            {
                vida -= Acceso.MisilDamage; if (vida <= 0) { muerto = true; } return true;
            }
            //LEFT
            else if (position.X >= i.position.X && position.X <= i.position.X + i.bitmap.Width && position.Y + CurrentFrame.Height / 2 >= i.position.Y && position.Y + CurrentFrame.Height / 2 <= i.position.Y + i.bitmap.Height)
            {
                vida -= Acceso.MisilDamage; if (vida <= 0) { muerto = true; } return true;
            }
            else { return false; }
        }

        public void MoveLeft()
        {
            position.X -= Acceso.Speed;
        }

    }
}
