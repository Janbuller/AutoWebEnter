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

        System.Timers.Timer aTimer = new System.Timers.Timer();

        public Form1()
        {
            InitializeComponent();
            aTimer.Elapsed += new ElapsedEventHandler(ClickTest);
            aTimer.Interval = 10;
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
            foreach (Process Browser in p)
            {
                if (Browser.MainWindowHandle != IntPtr.Zero)
                {
                    SetForegroundWindow(Browser.MainWindowHandle);
                    SendKeys.SendWait("{ENTER}");
                    aTimer.Interval = 1200 + RandomNumber(-150, 150);
                }
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

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
