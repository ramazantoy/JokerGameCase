
using Newtonsoft.Json;


namespace _Project.Scripts.LeonsExtensions
{
    public static class DeepCopyHelper 
    {
   
        public static T DeepCopy<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
