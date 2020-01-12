using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class OrganizationController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Organization>> GetAll()
        {
            var users = Enumerable.Range(0, 10)
                .Select(i => new Organization {Id = i.ToString(), Name = $"Name {i}"});
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<Organization> Get([FromRoute] string id)
        {
            return Ok(new Organization {Id = id, Name = $"Name {id}"});
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<Organization> Create([FromBody] OrganizationCreateModel model)
        {
            var id = new Random().Next(100).ToString();
            return Created($"api/v1/organization/{id}", new Organization {Id = id, Name = model.Name});
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<Organization> Update([FromRoute] string id, [FromBody] OrganizationUpdateModel model)
        {
            return Ok(new Organization {Id = id, Name = model.Name});
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] string id)
        {
            return NoContent();
        }
    }

    public class Organization
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class OrganizationCreateModel
    {
        public string Name { get; set; }
    }

    public class OrganizationUpdateModel
    {
        public string Name { get; set; }
    }
}