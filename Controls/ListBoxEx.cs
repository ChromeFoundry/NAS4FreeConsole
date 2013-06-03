using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NAS4FreeConsole
{
    public partial class ListBoxEx : ListBox
    {
        public ListBoxEx() { }

        private const int LB_ADDSTRING = 0x180;
        private const int LB_INSERTSTRING = 0x181;
        private const int LB_DELETESTRING = 0x182;
        private const int LB_RESETCONTENT = 0x184;

        public event EventHandler ItemsChanged = delegate { };
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == LB_ADDSTRING ||
                m.Msg == LB_INSERTSTRING ||
                m.Msg == LB_DELETESTRING ||
                m.Msg == LB_RESETCONTENT)
            {
                ItemsChanged(this, EventArgs.Empty);
            }
        }
    }
}
