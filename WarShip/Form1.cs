using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarShip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PictureBox[] myBox = new PictureBox[100];
        PictureBox[] cptBox = new PictureBox[100];
        int [] MS = new int[3];
        int[] CS = new int[3];                
        Image[] img = new Image[9];
        Random rdm = new Random();        
        int atkCnt = 0;
        int clkCnt = 0;
        int[] clkRec = new int[100];
        int[] lotRec = new int[100];
        bool myShp1Hit = false;
        bool myShp2Hit = false;
        bool myShp3Hit = false;
        bool cptShp1Hit = false;
        bool cptShp2Hit = false;
        bool cptShp3Hit = false;
        string win = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            img[0] = Image.FromFile("../../images/warship_front.png");
            img[1] = Image.FromFile("../../images/warship_rear.png");
            img[2] = Image.FromFile("../../images/warship_rotate_front.png");
            img[3] = Image.FromFile("../../images/warship_rotate_rear.png");
            img[4] = Image.FromFile("../../images/warship_front_fire.png");
            img[5] = Image.FromFile("../../images/warship_rear_fire.png");
            img[6] = Image.FromFile("../../images/warship_rotate_front_fire.png");
            img[7] = Image.FromFile("../../images/warship_rotate_rear_fire.png");
            img[8] = Image.FromFile("../../images/cannonball.png");           
            MyBoxForm();            
            CptBoxForm();
            MyShip();
            CShip();
            FirstFire();          
        }
        private void CptAttack()
        {
            
repeat:
            int where = rdm.Next(100);            
            lotRec[atkCnt] = where;            
            for (int i = 0; i < atkCnt; i++)
            {
                if (where == lotRec[i])
                {                    
                    goto repeat;
                }
            }    

            if (where == MS[0] )
            {
                myBox[where].Image = img[6];
                myShp1Hit = true;
                MessageBox.Show("my ship1 hit");                
            }
            else if (where ==  MS[0] - 10)
            {
                myBox[where].Image = img[7];
                myShp1Hit = true;
                MessageBox.Show("my ship1 hit");
            }
            else if (where == MS[1])
            {
                myBox[where].Image = img[4];
                myShp2Hit = true;
                MessageBox.Show("my ship2 hit");
            }
            else if (where == MS[1] + 1)
            {
                myBox[where].Image = img[5];
                myShp2Hit = true;
                MessageBox.Show("my ship2 hit");
            }
            else if (where == MS[2] )
            {
                myBox[where].Image = img[4];
                MessageBox.Show("my ship3 hit");
                myShp3Hit = true;
            }
            else if (where == MS[2] + 1)
            {
                myBox[where].Image = img[5];
                MessageBox.Show("my ship3 hit");
                myShp3Hit = true;
            }
            else
            {
                myBox[where].Image = img[8];
            }
            WinCheck();
            if ( win == "computer")
            {
                Replay();
                return;
            }
            else if(win == "gameover")
            {
                this.Close();
            }
            else if(win == null)
            {
                atkCnt++;
            }
            else
            {
                MessageBox.Show("something wrong");
            }
        }
        private string WinCheck()
        {
            if(myShp1Hit & myShp2Hit & myShp3Hit)
            {
                var result = MessageBox.Show("Player lose,would you want to play again","warship battle",MessageBoxButtons.YesNo);
                if (result ==DialogResult.Yes)
                {
                    return win = "computer";
                }
                else
                {
                    return win = "gameover"; 
                }                
                
            }
            else if (cptShp1Hit & cptShp2Hit & cptShp3Hit)
            {
                var result = MessageBox.Show("Player win,would you want to play again", "warship battle", MessageBoxButtons.YesNo);
                if (result ==DialogResult.Yes)
                {
                    return win = "me";
                }
                else
                {
                    return win = "gameover";
                }                
            }
            else
            {
                return win = null;
            }
        }
        
        private void Replay()
        {            
            atkCnt = 0;
            clkCnt = 0;
            myShp1Hit = false;
            myShp2Hit = false;
            myShp3Hit = false;
            cptShp1Hit = false;
            cptShp2Hit = false;
            cptShp3Hit = false;
            win = null;            
            for (int idx = 0; idx<myBox.Length; idx++)
            {
                myBox[idx].Image = null;
                lotRec[idx] = -1;
            }
            for (int idx = 0; idx<cptBox.Length; idx++)
            {
                cptBox[idx].Image = null;
                clkRec[idx] = -1;
            }
            MyShip();
            CShip();
            FirstFire();            
        }
        private void FirstFire()
        {            
            int who = rdm.Next(100);
            if (who % 2 ==0)
            {
                
                CptAttack();
            }           
            else
            {
                MessageBox.Show("player fire first");
            }
        }
        private void MyBoxForm()
        {
            int ex=0;
            int ey = 0;
            for ( int idx = 0; idx<myBox.Length; idx++) 
            {                
                myBox[idx] = new PictureBox();
                myBox[idx].Size = new Size(30, 30);
                myBox[idx].Image = null;
                myBox[idx].BackColor= Color.Gray;                              
                int px = 35+ ex * 31;
                int py = 35+ey*31;
                myBox[idx].Location = new Point(px,py);               
                this.Controls.Add(myBox[idx]);                
                ex+=1;
                if (ex == 10)
                {
                    ex=0;
                    ey+=1;
                }               
            }            
        }
        private void MyShip()
        {
            repeat:
            MS[0] = rdm.Next(10, 100);
            MS[1] = rdm.Next(0, 100);
            MS[2] = rdm.Next(0, 100);
            while(MS[0]==MS[1] | MS[0]==MS[1]+1 | MS[0]==MS[2] | MS[0]==MS[2]+1 | MS[0]-10==MS[1] | MS[0]-10==MS[1]+1 | MS[0]-10==MS[2] | MS[0]-10==MS[2]+1 | MS[1]==MS[2] | MS[1]==MS[2]+1 | MS[1]+1==MS[2] | MS[1]+1==MS[2]+1)
            {
                MS[1] = rdm.Next(0, 100);
                MS[2] = rdm.Next(0, 100);
            }
            for (int i = 1; i < MS.Length; i++)
            {
                if ((MS[i] + 1) % 10 == 0)
                {
                    goto repeat;
                }
            }
            myBox[MS[0]].Image = img[2];
            myBox[MS[0]-10].Image = img[3];
            myBox[MS[1]].Image = img[0];
            myBox[MS[1]+1].Image = img[1];
            myBox[MS[2]].Image = img[0];
            myBox[MS[2]+1].Image = img[1];
        }
        private void CShip()
        {
            repeat:
            CS[0] = rdm.Next(10, 100);
            CS[1] = rdm.Next(0, 100);
            CS[2] = rdm.Next(0, 100);
            while (CS[0]==CS[1] | CS[0]==CS[1]+1 | CS[0]==CS[2] | CS[0]==CS[2]+1 | CS[0]-10==CS[1] | CS[0]-10==CS[1]+1 | CS[0]-10==CS[2] | CS[0]-10==CS[2]+1 | CS[1]==CS[2] | CS[1]==CS[2]+1 | CS[1]+1==CS[2] | CS[1]+1==CS[2]+1)
            {
                CS[1] = rdm.Next(0, 100);
                CS[2] = rdm.Next(0, 100);
            }
            for (int i = 1; i < CS.Length; i++)
            {
                if ((CS[i] + 1) % 10 == 0)
                {
                    goto repeat;
                }
            }
            cptBox[CS[0]].Image = img[2];
            cptBox[CS[0]-10].Image = img[3];
            cptBox[CS[1]].Image = img[0];
            cptBox[CS[1]+1].Image = img[1];
            cptBox[CS[2]].Image = img[0];
            cptBox[CS[2]+1].Image = img[1];
        }
        private void CptBoxForm()
        {
            int ex = 0;
            int ey = 0;
            for (int idx = 0; idx<cptBox.Length; idx++)
            {
                cptBox[idx] = new PictureBox();
                cptBox[idx].Size = new Size(30, 30);
                cptBox[idx].Image = null;
                cptBox[idx].BackColor= Color.Gray;
                int px = 400+ ex * 31;
                int py = 35+ey*31;
                cptBox[idx].Location = new Point(px, py);
                cptBox[idx].Click += new EventHandler(cptBox_Click);
                this.Controls.Add(cptBox[idx]);
                ex+=1;
                if (ex == 10)
                {
                    ex=0;
                    ey+=1;
                }
            }           
        }
        private async void cptBox_Click(object sender, EventArgs e)
        {
            
            int clk = Array.IndexOf(cptBox,sender);
            
            clkRec[clkCnt] = clk;
            for (int i = 0; i < clkCnt; i++)
            {
                if (clk == clkRec[i])
                {
                    MessageBox.Show("you have hit the spot, hit other spot");
                    return;
                }
            }
            if (clk == CS[0] )
            {
                cptBox[clk].Image = img[6];
                cptShp1Hit = true;
                MessageBox.Show("enemy ship1 hit");

            }
            else if ( clk == CS[0] - 10)
            {
                cptBox[clk].Image = img[7];
                cptShp1Hit = true;
                MessageBox.Show("enemy ship1 hit");

            }
            else if (clk == CS[1] )
            {
                cptBox[CS[1]].Image = img[4];
                cptShp2Hit = true;
                MessageBox.Show("enemy ship2 hit");
            }
            else if (clk == CS[1]+1 )
            {
                cptBox[clk].Image = img[5];
                cptShp2Hit = true;
                MessageBox.Show("enemy ship2 hit");
            }
            else if (clk == CS[2])
            {
                cptBox[clk].Image = img[4];
                MessageBox.Show("enemy ship3 hit");
                cptShp3Hit = true;
            }
            else if (clk == CS[2] + 1)
            {
                cptBox[clk].Image = img[5];
                MessageBox.Show("enemy ship3 hit");
                cptShp3Hit = true;
            }
            else
            {
                cptBox[clk].Image = img[8];
            }
            WinCheck();            
            if (win == "me")
            {
                Replay();
                return;
            }
            else if (win == "gameover")
            {
                this.Close();
            }
            else if (win == null)
            {
                clkCnt++;
                await Task.Delay(500);
                CptAttack();               
            }
            else
            {
                MessageBox.Show("something wrong");
            }
            
        }
    }
}
