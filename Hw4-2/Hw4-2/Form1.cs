using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hw4_2
{
    public partial class Form1 : Form
    {
        private int money = 100;
        private int seeds = 5;
        private int fertilizer = 5;
        private int fruits = 0;

        private Label moneyLabel;
        private Label seedsLabel;
        private Label fertilizerLabel;
        private Label fruitsLabel;

        private Button[,] fieldButtons = new Button[4, 3];

        private RadioButton wateringCanRadioButton;
        private RadioButton seedsRadioButton;
        private RadioButton fertilizerRadioButton;
        private RadioButton sickleRadioButton;

        private CheckBox buySeedsCheckBox;
        private CheckBox buyFertilizerCheckBox;
        private CheckBox sellFruitsCheckBox;
        private Button shopButton;

        public Form1()
        {
            InitializeComponent();
            tabPage1.Text = "農場";
            tabPage2.Text = "商店";
            InitializeField();
            InitializeShop();
            InitializeImageList();
        }
        private void InitializeImageList()
        {
            ImageList imageList = new ImageList();

            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\視窗程式設計\\Hw4-2\\img4_2\\dirt.jpeg"));
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\視窗程式設計\\Hw4-2\\img4_2\\seed.jpg"));
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\視窗程式設計\\Hw4-2\\img4_2\\crop.jpg"));
            imageList.Images.Add(Image.FromFile("C:\\Users\\USER\\Desktop\\視窗程式設計\\Hw4-2\\img4_2\\watermelon.jpg"));

            imageList.ImageSize = new Size(90, 90);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    fieldButtons[i, j].ImageList = imageList;
                    fieldButtons[i, j].ImageIndex = 0;
                }
            }
        }
        private void FieldButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (wateringCanRadioButton.Checked)
            {
                if (clickedButton.ImageIndex == 1) // 種子狀態
                {
                    // 判斷是否需要澆水和施肥
                    if (clickedButton.Text == "肥")
                    {
                        clickedButton.ImageIndex = 2; // 作物狀態
                        clickedButton.Text = " ";
                        clickedButton.Font = new Font(clickedButton.Font.FontFamily, 1); // 設置字體大小
                        clickedButton.TextAlign = ContentAlignment.TopRight; // 文字位置
                    }
                    else
                    {
                        clickedButton.Text = "水";
                        clickedButton.Font = new Font(clickedButton.Font.FontFamily, 1); // 設置字體大小
                        clickedButton.TextAlign = ContentAlignment.TopRight; // 文字位置
                    }
                }
                else if (clickedButton.ImageIndex == 2) // 作物狀態
                {
                    // 判斷是否需要澆水和施肥
                    if (clickedButton.Text == "肥")
                    {
                        clickedButton.ImageIndex = 3; // 果實狀態
                        clickedButton.Text = " ";
                        clickedButton.Font = new Font(clickedButton.Font.FontFamily, 1); // 設置字體大小
                        clickedButton.TextAlign = ContentAlignment.TopRight; // 文字位置
                    }
                    else
                    {
                        clickedButton.Text = "水";
                        clickedButton.Font = new Font(clickedButton.Font.FontFamily, 1); // 設置字體大小
                        clickedButton.TextAlign = ContentAlignment.TopRight; // 文字位置
                    }
                }
            }
            else if (seedsRadioButton.Checked)
            {
                if (seeds > 0) 
                {
                    if (clickedButton.ImageIndex == 0) 
                    {
                        clickedButton.ImageIndex = 1; 
                        clickedButton.Text = ""; 
                        seeds--;
                    }
                }
                else
                {
                    MessageBox.Show("種子用完了");
                }
            }
            else if (fertilizerRadioButton.Checked)
            {
                if (fertilizer > 0)
                {
                    if (clickedButton.ImageIndex == 1) // 種子狀態
                    {
                        // 判斷是否需要澆水和施肥
                        if (clickedButton.Text == "水")
                        {
                            clickedButton.ImageIndex = 2; // 作物狀態
                            clickedButton.Text = " ";
                            clickedButton.Font = new Font(clickedButton.Font.FontFamily, 1); // 設置字體大小
                            clickedButton.TextAlign = ContentAlignment.TopRight; // 文字位置
                            fertilizer--;
                        }
                        else
                        {
                            clickedButton.Text = "肥";
                            clickedButton.Font = new Font(clickedButton.Font.FontFamily, 1); // 設置字體大小
                            clickedButton.TextAlign = ContentAlignment.TopRight; // 文字位置
                            fertilizer--;
                        }
                    }
                    else if (clickedButton.ImageIndex == 2) // 作物狀態
                    {
                        // 判斷是否需要澆水和施肥
                        if (clickedButton.Text == "水")
                        {
                            clickedButton.ImageIndex = 3; // 果實狀態
                            clickedButton.Text = " ";
                            clickedButton.Font = new Font(clickedButton.Font.FontFamily, 1); // 設置字體大小
                            clickedButton.TextAlign = ContentAlignment.TopRight; // 文字位置
                            fertilizer--;
                        }
                        else
                        {
                            clickedButton.Text = "肥";
                            clickedButton.Font = new Font(clickedButton.Font.FontFamily, 1); // 設置字體大小
                            clickedButton.TextAlign = ContentAlignment.TopRight; // 文字位置
                            fertilizer--;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("肥料用完了");
                }
                
            }
            else if (sickleRadioButton.Checked)
            {
                if (clickedButton.ImageIndex == 3) // 果實狀態
                {
                    clickedButton.ImageIndex = 0; // 泥土狀態
                    fruits++; // 增加果實數量
                }
            }
            UpdateShopDisplay();
        }
        private void shopButton_Click(object sender, EventArgs e)
        {
            if (buySeedsCheckBox.Checked)
            {
                if (money >= 10)
                {
                    seeds++;
                    money -= 10;
                }
                else
                {
                }
            }

            if (buyFertilizerCheckBox.Checked)
            {
                if (money >= 10)
                {
                    fertilizer++;
                    money -= 10;
                }
                else
                {
                }
            }

            if (sellFruitsCheckBox.Checked)
            {
                if (fruits > 0)
                {
                    fruits--;
                    money += 40; 
                }
                else
                {
                }
            }
            UpdateShopDisplay();
        }
        private void InitializeField()
        {
            // 初始化田地按钮
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    fieldButtons[i, j] = new Button();
                    fieldButtons[i, j].Size = new Size(90, 90);
                    fieldButtons[i, j].Location = new Point(j * 90, i * 90);
                    fieldButtons[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    fieldButtons[i, j].Click += new EventHandler(FieldButtonClick);
                    fieldButtons[i, j].Text = " ";
                    fieldButtons[i, j].ForeColor = Color.Transparent;
                    tabPage1.Controls.Add(fieldButtons[i, j]);
                }
            }
            GroupBox toolGroupBox = new GroupBox();
            toolGroupBox.Text = "工具";
            toolGroupBox.Location = new Point(275, 250);
            toolGroupBox.Size = new Size(375, 70);
            tabPage1.Controls.Add(toolGroupBox);

            wateringCanRadioButton = new RadioButton();
            wateringCanRadioButton.Text = "澆水壺";
            wateringCanRadioButton.Location = new Point(20, 30);
            wateringCanRadioButton.Font = new Font(wateringCanRadioButton.Font.FontFamily, 12, wateringCanRadioButton.Font.Style);
            wateringCanRadioButton.Size = new Size(90, 20);
            toolGroupBox.Controls.Add(wateringCanRadioButton);

            seedsRadioButton = new RadioButton();
            seedsRadioButton.Text = "種子";
            seedsRadioButton.Location = new Point(120, 30);
            seedsRadioButton.Font = new Font(seedsRadioButton.Font.FontFamily, 12, seedsRadioButton.Font.Style);
            seedsRadioButton.Size = new Size(90, 20);
            toolGroupBox.Controls.Add(seedsRadioButton);

            fertilizerRadioButton = new RadioButton();
            fertilizerRadioButton.Text = "肥料";
            fertilizerRadioButton.Location = new Point(220, 30);
            fertilizerRadioButton.Font = new Font(fertilizerRadioButton.Font.FontFamily, 12, fertilizerRadioButton.Font.Style);
            fertilizerRadioButton.Size = new Size(90, 20);
            toolGroupBox.Controls.Add(fertilizerRadioButton);

            sickleRadioButton = new RadioButton();
            sickleRadioButton.Text = "鐮刀";
            sickleRadioButton.Location = new Point(320, 30);
            sickleRadioButton.Font = new Font(sickleRadioButton.Font.FontFamily, 12, sickleRadioButton.Font.Style);
            sickleRadioButton.Size = new Size(90, 20);
            toolGroupBox.Controls.Add(sickleRadioButton);

        }
        private void InitializeShop()
        {
            buySeedsCheckBox = new CheckBox();
            buySeedsCheckBox.Text = "種子";
            buySeedsCheckBox.Location = new Point(75, 125);
            tabPage2.Controls.Add(buySeedsCheckBox);

            buyFertilizerCheckBox = new CheckBox();
            buyFertilizerCheckBox.Text = "肥料";
            buyFertilizerCheckBox.Location = new Point(285, 125);
            tabPage2.Controls.Add(buyFertilizerCheckBox);

            sellFruitsCheckBox = new CheckBox();
            sellFruitsCheckBox.Text = "果實";
            sellFruitsCheckBox.Location = new Point(475, 125);
            tabPage2.Controls.Add(sellFruitsCheckBox);

            shopButton = new Button();
            shopButton.Text = "買/賣";
            shopButton.Size = new Size(100, 40);
            shopButton.Location = new Point(275, 275);
            tabPage2.Controls.Add(shopButton);
            shopButton.Click += new EventHandler(shopButton_Click);

            moneyLabel = new Label();
            moneyLabel.Text = "金錢: " + money + "元";
            moneyLabel.Location = new Point(275, 50);
            tabPage2.Controls.Add(moneyLabel);

            seedsLabel = new Label();
            seedsLabel.Text = "擁有: " + seeds;
            seedsLabel.Location = new Point(75, 175);
            tabPage2.Controls.Add(seedsLabel);

            fertilizerLabel = new Label();
            fertilizerLabel.Text = "擁有: " + fertilizer;
            fertilizerLabel.Location = new Point(285, 175);
            tabPage2.Controls.Add(fertilizerLabel);

            fruitsLabel = new Label();
            fruitsLabel.Text = "擁有: " + fruits;
            fruitsLabel.Location = new Point(475, 175);
            tabPage2.Controls.Add(fruitsLabel);
        }
        private void UpdateShopDisplay()
        {
            moneyLabel.Text = "金錢: " + money + "元";
            seedsLabel.Text = "擁有: " + seeds;
            fertilizerLabel.Text = "擁有: " + fertilizer;
            fruitsLabel.Text = "擁有: " + fruits;
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
