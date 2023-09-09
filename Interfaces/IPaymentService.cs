namespace Consultation.Interfaces
{
    public interface IPaymentService
    {
        public Task<string> GetToken();
        public Task<string> OrderRegistation(string token, double amountCents);
        public Task<string> PaymentKey(string token, string orderId, double amountCents);

    }
}