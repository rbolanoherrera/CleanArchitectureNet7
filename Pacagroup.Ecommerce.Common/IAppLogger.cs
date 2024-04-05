namespace Pacagroup.Ecommerce.Transversal.Common
{
    public interface IAppLogger<T>
    {
        void LogInformation(string message);
        void LogInformation(string message, params object[] args);
        void LogWarning(string message);
        void LogWarning(string message, params object[] args);
        void LogError(string message);
        void LogError(string message, params object[] args);
    }
}