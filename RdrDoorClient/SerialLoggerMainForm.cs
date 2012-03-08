using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RdrDoorClient;

namespace SerialLogger
{
    public partial class SerialLogger_Mainform : Form
    {
        serialPortM sp = new serialPortM();
        //RdrClient vRdrDCli = new RdrClient("172.16.3.235");

        public SerialLogger_Mainform()
        {
            InitializeComponent();
        }

        private void SerialLogger_Mainform_Resize(object sender, EventArgs e)
        {
            Size csize = this.ClientSize;

            csize.Width -= 32;
            csize.Height -= 85;

            this.tbxConsole.Size = csize;


        }

        private void btnConnectDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (sp.isconnected)
                {
                    if (sp.disconnect())
                    {
                        btnConnectDisconnect.Text = "Connect";
                        RdrDoorClient.Properties.Settings.Default.IsCommConnected = false;
                        MessageBox.Show("COM port Disconnected");
                    }
                }
                else
                {
                    UInt32 baudrate = Convert.ToUInt32(cbBaudrate.Text);
                    sp.setportvalues(baudrate, 8, System.IO.Ports.StopBits.One, System.IO.Ports.Parity.None);
                    if (sp.connect(cbSelPort.SelectedItem.ToString()))
                    {
                        RdrDoorClient.Properties.Settings.Default.IsCommConnected = true;
                        
                        btnConnectDisconnect.Text = "Disconnect";
                        //MessageBox.Show("Connected");
                    }
                }
                RdrDoorClient.Properties.Settings.Default.Save();

            }
            catch (Exception ex)
            {
                applog.loggen(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void SerialLogger_Mainform_Load(object sender, EventArgs e)
        {

        foreach (string i in sp.availableports)
        cbSelPort.Items.Add(i);

        cBServerIP.Items.Add("172.16.3.235");
        cBServerIP.Items.Add("135.254.167.145");

        cbBaudrate.Items.Add("9600");
        cbBaudrate.Items.Add("19200");
        cbBaudrate.Items.Add("115200");


        if (RdrDoorClient.Properties.Settings.Default.IsSettingsAlreadyInitialised)
        {
            //MessageBox.Show("hihih");
            cbSelPort.SelectedItem = RdrDoorClient.Properties.Settings.Default.CommPort;
            cbBaudrate.SelectedItem = RdrDoorClient.Properties.Settings.Default.CommBaudrate.ToString();
            cBServerIP.SelectedItem = RdrDoorClient.Properties.Settings.Default.ServerIPAddr;

            if (RdrDoorClient.Properties.Settings.Default.IsCommConnected)
            {
                btnConnectDisconnect_Click(null, null);
            }

            chkbLockCtrl.Checked = RdrDoorClient.Properties.Settings.Default.LockControls;
            tbxDoorno.Text = RdrDoorClient.Properties.Settings.Default.DoorNo.ToString();
        }
        else
        {

            if (cbSelPort.Items.Count != 0)
            {
                cbSelPort.SelectedIndex = 0;
                RdrDoorClient.Properties.Settings.Default.CommPort = cbSelPort.SelectedItem.ToString();
            }

            if (cBServerIP.Items.Count != 0)
            {
                cBServerIP.SelectedIndex = 0;
                RdrDoorClient.Properties.Settings.Default.ServerIPAddr = cBServerIP.SelectedItem.ToString();
            }

            if (cbBaudrate.Items.Count != 0)
            {
                cbBaudrate.SelectedIndex = 0;
                RdrDoorClient.Properties.Settings.Default.CommBaudrate = Convert.ToUInt32(cbBaudrate.SelectedItem);
            }

            //RdrDoorClient.Properties.Settings.Default.DoorNo
            RdrDoorClient.Properties.Settings.Default.IsSettingsAlreadyInitialised = true;


            RdrDoorClient.Properties.Settings.Default.Save();

        }




        
        }

        private void btnPing_Click(object sender, EventArgs e)
        {
            sp.testsend(1);
            ;// AsynchronousClient.pingserver();
        }

        private void cBServerIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ipaddress = cBServerIP.SelectedItem.ToString();
            sp.ChangeServerIp(ipaddress);
            RdrDoorClient.Properties.Settings.Default.ServerIPAddr = ipaddress;
            RdrDoorClient.Properties.Settings.Default.Save();
        }



        private void tbxDoorno_TextChanged(object sender, EventArgs e)
        {
            int doorno;
            if (tbxDoorno.Text.Length != 0)
            {
                
                doorno = Convert.ToInt32(tbxDoorno.Text);

            }
            else
            {
                doorno = 0;
            }

            sp.set_doorno(doorno);
            RdrDoorClient.Properties.Settings.Default.DoorNo = doorno;
            RdrDoorClient.Properties.Settings.Default.Save();
            //MessageBox.Show("Textchanged");
        }

        private void chkbLockCtrl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbLockCtrl.CheckState == CheckState.Checked)
            {
                cbSelPort.Enabled = false;
                cBServerIP.Enabled = false;
                tbxDoorno.Enabled = false;
                btnConnectDisconnect.Enabled = false;
                btnPing.Enabled = false;
                cbBaudrate.Enabled = false;
                RdrDoorClient.Properties.Settings.Default.LockControls = true;
            }
            else
            {
                cbSelPort.Enabled = true;
                cBServerIP.Enabled = true;
                tbxDoorno.Enabled = true;
                btnConnectDisconnect.Enabled = true;
                btnPing.Enabled = true;
                cbBaudrate.Enabled = true;
                RdrDoorClient.Properties.Settings.Default.LockControls = false;
            }
            RdrDoorClient.Properties.Settings.Default.Save();
        }

        private void cbBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBaudrate.Text.Length > 0)
            {
                UInt32 baudrate=Convert.ToUInt32(cbBaudrate.Text);
                sp.setportvalues(baudrate, 8, System.IO.Ports.StopBits.One, System.IO.Ports.Parity.None);

                RdrDoorClient.Properties.Settings.Default.CommBaudrate = baudrate;
                RdrDoorClient.Properties.Settings.Default.Save();
            }
        }



        //private void tbxDoorno_Leave(object sender, EventArgs e)
        //{
        //    RdrDoorClient.Properties.Settings.Default.Save();
        //}


    }
}