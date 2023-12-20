using System.Runtime.InteropServices;

namespace Fortnite.Sys.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int Left, Top, Right, Bottom;
    }
}