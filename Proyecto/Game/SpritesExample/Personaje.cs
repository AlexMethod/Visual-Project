using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class Personaje : Animado
    {
        /*
         * El atributo piso es utilizado para alternar el nivel del piso cuando
         * el personaje principal se pare arriba de un objeto Base 
         * El atributo vida es la vida del personaje principal 
         * El atributo isfloor es para saber si el personaje principal ha tocado el piso
         *
        */
        public float piso=0f; //Sirve para detectar y alternar el piso del personaje
        public int vida = 10; //Cantidad de vidas que posee
        public int escudoContador; //El escudo solo protege con 50 vidas
        public bool toca = false; // Para que el enemigo no quite mas de una vida al ser tocado una vez
        public int coinOro = 0; //Cantidad de monedas de oro que posee
        public int coinPlata = 0; //Cantidad de monedas de plata que posee
        public bool isfloor = false; //Para saber si ha detectado el piso
        public bool rayo = false; //Para saber si tiene seleccionado el item espada rayo
        public bool escudo = false; //Para saber si tiene seleccionado el item escudo
        public bool pergamino = false; //Para saber si tiene seleccionado el item pergamino
        public bool posionA = false; //   "     "    "   "     "          "    " posionA
        public bool hacha = false; //   "     "    "   "     "          "    " hacha
        public bool corona = false;//   "     "    "   "     "          "    " corona
        public List<Item> items = new List<Item>(); //Items seleccionables armas
        public List<Misil> misilesHacha = new List<Misil>(); //Misiles hacha (para disparar hacha)
        public bool misil = false; //Para saber si se puede lanzar misil dependiendo de las teclas presioadas
        public bool move = false; //Para saber si el jugador se ha movido
        public Personaje(string nameFrame1, string nameFrame2, string nameFrame3,string nameFrame4, int numFrame1, int numFrame2, int numFrame3,int numFrame4) : base(nameFrame1, nameFrame2, nameFrame3,nameFrame4, numFrame1, numFrame2, numFrame3,numFrame4) 
        {
            //Se posiciona al personaje en una posicion inicial 
            position.X = 300;
            position.Y = 570;
            piso = 645;
        }

        public override void UpDate()
        {
            /*
             * Este metodo tiene la logica para las animaciones
             * dependiendo de los atributos bandera
             * */
            //Si tiene seleccionado la posion, incrementa su velocidad
            if (posionA == true) 
            { 
                Acceso.Speed = 20; 
                speedX = Acceso.Speed; 
            }
            //Si tiene seleccionado el escudo, le protege con 25 de vida
            else if (escudo == true) 
            { 
                Acceso.Speed = 8; 
                speedX = Acceso.Speed; 
            }
            else { Acceso.Speed = 10; speedX = Acceso.Speed; }

            if (Acceso.KeyUp != null && Acceso.KeyUp.KeyData == Keys.X)
            {
                if (right == true)
                {
                    if (hacha == true)
                    {
                        misilesHacha.Add(new Misil((int)position.X + CurrentFrame.Width / 2, (int)position.Y + 20, "MisilRight"));
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (hacha == true)
                    {
                        misilesHacha.Add(new Misil((int)position.X + CurrentFrame.Width / 2, (int)position.Y + 20, "MisilLeft"));
                    }
                    else
                    {

                    }
                }


                Acceso.KeyUp = null;
            }

            foreach (Misil m in misilesHacha)
            {
                if (m.position.X + m.bitmap.Width <= 0)
                {
                    m.outOfBounds = true;
                }
                if (m.position.X >= 928)
                {
                    m.outOfBounds = true;
                }
            }


            foreach (Misil m in misilesHacha)
            {
                if (m.tipo == "MisilRight")
                {
                    m.MoveRight();
                }
                else if (m.tipo == "MisilLeft")
                {
                    m.MoveLeft();
                }
            }


            
            for (int i = 0; i < misilesHacha.Count; i++)
            {
                if (misilesHacha[i].outOfBounds == true)
                {
                    misilesHacha.Remove(misilesHacha[i]);
                }
            }

            if (position.X + CurrentFrame.Width >= Acceso.GameSize.Width / 2 && move == true)
            {
                foreach (Misil m in misilesHacha) { m.Move(); }
            }
            /*
             * ***************************************************************************************************/
            if (isSmash == true)
            {
                elapsedFrames++;
                if (elapsedFrames == numFrames)
                {
                    elapsedFrames = 0;
                    index4++;
                    if (index4 >= frames4.Count) index4 = 0;
                }
                else
                {
                    return;
                }
            }
            else if (isRunning == true)
            {
                elapsedFrames++;
                if (elapsedFrames == numFrames)
                {
                    elapsedFrames = 0;
                    index2++;
                    if (index2 >= frames2.Count) index2 = 0;
                }
                else
                {
                    return;
                }
            }
            else if (isJumping == true)
            {
                elapsedFrames++;
                if (elapsedFrames == numFrames)
                {
                    elapsedFrames = 0;
                    index3++;
                    if (index3 >= frames3.Count) index3 = 0;
                }
                else
                {
                    return;
                }
            }
            else
            {
                elapsedFrames++;
                if (elapsedFrames == numFrames)
                {
                    elapsedFrames = 0;
                    index1++;
                    if (index1 >= frames1.Count) index1 = 0;
                }

            }

        }
        
        public Bitmap CurrentFrame
        {
            /*
             * Regresa el frame actual.
            */
            get
            {
                if (isSmash == true)
                {
                    return frames4[index4];
                }
                if (isRunning == true)
                {
                    return frames2[index2]; 
                }
                if (isJumping == true) 
                {
                    return frames3[index3]; 
                }
                else
                {
                    return frames1[index1]; 
                }
                
            }
        }

        public override bool Move()
        {
            Jump();
            StayOnLimits();
            position = position + speedV;
            if (Acceso.KeyPressed != null)
            {
                if (Acceso.KeyPressed.KeyData == Keys.X)
                {
                    misil = false;
                }
                if (Acceso.KeyUp != null && Acceso.KeyUp.KeyData == Keys.X)
                {
                    misil = true;
                }
                if (Acceso.KeyPressed.KeyCode == Keys.Up && hasJumped == false)
                {
                    isRunning = false;
                    position.Y -= jumpHeight;
                    speedV.Y = -velocity;
                    index3 = 6;
                    hasJumped = true;
                    isJumping = true;
                    isSmash = false;
                    move = false;
                    return false;
                }
                else if (Acceso.KeyPressed.KeyCode == Keys.A)
                {
                    isSmash = true;
                    move = false;
                    return false;
                }
                else if (Acceso.KeyPressed.KeyCode == Keys.Left)
                {
                    isRunning = true;
                    speedV.X = -speedX;
                    if (left == false)
                    {
                        Flip();
                        left = true;

                    }
                    right = false;
                    move = false;
                    return false;
                }
                else if (Acceso.KeyPressed.KeyCode == Keys.Right)
                {
                    isRunning = true;
                    speedV.X = speedX;
                    if (right == false)
                    {
                        Flip();
                        right = true;
                    }
                    left = false;
                    move = true;
                    return true;
                }
                move = false;
                return false;  
            }
            else
            {
                speedV.X = 0;
                isRunning = false;
                isSmash = false;
                misil = false;
                move = false;
                return false;
            }

            
        }

        public override void StayOnLimits()
        {
            if (position.X <= 0)
            {
                position.X = 0;
            }
            if (position.X + CurrentFrame.Width >= Acceso.GameSize.Width/2)
            {
                position.X = Acceso.GameSize.Width/2 - 46;
            }
        }

        public void Jump()
        {
            if (position.Y + CurrentFrame.Height >= piso)
            {
                hasJumped = false;
                isJumping = false;
                isfloor = true;
            }
            if (hasJumped == false)
            {
                //Cuando ha tocado el suelo la velocidad en Y es 0. Ya no cae :)
                speedV.Y = 0f;
                
            }
            if (hasJumped == true)
            {
                //Gravedad
                speedV.Y += gravity;
                isfloor = false;
            }
        }

        public override void Draw()
        {
            Acceso.PaintEventArgs.Graphics.DrawImage(CurrentFrame, position.X, position.Y);
            foreach(Misil  m in misilesHacha) { m.DrawHacha(); }
        }

        public override void Flip()
        {
            foreach (Bitmap bt in frames2)
            {
                bt.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            foreach (Bitmap bt in frames1)
            {
                bt.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            foreach (Bitmap bt in frames3)
            {
                bt.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            foreach (Bitmap bt in frames4)
            {
                bt.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
        }

        public override bool IsCollision(Enemigo i)
        {
            if (position.X >= i.position.X && position.X <= i.position.X + i.CurrentFrame.Width && position.Y >= i.position.Y && position.Y <= i.position.Y + i.CurrentFrame.Height) { toca = true; return true; }
            else if (position.X + CurrentFrame.Width >= i.position.X && position.X + CurrentFrame.Width <= i.position.X + i.CurrentFrame.Width && position.Y >= i.position.Y && position.Y <= i.position.Y + i.CurrentFrame.Height) { toca = true; return true; }
            else if (position.X >= i.position.X && position.X <= i.position.X + i.CurrentFrame.Width && position.Y + CurrentFrame.Height >= i.position.Y && position.Y + CurrentFrame.Height <= i.position.Y + i.CurrentFrame.Height) { toca = true; return true; }
            else if (position.X + CurrentFrame.Width >= i.position.X + CurrentFrame.Width && position.X <= i.position.X + i.CurrentFrame.Width && position.Y + CurrentFrame.Height >= i.position.Y + CurrentFrame.Height && position.Y <= i.position.Y + i.CurrentFrame.Height) { toca = true; return true; }
            else 
            {
                if (toca == true) 
                {
                    if (escudo == false)
                    {
                        vida--;
                        toca = false;
                        if (vida <= 0)
                        {
                            muerto = true;
                        }
                    }
                    else
                    {
                        escudoContador--;
                        toca = false;
                        if (escudoContador <= 0) 
                        { 
                            escudoContador = 0; 
                            escudo = false; 
                        }
                    }
                }
                return false;
            }
        }

        //Son las bases en las cuales el personaje se puede parar
        public override bool IsCollision(Base i) 
        {
            
            //UNDER
            if (position.X + CurrentFrame.Width/2 >= i.X && position.X + CurrentFrame.Width/2 <= i.X + i.bitmap.Width && position.Y + CurrentFrame.Height-10 >= i.Y && position.Y + CurrentFrame.Height -10 <= i.Y + i.bitmap.Height)
            {
                piso = i.Y;
                return true;
            }
            else
            {
                isfloor = false;
                if (isfloor == false)
                {
                    hasJumped = true;
                }
                
            }
            //ABOVE
            if (position.X + CurrentFrame.Width/2 >= i.X && position.X + CurrentFrame.Width/2 <= i.X + i.bitmap.Width && position.Y >= i.Y && position.Y  <= i.Y + i.bitmap.Height)
            {
                position.Y = i.Y + i.bitmap.Height;
                gravity = 5f;
                return true;
            }
            //RIGHT
            else if (position.X+CurrentFrame.Width >= i.X && position.X+CurrentFrame.Width <= i.X + i.bitmap.Width && position.Y + CurrentFrame.Height/2 >= i.Y && position.Y + CurrentFrame.Height/2 <= i.Y + i.bitmap.Height)
            {
                position.X = i.X - CurrentFrame.Width;
                if (Acceso.KeyPressed != null && Acceso.KeyPressed.KeyData == Keys.A)
                {
                    //Lo esta tocando pero con golpe
                    i.vida--;
                    if (i.vida <= 0)
                    {
                        i.muerto = true;
                    }

                }
                return true;
            }
            //LEFT
            else if (position.X >= i.X && position.X <= i.X + i.bitmap.Width && position.Y + CurrentFrame.Height/2 >= i.Y && position.Y + CurrentFrame.Height/2 <= i.Y + i.bitmap.Height)
            {
                position.X = i.X+ i.bitmap.Width;
                if (Acceso.KeyPressed != null && Acceso.KeyPressed.KeyData == Keys.A)
                {
                    //Lo esta tocando pero con golpe
                    i.vida--;
                    if (i.vida <= 0)
                    {
                        i.muerto = true;
                    }

                }
                return true;
            }
            
            else
            {
                gravity = Acceso.Gravity;
                piso = 645;
                return false;
            }
        }

        public bool IsCollision(Item i)
        {
            //UNDER
            if (position.X + CurrentFrame.Width / 2 >= i.position.X && position.X + CurrentFrame.Width / 2 <= i.position.X + i.bitmap.Width && position.Y + CurrentFrame.Height - 10 >= i.position.Y && position.Y + CurrentFrame.Height - 10 <= i.position.Y + i.bitmap.Height)
            {
                if (i.tipo == "Vida")
                {
                    //Incrementa la vida en 10 unidades
                    vida += 10;
                    i.tomado = true;
                    
                }
                else if (i.tipo == "Oro")
                {
                    //Incrementa una moneda de oro
                    coinOro++;
                    i.tomado = true;
                    
                }
                else if (i.tipo == "Plata")
                {
                    //incrementa una moneda de plata
                    coinPlata++;
                    i.tomado = true;
                    
                }
                else if (i.tipo == "Escudo")
                {
                    if (coinOro >= 4 && coinPlata >= 4)
                    {
                        //obtiene el escudo si lo puede comprar
                        items.Add(i);
                        i.tomado = true;
                        coinPlata -= 4;
                        coinOro -= 4;
                        escudoContador+=25;
                    }
                    
                }
                else if (i.tipo == "Pergamino")
                {
                    //Incrementa la vida del escudo
                    i.tomado = true;
                    escudoContador += 100;
                }
                else if (i.tipo == "Rayo")
                {
                    //Incrementa el daño del hacha
                    i.tomado = true;
                    Acceso.MisilDamage +=1;
                }
                else if (i.tipo == "Posion")
                {
                    if (coinOro >= 5 && coinPlata >= 5)
                    {
                        //Aumenta su velocidad si lo puede comprar
                        i.tomado = true;
                        items.Add(i);
                        coinOro -= 5;
                        coinPlata -= 5;
                    }     
                }
                else if (i.tipo == "Hacha")
                {
                    //Obtiene el hacha
                    i.tomado = true;
                    items.Add(i);
                   
                }
                else if (i.tipo == "Corona") { i.tomado = true; corona = true; }
                
                return true;
            }
            //ABOVE
            if (position.X + CurrentFrame.Width / 2 >= i.position.X && position.X + CurrentFrame.Width / 2 <= i.position.X + i.bitmap.Width && position.Y >= i.position.Y && position.Y <= i.position.Y + i.bitmap.Height)
            {
                if (i.tipo == "Vida")
                {
                    //Incrementa la vida en 10 unidades
                    vida += 10;
                    i.tomado = true;

                }
                else if (i.tipo == "Oro")
                {
                    //Incrementa una moneda de oro
                    coinOro++;
                    i.tomado = true;

                }
                else if (i.tipo == "Plata")
                {
                    //incrementa una moneda de plata
                    coinPlata++;
                    i.tomado = true;

                }
                else if (i.tipo == "Escudo")
                {
                    if (coinOro >= 4 && coinPlata >= 4)
                    {
                        //obtiene el escudo si lo puede comprar
                        items.Add(i);
                        i.tomado = true;
                        coinPlata -= 4;
                        coinOro -= 4;
                        escudoContador += 25;
                    }

                }
                else if (i.tipo == "Pergamino")
                {
                    //Incrementa la vida del escudo
                    i.tomado = true;
                    escudoContador += 100;
                }
                else if (i.tipo == "Rayo")
                {
                    //Incrementa el daño del hacha
                    i.tomado = true;
                    Acceso.MisilDamage += 1;
                }
                else if (i.tipo == "Posion")
                {
                    if (coinOro >= 5 && coinPlata >= 5)
                    {
                        //Aumenta su velocidad si lo puede comprar
                        i.tomado = true;
                        items.Add(i);
                        coinOro -= 5;
                        coinPlata -= 5;
                    }
                }
                else if (i.tipo == "Hacha")
                {
                    //Obtiene el hacha
                    i.tomado = true;
                    items.Add(i);

                }
                else if (i.tipo == "Corona") { i.tomado = true; corona = true; }
                return true;
            }
            //RIGHT
            else if (position.X + CurrentFrame.Width >= i.position.X && position.X + CurrentFrame.Width <= i.position.X + i.bitmap.Width && position.Y + CurrentFrame.Height / 2 >= i.position.Y && position.Y + CurrentFrame.Height / 2 <= i.position.Y + i.bitmap.Height)
            {
                if (i.tipo == "Vida")
                {
                    //Incrementa la vida en 10 unidades
                    vida += 10;
                    i.tomado = true;

                }
                else if (i.tipo == "Oro")
                {
                    //Incrementa una moneda de oro
                    coinOro++;
                    i.tomado = true;

                }
                else if (i.tipo == "Plata")
                {
                    //incrementa una moneda de plata
                    coinPlata++;
                    i.tomado = true;

                }
                else if (i.tipo == "Escudo")
                {
                    if (coinOro >= 4 && coinPlata >= 4)
                    {
                        //obtiene el escudo si lo puede comprar
                        items.Add(i);
                        i.tomado = true;
                        coinPlata -= 4;
                        coinOro -= 4;
                        escudoContador += 25;
                    }

                }
                else if (i.tipo == "Pergamino")
                {
                    //Incrementa la vida del escudo
                    i.tomado = true;
                    escudoContador += 100;
                }
                else if (i.tipo == "Rayo")
                {
                    //Incrementa el daño del hacha
                    i.tomado = true;
                    Acceso.MisilDamage += 1;
                }
                else if (i.tipo == "Posion")
                {
                    if (coinOro >= 5 && coinPlata >= 5)
                    {
                        //Aumenta su velocidad si lo puede comprar
                        i.tomado = true;
                        items.Add(i);
                        coinOro -= 5;
                        coinPlata -= 5;
                    }
                }
                else if (i.tipo == "Hacha")
                {
                    //Obtiene el hacha
                    i.tomado = true;
                    items.Add(i);

                }
                else if (i.tipo == "Corona") { i.tomado = true; corona = true; }
                return true;
            }
            //LEFT
            else if (position.X >= i.position.X && position.X <= i.position.X + i.bitmap.Width && position.Y + CurrentFrame.Height / 2 >= i.position.Y && position.Y + CurrentFrame.Height / 2 <= i.position.Y + i.bitmap.Height)
            {
                if (i.tipo == "Vida")
                {
                    //Incrementa la vida en 10 unidades
                    vida += 10;
                    i.tomado = true;

                }
                else if (i.tipo == "Oro")
                {
                    //Incrementa una moneda de oro
                    coinOro++;
                    i.tomado = true;

                }
                else if (i.tipo == "Plata")
                {
                    //incrementa una moneda de plata
                    coinPlata++;
                    i.tomado = true;

                }
                else if (i.tipo == "Escudo")
                {
                    if (coinOro >= 4 && coinPlata >= 4)
                    {
                        //obtiene el escudo si lo puede comprar
                        items.Add(i);
                        i.tomado = true;
                        coinPlata -= 4;
                        coinOro -= 4;
                        escudoContador += 25;
                    }

                }
                else if (i.tipo == "Pergamino")
                {
                    //Incrementa la vida del escudo
                    i.tomado = true;
                    escudoContador += 100;
                }
                else if (i.tipo == "Rayo")
                {
                    //Incrementa el daño del hacha
                    i.tomado = true;
                    Acceso.MisilDamage += 1;
                }
                else if (i.tipo == "Posion")
                {
                    if (coinOro >= 5 && coinPlata >= 5)
                    {
                        //Aumenta su velocidad si lo puede comprar
                        i.tomado = true;
                        items.Add(i);
                        coinOro -= 5;
                        coinPlata -= 5;
                    }
                }
                else if (i.tipo == "Hacha")
                {
                    //Obtiene el hacha
                    i.tomado = true;
                    items.Add(i);

                }
                else if (i.tipo == "Corona") { i.tomado = true; corona = true; }
                return true;
            }

            else
            {
                
                return false;
            }
        } //Para saber si colisiono con un Item

        public void misilCollisionEnemy(List<Animado> list)
        {
            foreach (Enemigo en in list)
            {
                foreach (Misil m in misilesHacha)
                {
                    if (en.IsCollision(m)) { m.outOfBounds = true; break; }
                }
            }
        } //Para saber si el misil colisiono con un Enemigo

        public void misilCollisionBase(List<Base> list)
        {
            foreach (Misil m in misilesHacha)
            {
                foreach (Base b in list)
                {
                    m.IsCollision(b);
                }
            }
        } //Para saber si el misil colisiono con una base
        
    }
}
