using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace nameGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //загрузка названий из файла во вторую вкладку
            string[] images = File.ReadAllLines("2images.txt");
            foreach (var item in images)
            {
                listBox1.Items.Add(item);
            }
            //загрузка изображения во вторую вкладку при старте
            loadImage(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //генерация Имени и ФАмилии
            try
            {
                string[] firstName = File.ReadAllLines("firstName.txt");
                string[] lastName = File.ReadAllLines("lastName.txt");

                int firstLenght = firstName.Length;//узнаю длину текста
                int lastLenght = lastName.Length;

                Random rnd = new Random();

                //чекбокс для вывода нескольких имен
                if (checkBox1.Checked)
                {
                    textBox1.Text = "";
                    for (int i = 0; i < numericUpDown1.Value; i++)
                    {
                        //вывод из массива вначале имени, затем фамилии
                        textBox1.Text += firstName[rnd.Next(firstLenght)] + " " + lastName[rnd.Next(lastLenght)] + Environment.NewLine;
                    }
                }
                else
                {
                    textBox1.Text = firstName[rnd.Next(firstLenght)] + " " + lastName[rnd.Next(lastLenght)];
                }
            }
            catch (Exception ex)
            {
                textBox1.Text += ex;
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadImage(listBox1.SelectedIndex);
        }
        //загрузка изображений во вторую вкладку
        void loadImage(int idList)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //поиск изображения по индексу listBox1
            if (File.Exists("images/" + (int)(idList + 1) + ".jpg"))
            {
                pictureBox1.Image = Image.FromFile("images/" + (int)(idList + 1) + ".jpg");
            }
            else
            {
                pictureBox1.Image = Image.FromFile("images/not detected.jpeg");
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //в файле хранятся все истории персонажей разделенные на список с разделителем |
            string input = File.ReadAllText("files/histori.txt");
            string forpatern = listBox2.SelectedItem.ToString().Replace(" ", @"\s");
            //поиск выбранного персонажа в файле по нику, считывание текста до символа |
            string pattern = listBox2.SelectedIndex + 1 + @"\.\s" + forpatern + @"[^\|]+\|";
            textBox2.Clear();

            //вывод найденного текста
            foreach (Match match in Regex.Matches(input, pattern))
            {
                textBox2.Text = match.Value;
            }
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            //вывод картинки к персонажу
            pictureBox2.Image = Image.FromFile("images/game/" + (listBox2.SelectedIndex + 1) + ".jpg");
        }
    }
}
