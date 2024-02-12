namespace TesteMiddleware.api.ErrosHTTP
{
    public class PaymentRequiredException : Exception
    {
        //public PaymentRequiredException() : base("Pagamento necessário.")
        //{
        //}

        public PaymentRequiredException(string message) : base(message)
        {
        }
    }
}
