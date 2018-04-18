using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrazyPost.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CrazyPost.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        public IPostRepository PostRepo { get; set; }

        public PostController(IPostRepository _repo)
        {
            PostRepo = _repo;
        }


        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var postList = await PostRepo.GetAll();
            return Ok(postList);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
