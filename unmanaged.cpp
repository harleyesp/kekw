#include <Windows.h>

extern "C" __declspec(dllexport) void TypeWriterEffect(const wchar_t* text)
{
    for (int i = 0; text[i] != L'\0'; i++)
    {
        wprintf(L"%c", text[i]);
        Sleep(100); // Adjust the delay to control typing speed
    }

    wprintf(L"\n");
}
