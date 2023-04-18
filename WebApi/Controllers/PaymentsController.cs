using ApiContracts;
using ApiContracts.ResponseStatus;
using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IMapper _mapper;
        private ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentsRepository paymentsRepository, IMapper mapper, ILogger<PaymentsController> logger)
        {
            _paymentsRepository = paymentsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Payments/employeeId
        [HttpGet("all/{employeeId}")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetPayments(int employeeId)
        {
            var payments = _mapper.Map<IEnumerable<PaymentResponse>>(await _paymentsRepository.GetAllEmployeePaymentsAsync(employeeId));
            if (payments == null)
                return NotFound();

            return Ok(payments);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentResponse>> GetPayment(int id)
        {
            var salary = _mapper.Map<PaymentResponse>(await _paymentsRepository.GetByIdAsync(id));
            if (salary == null)
                return NotFound();

            return Ok(salary);
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentRequest payment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id < 1)
                return BadRequestResponse();

            if (await PaymentExistsAsync(id))
                return NotFound();

            try
            {
                Payment modelPayment = _mapper.Map<Payment>(payment);
                modelPayment.Id = id;
                await _paymentsRepository.UpdateAsync(modelPayment);

                return CreatedAtAction(nameof(GetPayment), new { id = modelPayment.Id }, payment);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ServerErrorResponse();
            }
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<ActionResult<PaymentResponse>> AddPayment(PaymentRequest payment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int newId = 0;
            try
            {
                newId = await _paymentsRepository.AddAsync(_mapper.Map<Payment>(payment));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ServerErrorResponse();
            }
            return CreatedAtAction(nameof(GetPayment), new { id = newId }, payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (!(await PaymentExistsAsync(id)))
                return NotFound();

            try
            {
                var payment = new Payment() { Id = id };
                await _paymentsRepository.DeleteAsync(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ServerErrorResponse();
            }

            return NoContent();
        }

        private async Task<bool> PaymentExistsAsync(int id)
        {
            return await _paymentsRepository.GetByIdAsync(id) != null;
        }
        private ActionResult ServerErrorResponse() => Problem("Server Error", statusCode: StatusCodes.Status500InternalServerError);

        private ActionResult BadRequestResponse()
        {
            return BadRequest(new ErrorResponse
            {
                ErrorMessage = "Invalid id",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }
    }
}
