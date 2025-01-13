using System.Text.Json.Serialization;

namespace WebApi.Responses
{
    public class Response<T>
    {
        public bool status { get; set; }
        public T? value { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? msg { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? errors { get; set; }
    }
}
