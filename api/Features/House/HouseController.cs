using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.House
{
    [Route ("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly RoomMateDbContext dbContext;

        public HouseController (RoomMateDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<House>>> Get ()
        {
            return await dbContext.Houses.ToListAsync ().ConfigureAwait (false);
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public async Task<ActionResult<House>> Get (int id)
        {
            var s = await dbContext.Houses.Include (h => h.Address).FirstOrDefaultAsync (h => h.Id == id).ConfigureAwait (false);
            return s;
        }

        // POST api/values
        [HttpPost]
        public async Task Post ([FromBody] House house)
        {
            await dbContext.Houses.AddAsync (house);
            await dbContext.SaveChangesAsync ();
        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public async Task Delete (int id)
        {
            dbContext.Houses.RemoveRange (dbContext.Houses.Where (h => h.Id == id));
            await dbContext.SaveChangesAsync ();
        }
    }
}