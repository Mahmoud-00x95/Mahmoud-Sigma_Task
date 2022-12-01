namespace Core.Dtos
{
    public class ResponseModel
    {
        public string Message { get; set; } = null!;
        public ushort StatusCode { get; set; }
    }
}
