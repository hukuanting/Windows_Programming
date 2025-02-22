using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hw3_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(800, 500);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "資料輸入";
            label1.Font = new Font(label1.Font.FontFamily, 20.25F);
            label1.Location = new Point(340, 10);

            label2.Location = new Point(120, 80);
            label3.Location = new Point(120, 140);
            label4.Location = new Point(120, 200);
            label5.Location = new Point(120, 260);
            label6.Location = new Point(120, 320);

            textBox1.Location = new Point(250, 80);
            textBox1.Size = new Size(300, 30);

            textBox2.Location = new Point(250, 140);
            textBox2.Size = new Size(300, 30);

            textBox3.Location = new Point(250, 200);
            textBox3.Size = new Size(300, 30);

            textBox4.Location = new Point(250, 260);
            textBox4.Size = new Size(300, 30);

            textBox5.Location = new Point(250, 320);
            textBox5.Size = new Size(300, 30);

            button1.Location = new Point(250, 420);
            button1.Size = new Size(300, 30);

            label7.Text = "";
            label7.Location = new Point(580, 80);
            label8.Text = "";
            label8.Location = new Point(580, 140);
            label9.Text = "";
            label9.Location = new Point(580, 200);
            label10.Text = "";
            label10.Location = new Point(580, 260);
            label11.Text = "";
            label11.Location = new Point(580, 320);

            label12.Visible = false;
            label13.Visible = false;

            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            button1.Visible = true;
            button2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string gender = textBox2.Text;
            string birth = textBox3.Text;
            string today = textBox4.Text;
            string pet = textBox5.Text;

            if (string.IsNullOrEmpty(name))
            {
                label7.Text = "此欄尚未填寫";
            }
            else
            {
                label7.Text = "";
            }
        ////////////////////////////////////////////////
            if (string.IsNullOrEmpty(gender))
            {
                label8.Text = "此欄尚未填寫";
            }
            else if (gender == "男" || gender == "女") 
            {
                label8.Text = "";
            }
            else
            {
                label8.Text = "輸入應為男or女";
            }
            ////////////////////////////////////////////////

            string[] parts = birth.Split('/');

            if (string.IsNullOrEmpty(birth))
            {
                label9.Text = "此欄尚未填寫";
            }

            else if (parts.Length == 3)
            {
                if (!int.TryParse(parts[0], out int birthyear))
                {
                    label9.Text = "年份寫錯了";
                }

                else if (!int.TryParse(parts[1], out int birthmonth))
                {
                    label9.Text = "月份寫錯了";
                }

                else if (birthmonth > 12 || birthmonth < 1)
                {
                    label9.Text = "月份寫錯了";
                }

                else if (!int.TryParse(parts[2], out int birthday))
                {
                    label9.Text = "日期寫錯了";
                }

                else if (birthmonth==1 || birthmonth == 3 || birthmonth == 5 || birthmonth == 7 || birthmonth == 8 || birthmonth == 10 || birthmonth == 12)
                {
                    if(birthday<1 || birthday > 31)
                    {
                        label9.Text = "日期寫錯了";
                    }
                    else
                    {
                        label9.Text = "";
                    }
                }

                else if (birthmonth == 4 || birthmonth == 6 || birthmonth == 9 || birthmonth == 11 )
                {
                    if (birthday < 1 || birthday > 30)
                    {
                        label9.Text = "日期寫錯了";
                    }
                    else
                    {
                        label9.Text = "";
                    }
                }

                else if (birthmonth == 2)
                {
                    if(birthyear % 4 == 0 && (birthyear % 100 != 0 || birthyear % 400 == 0))
                    {
                        if (birthday < 1 || birthday > 29)
                        {
                            label9.Text = "日期寫錯了";
                        }
                        else
                        {
                            label9.Text = "";
                        }
                    }
                    else
                    {
                        if (birthday < 1)
                        {
                            label9.Text = "日期寫錯了";
                        }
                        else if(birthday > 28)
                        {
                            label9.Text = "日期寫錯了\n (今年為閏年))";
                        }
                        else
                        {
                            label9.Text = "";
                        }
                    }
                }
                else
                {
                    label9.Text = "";
                }
            }
            else
            {
                label9.Text = "輸入格式錯誤\n (YYYY/MM/DD)";
            }

            ////////////////////////////////////////////////

            parts = today.Split('/');

            if (string.IsNullOrEmpty(today))
            {
                label10.Text = "此欄尚未填寫";
            }

            else if (parts.Length == 3)
            {
                if (!int.TryParse(parts[0], out int birthyear))
                {
                    label10.Text = "年份寫錯了";
                }

                else if (!int.TryParse(parts[1], out int birthmonth))
                {
                    label10.Text = "月份寫錯了";
                }

                else if (birthmonth > 12 || birthmonth < 1)
                {
                    label10.Text = "月份寫錯了";
                }

                else if (!int.TryParse(parts[2], out int birthday))
                {
                    label10.Text = "日期寫錯了";
                }

                else if (birthmonth == 1 || birthmonth == 3 || birthmonth == 5 || birthmonth == 7 || birthmonth == 8 || birthmonth == 10 || birthmonth == 12)
                {
                    if (birthday < 1 || birthday > 31)
                    {
                        label10.Text = "日期寫錯了";
                    }
                    else
                    {
                        label10.Text = "";
                    }
                }

                else if (birthmonth == 4 || birthmonth == 6 || birthmonth == 9 || birthmonth == 11)
                {
                    if (birthday < 1 || birthday > 30)
                    {
                        label10.Text = "日期寫錯了";
                    }
                    else
                    {
                        label10.Text = "";
                    }
                }

                else if (birthmonth == 2)
                {
                    if (birthyear % 4 == 0 && (birthyear % 100 != 0 || birthyear % 400 == 0))
                    {
                        if (birthday < 1 || birthday > 29)
                        {
                            label10.Text = "日期寫錯了";
                        }
                        else
                        {
                            label10.Text = "";
                        }
                    }
                    else
                    {
                        if (birthday < 1)
                        {
                            label10.Text = "日期寫錯了";
                        }
                        else if (birthday > 28)
                        {
                            label10.Text = "日期寫錯了\n (今年為閏年))";
                        }
                        else
                        {
                            label10.Text = "";
                        }
                    }
                }
                else
                {
                    label10.Text = "";
                }
            }
            else
            {
                label10.Text = "輸入格式錯誤\n (YYYY/MM/DD)";
            }

            ////////////////////////////////////////////////

            if (string.IsNullOrEmpty(pet))
            {
                label11.Text = "此欄尚未填寫";
            }
            else if (pet == "貓" || pet == "狗")
            {
                label11.Text = "";
            }
            else
            {
                label11.Text = "輸入應為貓or狗";
            }
        ////////////////////////////////////////////////
        
            if (label7.Text == "" && label8.Text == "" && label9.Text == "" && label10.Text == "" && label11.Text == "")
            {
                label1.Text = "神諭時刻";
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;

                label7.Text = name;
                label8.Text = gender;
                label9.Text = birth;
                label10.Text = today;
                label11.Text = pet;

                label2.Location = new Point(220, 60);
                label7.Location = new Point(220, 85);

                label3.Location = new Point(510, 55);
                label8.Location = new Point(510, 80);

                label4.Location = new Point(70, 120);
                label9.Location = new Point(70, 145);

                label5.Location = new Point(370, 120);
                label10.Location = new Point(370, 145);

                label6.Location = new Point(640, 120);
                label11.Location = new Point(640, 145);

                string ana; string sug;

                String[] analysis = { "桃花運大漲", "家庭遭逢變故", "事業平步青雲，有升官可能", "事業起伏大", "親人病情好轉", "被財神眷顧", "近期一帆風順" };

                String[] suggest = { "少做壞事，夜路走多了總匯三明治",
                     "保持謙虛，一山還有一山高，雞蛋還有雞蛋糕",
                     "善待他人，不要任意嘲笑別人，除非你忍不住",
                     "早點睡覺，不要仗著自己長得醜，就任意熬夜",
                     "小心小人，可謂浮雲能蔽日，輕舟已過萬重山",
                     "不要偷懶，你在睡覺的時候，美國人還在上班阿",
                     "健康第一，定期身體檢查並謹記醫生怎麼說，doctor!",
                     "穩定情緒，今天不開心沒關係，反正明天也不會開心"};

                Random random = new Random();
                int randomNumber1 = random.Next(0, 7);
                int randomNumber2 = random.Next(0, 7);
           
                ana = analysis[randomNumber1];
                sug = suggest[randomNumber2];

                label12.Location = new Point(100, 240);
                label12.Text = "運勢： " + ana;
                label12.Visible = true;

                label13.Location = new Point(100, 280);
                label13.Text = "建議： " + sug;
                label13.Visible = true;

                button1.Visible = false;
                button2.Size = new Size(300, 30);
                button2.Text = "來，下面一位";
                button2.Location = new Point(250, 420);
                button2.Visible = true;

            }
        }
    }
}
