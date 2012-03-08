#define NO_OLD_FORMAT_OUT_FROM_RDR

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using RdrDoorClient;
using PacketFormat;




namespace SerialLogger
{


    class serialPortM
    {
        public SerialPort vSerialPort;
        private string[] portAvailable;
        public UInt32 vBaudRate;
        public int vDataBits;
        public StopBits vStopBits;
        public Parity vParity;
        public string vportSelected;
        string RxData;
        RdrClient rdrCli;
        private int doorno;
        //dataBaseM virtudbm;

        public serialPortM()
        {
            portAvailable = SerialPort.GetPortNames();

            vSerialPort = new SerialPort();
            vSerialPort.DataReceived += new SerialDataReceivedEventHandler(vSerialPort_DataReceived);
            vBaudRate = 9600;
            vDataBits = 8;
            vStopBits = StopBits.One;
            vParity = System.IO.Ports.Parity.None;
            vportSelected = null;
            vportSelected = "";

            rdrCli = new RdrClient("172.16.3.235");
        }

        public bool ChangeServerIp(string SrvIPAddress)
        {
            return rdrCli.ChangeServerIp(SrvIPAddress);
        }


        public void set_doorno(int drno)
        {
            doorno = drno;

        }
        public void getportvalues()
        {


        }

        public void setportvalues(UInt32 BaudRate, int DataBits, StopBits StopBits, Parity Parity)
        {
            vBaudRate = BaudRate;
            vDataBits = DataBits;
            vStopBits = StopBits;
            vParity = Parity;
        }



        public bool getid(string rxdata, ref int value)
        {

            char[] delimiters = { ' ', ',', ':' };
            string[] splitdata = rxdata.Split(delimiters);
            try
            {

                if (splitdata[0].CompareTo("ID") == 0)
                {
                    value = Convert.ToInt32(splitdata[2]);

                    return true;
                }
                //else if (splitdata[1].CompareTo("IDerr") == 0)
                //{
                //    value = Convert.ToInt32(splitdata[1]);
                //    return false;
                //}
                value = 0;

                return false;

            }
            catch (Exception ex)
            {

                applog.logexcep("getid", ex.Message);
                value = 0;

                return false;
            }

        }

        public void testsend(int inout)
        {
            RdrMsgFormat rdrmsg = new RdrMsgFormat();
            uint value = 1121;
            rdrmsg.cardid = (ushort)(value & 0x7fff);
            rdrmsg.doorno = (ushort)doorno;

            rdrmsg.key = 15432;
            rdrmsg.msgid = message_id.id;
            rdrmsg.swipedate = DateTime.Now;
            if (inout==1)
            {
                rdrmsg.inout = false;

                // applog.logtemp("Swipe Out :" + (value & 0x7fff).ToString(),now);
            }
            else
            {
                rdrmsg.inout = true;
                // applog.logtemp("Swipe In  :" + value.ToString(),now);
            }
            rdrCli.Send(rdrmsg);
        }

        public void vSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int value = 0;
            RdrMsgFormat rdrmsg = new RdrMsgFormat();
            DateTime now = new DateTime();
            try
            {
                RxData = vSerialPort.ReadLine();
                applog.logserial(RxData);


                if (getid(RxData, ref value))
                {
                    int rdr;
                    rdr = (value >> 15) & 0x1;
                    now = DateTime.Now;

                    rdrmsg.cardid = (ushort)(value & 0x7fff);
                    rdrmsg.doorno = (ushort)doorno;
                    rdrmsg.key = 15432;
                    rdrmsg.msgid = message_id.id;
                    rdrmsg.swipedate = now;


                    if (rdr == 1)
                    {
                        rdrmsg.inout = false;

                        applog.logtemp("Swipe Out :" + (value & 0x7fff).ToString(), now);
                    }
                    else
                    {
                        rdrmsg.inout = true;
                        applog.logtemp("Swipe In  :" + value.ToString(), now);
                    }
                    rdrCli.Send(rdrmsg);
                }


            }
            catch (Exception ex)
            {

                applog.logexcep("vSerialPort_DataReceived", ex.Message);
                return;
            }
            return;
            //MessageBox.Show(value.ToString());

        }


        public void readData(object sender, EventArgs e)
        {
            try
            {
                RxData = vSerialPort.ReadLine();

            }
            catch (Exception ex)
            {

                applog.logexcep("readData", ex.Message);
            }
            return;
        }




        ~serialPortM()
        {

            if (vSerialPort != null)
                if (vSerialPort.IsOpen)
                    vSerialPort.Close();
        }

        public bool disconnect()
        {

            if (vSerialPort.IsOpen)
            {
                vSerialPort.Close();
                return true;
            }
            else
                return false;
        }

        public string[] availableports
        {
            get { return portAvailable; }
        }
        public bool isconnected
        {
            get { return vSerialPort.IsOpen; }
        }

        public bool connect(string portName)
        {
            if (vSerialPort.IsOpen == false)
            {
                vSerialPort.PortName = portName;
                vSerialPort.ReadTimeout = 250; //Two hundred millisecond readline will wait
                vSerialPort.BaudRate = (int)vBaudRate;
                vSerialPort.DataBits = vDataBits;
                vSerialPort.StopBits = vStopBits;
                vSerialPort.Parity = vParity;
                vSerialPort.ReceivedBytesThreshold = 1;


                //Check if access control reader is present in the particular Port

                try
                {
                    vSerialPort.Open();

                    //Send data and confirm
                    if (vSerialPort.IsOpen)
                        return true;
                    else
                        return false;


                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    applog.logexcep("isAccessRdrpresent", e.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string getlist()
        {
            string comlist = "";

            foreach (string a in portAvailable)
            {
                comlist += a + "\r\n";
            }
            return comlist;
        }

    }
}
