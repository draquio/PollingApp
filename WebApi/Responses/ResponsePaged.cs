namespace WebApi.Responses
{
    public class ResponsePaged<T> : Response<T>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
