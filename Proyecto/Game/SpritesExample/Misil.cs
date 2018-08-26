using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Game
{
    class Misil : Item 
    {
        Bitmap hacha = new Bitmap(Game.Properties.Resources.Hacha);
       
        public Misil(int x, int y,string tipoo)
            : base(x, y)
        {
            tipo = tipoo;
            bitmap = new System.Drawing.Bitmap(Game.Properties.Resources.Hacha);
        }
        public override void Draw()
        {
            
        }
        public void DrawHacha()
        {
            Acceso.PaintEventArgs.Graphics.DrawImage(hacha, position.X, position.Y);
        }

        
        public override void Move()
        {
            position.X -= Acceso.Speed;
        }
        public void MoveLeft() 
        {
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            hacha.RotateFlip(RotateFlipType.Rotate180FlipX);
           
            position.X -= Acceso.SpeedMisil;
        }


        public void MoveRight() 
        {
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            hacha.RotateFlip(RotateFlipType.Rotate180FlipX);
            position.X += Acceso.SpeedMisil;
        }

        public bool IsCollision(Base i)
        {
            if (position.X + bitmap.Width >= i.X && position.X + bitmap.Width <= i.X + i.bitmap.Width && position.Y + bitmap.Height / 2 >= i.Y && position.Y + bitmap.Height / 2 <= i.Y + i.bitmap.Height)
            {
                outOfBounds = true;
                return true;
            }
            //LEFT
            else if (position.X >= i.X && position.X <= i.X + i.bitmap.Width && position.Y + bitmap.Height / 2 >= i.Y && position.Y + bitmap.Height / 2 <= i.Y + i.bitmap.Height)
            {
                outOfBounds = true;
                return true;
            }
            else { return false; }
        }

    }
}
