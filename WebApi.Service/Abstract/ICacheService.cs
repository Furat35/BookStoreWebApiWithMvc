namespace WebApi.Service.Abstract
{
    public interface ICacheService<T>
    {
        public void TryAdd(string key, T value);
        public T TryGet(string key);
        public void Remove(string key);
    }
}
