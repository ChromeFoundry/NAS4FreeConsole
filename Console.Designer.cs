namespace NAS4FreeConsole
{
    partial class Console
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Console));
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHostname = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.Label();
            this.txtBuiltOn = new System.Windows.Forms.Label();
            this.txtOSVersion = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPlatform = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSystemTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtUptime = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCPUTemperature = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCPUFrequency = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCPUUsage = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pgCPUUsage = new System.Windows.Forms.ProgressBar();
            this.btnShutdown = new System.Windows.Forms.Button();
            this.btnReboot = new System.Windows.Forms.Button();
            this.pgMemoryUsage = new System.Windows.Forms.ProgressBar();
            this.txtMemoryUsage = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbRememberPW = new System.Windows.Forms.CheckBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.lblBuildDate = new System.Windows.Forms.Label();
            this.lstDiskUsage = new NAS4FreeConsole.ListViewEx();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUsage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSpace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbLogging = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(124, 6);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(97, 20);
            this.txtServer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server / IP Address :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Username :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(124, 32);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(97, 20);
            this.txtUsername.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(124, 58);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(97, 20);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(124, 127);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(83, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(43, 127);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 8;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(219, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Hostname :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(216, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Version :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(216, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Built on :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(216, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "OS Version :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtHostname
            // 
            this.txtHostname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHostname.AutoEllipsis = true;
            this.txtHostname.Location = new System.Drawing.Point(319, 5);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(353, 20);
            this.txtHostname.TabIndex = 12;
            this.txtHostname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.AutoEllipsis = true;
            this.txtVersion.Location = new System.Drawing.Point(319, 25);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(353, 20);
            this.txtVersion.TabIndex = 13;
            this.txtVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBuiltOn
            // 
            this.txtBuiltOn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuiltOn.AutoEllipsis = true;
            this.txtBuiltOn.Location = new System.Drawing.Point(319, 45);
            this.txtBuiltOn.Name = "txtBuiltOn";
            this.txtBuiltOn.Size = new System.Drawing.Size(353, 20);
            this.txtBuiltOn.TabIndex = 14;
            this.txtBuiltOn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOSVersion
            // 
            this.txtOSVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOSVersion.AutoEllipsis = true;
            this.txtOSVersion.Location = new System.Drawing.Point(319, 66);
            this.txtOSVersion.Name = "txtOSVersion";
            this.txtOSVersion.Size = new System.Drawing.Size(353, 20);
            this.txtOSVersion.TabIndex = 15;
            this.txtOSVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(216, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "Platform :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPlatform
            // 
            this.txtPlatform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlatform.AutoEllipsis = true;
            this.txtPlatform.Location = new System.Drawing.Point(319, 88);
            this.txtPlatform.Name = "txtPlatform";
            this.txtPlatform.Size = new System.Drawing.Size(353, 20);
            this.txtPlatform.TabIndex = 17;
            this.txtPlatform.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(216, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "System time :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSystemTime
            // 
            this.txtSystemTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSystemTime.AutoEllipsis = true;
            this.txtSystemTime.Location = new System.Drawing.Point(319, 108);
            this.txtSystemTime.Name = "txtSystemTime";
            this.txtSystemTime.Size = new System.Drawing.Size(353, 20);
            this.txtSystemTime.TabIndex = 19;
            this.txtSystemTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(216, 128);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 20);
            this.label10.TabIndex = 20;
            this.label10.Text = "Uptime :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUptime
            // 
            this.txtUptime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUptime.AutoEllipsis = true;
            this.txtUptime.Location = new System.Drawing.Point(319, 128);
            this.txtUptime.Name = "txtUptime";
            this.txtUptime.Size = new System.Drawing.Size(353, 20);
            this.txtUptime.TabIndex = 21;
            this.txtUptime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(124, 99);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(36, 20);
            this.txtPort.TabIndex = 7;
            this.txtPort.Text = "22";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(12, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 20);
            this.label11.TabIndex = 6;
            this.label11.Text = "Port :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCPUTemperature
            // 
            this.txtCPUTemperature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCPUTemperature.AutoEllipsis = true;
            this.txtCPUTemperature.Location = new System.Drawing.Point(319, 148);
            this.txtCPUTemperature.Name = "txtCPUTemperature";
            this.txtCPUTemperature.Size = new System.Drawing.Size(353, 20);
            this.txtCPUTemperature.TabIndex = 25;
            this.txtCPUTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(216, 148);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 20);
            this.label13.TabIndex = 24;
            this.label13.Text = "CPU Temp :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCPUFrequency
            // 
            this.txtCPUFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCPUFrequency.AutoEllipsis = true;
            this.txtCPUFrequency.Location = new System.Drawing.Point(319, 168);
            this.txtCPUFrequency.Name = "txtCPUFrequency";
            this.txtCPUFrequency.Size = new System.Drawing.Size(353, 20);
            this.txtCPUFrequency.TabIndex = 27;
            this.txtCPUFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(216, 168);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 20);
            this.label14.TabIndex = 26;
            this.label14.Text = "CPU Freq :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCPUUsage
            // 
            this.txtCPUUsage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCPUUsage.AutoEllipsis = true;
            this.txtCPUUsage.Location = new System.Drawing.Point(427, 188);
            this.txtCPUUsage.Name = "txtCPUUsage";
            this.txtCPUUsage.Size = new System.Drawing.Size(245, 20);
            this.txtCPUUsage.TabIndex = 29;
            this.txtCPUUsage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(216, 188);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(97, 20);
            this.label15.TabIndex = 28;
            this.label15.Text = "CPU Usage :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgCPUUsage
            // 
            this.pgCPUUsage.ForeColor = System.Drawing.Color.DodgerBlue;
            this.pgCPUUsage.Location = new System.Drawing.Point(319, 193);
            this.pgCPUUsage.Name = "pgCPUUsage";
            this.pgCPUUsage.Size = new System.Drawing.Size(101, 11);
            this.pgCPUUsage.Step = 1;
            this.pgCPUUsage.TabIndex = 30;
            this.pgCPUUsage.Visible = false;
            // 
            // btnShutdown
            // 
            this.btnShutdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShutdown.Enabled = false;
            this.btnShutdown.Location = new System.Drawing.Point(230, 331);
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.Size = new System.Drawing.Size(83, 23);
            this.btnShutdown.TabIndex = 11;
            this.btnShutdown.Text = "Shutdown";
            this.btnShutdown.UseVisualStyleBackColor = true;
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);
            // 
            // btnReboot
            // 
            this.btnReboot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReboot.Enabled = false;
            this.btnReboot.Location = new System.Drawing.Point(230, 302);
            this.btnReboot.Name = "btnReboot";
            this.btnReboot.Size = new System.Drawing.Size(83, 23);
            this.btnReboot.TabIndex = 10;
            this.btnReboot.Text = "Reboot";
            this.btnReboot.UseVisualStyleBackColor = true;
            this.btnReboot.Click += new System.EventHandler(this.btnReboot_Click);
            // 
            // pgMemoryUsage
            // 
            this.pgMemoryUsage.ForeColor = System.Drawing.Color.DodgerBlue;
            this.pgMemoryUsage.Location = new System.Drawing.Point(319, 213);
            this.pgMemoryUsage.Name = "pgMemoryUsage";
            this.pgMemoryUsage.Size = new System.Drawing.Size(101, 11);
            this.pgMemoryUsage.Step = 1;
            this.pgMemoryUsage.TabIndex = 36;
            this.pgMemoryUsage.Visible = false;
            // 
            // txtMemoryUsage
            // 
            this.txtMemoryUsage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemoryUsage.AutoEllipsis = true;
            this.txtMemoryUsage.Location = new System.Drawing.Point(427, 208);
            this.txtMemoryUsage.Name = "txtMemoryUsage";
            this.txtMemoryUsage.Size = new System.Drawing.Size(245, 20);
            this.txtMemoryUsage.TabIndex = 35;
            this.txtMemoryUsage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(216, 208);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(97, 20);
            this.label16.TabIndex = 34;
            this.label16.Text = "Memory Usage :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(216, 231);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 20);
            this.label12.TabIndex = 37;
            this.label12.Text = "Disk Usage :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbRememberPW
            // 
            this.cbRememberPW.AutoSize = true;
            this.cbRememberPW.Location = new System.Drawing.Point(96, 81);
            this.cbRememberPW.Name = "cbRememberPW";
            this.cbRememberPW.Size = new System.Drawing.Size(126, 17);
            this.cbRememberPW.TabIndex = 39;
            this.cbRememberPW.Text = "Remember Password";
            this.cbRememberPW.UseVisualStyleBackColor = true;
            // 
            // btnBackup
            // 
            this.btnBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBackup.Enabled = false;
            this.btnBackup.Location = new System.Drawing.Point(138, 302);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(83, 23);
            this.btnBackup.TabIndex = 12;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestore.Enabled = false;
            this.btnRestore.Location = new System.Drawing.Point(138, 331);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(83, 23);
            this.btnRestore.TabIndex = 13;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // lblBuildDate
            // 
            this.lblBuildDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBuildDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuildDate.Location = new System.Drawing.Point(4, 346);
            this.lblBuildDate.Name = "lblBuildDate";
            this.lblBuildDate.Size = new System.Drawing.Size(116, 11);
            this.lblBuildDate.TabIndex = 41;
            this.lblBuildDate.Text = "19700101.000000";
            // 
            // lstDiskUsage
            // 
            this.lstDiskUsage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDiskUsage.BackColor = System.Drawing.SystemColors.Control;
            this.lstDiskUsage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstDiskUsage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colUsage,
            this.colSpace});
            this.lstDiskUsage.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstDiskUsage.LabelWrap = false;
            this.lstDiskUsage.Location = new System.Drawing.Point(319, 231);
            this.lstDiskUsage.MultiSelect = false;
            this.lstDiskUsage.Name = "lstDiskUsage";
            this.lstDiskUsage.Size = new System.Drawing.Size(353, 123);
            this.lstDiskUsage.TabIndex = 38;
            this.lstDiskUsage.UseCompatibleStateImageBehavior = false;
            this.lstDiskUsage.View = System.Windows.Forms.View.Details;
            this.lstDiskUsage.Visible = false;
            // 
            // colName
            // 
            this.colName.Text = "";
            this.colName.Width = 65;
            // 
            // colUsage
            // 
            this.colUsage.Text = "Usage";
            this.colUsage.Width = 90;
            // 
            // colSpace
            // 
            this.colSpace.Text = "Space";
            this.colSpace.Width = 135;
            // 
            // cbLogging
            // 
            this.cbLogging.AutoSize = true;
            this.cbLogging.Location = new System.Drawing.Point(101, 171);
            this.cbLogging.Name = "cbLogging";
            this.cbLogging.Size = new System.Drawing.Size(120, 17);
            this.cbLogging.TabIndex = 14;
            this.cbLogging.Text = "Show Message Log";
            this.cbLogging.UseVisualStyleBackColor = true;
            this.cbLogging.CheckedChanged += new System.EventHandler(this.cbLogging_CheckedChanged);
            // 
            // Console
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 366);
            this.Controls.Add(this.cbLogging);
            this.Controls.Add(this.lblBuildDate);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.cbRememberPW);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lstDiskUsage);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.pgMemoryUsage);
            this.Controls.Add(this.txtMemoryUsage);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnReboot);
            this.Controls.Add(this.btnShutdown);
            this.Controls.Add(this.pgCPUUsage);
            this.Controls.Add(this.txtCPUUsage);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtCPUFrequency);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtCPUTemperature);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtUptime);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtSystemTime);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPlatform);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtOSVersion);
            this.Controls.Add(this.txtBuiltOn);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.txtHostname);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Console";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NAS4Free Console";
            this.Activated += new System.EventHandler(this.Console_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Console_FormClosing);
            this.Load += new System.EventHandler(this.Console_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label txtHostname;
        private System.Windows.Forms.Label txtVersion;
        private System.Windows.Forms.Label txtBuiltOn;
        private System.Windows.Forms.Label txtOSVersion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label txtPlatform;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label txtSystemTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label txtUptime;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label txtCPUTemperature;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label txtCPUFrequency;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label txtCPUUsage;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ProgressBar pgCPUUsage;
        private System.Windows.Forms.Button btnShutdown;
        private System.Windows.Forms.Button btnReboot;
        private System.Windows.Forms.ProgressBar pgMemoryUsage;
        private System.Windows.Forms.Label txtMemoryUsage;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private ListViewEx lstDiskUsage;
        private System.Windows.Forms.ColumnHeader colUsage;
        private System.Windows.Forms.ColumnHeader colSpace;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.CheckBox cbRememberPW;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Label lblBuildDate;
        private System.Windows.Forms.CheckBox cbLogging;
    }
}

