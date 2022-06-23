using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface IPaymentsRepository
    {
        void AddPayment(Payment payment);
        void DeletePayment(Payment payment);
        List<Payment> GetAllPayments(int employeeId);
        Payment GetPayment(int paymentId);
        void UpdatePayment(Payment payment);
    }
}