
using System.Runtime.InteropServices;

internal static class App
{
    [DllImport("nativelib", EntryPoint = "UnmanagedTest", CallingConvention = CallingConvention.Cdecl)]
    public static extern double UnmanagedTest ([MarshalAs(UnmanagedType.LPStr)] string name, double num);

    public static void Main()
    {
        Console.WriteLine("Hello, World!");
        var val = UnmanagedTest("World!", 7);
        Console.WriteLine($"I got the number '{val}' back");
    }
}