using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Hw5_1
{
    public partial class Form1 : Form
    {
        private Button[] buttons;
        private int[] answerKeys;
        private int[] guessKeys= { -1,-1,-1};
        private Timer timer;
        private int remainingTime;
        private Label timerLabel; // 新增 timerLabel
        private Label Label1;
        int count=0;
        int playertime = 0;
        private enum GameState
        {
            Preparation,
            PlayerTurn
        }
        private GameState gameState;
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            this.Size = new Size(640, 400);
            buttons = new Button[36];
            remainingTime = 5;
            playertime = 5;
            button1.Text = "開始遊戲";
            guessKeys = null;

            // 創建36個按鍵，設定位置和事件處理函式
            for (int i = 0; i < 36; i++)
            {
                buttons[i] = new Button
                {
                    Text = (i < 10) ? i.ToString() : ((char)('A' + i - 10)).ToString(),
                    Width = 50,
                    Height = 50,
                    Location = new Point(10 + (i % 12) * 50, 150 + (i / 12) * 55),
                    BackColor = SystemColors.Control,
                    Visible = false,
                    Tag = i
                };
                buttons[i].Click += ButtonClick;
                this.Controls.Add(buttons[i]);
            }

            this.KeyPreview = true;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += TimerTick;

            timerLabel = new Label
            {
                Text = "",
                Font = new Font("Arial", 18),
                Location = new Point(300, 80),
                AutoSize = true
            };
            Label1 = new Label
            {
                Text = "",
                Font = new Font("Arial", 18),
                Location = new Point(300, 30),
                AutoSize = true,
                Visible = true
            };

            this.Controls.Add(timerLabel);
            answerKeys = new int[3]; 
            guessKeys = new int[3];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            StartGame();
            foreach (var button in buttons)
            {
                button.Visible = true;
            }
        }

        private void StartGame()
        {
            Label1.Text = "準備階段";
            Label1.Location = new Point(260, 40);
            timerLabel.Location = new Point(300, 80);
            this.Controls.Add(Label1);

            Random random = new Random();

            for (int i = 0; i < 3; i++)
            {
                int randomIndex = random.Next(0, 36);
                while(Array.IndexOf(answerKeys,randomIndex) != -1)
                {
                    randomIndex = random.Next(0, 36);
                }
                answerKeys[i] = randomIndex;
                buttons[randomIndex].BackColor = Color.LightGreen;
            }

            remainingTime = 6;
            playertime = 11;
            timer.Start();

        }
        private void ButtonClick(object sender, EventArgs e)
        {
            if (gameState == GameState.PlayerTurn)
            {
                Button button = (Button)sender;
                button.BackColor = Color.LightBlue;

                int clickedButtonIndex = (int)button.Tag;

                guessKeys[count] = clickedButtonIndex;
                count++;
                if (count >= 3)
                {
                    EndGame();
                }
            }
        }
        private void TimerTick(object sender, EventArgs e)
        {
            
            remainingTime--;
            playertime--;

            if (remainingTime <= 0)
            {
                timer.Stop();
                foreach (var button in buttons)
                {
                    button.BackColor = SystemColors.Control;
                }
                Label1.Text = "玩家階段";
                gameState = GameState.PlayerTurn;

                timer.Start();
                timerLabel.Text = $"{playertime}";
                if (playertime <= 0)
                {   
                    timer.Stop();
                    EndGame();
                }

            }
            else
            {
                timerLabel.Text = $"{remainingTime}";
            }
        }
        private void EndGame()
        {
            string result;
            int check = 0;

            for (int i = 0; i <= 2; i++)
            {
                buttons[answerKeys[i]].BackColor = Color.Red;
                if(guessKeys[i]!= -1)
                {
                    buttons[guessKeys[i]].BackColor = Color.Red;
                } 
            }

            Array.Sort(answerKeys);
            Array.Sort(guessKeys);

            //sort
            for (int i=0; i<=2; i++)
            {
                if (answerKeys[i] == guessKeys[i])
                {
                    buttons[answerKeys[i]].BackColor = Color.LightGreen;
                    check++;
                }
            }

            if (check == 3)
            {
                result = "You win!";
            }
            else
            {
                result = "You lose, try again!";
            }
            MessageBox.Show(result);

            ResetGame();
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            char keyPressed = char.ToUpper((char)e.KeyCode);

            int numericValue = -1;
            if (char.IsDigit(keyPressed))
            {
                numericValue = (int)(keyPressed - '0');
            }
            else if (char.IsLetter(keyPressed) && keyPressed >= 'A' && keyPressed <= 'Z')
            {
                numericValue = (int)(keyPressed - 'A' + 10);
            }

            if (numericValue >= 0 && gameState == GameState.PlayerTurn)
            {
                buttons[numericValue].BackColor = Color.LightBlue;
                RecordGuess(numericValue);
            }
        }

        private void RecordGuess(int guessValue)
        {
            guessKeys[count] = guessValue;
            count++;

            if (count >= 3)
            {
                EndGame();
            }
        }
        private void ResetGame()
        {
            this.Controls.Clear();
            gameState = GameState.Preparation;
            count = 0;
            InitializeComponent();
            InitializeGame();
            
        }
    }
}
