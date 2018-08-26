using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    class Posion : Item
    {
        public Posion(int x, int y)
            : base(x, y)
        {
            bitmap = new Bitmap(Game.Properties.Resources.PosionAzul1);
            tipo = "Posion";
        }

        public override void Draw()
        {
            Acceso.PaintEventArgs.Graphics.DrawImage(bitmap, position.X, position.Y);
        }
        public override void Move()
        {
            position.X -= Acceso.Speed;
        }
    }
}
