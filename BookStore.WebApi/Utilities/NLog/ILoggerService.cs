namespace BookStore.WebApi.Utilities.NLog
{
    public interface ILoggerService
    {
        public void LogInfo(string message);
        public void LogWarning(string message);
        public void LogError(string message);
        public void LogDebug(string message);
    }
}
