using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transaction.Data;
using Transaction.Models;

namespace Transaction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransContext _context;

        public TransactionController(TransContext context)
        {
            _context = context;
        }

        // POST: api/transaction
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] Transactions transaction)
        {
            // Validasi simple
            if (transaction.SenderId == transaction.ReceiverId)
                return BadRequest("Sender and receiver cannot be the same.");

            if (transaction.Amount <= 0)
                return BadRequest("Amount must be greater than zero.");

            transaction.CreatedAt = DateTime.Now;
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(transaction);
        }

        // GET: api/transaction/customer/1
        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetTransactionsByCustomerId(int id)
        {
            var transactions = await _context.Transactions
                .Where(t => t.SenderId == id || t.ReceiverId == id)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return Ok(transactions);
        }

        [HttpGet("balance/{customerId}")]
        public async Task<IActionResult> GetCustomerBalance(int customerId)
        {
            var incoming = await _context.Transactions
                .Where(t => t.ReceiverId == customerId)
                .SumAsync(t => t.Amount);

            var outgoing = await _context.Transactions
                .Where(t => t.SenderId == customerId)
                .SumAsync(t => t.Amount);

            var balance = incoming - outgoing;

            return Ok(new
            {
                CustomerId = customerId,
                TotalIn = incoming,
                TotalOut = outgoing,
                Balance = balance
            });
        }

    }
}
