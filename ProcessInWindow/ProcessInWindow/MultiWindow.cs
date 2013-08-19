using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ProcessInWindow
{
    class MultiWindow
    {
        List<Window> windows = new List<Window>();

        public bool AddWindow(Window window)
        {
            if (windows.Contains(window))
            {
                return false;
            }
            else
            {
                windows.Add(window);
                return true;
            }
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        public Window FindWindow(string name)
        {
            Window tempWindow = null;

            Process[] processes = Process.GetProcessesByName(name);

            string wName = processes[0].ProcessName;
            IntPtr wHandle = processes[0].MainWindowHandle;
            
            Rectangle tempRect = new Rectangle();
            GetWindowRect(wHandle, out tempRect);
            
            int wWidth = tempRect.Width;
            int wHeight = tempRect.Height;

            tempWindow = new Window(wName, wHandle, wWidth, wHeight, processes[0].Id);

            return tempWindow;
        }

        public Window FindWindow(int pid)
        {
            Window tempWindow = null;

            Process process = Process.GetProcessById(pid);

            string wName = process.ProcessName;
            IntPtr wHandle = process.MainWindowHandle;

            Rectangle tempRect = new Rectangle();
            GetWindowRect(wHandle, out tempRect);

            int wWidth = tempRect.Width;
            int wHeight = tempRect.Height;

            tempWindow = new Window(wName, wHandle, wWidth, wHeight, process.Id);

            return tempWindow;
        }

        public Window FindWindow(string name, bool add)
        {
            if (add == true)
            {
                Window tempWindow = FindWindow(name);
                windows.Add(tempWindow);
                return tempWindow;
            }
            else
            {
                return GetWindow(name);
            }
        }

        public Window FindWindow(int pid, bool add)
        {
            if (add == true)
            {
                Window tempWindow = FindWindow(pid);
                windows.Add(tempWindow);
                return tempWindow;
            }
            else
            {
                Window tempWindow = FindWindow(pid);
                return tempWindow;
            }
        }

        public void CloseWindow(string name)
        {
            int id = GetWindow(name).PID;
            GetWindow(name).Loaded = false;
            Process tempProc = Process.GetProcessById(id);
            tempProc.Kill();
        }


        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr Handle, int x, int y, int w, int h, bool repaint);

        public void LoadProcInControl(string name, Panel control)
        {
            Window tempWindow = GetWindow(name);

            SetParent(tempWindow.Handle, control.Handle);
            MoveWindow(tempWindow.Handle, 0, 0, control.Width, control.Height, true);
            tempWindow.Loaded = true;
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetDesktopWindow();

        public void LoadProcInExplorer(string name)
        {
            //Process[] explorer = Process.GetProcessesByName("explorer");

            //IntPtr handle = explorer[0].MainWindowHandle;
            IntPtr handle = GetDesktopWindow();

            Window tempWindow = GetWindow(name);

            Rectangle rect = new Rectangle();
            GetWindowRect(tempWindow.Handle, out rect);

            SetParent(tempWindow.Handle, handle);
            //MoveWindow(tempWindow.Handle, 0, 0, rect.Width, rect.Height, true);
        }

        private Window GetWindow(string name)
        {
            foreach (Window tempWindow in windows)
            {
                if (tempWindow.Name == name)
                {
                    return tempWindow;
                }
            }
            return null;
        }
    }
}
