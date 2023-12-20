using System;
using System.Runtime.InteropServices;
using Fortnite.Sys.Structs;

namespace Fortnite.Sys
{
    public static class Dwmapi
    {
        [DllImport("dwmapi.dll", SetLastError = true)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMargins);
    }
}