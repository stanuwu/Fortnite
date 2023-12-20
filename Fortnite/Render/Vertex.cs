using System.Runtime.InteropServices;
using SharpDX;

namespace Fortnite.Render
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector4 Position;
        public ColorBGRA Color;
    }
}