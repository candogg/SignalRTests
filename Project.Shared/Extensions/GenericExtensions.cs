using Project.Shared.Services.Derived;

namespace Project.Shared.Extensions
{
    public static class GenericExtensions
    {
        public static string Serialize(this object item)
        {
            return SerializationService.DerivedObject.SerializeObject(item);
        }

        public static T? Deserialize<T>(this object obj)
        {
            try
            {
                return SerializationService.DerivedObject.DeserializeObject<T>(obj);
            }
            catch
            { }

            return default;
        }
    }
}
