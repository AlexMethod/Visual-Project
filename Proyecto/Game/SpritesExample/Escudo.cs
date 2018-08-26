using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    class Escudo : Item
    {
        public Escudo(int x, int y) : base(x,y)
        {
            bitmap = new Bitmap(Game.Properties.Resources.EscudoUnicornio);
            tipo = "Escudo";
        }

        public override void Draw()
        {
            Acceso.PaintEventArgs.Graphics.DrawImage(bitmap, position.X, position.Y);
        }

        public override void Move() 
        {
            position.X-=Acceso.Speed;
        }
    }
}
