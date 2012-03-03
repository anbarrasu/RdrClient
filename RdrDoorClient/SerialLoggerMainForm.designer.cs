namespace SerialLogger
{
    partial class SerialLogger_Mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbSelPort = new System.Windows.Forms.ComboBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.btnConnectDisconnect = new System.Windows.Forms.Button();
            this.tbxConsole = new System.Windows.Forms.TextBox();
            this.btnPing = new System.Windows.Forms.Button();
            this.lblServerIPAddress = new System.Windows.Forms.Label();
            this.cBServerIP = new System.Windows.Forms.ComboBox();
            this.lblDoorno = new System.Windows.Forms.Label();
            this.tbxDoorno = new System.Windows.Forms.TextBox();
            this.chkbLockCtrl = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbSelPort
            // 
            this.cbSelPort.FormattingEnabled = true;
            this.cbSelPort.Location = new System.Drawing.Point(45, 12);
            this.cbSelPort.Name = "cbSelPort";
            this.cbSelPort.Size = new System.Drawing.Size(121, 21);
            this.cbSelPort.TabIndex = 0;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(14, 20);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 13);
            this.lblPort.TabIndex = 1;
            this.lblPort.Text = "Port:";
            // 
            // btnConnectDisconnect
            // 
            this.btnConnectDisconnect.Location = new System.Drawing.Point(189, 10);
            this.btnConnectDisconnect.Name = "btnConnectDisconnect";
            this.btnConnectDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnectDisconnect.TabIndex = 2;
            this.btnConnectDisconnect.Text = "Connect";
            this.btnConnectDisconnect.UseVisualStyleBackColor = true;
            this.btnConnectDisconnect.Click += new System.EventHandler(this.btnConnectDisconnect_Click);
            // 
            // tbxConsole
            // 
            this.tbxConsole.Enabled = false;
            this.tbxConsole.Location = new System.Drawing.Point(14, 94);
            this.tbxConsole.Multiline = true;
            this.tbxConsole.Name = "tbxConsole";
            this.tbxConsole.Size = new System.Drawing.Size(792, 337);
            this.tbxConsole.TabIndex = 3;
            // 
            // btnPing
            // 
            this.btnPing.Location = new System.Drawing.Point(540, 10);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(75, 23);
            this.btnPing.TabIndex = 4;
            this.btnPing.Text = "Ping";
            this.btnPing.UseVisualStyleBackColor = true;
            this.btnPing.Click += new System.EventHandler(this.btnPing_Click);
            // 
            // lblServerIPAddress
            // 
            this.lblServerIPAddress.AutoSize = true;
            this.lblServerIPAddress.Location = new System.Drawing.Point(291, 20);
            this.lblServerIPAddress.Name = "lblServerIPAddress";
            this.lblServerIPAddress.Size = new System.Drawing.Size(95, 13);
            this.lblServerIPAddress.TabIndex = 6;
            this.lblServerIPAddress.Text = "Server IP Address:";
            // 
            // cBServerIP
            // 
            this.cBServerIP.FormattingEnabled = true;
            this.cBServerIP.Location = new System.Drawing.Point(392, 12);
            this.cBServerIP.Name = "cBServerIP";
            this.cBServerIP.Size = new System.Drawing.Size(121, 21);
            this.cBServerIP.TabIndex = 5;
            this.cBServerIP.SelectedIndexChanged += new System.EventHandler(this.cBServerIP_SelectedIndexChanged);
            // 
            // lblDoorno
            // 
            this.lblDoorno.AutoSize = true;
            this.lblDoorno.Location = new System.Drawing.Point(14, 56);
            this.lblDoorno.Name = "lblDoorno";
            this.lblDoorno.Size = new System.Drawing.Size(50, 13);
            this.lblDoorno.TabIndex = 7;
            this.lblDoorno.Text = "Door No:";
            // 
            // tbxDoorno
            // 
            this.tbxDoorno.Location = new System.Drawing.Point(70, 53);
            this.tbxDoorno.Name = "tbxDoorno";
            this.tbxDoorno.Size = new System.Drawing.Size(96, 20);
            this.tbxDoorno.TabIndex = 8;
            this.tbxDoorno.TextChanged += new System.EventHandler(this.tbxDoorno_TextChanged);
            //this.tbxDoorno.Leave += new System.EventHandler(this.tbxDoorno_Leave);
            // 
            // chkbLockCtrl
            // 
            this.chkbLockCtrl.AutoSize = true;
            this.chkbLockCtrl.Location = new System.Drawing.Point(189, 55);
            this.chkbLockCtrl.Name = "chkbLockCtrl";
            this.chkbLockCtrl.Size = new System.Drawing.Size(91, 17);
            this.chkbLockCtrl.TabIndex = 9;
            this.chkbLockCtrl.Text = "Lock Controls";
            this.chkbLockCtrl.UseVisualStyleBackColor = true;
            this.chkbLockCtrl.CheckedChanged += new System.EventHandler(this.chkbLockCtrl_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(294, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 49);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LogSetting";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(76, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(89, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Include Error ";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // SerialLogger_Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 443);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkbLockCtrl);
            this.Controls.Add(this.tbxDoorno);
            this.Controls.Add(this.lblDoorno);
            this.Controls.Add(this.lblServerIPAddress);
            this.Controls.Add(this.cBServerIP);
            this.Controls.Add(this.btnPing);
            this.Controls.Add(this.tbxConsole);
            this.Controls.Add(this.btnConnectDisconnect);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.cbSelPort);
            this.Name = "SerialLogger_Mainform";
            this.Text = "Serial Logger with Time";
            this.Load += new System.EventHandler(this.SerialLogger_Mainform_Load);
            this.Resize += new System.EventHandler(this.SerialLogger_Mainform_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSelPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button btnConnectDisconnect;
        private System.Windows.Forms.TextBox tbxConsole;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.Label lblServerIPAddress;
        private System.Windows.Forms.ComboBox cBServerIP;
        private System.Windows.Forms.Label lblDoorno;
        private System.Windows.Forms.TextBox tbxDoorno;
        private System.Windows.Forms.CheckBox chkbLockCtrl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

