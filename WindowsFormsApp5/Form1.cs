using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        private TodoList todoList;
        ToolStripLabel dateLabel;
        ToolStripLabel timeLabel;
        ToolStripLabel infoLabel;
        Timer timer;
        public Form1()
        {
            InitializeComponent();
            todoList = new TodoList();
            dataGridView1.DataSource = todoList.getTable();
            infoLabel = new ToolStripLabel();
            infoLabel.Text = "Current date and time:";
            dateLabel = new ToolStripLabel();
            timeLabel = new ToolStripLabel();
            statusStrip1.Items.Add(infoLabel);
            statusStrip1.Items.Add(dateLabel);
            statusStrip1.Items.Add(timeLabel);
            timer = new Timer() { Interval = 1000 };
            timer.Tick += timer_Tick;
            timer.Start();
            this.FormClosed += MyClosedHandler;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            dateLabel.Text =
            DateTime.Now.ToLongDateString();
            timeLabel.Text =
            DateTime.Now.ToLongTimeString();
        }

        protected void MyClosedHandler(object sender, EventArgs e)
        {
            dataSave();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataRow dr;
                dr = todoList.getTable().Rows[e.RowIndex];

                if (dr[1].ToString() != "" && Uri.IsWellFormedUriString(dr[1].ToString(), UriKind.Absolute))
                {
                    DialogResult dialogResult = MessageBox.Show("Do you want to follow this link?", "Open?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        ProcessStartInfo sInfo = new ProcessStartInfo(dr[1].ToString());
                        Process.Start(sInfo);
                    }

                }
                else
                {
                    MessageBox.Show(
                        "Invalid address",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            return;

        }

        private void dataSave()
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save this table?", "Save?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                todoList.saveTable();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.ToString() == "" || textBox1.Text.ToString() == "")
            {
                MessageBox.Show(
                    "Some fields are empty!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            if (!Uri.IsWellFormedUriString(textBox1.Text.ToString(), UriKind.Absolute))
            {
                MessageBox.Show(
                     "Invalid address!",
                     "Error",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information,
                     MessageBoxDefaultButton.Button1,
                     MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            DataRow dr;
            dr = todoList.newRow();
            dr[0] = textBox2.Text.ToString();
            dr[1] = textBox1.Text.ToString();
            dr[2] = dateTimePicker2.Text.ToString();
            dr[3] = dateTimePicker1.Text.ToString();
            dr[4] = "Not yet";
            todoList.setNewRow(dr);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = todoList.getTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            todoList.tableReset();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = todoList.getTable();
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewTextBoxCell item in this.dataGridView1.SelectedCells)
            {
                if (item.RowIndex >= 0)
                {
                    dataGridView1.Rows.RemoveAt(item.RowIndex);

                }
                else
                {
                    break;
                }
            }
            todoList.setTable((DataTable)dataGridView1.DataSource);
            todoList.getTable().AcceptChanges();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                todoList.saveTableAs(saveFileDialog1.FileName.ToString());
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataSave();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Todo list written in C#",
                "About program",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            return;
        }

        private void открутToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                todoList.setXmlPath(openFileDialog1.FileName.ToString());
                openFile();

            }
        }

        private void открытьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openFile();
        }

        private void openFile()
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to open a new table and lose all unsaved data?", "Open?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                todoList.openNewTable();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = todoList.getTable();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewTextBoxCell item in this.dataGridView1.SelectedCells)
            {
                if (item.RowIndex >= 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Did you complete this task on time?", "Complete?", MessageBoxButtons.YesNoCancel);
                    if (dialogResult == DialogResult.Yes)
                    {
                        dataGridView1.Rows[item.RowIndex].Cells[4].Value = "Completed";
                    }
                    if (dialogResult == DialogResult.No)
                    {
                        dataGridView1.Rows[item.RowIndex].Cells[4].Value = "Failed";
                    }
                }
                else
                {
                    break;
                }
            }
            todoList.setTable((DataTable)dataGridView1.DataSource);
            todoList.getTable().AcceptChanges();
        }
    }
}
