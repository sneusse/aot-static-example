# Minimal example for direct P/Invoke calls with static linking

Build small(-ish) executables with .NET and C

## Notes

This is the function we would like to statically link against our executable written in C#

```c
#include <stdio.h>
#include <string.h>

double UnmanagedTest (const char* name, double num)
{
    printf("Hello, %s\n", name);
    return num * strlen(name);
}
```


This is our C# application. External functions to be linked statically are declared like normal P/Invoke functions.

```csharp
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

```

This part of the project file is responsible for generating the direct calls and linking statically against the library written in C:

`app.csproj`
```csharp

    <ItemGroup>
        <!-- Generate direct PInvokes for Dependency -->
        <DirectPInvoke Include="nativelib" />
        <!-- Specify library to link against -->
        <NativeLibrary Include="nativelib.lib" Condition="$(RuntimeIdentifier.StartsWith('win'))" />
        <!-- Specify the path to search for libraries -->
        <LinkerArg Include="/LIBPATH:..\\clib" Condition="$(RuntimeIdentifier.StartsWith('win'))" />
    </ItemGroup>

```

## References

https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/interop
https://github.com/dotnet/runtime/issues/89044

