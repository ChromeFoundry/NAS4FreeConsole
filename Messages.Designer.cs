namespace NAS4FreeConsole
{
    partial class Messages
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
            this.cbInfo = new System.Windows.Forms.CheckBox();
            this.cbError = new System.Windows.Forms.CheckBox();
            this.cbDebug = new System.Windows.Forms.CheckBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.cbAutoScroll = new System.Windows.Forms.CheckBox();
            this.lstMessages = new NAS4FreeConsole.ListBoxEx();
            this.SuspendLayout();
            // 
            // cbInfo
            // 
            this.cbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbInfo.Checked = true;
            this.cbInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInfo.Location = new System.Drawing.Point(2, 240);
            this.cbInfo.Margin = new System.Windows.Forms.Padding(0);
            this.cbInfo.Name = "cbInfo";
            this.cbInfo.Size = new System.Drawing.Size(86, 17);
            this.cbInfo.TabIndex = 1;
            this.cbInfo.Text = "Informational";
            this.cbInfo.UseVisualStyleBackColor = true;
            this.cbInfo.CheckedChanged += new System.EventHandler(this.cbCheckedChanged);
            // 
            // cbError
            // 
            this.cbError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbError.Checked = true;
            this.cbError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbError.Location = new System.Drawing.Point(94, 240);
            this.cbError.Margin = new System.Windows.Forms.Padding(0);
            this.cbError.Name = "cbError";
            this.cbError.Size = new System.Drawing.Size(48, 17);
            this.cbError.TabIndex = 2;
            this.cbError.Text = "Error";
            this.cbError.UseVisualStyleBackColor = true;
            this.cbError.CheckedChanged += new System.EventHandler(this.cbCheckedChanged);
            // 
            // cbDebug
            // 
            this.cbDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDebug.Location = new System.Drawing.Point(148, 240);
            this.cbDebug.Margin = new System.Windows.Forms.Padding(0);
            this.cbDebug.Name = "cbDebug";
            this.cbDebug.Size = new System.Drawing.Size(58, 17);
            this.cbDebug.TabIndex = 3;
            this.cbDebug.Text = "Debug";
            this.cbDebug.UseVisualStyleBackColor = true;
            this.cbDebug.CheckedChanged += new System.EventHandler(this.cbCheckedChanged);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(359, 234);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(0);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 27);
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "Clipboard";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // cbAutoScroll
            // 
            this.cbAutoScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAutoScroll.Checked = true;
            this.cbAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoScroll.Location = new System.Drawing.Point(276, 240);
            this.cbAutoScroll.Margin = new System.Windows.Forms.Padding(0);
            this.cbAutoScroll.Name = "cbAutoScroll";
            this.cbAutoScroll.Size = new System.Drawing.Size(77, 17);
            this.cbAutoScroll.TabIndex = 4;
            this.cbAutoScroll.Text = "Auto-Scroll";
            this.cbAutoScroll.UseVisualStyleBackColor = true;
            this.cbAutoScroll.CheckedChanged += new System.EventHandler(this.cbAutoScroll_CheckedChanged);
            // 
            // lstMessages
            // 
            this.lstMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMessages.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.lstMessages.FormattingEnabled = true;
            this.lstMessages.ItemHeight = 12;
            this.lstMessages.Location = new System.Drawing.Point(0, 0);
            this.lstMessages.Margin = new System.Windows.Forms.Padding(0);
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(434, 232);
            this.lstMessages.TabIndex = 0;
            this.lstMessages.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstMessages_DrawItem);
            // 
            // Messages
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(438, 266);
            this.ControlBox = false;
            this.Controls.Add(this.cbAutoScroll);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.cbDebug);
            this.Controls.Add(this.cbError);
            this.Controls.Add(this.cbInfo);
            this.Controls.Add(this.lstMessages);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(454, 282);
            this.Name = "Messages";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NAS4Free Console - Message Log";
            this.Activated += new System.EventHandler(this.Messages_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private ListBoxEx lstMessages;
        private System.Windows.Forms.CheckBox cbInfo;
        private System.Windows.Forms.CheckBox cbError;
        private System.Windows.Forms.CheckBox cbDebug;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.CheckBox cbAutoScroll;
    }
}