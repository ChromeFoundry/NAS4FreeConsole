using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NAS4FreeConsole
{
    public partial class Messages : Form
    {
        #region Private Fields
        private const int maxMessages = 500;
        private List<LogMessage> messageCollection = new List<LogMessage>(maxMessages);
        #endregion

        #region Constructors
        public Messages()
        {
            InitializeComponent();
            lstMessages.Width = this.ClientSize.Width;
            btnCopy.Left = this.ClientSize.Width - btnCopy.Width - 2;
            this.Top = 10;
            this.Left = 10;
            lstMessages.ItemsChanged += new EventHandler(lstMessages_ItemsChanged);
        }
        #endregion

        #region Event Handlers
        private void Messages_Activated(object sender, EventArgs e)
        {
            Win32.SetWindowPos(Program.MainForm.Handle, this.Handle, 0, 0, 0, 0, Win32.SWP.NOMOVE | Win32.SWP.NOSIZE | Win32.SWP.NOACTIVATE);
        }

        protected void lstMessages_ItemsChanged(object sender, EventArgs e)
        {
            if (cbAutoScroll.Checked)
            {
                int visibleItems = lstMessages.ClientSize.Height / lstMessages.ItemHeight;
                lstMessages.TopIndex = Math.Max((lstMessages.Items.Count) - visibleItems, 0);
            }
        }

        protected void lstMessages_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;

            LogMessage item = lstMessages.Items[e.Index] as LogMessage;

            if (item != null)
            {
                lstMessages.DrawMode = DrawMode.OwnerDrawFixed;

                // Decide on a color
                Color c;
                switch (item.LogType)
                {
                    case LogType.Debug:
                        c = Color.DarkGreen;
                        break;
                    case LogType.Warn:
                        c = Color.OrangeRed;
                        break;
                    case LogType.Error:
                        c = Color.Maroon;
                        break;
                    default:
                        c = Color.Black;
                        break;
                }

                e.DrawBackground();
                using (Brush brush = new SolidBrush(c))
                {
                    e.Graphics.DrawString( item.FormatedMessage,
                                           lstMessages.Font,
                                           brush,
                                           0,
                                           e.Bounds.Top);
                }
                e.DrawFocusRectangle();
            }
        }

        public void OnMessage(object sender, LogMessage msg)
        {
            lock (messageCollection)
            {
                while (messageCollection.Count > maxMessages - 1)
                {
                    messageCollection.RemoveAt(0);
                }
                messageCollection.Add(msg);
            }

            bool display = false;
            switch (msg.LogType)
            {
                case LogType.Debug:
                    if (cbDebug.Checked)
                        display = true;
                    break;
                case LogType.Info:
                    if (cbInfo.Checked)
                        display = true;
                    break;
                case LogType.Warn:
                case LogType.Error:
                    if (cbError.Checked)
                        display = true;
                    break;
            }

            if (display)
            {
                while (lstMessages.Items.Count > maxMessages - 1)
                {
                    RemoveAtListItem(0);
                }
                AddListItem(msg);
            }
        }

        public delegate void AddListItemCallback(LogMessage msg);
        private void AddListItem(LogMessage msg)
        {
            if (lstMessages.InvokeRequired)
            {
                AddListItemCallback cb = new AddListItemCallback(AddListItem);
                this.Invoke(cb, new object[] { msg });
            }
            else
            {
                this.lstMessages.Items.Add(msg);
            }
        }

        public delegate void AddAllListItemsCallback(LogMessage[] msgs);
        private void AddAllListItems(LogMessage[] msgs)
        {
            if (lstMessages.InvokeRequired)
            {
                AddAllListItemsCallback cb = new AddAllListItemsCallback(AddAllListItems);
                this.Invoke(cb, new object[] { msgs });
            }
            else
            {
                this.lstMessages.BeginUpdate();
                foreach (LogMessage msg in msgs)
                    this.lstMessages.Items.Add(msg);
                lstMessages_ItemsChanged(null, null);
                this.lstMessages.EndUpdate();
            }
        }

        public delegate void RemoveAtListItemCallback(int i);
        private void RemoveAtListItem(int i)
        {
            if (lstMessages.InvokeRequired)
            {
                RemoveAtListItemCallback cb = new RemoveAtListItemCallback(RemoveAtListItem);
                this.Invoke(cb, new object[] { i });
            }
            else
            {
                this.lstMessages.Items.RemoveAt(i);
            }
        }

        public delegate void ClearListItemsCallback();
        private void ClearListItems()
        {
            if (lstMessages.InvokeRequired)
            {
                ClearListItemsCallback cb = new ClearListItemsCallback(ClearListItems);
                this.Invoke(cb, null);
            }
            else
            {
                this.lstMessages.Items.Clear();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(String.Empty);
            lock (messageCollection)
            {
                foreach (LogMessage msg in lstMessages.Items)
                {
                    sb.AppendLine(msg.FormatedMessage);
                }
            }
            try
            {
                Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
            }
            catch (System.Runtime.InteropServices.ExternalException) { }
            MessageBox.Show("Messages copied to Clipboard.", "Clipboard Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbAutoScroll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoScroll.Checked)
            {
                lstMessages_ItemsChanged(null, null);
            }
        }

        private void cbCheckedChanged(object sender, EventArgs e)
        {
            lock (messageCollection)
            {
                ClearListItems();
                List<LogMessage> list = new List<LogMessage>();
                foreach (LogMessage msg in messageCollection)
                {
                    if ((msg.LogType.Equals(LogType.Info) && cbInfo.Checked) ||
                        (msg.LogType.Equals(LogType.Debug) && cbDebug.Checked) ||
                        (msg.LogType.Equals(LogType.Warn) && cbError.Checked) ||
                        (msg.LogType.Equals(LogType.Error) && cbError.Checked))
                    {
                        list.Add(msg);
                    }
                }
                AddAllListItems(list.ToArray());
            }
        }
        #endregion
    }
}
