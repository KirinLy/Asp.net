namespace MagicVilla_VillaApi.Log
{
    public class LoggingV1 : ILogging
    {
        public void LogInformation(string message)
        {
            Console.WriteLine(DateTime.Now.ToString("s") + " " + message);
        }

        public void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(DateTime.Now.ToString("s") + " " + message);
            Console.ResetColor();
        }

        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(DateTime.Now.ToString("s") + " " + message);
            Console.ResetColor();
        }
    }
}