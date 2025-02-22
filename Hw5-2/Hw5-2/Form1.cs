using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hw5_2;

namespace Hw5_2
{
    class Servant
    {
        public string character;
        public int hp;
        public int charge;
        public int attack;
        public int speed;
        public virtual void Skill()
        {
        }
        public override string ToString()
        {
            return character;
        }
    }
    class Berserker : Servant
    {
        public Berserker()
        {
            character = "Berserker";
            hp = 100;
            charge = 0;
            attack = 4;
            speed = 4;
        }
        public override void Skill()
        {
            attack *= 2;
            hp /= 2;
        }
    }
    class Saber : Servant
    {
        public Saber()
        {
            character = "Saber";
            hp = 100;
            charge = 0;
            attack = 3;
            speed = 1;
        }
        public override void Skill()
        {
            attack *= 2;
        }
    }
    class Caster : Servant
    {
        public Caster()
        {
            character = "Caster";
            hp = 100;
            charge = 0;
            attack = 2;
            speed = 2;
        }

        public override void Skill()
        {
            hp *= 2;
        }
    }
    class Beast : Servant
    {
        public Beast()
        {
            character = "Beast";
            hp = 500;
            charge = 0;
            attack = 2;
            speed = 3;
        }
        public override void Skill()
        {
            attack += 2;
        }
    }
    public partial class Form1 : Form
    {
        private Button startbutton;
        private Button[] chr_buttons;
        private Button[] skill_buttons;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label TimeLabel;
        private Timer timer;
        private int preparetime = 10;
        private int playtime = 0;
        private bool[] chr = { false, false, false };
        private Servant select1, select2, select3, player;

        private enum GameState { Preparation, PlayerTurn }
        private GameState gameState;

        private enum Turn { one, two, three }
        private Turn turn;

        private System.Windows.Forms.Label character;
        private System.Windows.Forms.Label Beast;
        private System.Windows.Forms.Label hp1;
        private System.Windows.Forms.Label hp2;
        private System.Windows.Forms.Label charge1;
        private System.Windows.Forms.Label charge2;
        private System.Windows.Forms.Label attack1;
        private System.Windows.Forms.Label attack2;
        private System.Windows.Forms.Label whosturn;

        private bool endgame = false;
        private int CD1=3, CD2=3, round=0;

