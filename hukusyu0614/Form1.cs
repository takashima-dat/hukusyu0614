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
        //label1用
        int[] vx = new int[3];
        int[] vy = new int[3];
        
        //labelsとenemyの数
        const int su = 10;
        const int suu = 5;
        int[] vxl = new int[su];
        int[] vyl = new int[su];
        int[] vxe = new int[suu];
        int[] vye = new int[suu];
        Label[] labels = new Label[su];
        Label[] enemy = new Label[suu];

        //labelsとenemyの動く速さ
        int rvx = -10;
        int rvy = 11;

        //スコアとHPの計算
        int scp = 100;//1labelsのスコア加算量
        const int hpm = 10;//1enemyあたりのHP減算量
        int sc = 0;
        int hp = suu * hpm;//初期HP = enemyの数 * HP減算量
        int co = 0;

        int hosi;

        int getCount;

        private static Random ra = new Random();

        enum SCENE
        {
            TITLE,
            GAME,
            GAMEOVER,
            CLEAR,
            NONE
        }
        /// <summary>
        /// 現在のシーン
        /// </summary>
        SCENE nowScene;

        /// <summary>
        /// 切り替えたいシーン
        /// </summary>
        SCENE nextScene;

        public Form1()
        {
            InitializeComponent();
            nextScene=SCENE.TITLE;
            nowScene = SCENE.NONE;

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
                vxl[i] = ra.Next(rvx, rvy);
                vyl[i] = ra.Next(rvx, rvy);
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
                vxe[i] = ra.Next(rvx, rvy);
                vye[i] = ra.Next(rvx, rvy);

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

        void initProc()
        {
            // nextSceneがNONEだったら、何もしない
            if (nextScene == SCENE.NONE) return;

            nowScene = nextScene;
            nextScene = SCENE.NONE;

            switch(nowScene)
            {
                case SCENE.TITLE:
                    label4.Visible = true;
                    button1.Visible = true;
                    for (int i = 0; i < su; i++)
                    {
                        labels[i].Visible = false;
                    }
                    for (int i = 0; i < suu; i++)
                    {
                        enemy[i].Visible = false;
                    }
                    label2.Visible = false;
                    label3.Visible = false;
                    break;
                case SCENE.GAME:
                    label4.Visible = false;
                    button1.Visible = false;
                    for (int i = 0; i < su; i++)
                    {
                        labels[i].Visible = true;
                    }
                    for (int i = 0; i < suu; i++)
                    {
                        enemy[i].Visible = true;
                    }
                    label2.Visible = true;
                    label3.Visible = true;
                    getCount = co;
                    break;
                case SCENE.CLEAR:
                    //ゲームクリアで最終スコア表示
                        int SC = sc * hp;
                        MessageBox.Show("GAME CLEAR  SCORE:" + SC);
                    break;
            }
        }

        void updateProc()
        {
            if (nowScene==SCENE.GAME)
            {
                updateGame();
            }
        }

        void updateGame()
        {
            //labelsの跳ね返り
            for (int i = 0; i < su; i++)
            {
                labels[i].Left += vxl[i];
                labels[i].Top += vyl[i];

                if (labels[i].Left <= 0)
                {
                    vxl[i] = Math.Abs(vxl[i]);
                }
                if (labels[i].Top <= 0)
                {
                    vyl[i] = Math.Abs(vyl[i]);
                }
                if (labels[i].Right >= ClientSize.Width)
                {
                    vxl[i] = -Math.Abs(vxl[i]);
                }
                if (labels[i].Bottom >= ClientSize.Height)
                {
                    vyl[i] = -Math.Abs(vyl[i]);
                }
            }

            //enemyの跳ね返り
            for (int i = 0; i < suu; i++)
            {
                enemy[i].Left += vxe[i];
                enemy[i].Top += vye[i];

                if (enemy[i].Left <= 0)
                {
                    vxe[i] = Math.Abs(vxe[i]);
                }
                if (enemy[i].Top <= 0)
                {
                    vye[i] = Math.Abs(vye[i]);
                }
                if (enemy[i].Right >= ClientSize.Width)
                {
                    vxe[i] = -Math.Abs(vxe[i]);
                }
                if (enemy[i].Bottom >= ClientSize.Height)
                {
                    vye[i] = -Math.Abs(vye[i]);
                }
            }

            //マウスのポイントによる動き
            Point p = PointToClient(MousePosition);

            //labelsをとる、消える・スコア加算
            for (int i = 0; i < su; i++)
            {
                if (labels[i].Left <= p.X &&
                    labels[i].Right >= p.X &&
                    labels[i].Top <= p.Y &&
                    labels[i].Bottom >= p.Y &&
                    labels[i].Visible==true)
                {
                    labels[i].Visible = false;
                    sc = sc + scp;
                    hosi++;
                }
            }

            //enemyをとる、消える・HP減算
            for (int i = 0; i < suu; i++)
            {
                if (enemy[i].Left <= p.X &&
                    enemy[i].Right >= p.X &&
                    enemy[i].Top <= p.Y &&
                    enemy[i].Bottom >= p.Y&&
                    enemy[i].Visible == true)
                {
                    enemy[i].Visible = false;
                    hp = hp - hpm;
                }
            }

            if (hosi == su)
                nextScene = SCENE.CLEAR;

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            initProc();
            updateProc();

            label1.Left += vx[0];
            label1.Top += vy[0];

            label1.Text = "" + hosi;
            label2.Text = "SCORE:" + sc;
            label3.Text = "HP:" + hp;

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

            /*
            //timerが止まる
            if (label1.Left <= p.X &&
                    label1.Right >= p.X &&
                    label1.Top <= p.Y &&
                    label1.Bottom >= p.Y)
            {
                timer1.Enabled = false;
            }*/
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            nextScene = SCENE.GAME;
        }
    }
}
