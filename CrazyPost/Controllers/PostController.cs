using CrazyPost.Models;
using CrazyPost.Repository;
using CrazyPost.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
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

            var postDto = Convertor.ToPostDTO(item);

            return Ok(postDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostAddOrUpdateDTO formData)
        {
            if (formData == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postItem = Convertor.ToPost(formData);
            await PostRepo.Add(postItem);

            return CreatedAtRoute("GetPost", new { Controller = "Post", id = postItem.Id }, postItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostAddOrUpdateDTO formData)
        {
            if (formData == null)
            {
                return BadRequest();
            }

            var contactObj = await PostRepo.Find(id);
            if (contactObj == null)
            {
                return NotFound();
            }

            var postItem = Convertor.ToPost(formData, id);
            await PostRepo.Update(id, postItem);

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
