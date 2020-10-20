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
using System.IO;
using NModbus;

namespace GlassStore
{
    public partial class Form1 : Form
    {

        DataTableCollection tableCollection;
        StringBuilder data = new StringBuilder();
        IModbusSlave slave;
        IModbusMaster master;
        public Form1()
        {
            InitializeComponent();
            Comport();
        }

        private void Comport()
        {
            string[] ports = SerialPort.GetPortNames();
            cbxComport.Items.AddRange (ports);
        }
        private void OpenComport_Aduino()
        {
 
                if(cbxComport.Text==""||cbxbaurate.Text=="")
                {
                    MessageBox.Show("select");
                }
                else
                {
                    serialPort1.PortName = cbxComport.Text;
                    serialPort1.BaudRate = Convert.ToInt32(cbxbaurate.Text);
                    //serialPort1.DataBits = Convert.ToInt32(cbxdatabit.Text);
                    //serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbxstopbit.Text);
                    //serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cbxparity.Text);
                    serialPort1.WriteTimeout = 500;
                    serialPort1.ReadTimeout = 500;

                    serialPort1.Open();
                    var factory = new ModbusFactory();
                    IModbusMaster master = factory.CreateRtuMaster(serialPort1);


                }

        }

        private void Modbus(byte slaveId,ushort startAddress,ushort numRegisters)
        {            
            ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

            for (int i = 0; i < numRegisters; i++)
            {
               data.Append($"Register {startAddress + i}={registers[i]}");
            }
        }

        private void OpenComport_BarcodeGun()
        {
            try
            {
                if (comboBox1.Text == "" || cbxComport.Text == "")
                {
                    MessageBox.Show("select");
                }
                else
                {
                    serialPort2.PortName = cbxComport.Text;
                    serialPort2.BaudRate = Convert.ToInt32(cbxbaurate.Text);
                    serialPort2.DataBits = Convert.ToInt32(cbxdatabit.Text);
                    serialPort2.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbxstopbit.Text);
                    serialPort2.Parity = (Parity)Enum.Parse(typeof(Parity), cbxparity.Text);

                    serialPort2.Open();

                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("error");
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

        private void btnCA_Click(object sender, EventArgs e)
        {

            OpenComport_Aduino();
            
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Modbus(3, 0, 10);
            txbShowA.Text = Convert.ToString(data);
        }
    }
}
