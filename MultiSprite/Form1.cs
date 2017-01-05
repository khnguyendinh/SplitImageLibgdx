using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MultiSprite
{
    public partial class Form1 : Form
    {
        //đường dẫn file 
        String input, output;
        Image image;
        int Count_PNG;
        Itemsssss[] items ;
        //string bb;
        int[] tdX, tdY;
        int[] wh, hg;
        /// <summary>
        ///tên từng cái ảnh trước khi cắt
        /// 
        /// </summary>
        string[] name_img;
        string image_atlas;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog open1 = new OpenFileDialog();
            DialogResult result = open1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = open1.FileName;
                try
                {
                    string text = open1.FileName;
                    txtInput.Text = open1.FileName;
                    image = Image.FromFile(text);
                    pictureBox1.Image = image;
                }
                catch (IOException)
                {
                }
            }
        }

        private void btnOutPut_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtOutPut.Text = fbd.SelectedPath;
            }

        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            int rows = 2;//No of Rows as per Desire
            int columns = 2;//No of columns as per Desire
            Image[] imgarray1 = new Image[Count_PNG];//Create Image Array of Size Rows X Colums
            Image img = image;//Get Image from anywhere, From File Or Using Dialogbox used previously

            int height = img.Height;
            int width = img.Width;//Get image Height & Width of Input Image
            //int one_img_h = height / rows;
            //int one_img_w = width / columns;//You need Rows x Columns, So get 1/rows Height, 1/columns width of original Image
            int one_img_h = 200;
            int one_img_w = 200;//You need Rows x Columns, So get 1/rows Height, 1/columns width of original Image
            for (int j = 0; j < Count_PNG; j++)
            {
                imgarray1[j] = new Bitmap(wh[j], hg[j]);//generating new bitmap
                Graphics graphics = Graphics.FromImage(imgarray1[j]);
                graphics.DrawImage(img, new Rectangle(0, 0, wh[j], hg[j]), new Rectangle(tdX[j], tdY[j], wh[j], hg[j]), GraphicsUnit.Pixel);//Generating Splitted Pieces of Image
                graphics.Dispose();
            }
            //Image Is spitted You can use it by getting image from **imgarray[Rows, Columns]**
            //Or You can Save it by using Following Code

            String destinationFolderName="";
            if (txtOutPut.Text != "")
            {
                 destinationFolderName = txtOutPut.Text;//Define a saving path
            }else{
                destinationFolderName = txtInput.Text.Replace(image_atlas,"")+"/"+"in";
                if (!Directory.Exists(destinationFolderName))
                Directory.CreateDirectory(destinationFolderName);
                txtOutPut.Text = destinationFolderName;
            }
            //FolderBrowserDialog FolderBrowserDialog1 = new FolderBrowserDialog();
            for (int j = 0; j < Count_PNG; j++)
            {
                imgarray1[j].Save(@"" + destinationFolderName + "/" + name_img[j] + ".jpg");//Save every image in Array [row][column] on local Path
            }
        }

        private void btnInputAtlas_Click(object sender, EventArgs e)
        {
            OpenFileDialog open1 = new OpenFileDialog();
            DialogResult result = open1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = open1.FileName;
                try
                {
                    string text = open1.FileName;
                    txtInputAtlas.Text = open1.FileName;
                    string[] lines = System.IO.File.ReadAllLines(txtInputAtlas.Text);
                    input = System.IO.Path.GetDirectoryName(open1.FileName);
                    Count_PNG = (lines.Length - 4) / 7;
                    items = new Itemsssss[Count_PNG];
                    tdX = new int[Count_PNG];
                    tdY = new int[Count_PNG];
                    wh = new int[Count_PNG];
                    hg = new int[Count_PNG];
                    name_img = new string[Count_PNG];
                    image_atlas = lines[0].Trim();
                    txtInput.Text = txtInputAtlas.Text.Replace("txt","png");
                    image = Image.FromFile(txtInput.Text);
                    pictureBox1.Image = image;
                    for (int i = 4; i < lines.Length-4; i += 7)
                    {
                        int index = (i - 4) / 7;
                        name_img[index] = getNameImg(lines[i]);
                        tdX[index] = Int32.Parse(getToadoX(lines[i + 2]));
                        tdY[index] = Int32.Parse(getToadoY(lines[i + 2]));
                        wh[index] = Int32.Parse(getToadoX(lines[i + 3]));
                        hg[index] = Int32.Parse(getToadoY(lines[i + 3]));

                    }
                }
                catch (IOException)
                {
                }
            }
        }
        /// <summary>
        /// đếm số file ảnh png có trong out
        /// </summary>
        public void CountFileImage()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                    MessageBox.Show("have " + Count_PNG + " PNG  in atlas"); 

        }
        public string getToadoX(string str)
        {
            str.Trim();
            string[] word = str.Split(' ');
            return ((str.Trim()).Split(' '))[1].Trim(',');
        }
        
             public string getToadoY(string str)
        {
            str.Trim();
            string[] word = str.Split(' ');
            return ((str.Trim()).Split(' '))[2].Trim(',');
        }
             
         public string getNameImg(string str)
        {
            str.Trim();
            string[] word = str.Split(' ');
            return ((str.Trim()).Split(' '))[0].Trim(',');
        }

         private void button1_Click_1(object sender, EventArgs e)
         {
             if (!txtOutPut.Equals(""))
             {
                 txtOutPut.Text = "";
             }
         }

    }
}
