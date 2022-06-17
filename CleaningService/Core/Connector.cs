namespace CleaningService.Core;

public static class Connector
{
    private static MyDbContext? _context;

    public static MyDbContext GetContext()
    {
        return _context ??= new MyDbContext();
    }

    public static void ReloadContext()
    {
        _context = new MyDbContext();
    }
}