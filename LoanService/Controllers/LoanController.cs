using LoanService.Data;
using LoanService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : Controller
    {
        private readonly LoanDbContext _context;

        public LoanController(LoanDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans() =>
            await _context.Loans.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            return loan == null ? NotFound() : Ok(loan);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoansByCustomer(int customerId) =>
            await _context.Loans.Where(l => l.CustomerId == customerId).ToListAsync();

        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLoan), new { id = loan.LoanId }, loan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(int id, Loan loan)
        {
            if (id != loan.LoanId) return BadRequest();
            _context.Entry(loan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null) return NotFound();
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return NoContent();
        }
       
    }
}
