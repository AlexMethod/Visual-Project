using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Game
{
    class Corona : Item 
    {
        public Corona(int x, int y)
            : base(x, y)
        {
            tipo = "Corona";
            bitmap = new System.Drawing.Bitmap(Game.Properties.Resources.Corona);
        }

        public override void Draw()
        {
            Acceso.PaintEventArgs.Graphics.DrawImage(bitmap,position.X,position.Y);
        }

        public override void Move()
        {
            position.X -= Acceso.Speed; 
        }
    }
}
