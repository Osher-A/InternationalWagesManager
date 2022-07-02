using AutoMapper;
using InternationalWagesManager.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.Domain
{
    public class PaymentsManager
    {
        private readonly IMapper _mapper;
        private readonly IPaymentsRepository _paymentsRepository;

        public PaymentsManager(IMapper mapper, IPaymentsRepository paymentsRepository)
        {
            _mapper = mapper;
            _paymentsRepository = paymentsRepository;
        }
        
        public void AddPayment(DTO.Payment payment)
        {
            var modelPayment = _mapper.Map<DTO.Payment, Models.Payment>(payment);
            if (payment.EmployeeId != 0 && payment.Amount != 0 && payment.Amount != null && payment.Date != null)
                _paymentsRepository.AddPayment(modelPayment);
        }

        public List<DTO.Payment> GetAllPayments(int employeeId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var allModelPayments = _mapper.Map<List<Models.Payment>, List<DTO.Payment>>(_paymentsRepository.GetAllPayments(employeeId));
            if (fromDate != null && toDate != null)
                return allModelPayments.Where(p => p.Date >= fromDate && p.Date <= toDate).ToList();
            else if (fromDate != null)
                return allModelPayments.Where(p => p.Date >= fromDate).ToList();
            else if(toDate != null)
                return allModelPayments.Where(p => p.Date <= toDate).ToList();

            return allModelPayments;
        }
    }
}
