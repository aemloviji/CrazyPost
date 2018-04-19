using CrazyPost.Models;
using CrazyPost.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var postList = await PostRepo.GetAll();
            return Ok(postList);
        }

        [HttpGet("{id}", Name = "GetPost")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await PostRepo.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Post item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            await PostRepo.Add(item);
            return CreatedAtRoute("GetContact", new { Controller = "Post", id = item.ID }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Post item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var contactObj = await PostRepo.Find(id);
            if (contactObj == null)
            {
                return NotFound();
            }
            await PostRepo.Update(item);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await PostRepo.Remove(id);
            return NoContent();
        }
    }
}
