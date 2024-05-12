using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarShopApi.Data;
using AutoMapper;
using CarShopApi.Models.Car;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;

namespace CarShopApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CarsController : ControllerBase
{
    private readonly CarShopDbContext _context;
    private readonly IMapper mapper;

    public CarsController(CarShopDbContext context, IMapper mapper)
    {
        _context = context;
        this.mapper = mapper;
    }
    // GET: api/Cars
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarReadOnlyDto>>> GetCars()
    {
        var cars = await _context.Cars
            .Include(a => a.Producer)
            .ProjectTo<CarReadOnlyDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(cars);
    }
    // GET: api/Cars/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CarDetailsDto>> GetCar(int id)
    {
        var car = await _context.Cars
            .Include(p => p.Producer)
            .ProjectTo<CarDetailsDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (car == null)
        {
            return NotFound();
        }

        return car;
    }

    // PUT: api/Cars/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PutCar(int id, CarUpdateDto carDto)
    {
        if (id != carDto.Id)
        {
            return BadRequest();
        }
        var car = await _context.Cars.FindAsync(id);
        if (car == null)
        {
            return NotFound();
        }

        mapper.Map(carDto, car);
        _context.Entry(car).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (! await CarExistsAsync(id))
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

    // POST: api/Cars
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<CarCreateDto>> PostCar(CarCreateDto carDto)
    {
        var car = mapper.Map<Car>(carDto);
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCar", new { id = car.Id }, car);
    }

    // DELETE: api/Cars/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null)
        {
            return NotFound();
        }

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> CarExistsAsync(int id)
    {
        return await _context.Cars.AnyAsync(e => e.Id == id);
    }
}
