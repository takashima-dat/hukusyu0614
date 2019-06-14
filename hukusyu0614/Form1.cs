using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hukusyu0614
{
    public partial class Form1 : Form
    {
        int[] vx = new int[3];
        int[] vy = new int[3];
        
        const int su = 30;
        const int suu = 20;
        int[] vxs = new int[su];
        int[] vys = new int[su];
        Label[] labels = new Label[su];
        Label[] enemy = new Label[suu];

        int rvx = -10;
        int rvy = 11;

        int scp = 100;
        const int hpm = 10;
        int sc = 0;
        int hp = suu * hpm;

        private static Random ra = new Random();

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < su; i++)
            {
                labels[i] = new Label();
                labels[i].AutoSize = true;
                labels[i].Text = "★";
                //labels[i].Text = label1.Text;
                Controls.Add(labels[i]);
                labels[i].Font = label1.Font;
                labels[i].ForeColor = Color.Orange;
                labels[i].Left = ra.Next(ClientSize.Width - labels[i].Width);
                labels[i].Top = ra.Next(ClientSize.Height - labels[i].Height);
                vxs[i] = ra.Next(rvx, rvy);
                vys[i] = ra.Next(rvx, rvy);
            }

            for (int i = 0; i < suu; i++)
            {
                enemy[i] = new Label();
                enemy[i].AutoSize = true;
                enemy[i].Text = "★";
                //enemy[i].Text = label1.Text;
                Controls.Add(enemy[i]);
                enemy[i].Font = label1.Font;
                enemy[i].ForeColor = Color.Black;
                enemy[i].Left = ra.Next(ClientSize.Width - enemy[i].Width);
                enemy[i].Top = ra.Next(ClientSize.Height - enemy[i].Height);
                vxs[i] = ra.Next(rvx, rvy);
                vys[i] = ra.Next(rvx, rvy);

            }

            //label1.Text = "ヌベヂョンヌゾジョンベルミッティスモゲロンボョｗｗｗｗｗｗイヒーｗｗイヒヒｗ";
            label1.Left = ra.Next(ClientSize.Width - label1.Width);
            label1.Top = ra.Next(ClientSize.Height - label1.Height);

            label2.Left = 0;
            label2.Top = 0;

            label3.Left = 0;
            label3.Top = label2.Height;

            vx[0] = ra.Next(rvx, rvy);
            vy[0] = ra.Next(rvx, rvy);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Left += vx[0];
            label1.Top += vy[0];


            label2.Text = "SCORE:" + sc;
            label3.Text = "HP:" + hp;

            for (int i = 0; i < su; i++)
            {
                labels[i].Left += vxs[i];
                labels[i].Top += vys[i];

                if (labels[i].Left <= 0)
                {
                    vxs[i] = Math.Abs(vxs[i]);
                }
                if (labels[i].Top <= 0)
                {
                    vys[i] = Math.Abs(vys[i]);
                }
                if (labels[i].Right >= ClientSize.Width)
                {
                    vxs[i] = -Math.Abs(vxs[i]);
                }
                if (labels[i].Bottom >= ClientSize.Height)
                {
                    vys[i] = -Math.Abs(vys[i]);
                }
            }

            for (int i = 0; i < suu; i++)
            {
                enemy[i].Left += vxs[i];
                enemy[i].Top += vys[i];

                if (enemy[i].Left <= 0)
                {
                    vxs[i] = Math.Abs(vxs[i]);
                }
                if (enemy[i].Top <= 0)
                {
                    vys[i] = Math.Abs(vys[i]);
                }
                if (enemy[i].Right >= ClientSize.Width)
                {
                    vxs[i] = -Math.Abs(vxs[i]);
                }
                if (enemy[i].Bottom >= ClientSize.Height)
                {
                    vys[i] = -Math.Abs(vys[i]);
                }
            }

            if (label1.Left <= 0)
            {
                vx[0] = Math.Abs(vx[0]);
            }
            if (label1.Top <= 0)
            {
                vy[0] = Math.Abs(vy[0]);
            }
            if (label1.Right >= ClientSize.Width)
            {
                vx[0] = -Math.Abs(vx[0]);
            }
            if (label1.Bottom >= ClientSize.Height)
            {
                vy[0] = -Math.Abs(vy[0]);
            }

            /*
            Point p = MousePosition;
            if (label1.Left<=p.X&&label1.Right>=p.X&&label1.Top<=p.Y&&label1.Bottom>=p.Y)
            {
                timer1.Enabled = false;
            }
            */
            //自力で何も見ないでここまで

            Point p = PointToClient(MousePosition);

            /*
            if (label1.Left <= p.X &&
                    label1.Right >= p.X &&
                    label1.Top <= p.Y &&
                    label1.Bottom >= p.Y)
            {
                timer1.Enabled = false;
            }*/

            for (int i = 0; i < su; i++)
            {
                if (labels[i].Left <= p.X &&
                    labels[i].Right >= p.X &&
                    labels[i].Top <= p.Y &&
                    labels[i].Bottom >= p.Y)
                {
                    labels[i].Visible = false;
                    sc = sc + scp;
                }
            }

            for (int i = 0; i < suu; i++)
            {
                if (enemy[i].Left <= p.X &&
                    enemy[i].Right >= p.X &&
                    enemy[i].Top <= p.Y &&
                    enemy[i].Bottom >= p.Y)
                {
                    enemy[i].Visible = false;
                    hp = hp - hpm;
                }
            }

            
            //if (/*黄色い星を全部取ったら*/)
            /*
            {
                int SC=sc*hp;
                MessageBox.Show("GAME CLEAR  SCORE:" + SC);
            }*/
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
