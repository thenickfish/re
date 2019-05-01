using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IPublishEndpoint publishEndpoint;
        private readonly RoomMateDbContext dbContext;

        public ValuesController (IPublishEndpoint publishEndpoint, RoomMateDbContext dbContext)
        {
            this.publishEndpoint = publishEndpoint;
            this.dbContext = dbContext;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomMate>>> Get ()
        {
            await dbContext.RoomMates.AddAsync (new RoomMate { FirstName = "firstName", LastName = "lastname1" });
            await dbContext.SaveChangesAsync ();
            await publishEndpoint.Publish<ITestEvent> (new { Name = "Nick" });
            return new RoomMate[] { new RoomMate { FirstName = "firstName", LastName = "lastname1" }, new RoomMate { FirstName = "firstName2", LastName = "lastname2" } };
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public ActionResult<string> Get (int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post ([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }
    }
}