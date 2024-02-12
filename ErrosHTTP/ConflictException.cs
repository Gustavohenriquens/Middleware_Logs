namespace TesteMiddleware.api.ErrosHTTP
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base (message) 
        {
            
        }
    }
}
