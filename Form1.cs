using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox1.Text = "";
            string SelectionStart = monthCalendar1.SelectionRange.Start.ToString();
            string[] date = SelectionStart.Split(' ');
            textBox2.Text = date[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string SelectionStart = monthCalendar1.SelectionRange.Start.ToString();
            string[] date = SelectionStart.Split(' ');
            string Date = date[0].Replace("-", "");

            dataGridView1.Rows.Add(Date, textBox1.Text);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("col1", "날짜");
            dataGridView1.Columns.Add("col2", "일정");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                dataGridView1.Rows.Remove(row);
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string fileName = saveFileDialog1.FileName;
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("C:\\output.csv");
            string strHeader = "";
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                strHeader += dataGridView1.Columns[i].HeaderText + ",";
            }
            streamWriter.WriteLine(strHeader);
            for (int m = 0; m < dataGridView1.Rows.Count - 1; m++)
            {
                string strRowValue = "";
                for (int n = 0; n < dataGridView1.Columns.Count; n++)
                {
                    strRowValue += dataGridView1.Rows[m].Cells[n].Value + ",";
                }
                streamWriter.WriteLine(strRowValue);
            }
            streamWriter.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Open CSV Files";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "CSV";
            openFileDialog1.Filter = "CSV files(*.csv)|*.csv|All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string rowValue;
                string[] cellValue;
                dataGridView1.Rows.Clear();

                if (System.IO.File.Exists(openFileDialog1.FileName))
                {
                    System.IO.StreamReader streamReader = new System.IO.StreamReader(openFileDialog1.FileName);
                    //Reading header
                    rowValue = streamReader.ReadLine();
                    cellValue = rowValue.Split(',');
                    for (int i = 0; i <= cellValue.Count() - 1; i++)
                    {
                        DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                        column.Name = cellValue[i];
                        column.HeaderText = cellValue[i];
                        dataGridView1.Columns.Add(column);
                    }
                    //Reading content
                    while (streamReader.Peek() != -1)
                    {
                        rowValue = streamReader.ReadLine();
                        cellValue = rowValue.Split(',');
                        dataGridView1.Rows.Add(cellValue);
                    }
                    streamReader.Close();
                }
                else
                {
                    MessageBox.Show("No File is Selected");
                }
            }
        }
    }
}