        Berserker berserker = new Berserker();
        Saber saber = new Saber();
        Caster caster = new Caster();
        Beast beast = new Beast();

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }
        private void InitializeGame()
        {
            Label1 = new System.Windows.Forms.Label
            {
                Text = "準備階段",
                Font = new Font("Arial", 18),
                Location = new Point(320, 30),
                AutoSize = true,
                Visible = false
            };
            this.Controls.Add(Label1);

            TimeLabel = new System.Windows.Forms.Label
            {
                Text = preparetime.ToString(),
                Font = new Font("Arial", 18),
                Location = new Point(355, 80),
                AutoSize = true,
                Visible = false
            };
            this.Controls.Add(TimeLabel);

            timer = new Timer();
            timer.Interval = 1000;

            chr_buttons = new Button[3];
            for (int i = 0; i < 3; i++)
            {
                chr_buttons[i] = new Button
                {
                    Width = 150,
                    Height = 60,
                    Location = new Point(100 + i * 200, 150),
                    Visible = false
                };
                this.Controls.Add(chr_buttons[i]);
            }

            skill_buttons = new Button[3];
            for (int i = 0; i < 3; i++)
            {
                skill_buttons[i] = new Button
                {
                    Width = 150,
                    Height = 60,
                    Location = new Point(100 + i * 200, 150),
                    Visible = false
                };
                this.Controls.Add(skill_buttons[i]);
                skill_buttons[i].Click += ButtonClick_2;
            }
            character = new System.Windows.Forms.Label
            {
                Text = "",
                Font = new Font("Arial", 20),
                Location = new Point(50, 130),
                AutoSize = true,
                Visible = false,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(character);
            Beast = new System.Windows.Forms.Label
            {
                Text = "",
                Font = new Font("Arial", 20),
                Location = new Point(575, 130),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            this.Controls.Add(Beast);
            hp1 = new System.Windows.Forms.Label
            {
                Text = "",
                Font = new Font("Arial", 16),
                Location = new Point(50, 190),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            this.Controls.Add(hp1);
            hp2 = new System.Windows.Forms.Label
            {
                Text = "",
                Font = new Font("Arial", 16),
                Location = new Point(575, 190),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            this.Controls.Add(hp2);
            charge1 = new System.Windows.Forms.Label
            {
                Text = "",
                Font = new Font("Arial", 16),
                Location = new Point(50, 230),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            this.Controls.Add(charge1);
            charge2 = new System.Windows.Forms.Label
            {
                Text = "",
                Font = new Font("Arial", 16),
                Location = new Point(575, 230),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            this.Controls.Add(charge2);
            attack1 = new System.Windows.Forms.Label
            {
                Text = "",
                Font = new Font("Arial", 16),
                Location = new Point(50, 270),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            this.Controls.Add(attack1);
            attack2 = new System.Windows.Forms.Label
            {
                Text = "",
                Font = new Font("Arial", 16),
                Location = new Point(575, 270),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            this.Controls.Add(attack2);
            whosturn = new System.Windows.Forms.Label
            {
                Text = "",
                Font = new Font("Arial", 18),
                Location = new Point(320, 500),
                AutoSize = true,
                Visible = false
            };
            this.Controls.Add(whosturn);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            gameState = GameState.Preparation;
            timer.Tick += TimerTick;
            timer.Start();
            button1.Visible = false;
            Label1.Visible = true;
            TimeLabel.Visible = true;
            TimeLabel.TextAlign = ContentAlignment.MiddleCenter;
            for (int i = 0; i < 3; i++)
            {
                chr_buttons[i].Visible = true;
                chr_buttons[i].Click += ButtonClick;
                chr_buttons[i].Font = new Font("Arial", 14);
            }
            chr_buttons[0].Text = "Berserker";
            chr_buttons[1].Text = "Saber";
            chr_buttons[2].Text = "Caster";
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int buttonIndex = Array.IndexOf(chr_buttons, button);
            int trueCount = chr.Count(b => b == true);

            if (trueCount >= 2)
            {
                if (button.BackColor == SystemColors.Control)
                {
                    MessageBox.Show("只能選則兩個servant");
                }
                else
                {
                    chr[buttonIndex] = !chr[buttonIndex];
                }
            }
            else
            {
                chr[buttonIndex] = !chr[buttonIndex];
            }

            for (int i = 0; i < 3; i++)
            {
                if (chr[i] == true)
                {
                    chr_buttons[i].BackColor = Color.LightGreen;
                }
                else
                {
                    chr_buttons[i].BackColor = SystemColors.Control;
                }
            }
        }
        private void TimerTick(object sender, EventArgs e)
        {
            int trueCount = chr.Count(b => b == true);

            if (gameState == GameState.Preparation)
            {
                preparetime--;
                if (preparetime <= -1)
                {
                    timer.Stop();
                    if (trueCount == 0)
                    {
                        chr[0] = true;
                        chr[1] = true;
                    }
                    if (trueCount == 1)
                    {
                        if (chr[0] == true) chr[1] = true;
                        else if (chr[0] == false) chr[0] = true;
                    }
                    Playgame();
                }
                else
                {
                    TimeLabel.Text = $"{preparetime}";
                }
            }
            else if (gameState == GameState.PlayerTurn)
            {
                playtime++;
                TimeLabel.Text = $"{playtime}";

                if (beast.hp == 0 || (saber.hp + caster.hp + berserker.hp) <= 100)
                {
                    endgame = true;
                }
                else endgame = false;
                if (endgame == true)
                {
                    timer.Stop();
                    Endgame();
                }

                Random random = new Random();
                int critical = random.Next(1, 3);
                int c = random.Next(20, 26);
                if (select2 == beast)                          // for beast
                {
                    if (turn == Turn.two)
                    {
                        if (round % 4 == 0)                    //施放技能
                        {
                            select2.attack += select2.attack * 2;
                            MessageBox.Show($"{select2} 使用了技能\n目前ATK：{select2.attack}");
                            if (beast.charge >= 100)           //beast大招
                            {
                                beast.charge -= 100;
                                select1.hp -= select2.attack * 2;
                                select3.hp -= select2.attack * 2;
                                MessageBox.Show($"{select2} 使用了寶具\n對全體隊友造成{select2.attack * 2}點傷害");
                            }
                            else                               //不施放大招
                            {
                                if (critical == 1)
                                {
                                    select1.hp -= select2.attack;   //beast普攻
                                    select3.hp -= select2.attack;
                                    select2.charge += c;
                                    MessageBox.Show($"{select2}對全體隊友造成{select2.attack}點傷害!");
                                }
                                else
                                {
                                    select1.hp -= select2.attack * 2;   //beast普攻
                                    select3.hp -= select2.attack * 2;
                                    select2.charge += 30;
                                    MessageBox.Show($"{select2}暴擊\n對全體造成{select2.attack * 2}傷害");
                                }
                            }
                        }
                        else
                        {
                            if (beast.charge >= 100)           //beast大招
                            {
                                beast.charge -= 100;
                                select1.hp -= select3.attack * 2;
                                select2.hp -= select3.attack * 2;
                                MessageBox.Show($"{select2} 使用了寶具\n對全體隊友造成{select2.attack * 2}點傷害");
                            }
                            else                               //不施放技能
                            {
                                if (critical == 1)
                                {
                                    select1.hp -= select3.attack;   //beast普攻
                                    select2.hp -= select3.attack;
                                    select3.charge += c;
                                    MessageBox.Show($"{select2}對全體隊友造成{select2.attack}點傷害!");
                                }
                                else
                                {
                                    select1.hp -= select3.attack * 2;   //beast普攻
                                    select2.hp -= select3.attack * 2;
                                    select3.charge += 30;
                                    MessageBox.Show($"{select2}暴擊\n對全體造成{select2.attack * 2}傷害");
                                }
                            }

                        }
                        turn = Turn.three;
                    }
                }
                else if (select3 == beast)              // for beast
                {
                    if (turn == Turn.three)
                    {
                        if (round % 4 == 0)                    //施放技能
                        {
                            select3.attack += select3.attack * 2;
                            MessageBox.Show($"{select3} 使用了技能\n目前ATK：{select3.attack}");

                            if (beast.charge >= 100)           //beast大招
                            {
                                beast.charge -= 100;
                                select1.hp -= select3.attack * 2;
                                select2.hp -= select3.attack * 2;
                                MessageBox.Show($"{select3} 使用了寶具\n對全體隊友造成{select3.attack * 2}點傷害");
                            }
                            else                               //不施放技能
                            {
                                if (critical == 1)
                                {
                                    select1.hp -= select3.attack;   //beast普攻
                                    select2.hp -= select3.attack;
                                    select3.charge += c;
                                    MessageBox.Show($"{select3}對全體隊友造成{select3.attack}點傷害!");
                                }
                                else
                                {
                                    select1.hp -= select3.attack * 2;   //beast普攻
                                    select2.hp -= select3.attack * 2;
                                    select3.charge += 30;
                                    MessageBox.Show($"{select3}暴擊\n對全體造成{select3.attack * 2}傷害");
                                }
                            }
                        }
                        else
                        {
                            if (beast.charge >= 100)           //beast大招
                            {
                                beast.charge -= 100;
                                select1.hp -= select3.attack * 2;
                                select2.hp -= select3.attack * 2;
                                MessageBox.Show($"{select3} 使用了寶具\n對全體隊友造成{select3.attack * 2}點傷害");
                            }
                            else                               //不施放技能
                            {
                                if (critical == 1)
                                {
                                    select1.hp -= select3.attack;   //beast普攻
                                    select2.hp -= select3.attack;
                                    select3.charge += c;
                                    MessageBox.Show($"{select3}對全體隊友造成{select3.attack}點傷害!");
                                }
                                else
                                {
                                    select1.hp -= select3.attack * 2;   //beast普攻
                                    select2.hp -= select3.attack * 2;
                                    select3.charge += 30;
                                    MessageBox.Show($"{select3}暴擊\n對全體造成{select3.attack * 2}傷害");
                                }
                            }
                        }
                        turn = Turn.three;
                        round++;
                    }
                }
                if (turn == Turn.one) player = select1;
                else if (turn == Turn.two) player = select2;
                else player = select3;

                character.Text = player.character;
                hp1.Text = $"Hp：{player.hp.ToString()}";
                charge1.Text = $"Charge：{player.charge.ToString()}%";
                attack1.Text = $"Attack：{player.attack.ToString()}";

                Beast.Text = beast.character;
                hp2.Text = $"Hp：{beast.hp.ToString()}";
                charge2.Text = $"Charge：{beast.charge.ToString()}%";
                attack2.Text = $"Attack：{beast.attack.ToString()}";

                whosturn.Text = $"{player.character} turn";
            }
        }
        private void Playgame()
        {
            gameState = GameState.PlayerTurn;
            timer.Start();
            timer.Tick -= TimerTick;
            timer.Tick += TimerTick;

            for (int i = 0; i < 3; i++) chr_buttons[i].Visible = false;

            Label1.Text = "時間";
            Label1.Location = new Point(343, 30);
            string[] word = { "普攻", "技能", "寶具" };
            whosturn.Visible = true;

            character.Visible = true;
            hp1.Visible = true;
            charge1.Visible = true;
            attack1.Visible = true;
            Beast.Visible = true;
            hp2.Visible = true;
            charge2.Visible = true;
            attack2.Visible = true;

            if (chr[0] == true)
            {
                if (chr[1] == true)
                {
                    select1 = saber;
                    select2 = beast;
                    select3 = berserker;
                }
                else
                {
                    select1 = caster;
                    select2 = beast;
                    select3 = berserker;
                }
            }
            else
            {
                select1 = saber;
                select2 = caster;
                select3 = beast;
            }

            turn = Turn.one;

            character.Text = select1.character;
            hp1.Text = $"Hp：{select1.hp.ToString()}";
            charge1.Text = $"Charge：{select1.charge.ToString()}%";
            attack1.Text = $"Attack：{select1.attack.ToString()}";

            Beast.Text = beast.character;
            hp2.Text = $"Hp：{beast.hp.ToString()}";
            charge2.Text = $"Charge：{beast.charge.ToString()}%";
            attack2.Text = $"Attack：{beast.attack.ToString()}";
            whosturn.Text = $"{select1.character} turn";

            for (int i = 0; i < 3; i++)
            {
                skill_buttons[i].Visible = true;
                skill_buttons[i].Width = 100;
                skill_buttons[i].Height = 50;
                skill_buttons[i].Location = new Point(215 + (i * 100), 150);
                skill_buttons[i].Font = new Font("Arial", 12);
                skill_buttons[i].Text = word[i];
                skill_buttons[i].BackColor = SystemColors.Control;
                skill_buttons[i].Click += ButtonClick_2;
            }
        }
        private void ButtonClick_2(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int buttonIndex = Array.IndexOf(chr_buttons, button);
            Random random = new Random();
            int critical = random.Next(1, 3);
            int c = random.Next(20, 26);

            if (turn == Turn.one)
            {
                if (CD1 > 0) CD1--;
                if (buttonIndex == 0) //普通攻擊
                {
                    if (critical == 1)
                    {
                        beast.hp -= select1.attack;
                        select1.charge += c;
                        MessageBox.Show($"{select1}對Beast造成{select1.attack}傷害");
                    }
                    else
                    {
                        beast.hp -= select1.attack * 2;
                        select1.charge += 30;
                        MessageBox.Show($"{select1}暴擊\n對Beast造成{select1.attack * 2}傷害");
                    }
                }
                else if (buttonIndex == 1)
                {
                    if (CD1 <= 0)
                    {
                        select1.Skill();
                        CD1 = 3;
                        MessageBox.Show($"{select1}使用了技能");
                    }
                    else
                    {
                        //跳出messagebox 提醒技能CD(round%3)
                        MessageBox.Show($"技能冷卻中 (剩餘回合：{CD1})");
                    }
                }
                else if (buttonIndex == 2)
                {
                    if (select1.charge >= 100)
                    {
                        select1.charge -= 100;
                        if (select1 == berserker) Berserker_Big();
                        else if (select1 == saber) Saber_Big();
                        else Caster_Big();
                    }
                    else
                    {
                        MessageBox.Show("能量不足!");
                    }
                }
                turn = Turn.two;
            }

            else if (turn == Turn.two)  // select2 回合
            {
                if (select2 != beast)
                {
                    if (CD2>0) CD2--;
                    if (buttonIndex == 0) //普通攻擊
                    {
                        if (critical == 1)
                        {
                            beast.hp -= select2.attack;
                            select2.charge += c;
                            MessageBox.Show($"{select2}對Beast造成{select2.attack}傷害");
                        }
                        else
                        {
                            beast.hp -= select2.attack * 2;
                            select2.charge += 30;
                            MessageBox.Show($"{select2}暴擊\n對Beast造成{select2.attack * 2}傷害");
                        }
                    }
                    else if (buttonIndex == 1) //技能
                    {
                        if (CD2 <= 0)
                        {
                            select2.Skill();
                            CD2 = 3;
                            MessageBox.Show($"{select2}使用了技能");
                        }
                        else
                        {
                            //跳出messagebox 提醒技能CD(round%3)
                            MessageBox.Show($"技能冷卻中 (剩餘回合：{CD2})");
                        }
                    }
                    else                         //大招
                    {
                        if (select2.charge >= 100)
                        {
                            select2.charge -= 100;
                            if (select2 == berserker) Berserker_Big();
                            else if (select2 == saber) Saber_Big();
                            else Caster_Big();
                        }
                        else
                        {
                            MessageBox.Show("能量不足!");
                        }
                    }
                    turn = Turn.three;
                }
            }
            else                  // select3 回合
            {
                if (select3 != beast)
                {
                    if (CD2 > 0) CD2--;
                    if (buttonIndex == 0) //普通攻擊
                    {
                        if (critical == 1)
                        {
                            beast.hp -= select3.attack;
                            select3.charge += c;
                            MessageBox.Show($"{select3}對Beast造成{select3.attack}傷害");
                        }
                        else
                        {
                            beast.hp -= select3.attack * 2;
                            select3.charge += 30;
                            MessageBox.Show($"{select3}暴擊\n對Beast造成{select3.attack * 2}傷害");
                        }
                    }
                    else if (buttonIndex == 1) //技能
                    {
                        if (CD2 <= 0)
                        {
                            select3.Skill();
                            CD2 = 3;
                            MessageBox.Show($"{select3}使用了技能");
                        }
                        else
                        {
                            MessageBox.Show($"技能冷卻中 (剩餘回合：{CD2})");
                        }
                    }
                    else                         //大招
                    {
                        if (select3.charge >= 100)
                        {
                            select3.charge -= 100;
                            if (select2 == berserker) Berserker_Big();
                            else if (select2 == saber) Saber_Big();
                            else Caster_Big();
                        }
                        else
                        {
                            MessageBox.Show("能量不足!");
                        }
                    }
                    turn = Turn.one;
                    round++;
                }
            }
        }
        private void Berserker_Big()
        {
            beast.hp -= berserker.attack+50;
            MessageBox.Show($"Berserker使用了寶具\n對Beast造成{berserker.attack + 50}點傷害");
        }
        private void Saber_Big()
        {
            beast.hp -= saber.attack + 25;
            saber.hp += 5;
            MessageBox.Show($"Saber使用了寶具\n對Beast造成{saber.attack + 25}點傷害並回復5點血量");
        }
        private void Caster_Big()
        {
            berserker.attack += 1;
            saber.attack += 1;
            caster.attack += 1;
            berserker.hp += 10;
            saber.hp += 10;
            caster.hp += 10;
            MessageBox.Show("Caster使用了寶具\n全體隊友增加1點攻擊、回復10點血量");
        }
        private void Endgame()
        {
            // 判斷是beast血量歸零(贏) 還是 Servant血量總和<=100 (輸)，並跳出messagebox顯示輸贏以及通關時間(playtime)
        }
    }
}
