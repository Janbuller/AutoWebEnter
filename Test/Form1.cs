using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        private bool Click = false;

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(ClickTest);
            aTimer.Interval = 500;
            aTimer.Enabled = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Click = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Click = false;
        }

        private void ClickTest(object sender, EventArgs e)
        {
            if (Click)
            {
                ActivateApp("msedge");
            }
        }

        void ActivateApp(string processName)
        {
            Process[] p = Process.GetProcessesByName(processName);

            // Activate the first application we find with this name
            if (p.Count() > 0)
                for (int i = 0; i < p.Length; i++)
                {
                    SetForegroundWindow(p[i].MainWindowHandle);
                    SendKeys.SendWait("{ENTER}");
                }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F8))
            {
                Click = false;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
