namespace WebApi.Service.Abstract
{
    public interface ICacheService
    {
        public void Add(string key, object value);
        public object Get(string key);
        public void Remove(string key);
        public bool Exists(string key);
    }
}
