using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hw6_1
{
    public partial class Form1 : Form
    {
        private string imagePath;
        private Image originalImage;
        private DateTime startTime;

        public Form1()
        {
            InitializeComponent();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "圖片檔素(*jpg *.jpeg; *.png; *.bmp; *.gif)| *.jpg; *.jpeg; *.png; *.bmp; *.gif|所有檔案(*.*)| *.*";
                openFileDialog.Title = "選擇一個圖片檔案";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    Image originalImage = Image.FromFile(filePath);

                    Image resizedImage = new Bitmap(360, 360);
                    using (Graphics graphics = Graphics.FromImage(resizedImage))
                    {
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(originalImage, 0, 0, 360, 360);
                    }

                    pictureBox1.Image = resizedImage;
                    imagePath = filePath;
                }
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pictureBox1.Visible = (trackBar1.Value > 0);
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            // 在定时器事件中更新label2的Text属性以显示计时
            if (startTime != DateTime.MinValue)
            {
                TimeSpan elapsed = DateTime.Now - startTime;
                label2.Text = elapsed.ToString(@"hh\:mm\:ss"); 
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                int smallImageSize = 360 / 3; // 每個小圖片的大小
                int buttonIndex = 1; // 用於設置Button背景圖片的索引

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Rectangle sourceRectangle = new Rectangle(j * smallImageSize, i * smallImageSize, smallImageSize, smallImageSize);
                        Bitmap smallBitmap = new Bitmap(smallImageSize, smallImageSize);
                        using (Graphics graphics = Graphics.FromImage(smallBitmap))
                        {
                            graphics.DrawImage(originalImage, new Rectangle(0, 0, smallImageSize, smallImageSize), sourceRectangle, GraphicsUnit.Pixel);
                        }

                        Button button = Controls.Find("button" + buttonIndex, true).FirstOrDefault() as Button;
                        if (button != null)
                        {
                            button.BackgroundImage = smallBitmap;
                            button.BackgroundImageLayout = ImageLayout.Stretch; 
                            buttonIndex++;
                        }
                    }
                }
            }
        }
    }
}
