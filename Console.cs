using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;

namespace NAS4FreeConsole
{
    /// <summary>
    /// NAS4Free Console for Windows .Net clients.
    /// </summary>
    public partial class Console : Form
    {
        #region Global Variables
        const string _regKey = @"SoftWare\NAS4free\Console";
        const string _encKey = @"!$)eerf4SAN%NAS4free($!";
        const string SecureStringExportHeader = "76492d1116743f0423413b16050a5345";
        private Client client;
        private ServerInfo si = null;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        #endregion

        #region Constructors
        public Console()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        private void Console_Load(object sender, EventArgs e)
        {
            Logger.LogMessage("NAS4free Console - Starting...", LogType.Info);
            Logger.LogMessage("Loading console Main form", LogType.Debug);
            Logger.LogMessage("Version: " + GetBuildDate(), LogType.Info);
            timer.Interval = 10000;
            timer.Enabled = false;
            timer.Tick += new EventHandler(AutoRefresh);
            ClearServerInfo();
            this.lblBuildDate.Text = "v " + GetBuildDate();
            this.txtServer.Text = ReadRegSZ("host");
            this.txtUsername.Text = ReadRegSZ("username");
            string savPort = ReadRegSZ("port");
            if (String.IsNullOrEmpty(savPort))
            {
                this.txtPort.Text = "22";
            }
            else
            {
                this.txtPort.Text = savPort;
            }
            string encpw = ReadRegSZ("password");
            if (!String.IsNullOrEmpty(encpw))
            {
                string decpw = GetDecString(encpw, GetKey(_encKey));
                this.txtPassword.Text = decpw;
                cbRememberPW.Checked = true;
            }
            else
            {
                this.txtPassword.Text = String.Empty;
                cbRememberPW.Checked = false;
            }
        }

        private void Console_Activated(object sender, EventArgs e)
        {
            if (cbLogging.Checked)
            {
                Win32.SetWindowPos(Program.MessageLog.Handle, this.Handle, 0, 0, 0, 0, Win32.SWP.NOMOVE | Win32.SWP.NOSIZE | Win32.SWP.NOACTIVATE);
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim().Length == 0)
            {
                cbRememberPW.Checked = false;
                cbRememberPW.Enabled = false;
                return;
            }
            cbRememberPW.Enabled = true;
        }

