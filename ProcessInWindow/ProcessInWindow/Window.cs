using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessInWindow
{
    class Window
    {
        
        public Window(string name, IntPtr handle, int width, int height, int pid)
        {
            this.Name = name; this.Handle = handle; this.Width = width; this.Height = height; this.PID = pid;
            Loaded = false;
        }

        public string Name { get; set; }
        public IntPtr Handle { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int PID { get; set; }
        public bool Loaded { get; set; }
    }
}
