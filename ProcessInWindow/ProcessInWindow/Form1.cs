using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace ProcessInWindow
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr Handle, int x, int y, int w, int h, bool repaint);

        private const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        MultiWindow mw = new MultiWindow();

        public Form1()
        {
            InitializeComponent();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            Process[] league = Process.GetProcessesByName("LolClient");
            Process p = league[0];
            p.WaitForInputIdle();
            SetParent(p.MainWindowHandle, panel1.Handle);

            Rectangle rect = new Rectangle();
            GetWindowRect(p.MainWindowHandle, out rect);
            //MoveWindow(p.MainWindowHandle, 0, 0, rect.Width, rect.Height, true);
            MoveWindow(p.MainWindowHandle, 0, 0, panel1.Width, panel1.Height, true);
            //ShowWindow(p.MainWindowHandle, SW_SHOWMAXIMIZED);
             * */

            mw.FindWindow("notepad", true);
            mw.FindWindow("WinRar", true);
            mw.LoadProcInControl("notepad", panel1);
            mw.LoadProcInControl("WinRar", panel2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //mw.CloseWindow("notepad");
            //mw.CloseWindow("iexplore");
            mw.LoadProcInExplorer("notepad");
            mw.LoadProcInExplorer("WinRar");
        }
    }
}
