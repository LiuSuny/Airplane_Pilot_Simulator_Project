namespace AirPane_Simulator_Movement
{
    public partial class Form1 : Form
    {
         Image AirPlane;
        bool goLeft, goRight, goUp, goDown;
        int speed = 10;
        int positionX = 200;
        int positionY =200;
        int width = 50;
        int height = 50;

        public Form1()
        {
            InitializeComponent();

            this.BackgroundImage = Image.FromFile("AirplaneTakeoff.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            AirPlane = Image.FromFile("Airplane.jpg");
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            if(goLeft && positionX > 0)
            {
                positionX -= speed;
            }
            if (goRight && positionX + width < this.ClientSize.Width)
            {
                positionX += speed;
            }
            if (goUp && positionY > 0)
            {
                positionY -= speed;
            }
            if (goDown && positionY + height < this.ClientSize.Height)
            {
                positionY += speed;
            }
            this.Invalidate(); //this func refresh or event every 10 second
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            else if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            else if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }

        }

        private void FormPaintEvent(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            canvas.DrawImage(AirPlane, positionX, positionY, width, height);

        }
    }
}