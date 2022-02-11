using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebProducts.Data;
using WebProducts.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebProducts.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCategory")]
        public IActionResult GetCategory()
        {
            try
            {
                return Ok(_context.Categories.OrderBy(x => x.Name).ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetCategory/{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                return Ok(_context.Categories.FirstOrDefault(x => x.Id == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] Category model)
        {
            try
            {
                if (model != null)
                {
                    _context.Categories.Add(model);
                    _context.SaveChanges();
                    return Ok(model);
                }
                return BadRequest(model);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Category model)
        {
            try
            {
                var Result = _context.Categories.FirstOrDefault(x => x.Id == id);
                if (Result != null)
                {
                    Result.Name = model.Name;
                    _context.Update(Result);
                    _context.SaveChanges();
                    return Ok(Result);
                }

                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                    return Ok(category);
                }
                return BadRequest(category);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
