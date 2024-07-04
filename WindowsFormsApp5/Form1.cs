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
        public DataTable table;
        public DataSet dataSet;
        public string xmlPath;
        ToolStripLabel dateLabel;
        ToolStripLabel timeLabel;
        ToolStripLabel infoLabel;
        Timer timer;
        public Form1()
        {
            InitializeComponent();
            table = new DataTable("Main");
            dataSet = new DataSet();
            xmlPath = "database.xml";
            dataSet.ReadXml(xmlPath);
            if (dataSet.Tables.Count == 0 /*|| dataSet.Tables[0].Rows.Count<2*/)
            {
                Table_Reset();
            }
            else
            {
                table = dataSet.Tables[0];
                table.AcceptChanges();
            }
            dataGridView1.DataSource = table;
            infoLabel = new ToolStripLabel();
            infoLabel.Text = "Текущие дата и время:";
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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataRow dr;
                dr = table.Rows[e.RowIndex];

                if (dr[1].ToString() != "" && Uri.IsWellFormedUriString(dr[1].ToString(), UriKind.Absolute))
                {
                    DialogResult dialogResult = MessageBox.Show("Хотите ли вы открыть данную ссылку во встроенном браузере?", "Открыть?", MessageBoxButtons.YesNoCancel);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Form3 newForm3 = new Form3(dr[1].ToString());
                        newForm3.Show();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        ProcessStartInfo sInfo = new ProcessStartInfo(dr[1].ToString());
                        Process.Start(sInfo);
                    }

                }
                else
                {
                    MessageBox.Show(
                        "Неверный адрес",
                        "Ошибка",
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
            DialogResult dialogResult = MessageBox.Show("Хотите ли вы сохранить таблицу?", "Сохранить?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dataSet.Tables.Clear();
                dataSet.Tables.Add(table);
                dataSet.WriteXml(xmlPath);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.ToString() == "" || textBox1.Text.ToString() == "")
            {
                MessageBox.Show(
                    "Вы оставили пустые поля!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            if (!Uri.IsWellFormedUriString(textBox1.Text.ToString(), UriKind.Absolute))
            {
                MessageBox.Show(
                     "Неверный адрес!",
                     "Ошибка",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information,
                     MessageBoxDefaultButton.Button1,
                     MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            DataRow dr;
            dr = table.NewRow();
            dr[0] = textBox2.Text.ToString();
            dr[1] = textBox1.Text.ToString();
            dr[2] = dateTimePicker2.Text.ToString();
            dr[3] = dateTimePicker1.Text.ToString();
            table.Rows.Add(dr);
            table.AcceptChanges();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = table;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Table_Reset();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = table;
        }

        private void Table_Reset()
        {
            table.Rows.Clear();
            table.Columns.Clear();
            table.Columns.Add("Задача".ToString());
            table.Columns.Add("Ссылка".ToString());
            table.Columns.Add("Дата начала выполнения".ToString());
            table.Columns.Add("Выполнить до".ToString());
            table.AcceptChanges();
            dataSet.Tables.Clear();
            dataSet.Tables.Add(table);
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
            table = (DataTable)dataGridView1.DataSource;
            table.AcceptChanges();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                xmlPath = saveFileDialog1.FileName.ToString();
                dataSet.WriteXml(xmlPath);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataSave();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Лабораторная работа №6 по дисциплине Визуальное проектирование программ в среде Microsoft Visual Studio",
                "О программе",
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
                xmlPath = openFileDialog1.FileName.ToString();
                openFile();

            }
        }

        private void открытьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openFile();
        }

        private void openFile()
        {
            DialogResult dialogResult = MessageBox.Show("Хотите ли вы открыть таблицу и потерять несохранённые данные?", "Открыть?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                table = new DataTable("Main");
                dataSet = new DataSet();
                dataSet.ReadXml(xmlPath);
                if (dataSet.Tables.Count == 0 /*|| dataSet.Tables[0].Rows.Count<2*/)
                {
                    Table_Reset();
                }
                else
                {
                    table = dataSet.Tables[0];
                    table.AcceptChanges();
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = table;
            }
        }
    }
}
