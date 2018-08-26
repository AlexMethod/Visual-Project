using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using WMPLib;


namespace Game
{
    public partial class Juego : Form
    {
        private System.Windows.Forms.Timer timer;
        List<Animado> enemigos = new List<Animado>();
        Personaje jugador;
        Menu menu;
        WindowsMediaPlayer musica = new WindowsMediaPlayer();
        Base fondo1 = new Base(Game.Properties.Resources.Background1, 0, 0);
        Base fondo2 = new Base(Game.Properties.Resources.Background1, 928, 0);
        List<Base> bases = new List<Base>();
        List<Item> items = new List<Item>();
 
        int cont = 0;
        public Juego()
        {
            musica.URL = Application.StartupPath + @"\Sad Songs.mp3";
            //The Acces class has all of the resources to be 
            Acceso.Speed = 10;
            Acceso.SpeedMisil = 15;
            Acceso.Gravity = 0.5f;
            Acceso.MisilDamage = 1;
            Acceso.Images.Add("ScootRun1", new Bitmap(Game.Properties.Resources.ScootRun1));
            Acceso.Images.Add("ScootRun2",new Bitmap(Game.Properties.Resources.ScootRun2));
            Acceso.Images.Add("ScootRun3",new Bitmap(Game.Properties.Resources.ScootRun3));
            Acceso.Images.Add("ScootRun4",new Bitmap(Game.Properties.Resources.ScootRun4));
            Acceso.Images.Add("ScootRun5",new Bitmap(Game.Properties.Resources.ScootRun5));
            Acceso.Images.Add("ScootRun6",new Bitmap(Game.Properties.Resources.ScootRun6));
            Acceso.Images.Add("ScootRun7",new Bitmap(Game.Properties.Resources.ScootRun7));
            Acceso.Images.Add("ScootRun8",new Bitmap(Game.Properties.Resources.ScootRun8));
            Acceso.Images.Add("ScootJump1",new Bitmap(Game.Properties.Resources.Scoot1));
            Acceso.Images.Add("ScootJump2",new Bitmap(Game.Properties.Resources.Scoot2));
            Acceso.Images.Add("ScootJump3",new Bitmap(Game.Properties.Resources.Scoot3));
            Acceso.Images.Add("ScootJump4",new Bitmap(Game.Properties.Resources.Scoot4));
            Acceso.Images.Add("ScootJump5",new Bitmap(Game.Properties.Resources.Scoot6));
            Acceso.Images.Add("ScootJump6",new Bitmap(Game.Properties.Resources.Scoot7));
            Acceso.Images.Add("ScootJump7",new Bitmap(Game.Properties.Resources.Scoot8));
            Acceso.Images.Add("ScootJump8",new Bitmap(Game.Properties.Resources.Scoot9));
            Acceso.Images.Add("ScootJump9",new Bitmap(Game.Properties.Resources.Scoot10));
            Acceso.Images.Add("ScootStill1",new Bitmap(Game.Properties.Resources.ScootStill1));
            Acceso.Images.Add("ScootStill2",new Bitmap(Game.Properties.Resources.ScootStill2));
            Acceso.Images.Add("ScootStill3",new Bitmap(Game.Properties.Resources.ScootStill3));
            Acceso.Images.Add("ScootStill4",new Bitmap(Game.Properties.Resources.ScootStill4));
            Acceso.Images.Add("ScootStill5",new Bitmap(Game.Properties.Resources.ScootStill5));
            Acceso.Images.Add("ScootStill6",new Bitmap(Game.Properties.Resources.ScootStill6));
            Acceso.Images.Add("ScootHit1", new Bitmap(Game.Properties.Resources.ScootHit1));
            Acceso.Images.Add("ScootHit2", new Bitmap(Game.Properties.Resources.ScootHit2));
            Acceso.Images.Add("ScootHit3", new Bitmap(Game.Properties.Resources.ScootHit3));
            Acceso.Images.Add("ScootHit4", new Bitmap(Game.Properties.Resources.ScootHit4));
            Acceso.Images.Add("ScootHit5", new Bitmap(Game.Properties.Resources.ScootHit5));
            Acceso.Images.Add("ScootHit6", new Bitmap(Game.Properties.Resources.ScootHit6));
            Acceso.Images.Add("Ghost1", new Bitmap(Game.Properties.Resources.Ghost1));
            Acceso.Images.Add("Ghost2", new Bitmap(Game.Properties.Resources.Ghost2));
            Acceso.Images.Add("Ghost3", new Bitmap(Game.Properties.Resources.Ghost3));
            Acceso.Images.Add("Ghost4", new Bitmap(Game.Properties.Resources.Ghost4));
            Acceso.Images.Add("Ghost5", new Bitmap(Game.Properties.Resources.Ghost5));
            Acceso.Images.Add("Ghost6", new Bitmap(Game.Properties.Resources.Ghost6));
            Acceso.Images.Add("Ghost7", new Bitmap(Game.Properties.Resources.Ghost7));
            Acceso.Images.Add("Ghost8", new Bitmap(Game.Properties.Resources.Ghost8));
            Acceso.Images.Add("Ghost9", new Bitmap(Game.Properties.Resources.Ghost8));
            Acceso.Images.Add("Ghost10", new Bitmap(Game.Properties.Resources.Ghost10));
            Acceso.Images.Add("GuitarSmash1", new Bitmap(Game.Properties.Resources.GuitarSmash1));
            Acceso.Images.Add("GuitarSmash2", new Bitmap(Game.Properties.Resources.GuitarSmash2));
            Acceso.Images.Add("GuitarSmash3", new Bitmap(Game.Properties.Resources.GuitarSmash3));
            Acceso.Images.Add("GuitarSmash4", new Bitmap(Game.Properties.Resources.GuitarSmash4));
            Acceso.Images.Add("GuitarSmash5", new Bitmap(Game.Properties.Resources.GuitarSmash5));
            Acceso.Images.Add("ScootPower1", new Bitmap(Game.Properties.Resources.ScootPower1));
            Acceso.Images.Add("ScootPower2", new Bitmap(Game.Properties.Resources.ScootPower2));
            Acceso.Images.Add("ScootPower3", new Bitmap(Game.Properties.Resources.ScootPower3));

            timer = new System.Windows.Forms.Timer();
            timer.Interval = (int)((1.0 / 60.0) * 1000.0);
            timer.Tick += intro;
            timer.Start();
            
            InitializeComponent();
        }

