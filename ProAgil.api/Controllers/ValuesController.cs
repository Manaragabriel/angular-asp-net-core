using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository.Data; 
namespace ProAgil.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            _context= context;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try{
                var ret= await _context.Eventos.ToListAsync();
                return Ok(ret);
            }catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Erro banco");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Evento> Get(int id)
        {
           return _context.Eventos.ToList().FirstOrDefault(x=> x.id== id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
