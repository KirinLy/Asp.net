namespace MagicVilla_VillaApi.Log
{
    public interface ILogging
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}
