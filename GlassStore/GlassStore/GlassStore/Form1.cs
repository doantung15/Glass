using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using NModbus.Serial;
namespace GlassStore
{
    public partial class Form1 : Form
    {

        DataTableCollection tableCollection;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Comport()
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange (ports);
        }
        private void openComport()
        {
            try
            {
                if(comboBox1.Text==""||comboBox2.Text=="")
                {
                    MessageBox.Show("select");
                }
                else
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.DataBits = Convert.ToInt32(comboBox3.Text);
                    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox4.Text);


                }
            }
        }

        private void open_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx" })
            {
                if(openFileDialog.ShowDialog()==DialogResult.OK)
                {
                    txbfilename.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tableCollection = result.Tables;
                            comboBox1.Items.Clear();
                            foreach (DataTable table in tableCollection)
                                comboBox1.Items.Add(table.TableName);
                        }
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[comboBox1.SelectedItem.ToString()];
            dataGridView2.DataSource = dt;
        }
    }
}
