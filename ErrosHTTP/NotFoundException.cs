namespace TesteMiddleware.api.ErrosHTTP
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
