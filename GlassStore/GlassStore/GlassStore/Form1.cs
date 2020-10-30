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
using System.Threading;
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
        IModbusSerialMaster master;
        Thread thrd;
        Thread thrd1;
        StringBuilder stt = new StringBuilder();
        int k;

        

        private void mobang()
        {
            Dulieu.InitDataTable();
            dataGridView2.DataSource = Dulieu.zDataAll;
        }
   

        public Form1()
        {
            InitializeComponent();
            Comport();
            Control.CheckForIllegalCrossThreadCalls = false;
            
        }

        private void Comport()
        {
            string[] ports = SerialPort.GetPortNames();
            cbxComport.Items.AddRange (ports);
            cbxcomB.Items.AddRange(ports);
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
                    serialPort1.DataBits = Convert.ToInt32(cbxdatabit.Text);
                    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbxstopbit.Text);
                    serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cbxparity.Text);
                    //serialPort1.ReadTimeout = 1000;

                    serialPort1.Open();
                    var factory = new ModbusFactory();
                    master = factory.CreateRtuMaster(serialPort1);
                 
                    
                }

        }

        private void ModbusRead(byte slaveId,ushort startAddress,ushort numRegisters)
        {

            try
            {
                ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);
                data.Clear();
                for (int i = 0; i < numRegisters; i++)
                {
                    
                    
                    data.Append($"Register {startAddress + i}={registers[i].ToString("X")}" + "\r\n");
                }
                
                
                    txbShowA.Text = Convert.ToString(data);
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ModbusTrasmit(byte slaveId,ushort startAddress,ushort[] numRegister)
        {
            try
            {
                master.WriteMultipleRegisters(slaveId, startAddress, numRegister);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void modbus1()
        {
            
            ModbusRead(3, 0, 10);
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
            dataGridView1.DataSource = dt;
        }

        private void btnCA_Click(object sender, EventArgs e)
        {
            
            OpenComport_Aduino();                        
            timer1.Enabled = true;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            thrd = new Thread(modbus1)
            {
                IsBackground = true
            };
            thrd.Start();
                                  
        }
        
        private void BTNDIS_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            timer1.Enabled = false;
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mobang();
            //timer2.Enabled = true;
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            thrd1 = new Thread(Dulieu.InitDataTable);
            //thrd1 = new Thread()
            thrd1.IsBackground = true;
            thrd1.Start();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            byte a = Convert.ToByte(txbSlaveId.Text);
            ushort addr =Convert.ToUInt16(txBaddress.Text);
            string ox = Convert.ToString(txBSendData.Text);
            ox = ox.Replace(" ", "");
            string[] mx = ox.Split(';');
            ushort[] bbb = new ushort[10];
            for(int i=0;i<mx.Length;i++)
            {
                bbb[i] = Convert.ToUInt16(mx[i]);
            }                   
                    

            ModbusTrasmit(a, addr,bbb);

        }

        private void btnspecial_Click(object sender, EventArgs e)
        {
            Mahoa.addbit();
            for (int i = 0; i < 30; i++)
            {
                stt.Append(Convert.ToString(Mahoa.thongtinkho[i])+ "\r\n");
            }
           
        }
    }
}
