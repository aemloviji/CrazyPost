using CrazyPost.Models;
using CrazyPost.Repository;
using CrazyPost.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

            var resultModel = new List<PostEnhanceDTO>();
            foreach (var item in postList)
            {
                resultModel.Add(Convertor.ToPostEnhanceDTO(item));
            }

            return Ok(resultModel);
        }

        [HttpGet("{id}", Name = "GetPost")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await PostRepo.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            var resultModel = Convertor.ToPostEnhanceDTO(item);
            return Ok(resultModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddOrUpdatePostDTO formData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultModel = Convertor.ToPost(formData);
            await PostRepo.Add(resultModel);

            return CreatedAtRoute("GetPost", new { Controller = "Post", id = resultModel.Id }, resultModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] AddOrUpdatePostDTO formData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!PostExists(id).Result)
            {
                return NotFound();
            }

            var postItem = Convertor.ToPost(formData, id);
            await PostRepo.Update(id, postItem);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!PostExists(id).Result)
            {
                return NotFound();
            }

            await PostRepo.Remove(id);
            return NoContent();
        }


        private async Task<bool> PostExists(int id)
        {
            var result = await PostRepo.Find(id);

            if (result == null)
                return false;

            return true;
        }
    }
}
