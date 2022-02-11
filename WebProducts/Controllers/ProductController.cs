using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProducts.Data;
using WebProducts.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Products.Include(x=>x.Category).OrderBy(x => x.Name).ToList());
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Result = _context.Products.Include(x => x.Category).FirstOrDefault(x=>x.Id == id);
            return Ok(Result);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] Product model)
        {
            try
            {
                if (model != null)
                {
                    _context.Products.Add(model);
                    _context.SaveChanges();
                    return Ok(model);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product model)
        {
            try
            {
                var result = _context.Products.FirstOrDefault(x => x.Id == id);
                if (result != null)
                {
                    result.CategoryId = model.CategoryId;
                    result.Name = model.Name;
                    result.Price = model.Price;
                    result.Quntity = model.Quntity;
                    result.Price = model.Price;
                    result.Descount = model.Descount;
                    result.Total = model.Total;
                    _context.Products.Update(result);
                    _context.SaveChanges();
                    return Ok(result);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _context.Products.FirstOrDefault(x => x.Id == id);
                if (result != null)
                {
                    _context.Products.Remove(result);
                    _context.SaveChanges();
                    return Ok(result);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
