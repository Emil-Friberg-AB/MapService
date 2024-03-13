using System.Text.Json;

namespace Infra.Exstentions
{
    public static class JsonExtensions
    {
        public static string SerializeToLowercaseJson<T>(this T obj)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Serialize(obj, options);
        }
    }
}
