using BaseMod;

public class LoggerUtils
{
    public static void LogMessage(string msg)
    {
        if (ModLoader.IsDebugging)
        {
            Log.Out($"{ModLoader.MessagePrefix} {msg}");
        }
    }
}