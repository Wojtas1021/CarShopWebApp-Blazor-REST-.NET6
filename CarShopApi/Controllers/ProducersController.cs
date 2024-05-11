using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarShopApi.Data;
using CarShopApi.Models.Producer;
using AutoMapper;
using CarShopApi.Static;

namespace CarShopApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProducersController : ControllerBase
{
    private readonly CarShopDbContext _context;
    private readonly IMapper mapper;
    private readonly ILogger<ProducersController> logger;

    public ProducersController(CarShopDbContext context, IMapper mapper, ILogger<ProducersController> logger)
    {
        _context = context;
        this.mapper = mapper;
        this.logger = logger;
    }

    // GET: api/Producers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProducerReadOnlyDto>>> GetProducers()
    {
        try
        {
            var producers = await _context.Producers.ToListAsync();
            var producersDto = mapper.Map<IEnumerable<ProducerReadOnlyDto>>(producers);
            return Ok(producersDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error on GET method call in {nameof(GetProducer)}");
            return StatusCode(500, Messages.Error500Message);
        }
    }
    // GET: api/Producers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProducerReadOnlyDto>> GetProducer(int id)
    {
        try
        {
            var producer = await _context.Producers.FindAsync(id);

            if (producer == null)
            {
                return NotFound();
            }
            var producerDto = mapper.Map<ProducerReadOnlyDto>(producer);
            return Ok(producerDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error on GET method call in {nameof(GetProducer)}");
            return StatusCode(500, Messages.Error500Message);
        }
    }

    // PUT: api/Producers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProducer(int id, ProducerUpdateDto producerDto)
    {
        if (id != producerDto.Id)
        {
            logger.LogWarning($"Update ID is invalid in {nameof(PutProducer)}");
            return BadRequest();
        }
        var producer = await _context.Producers.FindAsync(id);

        if (producer == null)
        {
            logger.LogWarning($"{nameof(Producer)} record not found in {nameof(PutProducer)}");
            return NotFound();
        }

        mapper.Map(producerDto,producer);
        _context.Entry(producer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ProducerExists(id))
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

    // POST: api/Producers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ProducerCreateDtocs>> PostProducer(ProducerCreateDtocs producerDto)
    {
        try
        {
            var producer = mapper.Map<Producer>(producerDto);
            await _context.Producers.AddAsync(producer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducer), new { id = producer.Id }, producer);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error POST in {nameof(PostProducer)}");
            return StatusCode(500, Messages.Error500Message);
        }

    }

    // DELETE: api/Producers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducer(int id)
    {
        try
        {
            if (_context.Producers == null)
            {
                return NotFound();
            }
            var producer = await _context.Producers.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }

            _context.Producers.Remove(producer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error Deleting in {nameof(DeleteProducer)}");
            return StatusCode(500, Messages.Error500Message);
        }
    }
    private async Task<bool> ProducerExists(int id)
    {
        return await _context.Producers.AnyAsync(e => e.Id == id);
    }
}
