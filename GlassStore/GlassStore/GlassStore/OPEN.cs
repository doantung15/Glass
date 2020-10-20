using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NModbus;
using NModbus.Utility;
using NModbus.Serial;
using System.IO.Ports;
namespace GlassStore
{
    using NModbus.Logging;
    
    class OPEN
    {
        private const string Linktoarduino = "COM4";
        private const string Linktobarcode = "COM22";



        public static void ModbusSerialRtuMasterWriteRegisters(byte slaveID, ushort slaveaddress,int result)
        {
            using (SerialPort port = new SerialPort(Linktoarduino))
            {
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.ReadTimeout = 500;
                port.Open();

                var factory = new ModbusFactory();
                IModbusMaster master = factory.CreateRtuMaster(port);

                ushort[] value = { 1 };
                value[0] = (ushort)result;
                master.WriteMultipleRegisters(slaveID, slaveaddress, value);

                

            }
        }
    }
}
