using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game
{
    public abstract class Item
    {
        public Vector2 position;
        public bool outOfBounds=false;
        public bool tomado = false;
        public string tipo;
        public Bitmap bitmap;

        public Item(int x, int y)
        {

            position = new Vector2(x, y);
            tipo = "Item";
        }

        public abstract void Draw();

        public abstract void Move();
    }
}
