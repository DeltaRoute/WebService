using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Model;
using System.Data.SqlClient;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;

namespace DB_Lab_4.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        ApplicationContext context;
        public CarController(ApplicationContext _context)
        {
            context = _context;
        }
        // GET: api/Product
        [HttpGet]
        public IEnumerable<Car> GetAll()
        {
            return context.CarTable.ToList();
        }
        [HttpGet("{id}", Name = "Get4ID")]
        public IActionResult Get4ID(int id)
        {
            var item = context.CarTable.FirstOrDefault<Car>(t => t.ID == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Car item)
        {
            if (item == null)
                return BadRequest();
            Car car = new Car { ID = item.ID, Name = item.Name, Volume = item.Volume, Power = item.Power, Price = item.Price };
            context.CarTable.Add(car);
            context.SaveChanges();
            return new NoContentResult();
        }
        [HttpPut("{id}")]
        public IActionResult Edit(long id, [FromBody] Car item)
        {
            if (item == null || item.ID != id)
                return BadRequest();
            var car = context.CarTable.FirstOrDefault<Car>(t => t.ID == id);
            if (car == null)
                return NotFound();
            car.Name = item.Name;
            car.Volume = item.Volume;
            car.Power = item.Power;
            car.Price = item.Price;
            context.CarTable.Update(car);
            context.SaveChanges();
            return new NoContentResult();
        }
        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var car = context.CarTable.FirstOrDefault<Car>(t => t.ID == id);
            if (car == null)
                return BadRequest();
            context.CarTable.Remove(car);
            context.SaveChanges();
            return new NoContentResult();
        }
    }
}

