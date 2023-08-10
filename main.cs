using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr GetConsoleWindow();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteConsole(IntPtr hConsoleOutput, string lpBuffer, uint nNumberOfCharsToWrite, out uint lpNumberOfCharsWritten, IntPtr lpReserved);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetConsoleCursorPosition(IntPtr hConsoleOutput, COORD dwCursorPosition);

    [StructLayout(LayoutKind.Sequential)]
    public struct COORD
    {
        public short X;
        public short Y;
    }

    const int STD_OUTPUT_HANDLE = -11;
    const uint STD_OUTPUT_HANDLE_ID = 7;

    static void Main(string[] args)
    {
        string message = "Hello World";
        IntPtr hConsole = GetStdHandle(STD_OUTPUT_HANDLE);

        TypeWriterEffect(hConsole, message);
        Console.WriteLine(); // Move to the next line after typing is complete
    }

    static void TypeWriterEffect(IntPtr hConsole, string text)
    {
        uint charsWritten;
        COORD initialCursorPosition;
        GetCursorPosition(hConsole, out initialCursorPosition);

        foreach (char c in text)
        {
            WriteConsole(hConsole, c.ToString(), 1, out charsWritten, IntPtr.Zero);
            Thread.Sleep(100); // Adjust the delay to control typing speed
        }

        COORD finalCursorPosition = new COORD { X = initialCursorPosition.X, Y = initialCursorPosition.Y };
        SetConsoleCursorPosition(hConsole, finalCursorPosition);
    }

    static void GetCursorPosition(IntPtr hConsole, out COORD cursorPosition)
    {
        cursorPosition = new COORD();
        if (hConsole != IntPtr.Zero)
        {
            IntPtr consoleHandle = GetConsoleWindow();
            if (consoleHandle != IntPtr.Zero)
            {
                if (GetConsoleScreenBufferInfo(hConsole, out CONSOLE_SCREEN_BUFFER_INFO bufferInfo))
                {
                    cursorPosition = bufferInfo.dwCursorPosition;
                }
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COORD
    {
        public short X;
        public short Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SMALL_RECT
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_SCREEN_BUFFER_INFO
    {
        public COORD dwSize;
        public COORD dwCursorPosition;
        public short wAttributes;
        public SMALL_RECT srWindow;
        public COORD dwMaximumWindowSize;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool GetConsoleScreenBufferInfo(IntPtr hConsoleOutput, out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);
}
