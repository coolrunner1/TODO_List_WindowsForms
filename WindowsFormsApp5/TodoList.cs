using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public class TodoList
    {
        private DataSet dataSet;
        private DataTable table;
        private string xmlPath;
        public TodoList()
        {
            table = new DataTable("Main");
            dataSet = new DataSet();
            xmlPath = "database.xml";
            dataSet.ReadXml(xmlPath);
            if (dataSet.Tables.Count == 0)
            {
                tableReset();
            }
            else
            {
                table = dataSet.Tables[0];
                table.AcceptChanges();
            }
        }
        public void setXmlPath(string xmlPath)
        {
            this.xmlPath = xmlPath;
        }

        public string getXmlPath()
        {
            return this.xmlPath;
        }

        public void setTable(DataTable table)
        {
            this.table = table;
        }

        public DataTable getTable()
        {
            return table;
        }

        public void tableReset()
        {
            table.Rows.Clear();
            table.Columns.Clear();
            table.Columns.Add("Task".ToString());
            table.Columns.Add("Link".ToString());
            table.Columns.Add("Start on".ToString());
            table.Columns.Add("Finish before".ToString());
            table.Columns.Add("Completion").ToString();
            table.AcceptChanges();
            dataSet.Tables.Clear();
            dataSet.Tables.Add(table);
        }

        public void saveTable()
        {
            dataSet.Tables.Clear();
            dataSet.Tables.Add(table);
            dataSet.WriteXml(xmlPath);
        }

        public DataRow newRow()
        {
            return table.NewRow();
        }

        public void setNewRow(DataRow dr)
        {
            table.Rows.Add(dr);
            table.AcceptChanges();
        }

        public void saveTableAs(string path)
        {
            xmlPath = path;
            dataSet.WriteXml(xmlPath);
        }

        public void openNewTable()
        {
            table = new DataTable("Main");
            dataSet = new DataSet();
            dataSet.ReadXml(xmlPath);
            if (dataSet.Tables.Count == 0)
            {
                tableReset();
            }
            else
            {
                table = dataSet.Tables[0];
                table.AcceptChanges();
            }
        }
    }
}
