public static class Logger
{
    static readonly ILogger _logger = new UnityDebugLogger();

    public static void Info(string msg)
    {
        _logger.Info(msg);
    }

    public static void Error(string msg)
    {
        _logger.Error(msg);
    }

    public static void Warning(string msg)
    {
        _logger.Warning(msg);
    }
}