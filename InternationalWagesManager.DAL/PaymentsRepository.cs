using InternationalWagesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.DAL
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private MyDbContext _db;

        public PaymentsRepository(MyDbContext db)
        {
            _db = db;
        }

        public void AddPayment(Payment payment)
        {
            _db.Payments.Add(payment);
            _db.SaveChanges();
        }

        public void UpdatePayment(Payment payment)
        {
            _db.Payments.Update(payment);
            _db.SaveChanges();
        }

        public void DeletePayment(Payment payment)
        {
            _db.Payments.Remove(payment);
            _db.SaveChanges();
        }

        public Payment GetPayment(int paymentId)
        {
            var result = _db.Payments.FirstOrDefault(x => x.Id == paymentId);
            return result ?? new();
        }

        public List<Payment> GetAllPayments(int employeeId)
        {
            var result = _db.Payments.Where(p => p.EmployeeId == employeeId).ToList();
            return result ?? new();
        }
    }
}
