using System.Linq;
using Microsoft.AspNetCore.Mvc;
using JJK.Entities;

namespace JJK.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly UrsalcoopContext _context;

        public PaymentsController(UrsalcoopContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("payments/viewpayments/{clientId}")]
        public IActionResult ViewPayments(int clientId)
        {
            var payments = _context.PaymentsTbs.Where(p => p.ClientId == clientId).ToList();
            if (payments == null || payments.Count == 0)
            {
                return NotFound();
            }
            return View(payments);
        }

        [HttpGet]
        public IActionResult Pay(int id)
        {
            var payment = _context.PaymentsTbs.FirstOrDefault(p => p.Id == id);
            if (payment == null || payment.Status != "Pending")
            {
                return NotFound();
            }

            return View(payment);
        }

        [HttpPost]
        public IActionResult ProcessPayment(int paymentId, decimal amount)
        {
            var payment = _context.PaymentsTbs.FirstOrDefault(p => p.Id == paymentId);
            if (payment == null || payment.Status != "Pending")
            {
                return NotFound();
            }

            if (amount > payment.Collectable)
            {
                ModelState.AddModelError("", "Payment amount exceeds the collectable amount.");
                return View("Pay", payment);
            }

            payment.Collectable -= amount;
            if (payment.Collectable == 0)
            {
                payment.Status = "Paid";
            }

            var loan = _context.LoanDbs.FirstOrDefault(l => l.Id == payment.LoanId);
            if (loan != null)
            {
                loan.PayableAmount -= amount;
                if (loan.PayableAmount < 0)
                {
                    loan.PayableAmount = 0;
                }
            }

            _context.SaveChanges();

            return RedirectToAction("ViewPayments", new { clientId = payment.ClientId });
        }
    }
}
