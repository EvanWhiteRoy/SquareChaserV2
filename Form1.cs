using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//June 16th
//Evan White-Roy
//Square Chaser

namespace SquareChaserV2
{
    public partial class Form1 : Form
    {
        //Player Speeds
        int player1Speed = 3;
        int player2Speed = 3;
        int totalSpeed = 7;

        int stop = 175;

        //Player Score
        int player1Score = 0;
        int player2Score = 0;

        //Movement Initlaize
        bool wPressed = false;
        bool sPressed = false;
        bool aPressed = false;
        bool dPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        bool rightPressed = false;
        bool leftPressed = false;

        //Random Generator
        Random randGen = new Random();

        //Players
        Rectangle player1 = new Rectangle(75, 130, 20, 20);
        Rectangle player2 = new Rectangle(75, 130, 20, 20);
        Rectangle pointSquare = new Rectangle(75, 130, 20, 20);
        Rectangle speedCircle = new Rectangle(75, 130, 20, 20);
        Rectangle boundary = new Rectangle(20, 20, 310, 250);

        //Colours and Brushes
        SolidBrush blueBrush = new SolidBrush(Color.AliceBlue);
        SolidBrush grayBrush = new SolidBrush(Color.Gray);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush goldBrush = new SolidBrush(Color.DarkGoldenrod);
        SolidBrush blackBrush = new SolidBrush(Color.Transparent);

        //Sounds for Interactions
        SoundPlayer collect = new SoundPlayer(Properties.Resources.Collect);
        SoundPlayer win = new SoundPlayer(Properties.Resources.Winner);
        //Backround Image
        Image backImage = Properties.Resources.backPicutre;

        //Boundry Colour
        Pen blackPen = new Pen(Color.Black, 6);

        public Form1()
        {
            InitializeComponent();
            PointRandomizer();
            SpeedRandomizer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //Enable when the keys are not pressed
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //When the keys are pressed
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Colour Rectangles
            e.Graphics.FillRectangle(whiteBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillEllipse(grayBrush, speedCircle);
            e.Graphics.FillRectangle(goldBrush, pointSquare);
            e.Graphics.FillRectangle(blackBrush, boundary);
        }

        public void PointRandomizer()
        {
            //Randomizer for point square
            pointSquare.X = randGen.Next(20, 300);
            pointSquare.Y = randGen.Next(20, 250);
        }
        public void SpeedRandomizer()
        {
            //Randomizer for point square
            speedCircle.X = randGen.Next(20, 300);
            speedCircle.Y = randGen.Next(20, 250);
        }

        public void Movement()
        {
            if(player1Speed > totalSpeed)
            {
                player1Speed = totalSpeed;
            }
            if (player2Speed > totalSpeed)
            {
                player2Speed = totalSpeed;
            }
        }
        public void PlayerMovement()
        {
            //move player 1
            if (wPressed == true && player1.Y > 20)
            {
                player1.Y = player1.Y - player1Speed;
            }
            if (sPressed == true && player1.Y < 250)
            {
                player1.Y = player1.Y + player1Speed;
            }
            if (aPressed == true && player1.X > 20)
            {
                player1.X = player1.X - player1Speed;
            }
            if (dPressed == true && player1.X < 310)
            {
                player1.X = player1.X + player1Speed;
            }

            //move player 2
            if (upPressed == true && player2.Y > 20.5)
            {
                player2.Y = player2.Y - player2Speed;
            }
            if (downPressed == true && player2.Y < 250)
            {
                player2.Y = player2.Y + player2Speed;
            }
            if (leftPressed == true && player2.X > 20)
            {
                player2.X = player2.X - player2Speed;
            }
            if (rightPressed == true && player2.X < 310)
            {
                player2.X = player2.X + player2Speed;
            }
        }
        public void CollisonGame()
        {
            //Check if orbs hit player 1
            if(pointSquare.IntersectsWith(player1))
            {
                player1Score++;
                player1Label.Text = $"{player1Score}";

                collect.Play();
                pointSquare.X = randGen.Next(20, 300);
                pointSquare.Y = randGen.Next(20, 250);

            }
            else if(speedCircle.IntersectsWith(player1))
            {
                player1Speed++;
            
                collect.Play();
                speedCircle.X = randGen.Next(20, 300);
                speedCircle.Y = randGen.Next(20, 250);
            }
            //Check if orbs hit player 2
            if (pointSquare.IntersectsWith(player2))
            {
                player2Score++;
                player2Label.Text = $"{player2Score}";

                collect.Play();
                pointSquare.X = randGen.Next(20, 300);
                pointSquare.Y = randGen.Next(20, 250);

            }
            else if (speedCircle.IntersectsWith(player2))
            {
                player2Speed++;

                collect.Play();
                speedCircle.X = randGen.Next(20, 300);
                speedCircle.Y = randGen.Next(20, 250);
            }

        }

        public void WinnerCheck()
        {
            if (player1Score == 5)
            {
                winLabel2.Text = "Player 1 Wins";
                gameTimer.Stop();
                win.Play();
            }
            if (player2Score == 5)
            {
                winLabel2.Text = "Player 2 Wins";
                gameTimer.Stop();
                win.Play();
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            Movement();
            PlayerMovement();
            CollisonGame();
            WinnerCheck();
            Refresh();
        }
    }
    }
    