        void UpDate(object sender, EventArgs e)
        {
            //Si el jugador toma la corona, salva al pueblo
            if (jugador.corona == true)
            {
                label10.Visible = true;
                timer.Tick -= UpDate;
                timer.Tick += Espera;
            }
            if (jugador.muerto == true)
            {
                label6.Visible = true;
                if(jugador.left==true){
                    jugador.Flip();
                }
                timer.Tick -= UpDate;
                timer.Tick += Espera;
            }
            //Actualiza los labels de los atributos del jugador y la cantidad de enemigos
            label2.Text = "x" + jugador.vida;
            label3.Text = "x" + jugador.coinOro;
            label4.Text = "x" + jugador.coinPlata;
            label5.Text = "x" + enemigos.Count;
            label9.Text = "x" + jugador.escudoContador;
            //solo hace visible el label del escudo cuando este esta seleccionado
            if (jugador.escudo == true) { label9.Visible = true; } else { label9.Visible = false; }
            //Pone el mapa de bits del item capturado
            if (jugador.items.Count == 1)
            {
                    pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
                    pictureBox2.BackgroundImage = jugador.items[0].bitmap;    
            }
            if (jugador.items.Count == 2)
            {

                pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
                pictureBox3.BackgroundImage = jugador.items[1].bitmap;
                label7.Visible = false;
            }
            if (jugador.items.Count == 3)
            {
                pictureBox4.BackgroundImageLayout = ImageLayout.Stretch;
                pictureBox4.BackgroundImage = jugador.items[2].bitmap;
                label8.Visible = false;
            }
            //Si la musica se ha detenido entonces se reinicia
            if (musica.status == "Detenido") { musica.controls.play(); }
            //El metodo UpDate cambia los frames de la animacion
            if (fondo1.X + fondo1.bitmap.Width <= 0)
            {
                fondo1.X = fondo2.X+fondo2.bitmap.Width;
            }
            if (fondo2.X + fondo2.bitmap.Width <= 0)
            {
                fondo2.X = fondo1.X + fondo1.bitmap.Width;
            }
            //Para eliminar los objetos que esten fuera del escenario
            foreach (Base b in bases)
            {
                if (b.X + b.bitmap.Width <= 0 || b.muerto==true)
                {
                    b.outOfBounds = true;
                }
            }

            //Para saber si estan fuera del escenario, si lo estan solo se eliminan
            foreach (Item i in items)
            {
                if (i.position.X + i.bitmap.Width <= 0)
                {
                    i.outOfBounds = true;
                }
            }
            //Para saber si estan fuera del escenario, si lo esta solo se eliminan
            foreach (Enemigo b in enemigos) 
            {
                if (b.position.X + b.CurrentFrame.Width <= 0)
                {
                    b.muerto = true;
                }
            }

            

                //Este foreach hace el metodo UpDate y el metodo Move para cada objeto de animados
            foreach (Animado it1 in enemigos)
            {
                it1.UpDate();
                it1.Move();
                //con este foreach se detectan las colisiones de los objetos de animados
                //con ellos mismos. Y con la condicion para que no detecte colisiones con
                //el mismo objeto.
                foreach (Animado it2 in enemigos)
                {
                    if (it1.ID != it2.ID)
                    {
                        it1.IsCollision((Enemigo)it2);
                    }
                }
                //con esto nos aseguramos que de se detecte la colision de todos los 
                //elementos de animados con el jugador
                jugador.IsCollision((Enemigo)it1);
            }
            jugador.misilCollisionEnemy(enemigos);
            //Este ciclo detecta si el jugador esta en alguna base 
            foreach (Base b in bases)
            {
                if(jugador.IsCollision(b)==true)break;
            }
            //Detecta las coliciones con los items
            foreach (Item i in items) 
            {
                if (jugador.IsCollision(i) == true)
                {
                    break;
                }
            }
            //Este ciclo detecta colisiones de los elementos de animados con elementos de las bases
            foreach (Base b in bases)
            {
                foreach (Animado a in enemigos)
                {
                    if (a.IsCollision(b) == true) break;
                }
            }

            jugador.misilCollisionBase(bases);
            //Se hace UpDate para el jugador
            jugador.UpDate();
            //Esta condicion hace el scroll del fondo
            //Cuando el personaje llega al medio, todos los objetos se mueven hacia atras
            //y asi se simula scroll :)
            bool move = jugador.Move();
            if (jugador.position.X + jugador.CurrentFrame.Width >= Acceso.GameSize.Width/2 && move==true)
            {
                fondo1.Move(); fondo2.Move();
                foreach (Base b in bases) { b.Move(); }
                foreach (Enemigo i in enemigos) { i.MoveLeft(); }
                foreach (Item i in items) { i.Move(); }
                //Label del precio del segundo item recolectado
                label7.Location = new Point(label7.Location.X-(int)Acceso.Speed,630);
                label8.Location = new Point(label8.Location.X - (int)Acceso.Speed, 630);
            }
            //Condicion para saber si los enemigos estan muertos o si estan fuera del escenario
            for (int i = 0; i < enemigos.Count; i++)
            {
                if (enemigos[i].muerto == true)
                {
                    enemigos.Remove(enemigos[i]);
                }
            }
            //Condicion para saber si las bases estan fuera del escenario, si lo estan se eliminan
            for (int i = 0; i < bases.Count; i++)
            {
                if (bases[i].outOfBounds == true)
                {
                    bases.Remove(bases[i]);
                }
            }
            //Condicion para saber si los items fueron tomados o estan fuera del escenario, si lo estan se eliminan
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].outOfBounds == true)
                {
                    items.Remove(items[i]);
                }
            }
            //Para saber si fueron tomados, si fueron tomados por el jugador, se eliminan
            for (int i = 0; i < items.Count;i++) 
            {
                if (items[i].tomado == true)
                {
                    items.Remove(items[i]);
                }
            }

            
                //Repaints everything :3
                Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox11.Visible = true;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            pictureBox15.Visible = false;
            pictureBox16.Visible = false;
            this.ClientSize = new Size(928, 700);
            Acceso.GameSize = this.ClientSize;
            newGame();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Obtiene el evento del paint para ser utilizado por los metodos de Draw de todos los objetos
            //que se dibujan
            Acceso.PaintEventArgs = e;
            //Dibuja los fondos
            fondo1.Draw();
            fondo2.Draw();
            //Dibuja al judagor
            jugador.Draw();
            foreach (Item it in items) 
            { 
                if(it.position.X<=Acceso.GameSize.Width)
                {
                    it.Draw();
                }
            }
            //Dibuja a todos los elementos de animados
            foreach (Animado a in enemigos) 
            {
                if (a.position.X <= Acceso.GameSize.Width) { a.Draw(); } 
            }
            //Dibuja a todos los elementos de bases
            foreach (Base b in bases) { if (b.X <= Acceso.GameSize.Width) { b.Draw(); } }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Obtiene el evento e para ser utilizado por los metodos 
            //de las clases que requieran saber la tecla presionada
            Acceso.KeyPressed = e;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //Se hace null el atributo KeyPressed puesto que la tecla ya se solto
            Acceso.KeyPressed = null;
            //Se obtiene el evento para ser utilizado por los metodos de las clases 
            //que requieran saber la tecla que fue soltada
            Acceso.KeyUp = e;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //Obtiene el tamaño de la clase
            Acceso.GameSize = this.ClientSize;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //Boton menu, esconde la forma actual y muestra el menu
            this.Hide();
            menu.Show();
        }

        public void getRef(Menu m)
        {
            //Es usado para tener una referencia de la instancia del menu y poder acceder a el
            menu = m;
        }

        public void intro(object sender, EventArgs e)
        {
            //Aqui esta el codigo para mostrar la historia del videojuego y para que reproduzca la 
            //musica
            musica.controls.play();
            if (Acceso.KeyUp != null && Acceso.KeyUp.KeyData == Keys.Enter && cont == 0)
            {
                pictureBox11.Visible = false;
                pictureBox12.Visible = true;
                Acceso.KeyUp = null;
                cont++;
            }
            else if (Acceso.KeyUp != null && Acceso.KeyUp.KeyData == Keys.Enter && cont == 1)
            {
                pictureBox12.Visible = false;
                pictureBox13.Visible = true;
                Acceso.KeyUp = null;
                cont++;
            }
            else if (Acceso.KeyUp != null && Acceso.KeyUp.KeyData == Keys.Enter && cont == 2)
            {
                pictureBox13.Visible = false;
                pictureBox14.Visible = true;
                Acceso.KeyUp = null;
                cont++;
            }
            else if (Acceso.KeyUp != null && Acceso.KeyUp.KeyData == Keys.Enter && cont == 3)
            {
                pictureBox14.Visible = false;
                pictureBox15.Visible = true;
                Acceso.KeyUp = null;
                cont++;
            }
            else if (Acceso.KeyUp != null && Acceso.KeyUp.KeyData == Keys.Enter && cont == 4)
            {
                pictureBox15.Visible = false;
                pictureBox16.Visible = true;
                Acceso.KeyUp = null;
                cont++;
            }
            else if (Acceso.KeyUp != null && Acceso.KeyUp.KeyData == Keys.Enter && cont == 5)
            {
                pictureBox16.Visible = false;
                Acceso.KeyUp = null;
                timer.Tick += UpDate;
                timer.Tick -= intro;
                
                cont++;
            }
            else if (Acceso.KeyPressed != null && Acceso.KeyPressed.KeyData == Keys.S)
            {
                pictureBox11.Visible = false;
                pictureBox12.Visible = false;
                pictureBox13.Visible = false;
                pictureBox14.Visible = false;
                pictureBox15.Visible = false;
                pictureBox16.Visible = false;
                timer.Tick += UpDate;
                timer.Tick -= intro;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //Item 1
            if (jugador.items.Count > 0)
            {
                pictureBox10.BackgroundImage = pictureBox2.BackgroundImage;
                pictureBox10.BackgroundImageLayout = ImageLayout.Stretch;
                if (jugador.items[0].tipo == "Escudo")
                {
                    jugador.escudo = true;
                    jugador.pergamino = false;
                    jugador.rayo = false;
                    jugador.posionA = false;
                    jugador.hacha = false;
                    jugador.corona = false;
                }
                if (jugador.items[0].tipo == "Pergamino")
                {
                    jugador.escudo = false;
                    jugador.pergamino = true;
                    jugador.rayo = false;
                    jugador.posionA = false;
                    jugador.hacha = false;
                    jugador.corona = false;
                }
                if (jugador.items[0].tipo == "Rayo")
                {
                    jugador.escudo = false;
                    jugador.pergamino = false;
                    jugador.rayo = true;
                    jugador.posionA = false;
                    jugador.hacha = false;
                    jugador.corona = false;
                }
                if (jugador.items[0].tipo == "Posion")
                {
                    jugador.escudo = false;
                    jugador.pergamino = false;
                    jugador.rayo = false;
                    jugador.posionA = true;
                    jugador.hacha = false;
                    jugador.corona = false;
                }
                if (jugador.items[0].tipo == "Hacha")
                {
                    jugador.escudo = false;
                    jugador.pergamino = false;
                    jugador.rayo = false;
                    jugador.posionA = false;
                    jugador.hacha = true;
                    jugador.corona = false;
                }
                if (jugador.items[0].tipo == "Corona")
                {
                    jugador.escudo = false;
                    jugador.pergamino = false;
                    jugador.rayo = false;
                    jugador.posionA = false;
                    jugador.hacha = false;
                    jugador.corona = true;
                }
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //Item 2
            if (jugador.items.Count > 1)
            {
                pictureBox10.BackgroundImage = pictureBox3.BackgroundImage;
                pictureBox10.BackgroundImageLayout = ImageLayout.Stretch;
                if (jugador.items[1].tipo == "Escudo")
                {
                    jugador.escudo = true;
                    jugador.pergamino = false;
                    jugador.rayo = false;
                    jugador.posionA = false;
                    jugador.hacha = false;
                    jugador.corona = false;
                }
                if (jugador.items[1].tipo == "Pergamino")
                {
                    jugador.escudo = false;
                    jugador.pergamino = true;
                    jugador.rayo = false;
                    jugador.posionA = false;
                    jugador.hacha = false;
                    jugador.corona = false;
                }
                if (jugador.items[1].tipo == "Rayo")
                {
                    jugador.escudo = false;
                    jugador.pergamino = false;
                    jugador.rayo = true;
                    jugador.posionA = false;
                    jugador.hacha = false;
                    jugador.corona = false;
                }
                if (jugador.items[1].tipo == "Posion")
                {
                    jugador.escudo = false;
                    jugador.pergamino = false;
                    jugador.rayo = false;
                    jugador.posionA = true;
                    jugador.hacha = false;
                    jugador.corona = false;
                }
                if (jugador.items[1].tipo == "Hacha")
                {
                    jugador.escudo = false;
                    jugador.pergamino = false;
                    jugador.rayo = false;
                    jugador.posionA = false;
                    jugador.hacha = true;
                    jugador.corona = false;
                }
                if (jugador.items[1].tipo == "Corona")
                {
                    jugador.escudo = false;
                    jugador.pergamino = false;
                    jugador.rayo = false;
                    jugador.posionA = false;
                    jugador.hacha = false;
                    jugador.corona = true;
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //Item 3
            if (jugador.items.Count > 2)
            {
                pictureBox10.BackgroundImage = pictureBox4.BackgroundImage;
                pictureBox10.BackgroundImageLayout = ImageLayout.Stretch;
                if (jugador.items[2].tipo == "Escudo")
                {
                    jugador.escudo = true;
                    jugador.pergamino = false;
                    jugador.rayo = false;
                    jugador.posionA = false;
                    jugador.hacha = false;
                    jugador.corona = false;
                }
                if (jugador.items[2].tipo == "Pergamino")
                {
                    jugador.escudo = false;
                    jugador.pergamino = true;
                    jugador.rayo = false;
                    jugador.posionA = false;
                    jugador.hacha = false;
                    jugador.corona = false;
                }
                if (jugador.items[2].tipo == "Rayo")
                {
                    jugador.escudo = false;
                    jugador.pergamino = false;
                    jugador.rayo = true;
                    jugador.posionA = false;
                    jugador.hacha = false;
                    jugador.corona = false;
                }
                if (jugador.items[2].tipo == "Posion")
                {
                    jugador.escudo = false;
                    jugador.pergamino = false;
                    jugador.rayo = false;
                    jugador.posionA = true;
                    jugador.hacha = false;
                    jugador.corona = false;
                }
                if (jugador.items[2].tipo == "Hacha")
                {
                    jugador.escudo = false;
                    jugador.pergamino = false;
                    jugador.rayo = false;
                    jugador.posionA = false;
                    jugador.hacha = true;
                    jugador.corona = false;
                }
                if (jugador.items[2].tipo == "Corona")
                {
                    jugador.escudo = false;
                    jugador.pergamino = false;
                    jugador.rayo = false;
                    jugador.posionA = false;
                    jugador.hacha = false;
                    jugador.corona = true;
                }
            }
        }


        //En estos metodos llamados bloque se crean enemigos, bases e items en diferente posicion para
        //darle variedad de entorno al jugador, solo se mandan a llamar como uno quiera 
        public int bloque1(int x)
        {
            int y =580;
            Vector2 base1= new Vector2(Game.Properties.Resources.Base1.Width, Game.Properties.Resources.Base1.Height);
            Vector2 base5 = new Vector2(Game.Properties.Resources.Base5.Width, Game.Properties.Resources.Base5.Height);
            Vector2 enemigo = new Vector2(Game.Properties.Resources.Ghost1.Width, Game.Properties.Resources.Ghost1.Height);
            Vector2 item = new Vector2(Game.Properties.Resources.CoinOro.Width, Game.Properties.Resources.CoinOro.Height);
            items.Add(new CoinOro(x, 620));
            enemigos.Add(new Enemigo("Ghost",10,x, 360));
            bases.Add(new Base(Game.Properties.Resources.Base1, x+=(int)item.X + 40, y));
            items.Add(new CoinPlata(x+20, y-25));
            items.Add(new CoinPlata(x + 40, y - 25));
            enemigos.Add(new Enemigo("Ghost", 10, x + (int)base1.X+30, 645 - (int)enemigo.Y));
            bases.Add(new Base(Game.Properties.Resources.Base5, x+=(int)base1.X + 80, y -= (int)base5.Y + 60));
            bases.Add(new Base(Game.Properties.Resources.Base5,x+=(int)base5.X + 50,(y-=(int)base5.Y+40)));
            bases.Add(new Base(Game.Properties.Resources.Base5, x += (int)base5.X + 30, y -= (int)base5.Y + 40));
            items.Add(new CoinOro(x + 20, y - 25));
            bases.Add(new Base(Game.Properties.Resources.Base1, x += (int)base5.X + 60,580));
            enemigos.Add(new Enemigo("Ghost", 10, x -= 60, 360));
            bases.Add(new Base(Game.Properties.Resources.Base1, x += 60 + (int)base1.X+60, y-=(int)base1.Y+40));
            return x;


        }

        public int bloque2(int x)
        {
            int y=580;
            Vector2 base1= new Vector2(Game.Properties.Resources.Base1.Width, Game.Properties.Resources.Base1.Height);
            Vector2 base5 = new Vector2(Game.Properties.Resources.Base5.Width, Game.Properties.Resources.Base5.Height);
            Vector2 base7 = new Vector2(Game.Properties.Resources.Base7.Width, Game.Properties.Resources.Base7.Height);
            Vector2 enemigo = new Vector2(Game.Properties.Resources.Ghost1.Width, Game.Properties.Resources.Ghost1.Height);
            Vector2 item = new Vector2(Game.Properties.Resources.CoinOro.Width, Game.Properties.Resources.CoinOro.Height);
            enemigos.Add(new Enemigo("Ghost",10,x,280));
            bases.Add(new Base(Game.Properties.Resources.Base1, x, y));
            bases.Add(new Base(Game.Properties.Resources.Base7, x += (int)base1.X + 50, y -= 70));
            bases.Add(new Base(Game.Properties.Resources.Base5, x += (int)base7.X + 50, y -= 70));
            bases.Add(new Base(Game.Properties.Resources.Base5, x += (int)base5.X + 50, y -= 70));
            items.Add(new CoinPlata(x+20,y-25));
            enemigos.Add(new Enemigo("Ghost", 10, x-100, y + 200));
            items.Add(new CoinOro(x - 200, 620));
            items.Add(new CoinPlata(x - 130, 620));
            bases.Add(new Base(Game.Properties.Resources.Base1,x,580));
            enemigos.Add(new Enemigo("Ghost",10,x+60,580-(int)base1.Y-10));
            bases.Add(new Base(Game.Properties.Resources.Base7, x += (int)base5.X + 80, y -= 120));
            items.Add(new CoinPlata(x+20,y-25));
            items.Add(new CoinPlata(x + 50, y - 25));
            enemigos.Add(new Enemigo("Ghost", 10, x += 60, y+=40));
            bases.Add(new Base(Game.Properties.Resources.Base5,x+=200,y+=200));
            return x;
        }

        public int lastLevel(int x)
        {
            int y=580;
            Vector2 base1 = new Vector2(Game.Properties.Resources.Base1.Width,Game.Properties.Resources.Base1.Height);
            bases.Add(new Base(Game.Properties.Resources.Base1,x,y));
            bases.Add(new Base(Game.Properties.Resources.Base1, x, y-=(int)base1.Y));
            bases.Add(new Base(Game.Properties.Resources.Base1, x + 600, y+20));
            enemigos.Add(new Enemigo("Ghost", 10, x + Game.Properties.Resources.Base1.Width + 30, y));
            enemigos.Add(new Enemigo("Ghost", 10, x + Game.Properties.Resources.Base1.Width + 200, y));

            bases.Add(new Base(Game.Properties.Resources.Base1, x, y -= (int)base1.Y));
            bases.Add(new Base(Game.Properties.Resources.Base1, x + 1000, y + 20));
            bases.Add(new Base(Game.Properties.Resources.Base1, x, y -= (int)base1.Y));
            bases.Add(new Base(Game.Properties.Resources.Base1, x, y -= (int)base1.Y));
            bases.Add(new Base(Game.Properties.Resources.Base1, x + 600, y+20));
            enemigos.Add(new Enemigo("Ghost", 10, x + Game.Properties.Resources.Base1.Width + 30, y));
            enemigos.Add(new Enemigo("Ghost", 10, x + Game.Properties.Resources.Base1.Width + 200, y));
            enemigos.Add(new Enemigo("Ghost", 10, x + 600 + Game.Properties.Resources.Base1.Width + 30, y));
            enemigos.Add(new Enemigo("Ghost", 10, x + 600 + Game.Properties.Resources.Base1.Width + 200, y));

            bases.Add(new Base(Game.Properties.Resources.Base1, x, y -= (int)base1.Y));
            bases.Add(new Base(Game.Properties.Resources.Base1, x + 1000, y + 20));
            bases.Add(new Base(Game.Properties.Resources.Base1, x, y -= (int)base1.Y));
            bases.Add(new Base(Game.Properties.Resources.Base1, x, y -= (int)base1.Y));
            bases.Add(new Base(Game.Properties.Resources.Base1, x + 600, y+20));
            enemigos.Add(new Enemigo("Ghost", 10, x + Game.Properties.Resources.Base1.Width + 30, y));
            enemigos.Add(new Enemigo("Ghost", 10, x + Game.Properties.Resources.Base1.Width + 200, y));
            enemigos.Add(new Enemigo("Ghost", 10, x + 600 + Game.Properties.Resources.Base1.Width + 30, y));
            enemigos.Add(new Enemigo("Ghost", 10, x + 600 + Game.Properties.Resources.Base1.Width + 200, y));
            bases.Add(new Base(Game.Properties.Resources.Base1, x, y -= (int)base1.Y));
            bases.Add(new Base(Game.Properties.Resources.Base1, x + 1000, y + 20));
            bases.Add(new Base(Game.Properties.Resources.Base1, x, y -= (int)base1.Y));
            y=580;
            x+=500;
            for (int i = 0; i <= 8; i++)
            {
                enemigos.Add(new Enemigo("Ghost", 10, x+=300, y-=Game.Properties.Resources.Ghost1.Height+10));
                enemigos.Add(new Enemigo("Ghost",10,x+=Game.Properties.Resources.Ghost1.Width+10,530));
            }
            y = 580;
            bases.Add(new Base(Game.Properties.Resources.Base1, x+600, y));
            for (int i = 0; i < 8; i++)
            {
                bases.Add(new Base(Game.Properties.Resources.Base1, x+600, y -= (int)base1.Y));
            }
                bases.Add(new Base(Game.Properties.Resources.Car2, x += 2000, 600));
            items.Add(new Corona(x += 1000, 620));
            //foreach (Enemigo en in enemigos) { en.vida = 100; }


            return x;
        }

        public void newGame()
        {
            label7.Visible = true;
            label8.Visible = true;
            Acceso.GameSize = this.ClientSize;
            jugador = new Personaje("ScootStill", "ScootRun", "ScootJump", "ScootHit", 6, 8, 9, 6);
            bases.Add(new Base(Game.Properties.Resources.Car1, 0, 645 - Game.Properties.Resources.Car1.Height)); //530
            items.Add(new Hacha(350, 620));
            items.Add(new Vida(400, 620));
            int lastP = bloque1(bloque1(bloque1(500)));
            items.Add(new Posion(lastP, 620));
            label7.Location = new Point(lastP, 630);
            int lastP2 = bloque2(bloque2(bloque2(lastP + 100)));
            items.Add(new Escudo(lastP2, 620));
            label8.Location = new Point(lastP2, 630);
            int lastP3 = bloque1(bloque1(bloque1(lastP2 + 200)));
            items.Add(new Pergamino(lastP3 +40, 620));
            int lastP4 = bloque2(bloque2(bloque2(lastP3+100)));
            items.Add(new Rayo(lastP4 += 300, 620));
            int lastP5 = bloque1(bloque1(bloque1(bloque1(lastP4 + 200))));
            lastLevel(lastP5+500);
            pictureBox2.BackgroundImage = null;
            pictureBox3.BackgroundImage = null;
            pictureBox4.BackgroundImage = null;
            pictureBox10.BackgroundImage = null;
        }

        //Este metodo es para espera del usuario cuando el jugador ha muerto o ha ganado
        public void Espera(object sender, EventArgs e) 
        {
            if (Acceso.KeyPressed != null && Acceso.KeyPressed.KeyData == Keys.Enter)
            {
                label6.Visible = false;
                label10.Visible = false;
                jugador = null;
                bases.Clear();
                items.Clear();
                enemigos.Clear();
                newGame();
                pictureBox11.Visible = false;
                timer.Tick -= Espera;
                timer.Tick += UpDate;
            }
        }

    }
}
