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
        serialPortM sp= new serialPortM();
        //RdrClient vRdrDCli = new RdrClient("172.16.3.235");

        public SerialLogger_Mainform()
        {
            InitializeComponent();
        }

        private void SerialLogger_Mainform_Resize(object sender, EventArgs e)
        {
            Size csize = this.ClientSize;

            csize.Width -=32;
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
                        MessageBox.Show("COM port Disconnected");
                    }
                }

                else
                {
                    if (sp.connect(cbSelPort.SelectedItem.ToString()))
                    {
                        btnConnectDisconnect.Text = "Disconnect";
                        MessageBox.Show("Connected");
                    }
                }
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
        if(cbSelPort.Items.Count!=0)
        cbSelPort.SelectedIndex = 0;

        cBServerIP.Items.Add("172.16.3.235");
        cBServerIP.Items.Add("135.254.167.145");
        if (cBServerIP.Items.Count != 0)
            cBServerIP.SelectedIndex = 0;

        
        }

        private void btnPing_Click(object sender, EventArgs e)
        {
            sp.testsend(1);
            ;// AsynchronousClient.pingserver();
        }

        private void cBServerIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            sp.ChangeServerIp(cBServerIP.SelectedItem.ToString());
        }

        private void tbxDoorno_TextChanged(object sender, EventArgs e)
        {
            sp.set_doorno(Convert.ToInt32(tbxDoorno.Text));
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
                
            }
            else
            {
                cbSelPort.Enabled = true;
                cBServerIP.Enabled = true;
                tbxDoorno.Enabled = true;
                btnConnectDisconnect.Enabled = true;
                btnPing.Enabled = true;

            }
        }


    }
}