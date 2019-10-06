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
    public partial class Form2 : Form
    {
        int state = 0;
        int strategy = 0;
        int step = 1;
        int maxStep = 1;
        double[,] q = new double[100, 100] ;
        double[,] mass = new double[100, 100];
        double[,] expectMass = new double[100, 100];
        int[,] optimalStrategy = new int[100, 100];

        public Form2(double[,] massProb, double[,] massProf, int str, int st)
        {
            InitializeComponent();
            strategy = str;
            state = st;

            for (int i = 0; i < strategy; i++)
            {
                for (int j = 0; j < state; j++)
                {
                    q[i, j] = 0;

                    for (int k = 0; k < state; k++)
                    {
                        q[i, j] += massProb[(i * state) + k, j] * massProf[(i * state) + k, j];

                    }

                }

            }

            for (int i = 0; i < state; i++)
            {
                expectMass[0, i] = 0;

                for (int j = 0; j < strategy; j++)
                {
                    if (expectMass[0, i] < q[j, i])
                    {
                        expectMass[0, i] = q[j, i];
                        optimalStrategy[0, i] = j + 1;

                    }

                }

            }

            for (int i = 0; i < strategy; i++)
            {
                for (int j = 0; j < state; j++)
                {
                    for (int k = 0; k < state; k++)
                    {
                        mass[(i * state) + k, j] = massProb[(i * state) + k, j];

                    }

                }

            }

            for(int i = 0; i < state; i++)
            {
                dataGridView1.Rows.Add(expectMass[0, i].ToString());
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                dataGridView2.Rows.Add(optimalStrategy[0, i].ToString());
                dataGridView2.Rows[i].HeaderCell.Value = (i + 1).ToString();
                
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown1.Enabled = false;
            button1.Enabled = false;

            if (step < numericUpDown1.Value)
            {
                if (maxStep < numericUpDown1.Value)
                {
                    for(int i = maxStep; i < Convert.ToInt32(numericUpDown1.Value); i++)
                    {
                        for (int j = 0; j < state; j++)
                        {
                            expectMass[i, j] = 0;

                            for (int k = 0; k < strategy; k++)
                            {
                                double expectMassM = q[k, j];

                                for(int z = 0; z < state; z++)
                                {
                                    expectMassM += expectMass[(i - 1), z] * mass[(k * state) + z, j];
                                
                                }

                                if(expectMass[i,j] < expectMassM)
                                {
                                    expectMass[i, j] = expectMassM;
                                    optimalStrategy[i, j] = k + 1;

                                }

                            }

                        }

                    }

                    maxStep = Convert.ToInt32(numericUpDown1.Value);

                }
                else
                {


                }

                for(int i = step; i < numericUpDown1.Value; i++)
                {
                    dataGridView1.Columns.Add(i.ToString(), (i + 1).ToString());
                    dataGridView2.Columns.Add(i.ToString(), (i + 1).ToString());

                    for (int j = 0; j < state; j++)
                    {
                        dataGridView1[i, j].Value = expectMass[i, j];
                        dataGridView2[i, j].Value = optimalStrategy[i,j];

                    }

                }

                step = Convert.ToInt32(numericUpDown1.Value);

            }
            else if (step > numericUpDown1.Value)
            {
                for (int i = 0; i < (step - Convert.ToInt32(numericUpDown1.Value)); i++)
                {
                    dataGridView1.Columns.RemoveAt(dataGridView1.Columns.Count - 1);
                    dataGridView2.Columns.RemoveAt(dataGridView2.Columns.Count - 1);

                }

                step = Convert.ToInt32(numericUpDown1.Value);

            }
            else
            {

            }

            numericUpDown1.Enabled = true;
            button1.Enabled = true;

        }

    }

}
