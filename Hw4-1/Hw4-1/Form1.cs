using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hw4_1
{

    public partial class Form1 : Form
    {
        // 宣告全域變數
        int [] ans = new int[4];

        int[] currentGuess = { 0, 0, 0, 0 };
        ImageList imageList;
        int currentImageIndex = 0;
        private System.Windows.Forms.Button[] buttons = new System.Windows.Forms.Button[4];
        private int[] buttonImageIndices = new int[4]; // 保存每個按鈕的圖片索引
        public Form1()
        {
            InitializeComponent();
        }
        private void InitializeImageList()
        {
            imageList = new ImageList();
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\F94096071_practice_4_1\\Hw4-2\\img4_1\\0.jpeg"));
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\F94096071_practice_4_1\\Hw4-2\\img4_1\\1.jpeg"));
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\F94096071_practice_4_1\\Hw4-2\\img4_1\\2.jpeg"));
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\F94096071_practice_4_1\\Hw4-2\\img4_1\\3.jpeg"));
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\F94096071_practice_4_1\\Hw4-2\\img4_1\\4.jpeg"));
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\F94096071_practice_4_1\\Hw4-2\\img4_1\\5.jpeg"));
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\F94096071_practice_4_1\\Hw4-2\\img4_1\\6.jpeg"));
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\F94096071_practice_4_1\\Hw4-2\\img4_1\\7.jpeg"));
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\F94096071_practice_4_1\\Hw4-2\\img4_1\\8.jpeg"));
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\F94096071_practice_4_1\\Hw4-2\\img4_1\\9.jpeg"));
            
            imageList.ImageSize = new System.Drawing.Size(80, 80); // 設定圖片大小
        }
        private void InitializeButtons()
        {
            for (int i = 0; i < 4; i++)
            {
                buttons[i] = new System.Windows.Forms.Button();
                buttons[i].Size = new System.Drawing.Size(100, 200);
                buttons[i].Image = imageList.Images[0]; // 初始圖片索引為0
                buttons[i].Click += Button_Click;
                buttons[i].Name = "button" + i; // 設定按鈕的名稱
                buttonImageIndices[i] = 0; // 初始化按鈕的圖片索引
                Controls.Add(buttons[i]);
            }

            buttons[0].Location = new System.Drawing.Point(90, 80);
            buttons[1].Location = new System.Drawing.Point(190, 80);
            buttons[2].Location = new System.Drawing.Point(290, 80);
            buttons[3].Location = new System.Drawing.Point(390, 80);

            System.Windows.Forms.Button unlockButton = new System.Windows.Forms.Button();
            unlockButton.Text = "解鎖";
            unlockButton.Click += UnlockButton_Click;
            unlockButton.Location = new System.Drawing.Point(255, 310);
            Controls.Add(unlockButton);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random random = new Random();
            InitializeComponent();
            InitializeImageList(); // 初始化圖像列表
            InitializeButtons();   // 初始化按鈕
            for (int i = 0; i < ans.Length; i++)
            {
                ans[i] = random.Next(10); // 生成0到9的隨機整數
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button clickedButton = (System.Windows.Forms.Button)sender;
            int buttonIndex = Array.IndexOf(buttons, clickedButton); 
            buttonImageIndices[buttonIndex] = (buttonImageIndices[buttonIndex] + 1) % imageList.Images.Count;
            clickedButton.Image = imageList.Images[buttonImageIndices[buttonIndex]];
            currentGuess[buttonIndex] = (currentGuess[buttonIndex] + 1) % 10; 
        }
        private void UnlockButton_Click(object sender, EventArgs e)
{
    // 比較 currentGuess 和 ans 陣列，計算正確的位置數
    int correctPositions = 0;
    for (int i = 0; i < 4; i++)
    {
        if (currentGuess[i] == ans[i])
        {
            correctPositions++;
        }
    }

    if (correctPositions == 4)
    {
                // 如果所有位置都正確，顯示「解鎖成功」
        DialogResult result = MessageBox.Show("解鎖成功", "成功", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk);
                this.Close();
            }
    else
    {
        DialogResult result = MessageBox.Show("猜對" + correctPositions + "個位置", "失敗", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

        if (result == DialogResult.Retry)
        {
        }
        else if (result == DialogResult.Cancel)
        {
            MessageBox.Show("答案是 " + string.Join("", ans), "答案");

            this.Close();
        }
    }

}
    }
}
