#include <stdio.h>
#include <string.h>

double UnmanagedTest (const char* name, double num)
{
    printf("Hello, %s\n", name);
    return num * strlen(name);
}
