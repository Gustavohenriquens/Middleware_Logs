namespace TesteMiddleware.api.ErrosHTTP
{
    public class CustomAppException : Exception
    {
        public string ErrorCode { get; }

        public CustomAppException(string errorCode, string message) : base(message)
        {
             // Atribui o valor do parâmetro errorCode à propriedade ErrorCode
            ErrorCode = errorCode;
        }
    }
}
