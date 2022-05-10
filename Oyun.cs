using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BouncingBallProje
{
    public class Oyun
    {
        int posX = 6, posY = 6, score = 0;
        public Timer timer1;
        //public Timer timer2;
        public Button ball;
        public Form1 form;
        public Button button1;
        public Button button2;
        public Button button3;
        public Button button4;
        public Button button5;
        public Button button6;
        public Label label1;
        public Label label2;
        public Label label3;
        public Label label4;
        public Label label5;
        public Button ControlBar;

        public Oyun(Timer timer1, Button ball, Form1 form, Button button1, Button button2, Button button3, Button button4, Button button5, Button button6,
            Label label1, Label label2, Label label3, Label label4, Label label5, Button controlBar)
        {
            this.timer1 = timer1;
            this.ball = ball;
            this.form = form;
            this.button1 = button1;
            this.button2 = button2;
            this.button3 = button3;
            this.button4 = button4;
            this.button5 = button5;
            this.button6 = button6;
            this.label1 = label1;
            this.label2 = label2;
            this.label3 = label3;
            this.label4 = label4;
            this.label5 = label5;
            ControlBar = controlBar;
            this.timer1.Interval = 35;
        }

        public void HitEvent()
        {
            //ust labellere carpma
            if (ball.Top <= label2.Bottom && ball.Top <= label3.Bottom)
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


        public void ExitEvent()
        {
            if (ball.Top >= label1.Bottom && ball.Top >= label4.Bottom)
            {
                timer1.Stop();
                score -= 20;
                Load();
            }
        }


        public void Load()
        {
            AgainBall();
            timer1.Enabled = true;
            button1.Text = "PAUSE";
            button2.Text = "PLAY";
            button3.Text = "Backup";
            button4.Text = "Restore";
            button5.Text = "Encrypt";
            button6.Text = "Upload";
        }


        public void AgainBall()
        {
            Random rnd = new Random();
            string[] ballColors = { "red", "green", "blue", "yellow", "gray" };

            if (ball.Top >= label1.Bottom && ball.Top >= label4.Bottom)
            {
                ball.Location = new Point(rnd.Next(500), rnd.Next(300));

                if (ballColors[rnd.Next(5)] == "red")
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


        public void ControlMove(MouseEventArgs e) {
            ControlBar.Left = e.X;
        }

        public void ChangeBallLocation()
        {
            ball.Location = new Point(ball.Location.X + posX, ball.Location.Y + posY);
        }

        public void UpdateLabel()
        {
            label5.Text = "Score: " + score.ToString();
        }


        public void StopGame()
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

        public void StartGame()
        {
            timer1.Enabled = true;
        }

        //YEDEKLEME
        public void AskForInstallingFromBackup()
        {
            BEGIN:
            var response = Console.ReadLine();
            const string message = 
                "Yedekten Yükleme Yapılsın Mı?";
            const string caption = "Installing From Backup";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if(File.Exists(response))
            {
                // Eger hayır tuşuna basılmışsa
                if (result == DialogResult.No)
                {
                    //DeleteJsonFile(jsonFile);
                }
                else
                {
                    ReadJsonFile(jsonFile);
                }
            }
            else
            {
                Console.WriteLine("\nHata: Dosya Bulunamadı!");
                goto BEGIN;
            }
        }
        

        public void ReadJsonFile(string jsonFileIn)
        {
            dynamic jsonFile = JsonConvert.DeserializeObject(File.ReadAllText(jsonFileIn));

            Console.WriteLine($"Top Rengi: {jsonFile["ballColor"]}");
            Console.WriteLine($"Top Sayısı: {jsonFile["ballCount"]}");
            Console.WriteLine($"Skor: {jsonFile["Score"]}");
        }
      
        
       /*public void DeleteJsonFile(string jsonFileIn)
        {
            
        }*/
        
        //SİFRELEME VE YUKLEME
        public void EncryptionFile()
        {
            Byte[] eFile = File.ReadAllBytes(jsonFile.Text);

            for(int i = 0; i < eFile.Length; i++)
            {
                eFile[i] = (Byte)((int)eFile[i] + 1);
                if(eFile[i] > 255)
                {
                    eFile[i] = 0;
                }
            }

            File.WriteAllBytes(jsonFile.Text, eFile);
        }

        public void UploadFile()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog == DialogResult.OK)
                {
                    string path = Path.Combine("");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var fileName = System.IO.Path.GetFileName(dialog.FileName);
                    path += fileName;
                    File.Copy(dialog.FileName, path);
                }

            }
            catch (Exception ex) { }
        }
    }
}
