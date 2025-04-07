using CustomerService.Data;
using CustomerService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Services
{
    public class CustomerServices
    {
        private readonly AppDbContext _context;

        public CustomerServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync() =>
            await _context.Customers.ToListAsync();

        public async Task<Customer?> GetByIdAsync(int id) =>
            await _context.Customers.FindAsync(id);

        public async Task<Customer> CreateAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> UpdateAsync(int id, Customer updatedCustomer)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            customer.FullName = updatedCustomer.FullName;
            customer.Email = updatedCustomer.Email;
            customer.PhoneNumber= updatedCustomer.PhoneNumber;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
