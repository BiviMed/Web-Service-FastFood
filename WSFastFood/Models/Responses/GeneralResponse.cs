namespace WSFastFood.Models.Responses
{
    public class GeneralResponse
    {
        public int Success { get; set; } = 0;

        public string? Message { get; set; } = null;

        public object? Data { get; set; } = null;
    }
}
