using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rust_crosshair_kylos
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    class CrosshairOverlay : Form
    {
        //kenankylos rust crosshair uygulaması
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int LWA_COLORKEY = 0x1;
        private const int WS_EX_TOPMOST = 0x00000008;

        public CrosshairOverlay()
        {
            
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true; 
            this.BackColor = Color.Black; 
            this.TransparencyKey = Color.Black; 
            this.WindowState = FormWindowState.Maximized; 

            
            int exStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, exStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOPMOST);
            SetLayeredWindowAttributes(this.Handle, (uint)Color.Black.ToArgb(), 0, LWA_COLORKEY);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            
            int centerX = Screen.PrimaryScreen.Bounds.Width / 2;
            int centerY = Screen.PrimaryScreen.Bounds.Height / 2;
            int lineLength = 10; 
            int thickness = 4;   

            
            Brush redBrush = new SolidBrush(Color.Red);

            
            g.FillRectangle(redBrush, centerX - (thickness / 2), centerY - lineLength - thickness, thickness, lineLength); 
            g.FillRectangle(redBrush, centerX - (thickness / 2), centerY + thickness, thickness, lineLength); 

            
            g.FillRectangle(redBrush, centerX - lineLength - thickness, centerY - (thickness / 2), lineLength, thickness); 
            g.FillRectangle(redBrush, centerX + thickness, centerY - (thickness / 2), lineLength, thickness); 
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new CrosshairOverlay());
        }
    }

}
