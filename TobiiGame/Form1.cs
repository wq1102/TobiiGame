using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.Interaction;
using Tobii.Interaction.Framework;
using System.Runtime.InteropServices;


namespace TobiiGame
{
    
    public partial class Form1 : Form

    {
        int score = 0;
        int n = 0;
        List<int> Position = new List<int>();
        public Form1()
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile("D:\\TobiiGame\\green.jpg");
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = false;
            var host = new Host();
            int[,] CenterPoint = new int[,] { { Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 } };
            int P;
            Position.Add(0);
            var gazePointDataStream = host.Streams.CreateGazePointDataStream(Tobii.Interaction.Framework.GazePointDataMode.LightlyFiltered);
            axShockwaveFlash1.Movie = "D:\\rabbit.swf";
            axShockwaveFlash1.Rewind();
            axShockwaveFlash1.StopPlay();
            gazePointDataStream.GazePoint((x, y, _) =>
            {
                if (flag)
                {
                    this.axShockwaveFlash1.Play();
                    System.Timers.Timer t = new System.Timers.Timer(3000);
                    t.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；
                    t.AutoReset = false;//设置是执行一次（false）还是一直执行(true)；
                    t.Enabled = true;
                    flag = false;
                    
                }
                else
                {
                    if (x >= 0 && y >= 0)
                    {
                        if (x <= CenterPoint[0, 0] && y <= CenterPoint[0, 1])
                        {
                            P = 0;
                        }
                        else if (x > CenterPoint[0, 0] && y <= CenterPoint[0, 1])
                        {
                            P = 1;
                        }
                        else if (x > CenterPoint[0, 0] && y > CenterPoint[0, 1])
                        {
                            P = 2;
                        }
                        else
                        {
                            P = 3;
                        }
                    
                        if (Position[n] != P)
                        {
                            Position.Add(P);
                            n += 1;
                            
                            if (Position.Count() == 5)
                            {
                                //Console.WriteLine("{0},{1},{2},{3}", Position[0].ToString(), Position[1].ToString(), Position[2].ToString(), Position[3].ToString());
                                if ((Position[0] == 0 && Position[1] == 1 && Position[2] == 2 && Position[3] == 3 && Position[4] == 0) || (Position[0] == 0 && Position[1] == 3 && Position[2] == 2 && Position[3] == 1 && Position[4] == 0))
                                {
                                    Position.Clear();
                                    Position.Add(0);
                                    flag = true;
                                    n = 0;
                                }
                                else
                                {


                                    MessageBox.Show("请从左上角重新开始,3s后开始", "提示");

                                    System.Timers.Timer t1 = new System.Timers.Timer(3000);
                                    t1.Elapsed += new System.Timers.ElapsedEventHandler(KillMessageBox);
                                    t1.AutoReset = false;//设置是执行一次（false）还是一直执行(true)；
                                    t1.Enabled = true;
                                    //Console.WriteLine("失败：{0},{1},{2},{3},{4}", Position[0].ToString(), Position[1].ToString(), Position[2].ToString(), Position[3].ToString(), Position[4].ToString());
                                    



                                }
                            }
            
                        }
                    }
                    




                }
            });
            





        }
        public void theout(object source, System.Timers.ElapsedEventArgs e)

        {
            this.axShockwaveFlash1.StopPlay();
            score += 5;
            textBox3.Invoke((MethodInvoker)(() => textBox3.Text = score.ToString()));

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }
        private void KillMessageBox(object source, System.Timers.ElapsedEventArgs e)
        {
          
            Position.Clear();
            Position.Add(0);
            n = 0;
        }
    }
}
