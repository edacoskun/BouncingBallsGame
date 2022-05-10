using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncingBallProje
{
    public partial class Form1 : Form
    {
        int posX = 6, posY = 6, score = 0;
        
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 35;
            //timer1.Interval = 10000; //10 sn
        }

        private void HitEvent()
        {
            //ust labellere carpma
            if(ball.Top <= label2.Bottom && ball.Top <= label3.Bottom)
            {
                posY *= -1;
            }
            //ortadan disari cikma
            if (ball.Top <= label2.Bottom && ball.Top <= label3.Bottom && ball.Left > label2.Right && ball.Right < label3.Left)
            {
                posX *= -1;
                posY *= -1;
            }

            //kontrol cubuguna carpma 
            if (ball.Bottom >= ControlBar.Top && ball.Left >= ControlBar.Left && ball.Right <= ControlBar.Right)
            {
                posY *= -1;
                score++;
            }
            //sol labele carpma
            else if (ball.Left <= label1.Right)
            {
                posX *= -1;
            }
            //sag labele carpma
            else if (ball.Right >= label4.Left)
            {
                posX *= -1;
            }
        }

        private void ExitEvent(object sender, EventArgs e)
        {
            if (ball.Top >= label1.Bottom && ball.Top >= label4.Bottom)
            {
                timer1.Stop();
                score -= 20;
                Form1_Load(sender, e);
            }
        }

        private void AgainBall()
        {
            Random rnd = new Random();
            string[] ballColors = { "red", "green", "blue", "yellow", "gray" };

            if (ball.Top >= label1.Bottom && ball.Top >= label4.Bottom)
            {
                ball.Location = new Point(rnd.Next(500), rnd.Next(300));

                if (ballColors[rnd.Next(5)]=="red")
                {
                    this.ball.BackColor = Color.Red;
                }
                if (ballColors[rnd.Next(5)] == "green")
                {
                    this.ball.BackColor = Color.Green;
                }
                if (ballColors[rnd.Next(5)] == "blue")
                {
                    this.ball.BackColor = Color.Blue;
                }
                if (ballColors[rnd.Next(5)] == "yellow")
                {
                    this.ball.BackColor = Color.Yellow;
                }
                if (ballColors[rnd.Next(5)] == "gray")
                {
                    this.ball.BackColor = Color.Gray;
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            ControlBar.Left = e.X;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            HitEvent();
            ExitEvent(sender, e);
            ball.Location = new Point(ball.Location.X + posX, ball.Location.Y + posY);

            label5.Text = "Score: " + score.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AgainBall();
            timer1.Enabled = true;
            button1.Text = "PAUSE";
            button2.Text = "PLAY";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
            }
            /*else
            {
                timer1.Enabled = true;
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
