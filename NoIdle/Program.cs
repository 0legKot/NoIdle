using System.Runtime.InteropServices;

Console.WriteLine("Anti-idle started");
while (true) {
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

struct POINT {
    public int X;
    public int Y;
}