using System.Runtime.InteropServices;

namespace Fortnite.Sys.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Margins
    {
        public int Left, Right, Top, Bottom;
    }
}