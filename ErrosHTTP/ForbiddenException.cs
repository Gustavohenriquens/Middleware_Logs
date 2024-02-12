namespace TesteMiddleware.api.ErrosHTTP
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message)
        {           
        }
    }
}
