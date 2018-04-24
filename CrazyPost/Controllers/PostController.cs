using CrazyPost.Models;
using CrazyPost.Repository;
using CrazyPost.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrazyPost.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        public IPostRepository PostRepo { get; set; }

        public PostController(IPostRepository _repo)
        {
            PostRepo = _repo;
        }


        /// <summary>
        /// Returns all Post
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Post>), 200)]
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

        /// <summary>
        /// Returns Post by given id
        /// </summary>
        /// <param name="id">id of Post to return</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetPost")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(PostEnhanceDTO), 200)]
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

        /// <summary>
        /// Creates new Post with given values
        /// </summary>
        /// <param name="formData">new post entity values</param>
        /// <returns>A newly created Post</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Post), 201)]
        [ProducesResponseType(400)]
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

        /// <summary>
        /// Updates a specific Post.
        /// </summary>
        /// <param name="id">id of entity to be updated</param>
        /// <param name="formData">updatable values</param>
        /// <returns></returns>
        [HttpPut("{id}")]     
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
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


        /// <summary>
        /// Deletes a specific Post.
        /// </summary>
        /// <param name="id">id of Post entity</param>     
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
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
