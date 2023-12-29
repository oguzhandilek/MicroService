namespace CustomerAPI.Interface;

public interface ICapHelper
{
    Task ExecuteWithTransactionAsync<T>(string eventName, T entity);
}