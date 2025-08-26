using System.Runtime.InteropServices;

Console.WriteLine("Anti-idle started");
var ok = SetThreadExecutionState(
            EXECUTION_STATE.ES_CONTINUOUS |
            EXECUTION_STATE.ES_SYSTEM_REQUIRED |
            EXECUTION_STATE.ES_DISPLAY_REQUIRED);

if (ok == 0) {
    Console.WriteLine("Failed to set execution state.");
}
while (true) {
    SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED);
    GetCursorPos(out POINT pos);
    SetCursorPos(pos.X + 1, pos.Y);
    Thread.Sleep(100);
    SetCursorPos(pos.X, pos.Y);
    Thread.Sleep(3*60*1000);
    Console.WriteLine($"Anti-idle activated at {TimeOnly.FromDateTime(DateTime.Now)} ");
}

[DllImport("user32.dll")]
static extern bool SetCursorPos(int X, int Y);

[DllImport("user32.dll")]
static extern bool GetCursorPos(out POINT lpPoint);

[DllImport("kernel32.dll")]
static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

struct POINT {
    public int X;
    public int Y;
}

[Flags]
enum EXECUTION_STATE : uint {
    ES_AWAYMODE_REQUIRED = 0x00000040, // (для медіацентрів; можна не юзати)
    ES_CONTINUOUS = 0x80000000,
    ES_DISPLAY_REQUIRED = 0x00000002,
    ES_SYSTEM_REQUIRED = 0x00000001
}