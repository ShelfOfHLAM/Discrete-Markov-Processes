using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лаб1
{
    public partial class Form1 : Form
    {
        int countStrategy = 1;

        public Form1()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].HeaderCell.Value = dataGridView1.Rows.Count.ToString();
            dataGridView2.Rows[dataGridView1.Rows.Count - 1].HeaderCell.Value = dataGridView2.Rows.Count.ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < numericUpDown1.Value)
            {
                dataGridView1.Rows.Add();
                dataGridView2.Rows.Add();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].HeaderCell.Value = dataGridView1.Rows.Count.ToString();
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].HeaderCell.Value = dataGridView2.Rows.Count.ToString();

                while(dataGridView1.Columns.Count > 1)
                {
                    dataGridView1.Columns.RemoveAt(dataGridView1.Columns.Count - 1);
                    dataGridView2.Columns.RemoveAt(dataGridView2.Columns.Count - 1);

                }

                for (int i = 0; i < numericUpDown2.Value; i++)
                {
                    for (int j = 0; j < numericUpDown1.Value; j++)
                    {
                        if (i != 0 || j != 0)
                        {
                            dataGridView1.Columns.Add((i + 1).ToString() + "." + (j + 1).ToString(), (i + 1).ToString() + "." + (j + 1).ToString());
                            dataGridView2.Columns.Add((i + 1).ToString() + "." + (j + 1).ToString(), (i + 1).ToString() + "." + (j + 1).ToString());

                        }

                    }

                }

            }
            else
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                dataGridView2.Rows.RemoveAt(dataGridView2.Rows.Count - 1);

                while (dataGridView1.Columns.Count > 1)
                {
                    dataGridView1.Columns.RemoveAt(dataGridView1.Columns.Count - 1);
                    dataGridView2.Columns.RemoveAt(dataGridView2.Columns.Count - 1);

                }

                for (int i = 0; i < numericUpDown2.Value; i++)
                {
                    for (int j = 0; j < numericUpDown1.Value; j++)
                    {
                        if (i != 0 || j != 0)
                        {
                            dataGridView1.Columns.Add((i + 1).ToString() + "." + (j + 1).ToString(), (i + 1).ToString() + "." + (j + 1).ToString());
                            dataGridView2.Columns.Add((i + 1).ToString() + "." + (j + 1).ToString(), (i + 1).ToString() + "." + (j + 1).ToString());

                        }

                    }

                }

            }

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if(countStrategy < numericUpDown2.Value)
            {
                for(int i = 0; i < numericUpDown1.Value; i++)
                {
                    dataGridView1.Columns.Add(numericUpDown2.Value.ToString() + "." + (i + 1).ToString(), numericUpDown2.Value.ToString() + "." + (i + 1).ToString());
                    dataGridView2.Columns.Add(numericUpDown2.Value.ToString() + "." + (i + 1).ToString(), numericUpDown2.Value.ToString() + "." + (i + 1).ToString());

                }
                countStrategy++;
            }
            else
            {
                for (int i = 0; i < numericUpDown1.Value; i++)
                {
                    dataGridView1.Columns.RemoveAt(dataGridView1.Columns.Count - 1);
                    dataGridView2.Columns.RemoveAt(dataGridView2.Columns.Count - 1);

                }

                countStrategy--;

            }

        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            double[,] massProb = new double[100, 100];
            double[,] massProf = new double[100, 100];

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for(int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    massProb[i, j] = Convert.ToDouble(dataGridView1[i, j].Value.ToString());

                }

            }

            for (int i = 0; i < dataGridView2.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                {
                    massProf[i, j] = Convert.ToDouble(dataGridView2[i, j].Value.ToString());

                }

            }

            Form2 f = new Form2(massProb,massProf,Convert.ToInt32(numericUpDown2.Value), Convert.ToInt32(numericUpDown1.Value));
            f.Show();

        }

        private void button3_MouseClick(object sender, MouseEventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл

            string saveText = "";

            saveText += numericUpDown1.Value.ToString() + " " + numericUpDown2.Value.ToString() + " ";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    saveText += dataGridView1[j, i].Value.ToString() + " ";

                }

            }

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    saveText += dataGridView2[j, i].Value.ToString() + " ";

                }

            }

            System.IO.File.WriteAllText(filename, saveText);
            MessageBox.Show("Файл сохранен");

        }

        private void button4_MouseClick(object sender, MouseEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            string fileText = System.IO.File.ReadAllText(filename);

            string[] text = fileText.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            int numberState = Convert.ToInt32(text[0]);
            int numberStrategy = Convert.ToInt32(text[1]);

            for (int j = 0; j < numberState-1; j++)
            {
                numericUpDown1.Value++;

            }

            for (int j = 0; j < numberStrategy-1; j++)
            {
                numericUpDown2.Value++;

            }

            int i = 2;
    
            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
               for (int k = 0; k < dataGridView1.Columns.Count; k++)
               {
                    dataGridView1[k, j].Value = text[i];
                    i++;

               }

            }

            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                for (int k = 0; k < dataGridView1.Columns.Count; k++)
                {
                    dataGridView2[k, j].Value = text[i];
                    i++;

                }

            }

            MessageBox.Show("Файл открыт");

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
