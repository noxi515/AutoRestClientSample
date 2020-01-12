using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = Enumerable.Range(0, 10)
                .Select(i => new User {Id = i.ToString(), Name = $"Name {i}"});
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get([FromRoute] string id)
        {
            return Ok(new User {Id = id, Name = $"Name {id}"});
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<User> Create([FromBody] UserCreateModel model)
        {
            var id = new Random().Next(100).ToString();
            return Created($"api/v1/user/{id}", new User {Id = id, Name = model.Name});
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<User> Update([FromRoute] string id, [FromBody] UserUpdateModel model)
        {
            return Ok(new User {Id = id, Name = model.Name});
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] string id)
        {
            return NoContent();
        }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class UserCreateModel
    {
        public string Name { get; set; }
    }

    public class UserUpdateModel
    {
        public string Name { get; set; }
    }
}