        private void AutoRefresh(object obj, System.EventArgs e)
        {
            timer.Enabled = false;
            RefreshServerInfo();
            UpdateLogonControlState();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            int port;
            this.txtServer.Text = this.txtServer.Text.Trim();
            this.txtUsername.Text = this.txtUsername.Text.Trim();
            this.txtPassword.Text = this.txtPassword.Text.Trim();
            this.txtPort.Text = this.txtPort.Text.Trim();
            if (this.txtServer.Text.Length == 0) { MessageBox.Show("Server or IP Address is a required field.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.txtServer.Focus();  return; }
            if (Uri.CheckHostName(this.txtServer.Text) == UriHostNameType.Unknown) { MessageBox.Show("Invalid Server or IP Address.", "Connection Error", MessageBoxButtons.OK); this.txtServer.Focus(); return; }
            if (this.txtUsername.Text.Length == 0) { MessageBox.Show("Username is a required field.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.txtUsername.Focus();  return; }
            if (!int.TryParse(this.txtPort.Text, out port)) { MessageBox.Show("Invalid port number specified.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error); txtPort.Focus(); return; }

            Logger.LogMessage("Connecting: " + txtServer.Text + ":" + port.ToString(), LogType.Info);

            SetControlPropertyThreadSafe(this.btnConnect, "Enabled", false);
            SetControlPropertyThreadSafe(this.btnConnect, "Text", "Connecting");
            SetControlPropertyThreadSafe(this.txtServer, "Enabled", false);
            SetControlPropertyThreadSafe(this.txtUsername, "Enabled", false);
            SetControlPropertyThreadSafe(this.txtPassword, "Enabled", false);
            SetControlPropertyThreadSafe(this.txtPort, "Enabled", false);

            client = new Client(txtServer.Text, port, txtUsername.Text, txtPassword.Text);
            if (client.IsConnected)
            {
                Logger.LogMessage("Connection established", LogType.Debug);
                WriteRegSZ("host", txtServer.Text);
                WriteRegSZ("username", txtUsername.Text);
                WriteRegSZ("port", txtPort.Text);
                if (cbRememberPW.Checked)
                {
                    WriteRegSZ("password", GetEncString(txtPassword.Text, GetKey(_encKey)));
                }
                else
                {
                    WriteRegSZ("password", null);
                }
                this.si = new ServerInfo(client, 10);
                si.UpdateCompleted += this.PopulateServerInfo;

                UpdateLogonControlState();
                RefreshServerInfo();
                timer.Enabled = true;
            }
            else
            {
                Logger.LogMessage("Connection failed", LogType.Warn);
                MessageBox.Show("Cannot connect to server with specified settings.", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateLogonControlState();
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Logger.LogMessage("Disconnecting server connection", LogType.Info);
            if (si != null)
            {
                this.si.Dispose();
            }
            client.Dispose();
            ClearServerInfo();
            UpdateLogonControlState();
            btnConnect.Focus();
            this.si = null;
            Logger.LogMessage("Disconnected", LogType.Debug);
        }

        private void cbLogging_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLogging.Checked)
            {
                Program.MessageLog.Show();
                Console_Activated(null, null);
            }
            else
                Program.MessageLog.Hide();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            Logger.LogMessage("Begin backup server configuration", LogType.Info);
            if (client != null && client.IsConnected && si != null)
            {
                si.Refresh_ConfigXML();
                if (si.ConfigXML.Length > 0)
                {
                    string config = si.ConfigXML;
                    bool success = false;
                    using (SaveFileDialog save = new SaveFileDialog())
                    {
                        save.Filter = "Config File|*.xml";
                        save.Title = "Save Configuration Backup";
                        save.AddExtension = true;
                        save.AutoUpgradeEnabled = true;
                        save.CheckPathExists = true;
                        save.DefaultExt = "xml";
                        save.ValidateNames = true;
                        save.OverwritePrompt = true;
                        save.RestoreDirectory = true;
                        save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        save.FileName = "Config-" + si.HostName + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml";
                        if (save.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(save.FileName))
                        {
                            using (Stream stream = save.OpenFile())
                            {
                                if (stream != null)
                                {
                                    using (StreamWriter sw = new StreamWriter(stream))
                                    {
                                        sw.Write(config);
                                        success = true;
                                    }
                                }
                                else
                                {
                                    Logger.LogMessage("Backup failed, path or access fault", LogType.Error);
                                    MessageBox.Show("Unable to create backup configuration file.", "Path or Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            if (success)
                            {
                                Logger.LogMessage("Backup file created", LogType.Info);
                                MessageBox.Show("Backup file saved.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            else
            {
                Logger.LogMessage("Backup failed, connectivity fault", LogType.Error);
                MessageBox.Show("Unable to read configuration. Check connectivity.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Logger.LogMessage("Exiting backup server configuration", LogType.Debug);
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            Logger.LogMessage("Begin restore server configuration", LogType.Info);
            if (client != null && client.IsConnected && si != null)
            {
                si.Refresh_ConfigXML();
                if (si.ConfigXML.Length > 0)
                {
                    string config = String.Empty;
                    using (OpenFileDialog open = new OpenFileDialog())
                    {
                        open.Filter = "Config File|*.xml";
                        open.Title = "Restore Configuration Backup";
                        open.AddExtension = true;
                        open.AutoUpgradeEnabled = true;
                        open.CheckPathExists = true;
                        open.DefaultExt = "xml";
                        open.ValidateNames = true;
                        open.RestoreDirectory = true;
                        open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        if (open.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(open.FileName))
                        {
                            FileInfo fileinfo = new FileInfo(open.FileName);
                            if (DialogResult.Yes == MessageBox.Show("Upload config and Reboot server?", "Confirm Reboot", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                            {
                                if (si.Update_ConfigXML(fileinfo))
                                {
                                    Logger.LogMessage("Restore configuration was successful", LogType.Info);
                                    client.RebootServer();
                                    client.Dispose();
                                    ClearServerInfo();
                                    UpdateLogonControlState();
                                    btnConnect.Focus();
                                    Logger.LogMessage("Exiting restore server configuration", LogType.Debug);
                                    return;
                                }
                                Logger.LogMessage("Restore failed to process", LogType.Error);
                                MessageBox.Show("Unable to copy configuration to server.", "File Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                Logger.LogMessage("Restore failed, connectivity fault", LogType.Error);
                MessageBox.Show("Unable to communicate with server. Check connectivity.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnReboot_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to reboot the server?", "Reboot Server", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Logger.LogMessage("Begin reboot server", LogType.Info);
                client.RebootServer();
                client.Dispose();
                ClearServerInfo();
                UpdateLogonControlState();
                btnConnect.Focus();
            }
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to shutdown the server?", "Shutdown Server", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Logger.LogMessage("Begin shutdown server", LogType.Info);
                client.ShutdownServer();
                client.Dispose();
                ClearServerInfo();
                UpdateLogonControlState();
                btnConnect.Focus();
            }
        }

        private void Console_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.LogMessage("Closing console form", LogType.Info);
            timer.Enabled = false;
            if (si != null)
            {
                si.Dispose();
                int counter = 0;
                while (si.IsUpdating)
                {
                    if (counter > 10) break;
                    System.Threading.Thread.Sleep(100);
                    counter++;
                }
            }
        }
        #endregion

        #region Methods
        private void ClearServerInfo()
        {
            Logger.LogMessage("Begin ClearServerInfo method", LogType.Debug);
            lock (this)
            {
                SetControlPropertyThreadSafe(this.txtHostname, "Text", String.Empty);
                SetControlPropertyThreadSafe(this.txtVersion, "Text", String.Empty);
                SetControlPropertyThreadSafe(this.txtBuiltOn, "Text", String.Empty);
                SetControlPropertyThreadSafe(this.txtOSVersion, "Text", String.Empty);
                SetControlPropertyThreadSafe(this.txtPlatform, "Text", String.Empty);
                SetControlPropertyThreadSafe(this.txtSystemTime, "Text", String.Empty);
                SetControlPropertyThreadSafe(this.txtUptime, "Text", String.Empty);
                SetControlPropertyThreadSafe(this.txtCPUTemperature, "Text", String.Empty);
                SetControlPropertyThreadSafe(this.txtCPUFrequency, "Text", String.Empty);
                SetControlPropertyThreadSafe(this.pgCPUUsage, "Value", 0);
                SetControlPropertyThreadSafe(this.txtCPUUsage, "Text", String.Empty);
                SetControlPropertyThreadSafe(this.pgMemoryUsage, "Value", 0);
                SetControlPropertyThreadSafe(this.txtMemoryUsage, "Text", String.Empty);
                BuildDiskListItems(this.lstDiskUsage, null);
            }
            Logger.LogMessage("End ClearServerInfo method", LogType.Debug);
        }

        public delegate void UpdateServerInfoCallback();
        private void RefreshServerInfo()
        {
            if (si != null && !si._isUpdating)
            {
                Thread thread = new Thread(new ThreadStart(si.Refresh));
                thread.Start();
            }
        }
        protected void PopulateServerInfo()
        {
            if (si != null)
            {
                Logger.LogMessage("Begin PopulateServerInfo method", LogType.Debug);
                try
                {
                    SetControlPropertyThreadSafe(this.txtHostname, "Text", si.HostName);
                    SetControlPropertyThreadSafe(this.txtVersion, "Text", si.ProductVersion);
                    SetControlPropertyThreadSafe(this.txtBuiltOn, "Text", si.BuildTime);
                    SetControlPropertyThreadSafe(this.txtOSVersion, "Text", si.OSVersion);
                    SetControlPropertyThreadSafe(this.txtPlatform, "Text", si.Platform);
                    SetControlPropertyThreadSafe(this.txtSystemTime, "Text", si.SystemDate);
                    SetControlPropertyThreadSafe(this.txtUptime, "Text", si.Uptime);
                    SetControlPropertyThreadSafe(this.txtCPUTemperature, "Text", si.CPUTemperatureString);
                    SetControlPropertyThreadSafe(this.txtCPUFrequency, "Text", si.CPUFrequencyString);
                    SetControlPropertyThreadSafe(this.pgCPUUsage, "Value", si.CPUUsage);
                    SetControlPropertyThreadSafe(this.txtCPUUsage, "Text", si.CPUUsageString);
                    SetControlPropertyThreadSafe(this.pgMemoryUsage, "Value", si.RAMUsage);
                    SetControlPropertyThreadSafe(this.txtMemoryUsage, "Text", si.RAMUsageString);
                    if (si.DiskUsages.Count > 0)
                    {
                        BuildDiskListItems(this.lstDiskUsage, si.DiskUsages);
                    }
                    else
                    {
                        BuildDiskListItems(this.lstDiskUsage, null);
                    }
                }
                catch (Renci.SshNet.Common.SshAuthenticationException)
                {
                    Logger.LogMessage("Cannot connect to server: SshAuthenticationException", LogType.Error);
                    MessageBox.Show("Invalid credentials!", "Unable to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txtUsername.Focus();
                }
                catch (System.Net.Sockets.SocketException)
                {
                    Logger.LogMessage("Cannot connect to server: SocketException", LogType.Error);
                    MessageBox.Show("Bad Server Name or IP Address.", "Unable to Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txtServer.Focus();
                }
                catch (NullReferenceException)
                {
                    Logger.LogMessage("PopulateServerInfo caught NullReferenceException", LogType.Error);
                    // continue
                }
                Logger.LogMessage("End PopulateServerInfo method", LogType.Debug);
            }
        }

        private void UpdateLogonControlState()
        {
            Logger.LogMessage("Begin UpdateLogonControlState method", LogType.Debug);
            SetControlPropertyThreadSafe(this.btnDisconnect, "Enabled", client.IsConnected);
            SetControlPropertyThreadSafe(this.btnReboot, "Enabled", client.IsConnected);
            SetControlPropertyThreadSafe(this.btnShutdown, "Enabled", client.IsConnected);
            SetControlPropertyThreadSafe(this.btnBackup, "Enabled", client.IsConnected);
            SetControlPropertyThreadSafe(this.btnRestore, "Enabled", client.IsConnected);
            SetControlPropertyThreadSafe(this.btnConnect, "Enabled", !client.IsConnected);
            SetControlPropertyThreadSafe(this.btnConnect, "Text", client.IsConnected ? "Connected" : "Connect");
            SetControlPropertyThreadSafe(this.txtServer, "Enabled", !client.IsConnected);
            SetControlPropertyThreadSafe(this.txtUsername, "Enabled", !client.IsConnected);
            SetControlPropertyThreadSafe(this.txtPassword, "Enabled", !client.IsConnected);
            SetControlPropertyThreadSafe(this.txtPort, "Enabled", !client.IsConnected);
            SetControlPropertyThreadSafe(this.pgCPUUsage, "Visible", client.IsConnected);
            SetControlPropertyThreadSafe(this.pgMemoryUsage, "Visible", client.IsConnected);
            SetControlPropertyThreadSafe(this.lstDiskUsage, "Visible", client.IsConnected);

            timer.Enabled = client.IsConnected;
            Logger.LogMessage("End UpdateLogonControlState method", LogType.Debug);
        }

        delegate void BuildDiskListItemsDelegate(ListViewEx owner, List<DiskUsage> disks);
        private void BuildDiskListItems(ListViewEx owner, List<DiskUsage> disks)
        {
            if (owner.InvokeRequired)
            {
                if (owner.IsHandleCreated)
                {
                    try
                    {
                        owner.BeginInvoke(new BuildDiskListItemsDelegate(BuildDiskListItems), owner, disks);
                    }
                    catch { }
                }
                return;
            }
            else
            {
                if (!owner.IsDisposed)
                {
                    try
                    {
                        owner.RemoveAllEmbeddedControls();
                        owner.Items.Clear();
                        if (disks != null)
                        {
                            lock (disks)
                            {
                                int curRow = 0;
                                foreach (DiskUsage disk in disks)
                                {
                                    Logger.LogMessage("Adding Disk Usage: " + disk.Name, LogType.Debug);
                                    ListViewItem listViewItem = new ListViewItem("Item" + curRow.ToString());
                                    owner.AddListViewItem(listViewItem);

                                    Label item1 = new Label()
                                    {
                                        Name = "Label_Name" + curRow.ToString(),
                                        TextAlign = ContentAlignment.MiddleLeft,
                                        Padding = new System.Windows.Forms.Padding(0,0,0,0),
                                        Margin = new System.Windows.Forms.Padding(0,0,0,0),
                                        Width = owner.Columns[0].Width,
                                        FlatStyle = System.Windows.Forms.FlatStyle.System,
                                        AutoSize = false,
                                        Text = disk.Name
                                    };
                                    item1.Font = new Font(item1.Font, FontStyle.Bold);
                                    FitToWidth(item1, 3.0F);
                                    owner.AddEmbeddedControl(item1, 0, curRow);
                                    owner.AddEmbeddedControl(new ProgressBar()
                                    {
                                        Name = "Progress_Usage" + curRow.ToString(),
                                        Size = new System.Drawing.Size(owner.Columns[1].Width, 11),
                                        Step = 1,
                                        Minimum = 0,
                                        Maximum = 100,
                                        Value = disk.PercentUsed
                                    }, 1, curRow, DockStyle.None);
                                    owner.AddEmbeddedControl(new Label()
                                    {
                                        Name = "Label_Usage" + curRow.ToString(),
                                        TextAlign = ContentAlignment.MiddleLeft,
                                        Padding = new System.Windows.Forms.Padding(0,0,0,0),
                                        Margin = new System.Windows.Forms.Padding(0,0,0,0),
                                        AutoSize = true,
                                        Text = String.Format("{0}% of {2}; {1} Free{3}", disk.PercentUsed, disk.FreeSpace, disk.TotalSpace, (String.IsNullOrEmpty(disk.PoolStatus) ? String.Empty : ", " + disk.PoolStatus))
                                    }, 2, curRow);
                                    curRow++;
                                }
                            }
                        }
                        owner.Columns[2].Width = -2;
                    }
                    catch { }
                }
            }
        }

        private void FitToWidth(Label ctrl, float maxAdjust)
        {
            Font retFont = (Font)ctrl.Font.Clone();
            float minSize = retFont.Size - maxAdjust;
            using (Graphics graphics = ctrl.CreateGraphics())
            {
                float textWidth = graphics.MeasureString(ctrl.Text, retFont).Width;
                while (textWidth > (float)ctrl.Width && retFont.Size > minSize)
                {
                    retFont = new Font(retFont.Name, (retFont.Size - 0.1F));
                    textWidth = graphics.MeasureString(ctrl.Text, retFont).Width;
                }
                ctrl.Font = retFont;
            }
        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);
        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            if (control.InvokeRequired)
            {
                if (control.IsHandleCreated)
                {
                    try
                    {
                        control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
                    }
                    catch { }
                }
            }
            else
            {
                if (!control.IsDisposed)
                {
                    try
                    {
                        control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
                    }
                    catch { }
                }
            }
        }

        private string ReadRegSZ(string regValueName)
        {
            string retVal = String.Empty;
            try
            {
                using (RegistryKey root = Registry.CurrentUser.OpenSubKey(_regKey, false))
                {
                    if (root != null)
                    {
                        retVal = root.GetValue(regValueName, String.Empty).ToString();
                    }
                }
            }
            catch { }
            return retVal;
        }
        private void WriteRegSZ(string regValueName, string value)
        {
            try
            {
                using(RegistryKey root = Registry.CurrentUser.CreateSubKey(_regKey))
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        root.DeleteValue(regValueName, false);
                        return;
                    }
                    root.SetValue(regValueName, value, RegistryValueKind.String);
                }
            }
            catch { }
        }

        private byte[] GetKey(string key)
        {
            if (key.Length > 32) 
            { 
                key = key.Substring(0,32);
            }
            Encoding encoding = new System.Text.ASCIIEncoding();
	        byte[] returnVal = encoding.GetBytes(key.PadRight(32, '0'));
	        return returnVal;
        }
        private string GetEncString(string value, byte[] key)
        {
            if ((value.Length < 1) || (value.Length > 65536)) 
            {
		        throw new ArgumentOutOfRangeException("value", "Value size is out of range."); 
            }
            string valueEncrypted = String.Empty;
	        SecureString secureString = new SecureString();
	        foreach (char c in (value.ToCharArray())) 
            {
		        secureString.AppendChar(c);
            }
            SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create();   
            ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor(key, symmetricAlgorithm.IV);
		    MemoryStream memoryStream = new MemoryStream();
		    CryptoStream cryptoStream2;
		    CryptoStream cryptoStream = cryptoStream2 = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
		    try
		    {
                byte[] data = new byte[secureString.Length * 2];
				if (secureString.Length > 0)
				{
					IntPtr intPtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
					try
					{
						Marshal.Copy(intPtr, data, 0, data.Length);
					}
					finally
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
			    cryptoStream.Write(data, 0, data.Length);
			    cryptoStream.FlushFinalBlock();
			    Array.Clear(data, 0, data.Length);
			    byte[] data2 = memoryStream.ToArray();
                StringBuilder stringBuilder = new StringBuilder();
		        for (int i = 0; i < data2.Length; i++)
		        {
			        stringBuilder.Append(data2[i].ToString("x2", CultureInfo.InvariantCulture));
		        }
		        String encryptedData = stringBuilder.ToString();
                String IV = Convert.ToBase64String(symmetricAlgorithm.IV);

                string s = string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}", new object[] {2,IV,encryptedData});
				byte[] bytes = Encoding.Unicode.GetBytes(s);
				valueEncrypted = SecureStringExportHeader + Convert.ToBase64String(bytes);
		    }
		    finally
		    {
			    if (cryptoStream2 != null)
			    {
				    ((IDisposable)cryptoStream2).Dispose();
			    }
		    }
	        return valueEncrypted;
        }
        private string GetDecString(string value, byte[] key)
        {
	        if ((value.Length < 1) || (value.Length > 65536)) 
            {
		        throw new ArgumentOutOfRangeException("value", "Value size is out of range.");
            }
	        string valueDecrypted = String.Empty;

	        SecureString secureString = null;
            string input = value;
            byte[] iV = null;
            if (value.IndexOf(SecureStringExportHeader, StringComparison.OrdinalIgnoreCase) == 0)
            {
                try
                {
                    string text = value.Substring(SecureStringExportHeader.Length, value.Length - SecureStringExportHeader.Length);
                    byte[] bytes = Convert.FromBase64String(text);
                    string @string = Encoding.Unicode.GetString(bytes);
                    string[] sAry = @string.Split(new char[] { '|' });
                    if (sAry.Length == 3)
                    {
                        input = sAry[2];
                        iV = Convert.FromBase64String(sAry[1]);
                    }
                }
                catch (FormatException)
                {
                    input = value;
                    iV = null;
                }
            }
            SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create();
            int num = input.Length / 2;
            byte[] array = new byte[num];
            if (input.Length > 0)
            {
                for (int i = 0; i < num; i++)
                {
                    string t = input.Substring(2 * i, 2);
                    byte b = byte.Parse(t, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                    array[i] = b;
                }
            }
            ICryptoTransform transform;
            transform = symmetricAlgorithm.CreateDecryptor(key, iV);
            MemoryStream stream = new MemoryStream(array);
            using (CryptoStream cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Read))
            {
                byte[] array3 = new byte[array.Length];
                int num2 = cryptoStream.Read(array3, 0, array3.Length);
                byte[] array4 = new byte[num2];
                for (int i = 0; i < num2; i++)
                {
                    array4[i] = array3[i];
                }
                SecureString sS = new SecureString();
                int num3 = array4.Length / 2;
                for (int i = 0; i < num3; i++)
                {
                    char c = (char)((int)array4[2 * i + 1] * 256 + (int)array4[2 * i]);
                    sS.AppendChar(c);
                    array4[2 * i] = 0;
                    array4[2 * i + 1] = 0;
                }
                Array.Clear(array4, 0, array4.Length);
                Array.Clear(array3, 0, array3.Length);
                secureString = sS;
            }
	        valueDecrypted = Marshal.PtrToStringAuto(Marshal.SecureStringToBSTR(secureString));
	        return	valueDecrypted;
        }

        private String GetBuildDate()
        {
            DateTime dt = RetrieveLinkerTimestamp();
            return dt.ToString("yyyyMMdd.HHmmss");
        }
        private DateTime RetrieveLinkerTimestamp()
        {
            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }
        #endregion
    }
}
