using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarDealershipProject.Models;

namespace CarDealershipProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarDealershipDBContext _context;

        public CarsController(CarDealershipDBContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //[Route("filter/{make}/{model}/{year}/{color")]
        //public IEnumerable<Car> SearchCom(string make = null, string model = null, int year = 0, string color =null)
        //{
        //    List<Car> result = new List<Car>();
        //    result = _context.Cars.Where(x => x.Make.ToLower() == make.ToLower() && x.Make.ToLower() == make.ToLower() && x.Year == year && x.Color.ToLower() == color).ToList();
        //    return result;
        //}


        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            return car;
        }

        [HttpGet]
        [Route("make/{make}")]
        public IEnumerable<Car> SearchMake(string make)
        {
            List<Car> result = new List<Car>();
           
            result = _context.Cars.Where(x => x.Make.ToLower() == make.ToLower()).ToList();

            if (result.Count == 0)
            {
                Car car = new Car(){ Make = "not found" };
                result.Add(car);
            }
            return result;
        }


        [HttpGet]
        [Route("model/{model}")]
        public IEnumerable<Car> SearchModel(string model)
        {
            List<Car> result = new List<Car>();
            result = _context.Cars.Where(x => x.Model.ToLower() == model.ToLower()).ToList();
            if (result.Count == 0)
            {
                Car car = new Car() { Make = "not found" };
                result.Add(car);
            }
            return result;
        }


        [HttpGet]
        [Route("year/{year}")]
        public IEnumerable<Car> SearchYear(int year)
        {
            List<Car> result = new List<Car>();
            result = _context.Cars.Where(x => x.Year == year).ToList();
            if (result.Count == 0)
            {
                Car car = new Car() { Make = "not found" };
                result.Add(car);
            }

            return result;
        }

        [HttpGet]
        [Route("color/{color}")]
        public IEnumerable<Car> SearchColor(string color)
        {
            List<Car> result = new List<Car>();
            result = _context.Cars.Where(x => x.Color.ToLower() == color.ToLower()).ToList();
            if (result.Count == 0)
            {
                Car car = new Car() { Make = "not found" };
                result.Add(car);
            }

            return result;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
