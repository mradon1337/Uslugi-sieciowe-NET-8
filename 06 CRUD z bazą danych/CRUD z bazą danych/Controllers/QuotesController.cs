using Microsoft.AspNetCore.Mvc;
using TravelQuotesApi.Interfaces;
using TravelQuotesApi.Models;

namespace TravelQuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IRepository<Quote> _quoteRepository;

        public QuotesController(IRepository<Quote> quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        // GET: api/quotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuotes()
        {
            var quotes = await _quoteRepository.GetAllAsync();
            return Ok(quotes);
        }

        // GET: api/quotes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> GetQuote(int id)
        {
            var quote = await _quoteRepository.GetByIdAsync(id);
            if (quote == null)
            {
                return NotFound();
            }
            return Ok(quote);
        }

        // POST: api/quotes
        [HttpPost]
        public async Task<ActionResult<Quote>> PostQuote(Quote quote)
        {
            await _quoteRepository.CreateAsync(quote);
            
            return CreatedAtAction(nameof(GetQuote), new { id = quote.Id }, quote);
        }

        // PUT: api/quotes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuote(int id, Quote quote)
        {
            // id z adresu musi pasować do id w przesłanym obiekcie
            if (id != quote.Id)
            {
                return BadRequest();
            }

            var existing = await _quoteRepository.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _quoteRepository.UpdateAsync(quote);
            return NoContent();
        }

        // DELETE: api/quotes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var existing = await _quoteRepository.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _quoteRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
