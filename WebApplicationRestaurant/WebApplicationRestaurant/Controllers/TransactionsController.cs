using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationRestaurant.Models;

namespace WebApplicationRestaurant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionContext _context;

        public TransactionsController(TransactionContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transactions>>> GetTransactions()
        {
            var transactions = await _context.Transactions.Include(t => t.Customer).Include(t => t.Food).ToListAsync();
            var transactionList = transactions.Select(t => new
            {
                transaction_id = t.transaction_id,
                customer_id = t.customer_id,
                customer = new
                {
                    customer_id = t.Customer.customer_id,
                    customer_name = t.Customer.customer_name,
                    email = t.Customer.email,
                    phone_number = t.Customer.phone_number
                },
                food_id = t.food_id,
                food = new
                {
                    food_id = t.Food.food_id,
                    food_name = t.Food.food_name,
                    price = t.Food.price,
                    stock = t.Food.stock
                },
                qty = t.qty,
                total_price = t.total_price,
                transaction_date = t.transaction_date
            });

            return Ok(transactionList);
            //return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transactions>> GetTransactions(int id)
        {
            var transactions = await _context.Transactions
                .Include(t => t.Customer)
                .Include(t => t.Food)
                .FirstOrDefaultAsync(t => t.transaction_id == id);


            if (transactions == null)
            {
                return NotFound();
            }

            var transaction =  new
            {
                transaction_id = transactions.transaction_id,
                customer_id = transactions.customer_id,
                customer = new
                {
                    customer_id = transactions.Customer.customer_id,
                    customer_name = transactions.Customer.customer_name,
                    email = transactions.Customer.email,
                    phone_number = transactions.Customer.phone_number
                },
                food_id = transactions.food_id,
                food = new
                {
                    food_id = transactions.Food.food_id,
                    food_name = transactions.Food.food_name,
                    price = transactions.Food.price,
                    stock = transactions.Food.stock
                },
                qty = transactions.qty,
                total_price = transactions.total_price,
                transaction_date = transactions.transaction_date
            };
            return Ok(transaction);
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactions(int id, Transactions transactions)
        {
            if (id != transactions.transaction_id)
            {
                return BadRequest();
            }

            _context.Entry(transactions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Transactions>> PostTransactions(Transactions transactions)
        {
            _context.Transactions.Add(transactions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactions", new { id = transactions.transaction_id }, transactions);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transactions>> DeleteTransactions(int id)
        {
            var transactions = await _context.Transactions.FindAsync(id);
            if (transactions == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transactions);
            await _context.SaveChangesAsync();

            return transactions;
        }

        private bool TransactionsExists(int id)
        {
            return _context.Transactions.Any(e => e.transaction_id == id);
        }
    }
}
