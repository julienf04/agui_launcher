using System.Runtime.InteropServices;

namespace agui
{
    internal static class WINAPI
    {
        public static int ESC_KEY = 0x1B;

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);
    }
}
