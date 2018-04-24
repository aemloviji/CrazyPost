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
    public class CommentController : Controller
    {
        public ICommentRepository CommentRepo { get; set; }

        public CommentController(ICommentRepository _repo)
        {
            CommentRepo = _repo;
        }

        /// <summary>
        /// Returns all Comment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Comment>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var commentList = await CommentRepo.GetAll();

            var resultModel = new List<CommentEnhanceDTO>();
            foreach (var item in commentList)
            {
                resultModel.Add(Convertor.ToCommentEnhanceDTO(item));
            }

            return Ok(resultModel);
        }


        /// <summary>
        /// Returns Comment by given id
        /// </summary>
        /// <param name="id">id of Comment to return</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetComment")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(CommentEnhanceDTO), 200)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await CommentRepo.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            var resultModel = Convertor.ToCommentEnhanceDTO(item);
            return Ok(resultModel);
        }

        /// <summary>
        /// Creates new Comment with given values
        /// </summary>
        /// <param name="formData">new Comment entity values</param>
        /// <returns>A newly created Comment</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Comment), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] AddOrUpdateCommentDTO formData)
        {
            if (formData == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postItem = Convertor.ToComment(formData);
            await CommentRepo.Add(postItem);

            return CreatedAtRoute("GetComment", new { Controller = "Comment", id = postItem.Id }, postItem);

        }

        /// <summary>
        /// Updates a specific Comment.
        /// </summary>
        /// <param name="id">id of entity to be updated</param>
        /// <param name="formData">updatable values</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] AddOrUpdateCommentDTO formData)
        {
            if (formData == null)
            {
                return BadRequest();
            }

            if (!CommentExists(id).Result)
            {
                return NotFound();
            }

            var postItem = Convertor.ToComment(formData, id);
            await CommentRepo.Update(id, postItem);

            return NoContent();
        }


        /// <summary>
        /// Deletes a specific Comment.
        /// </summary>
        /// <param name="id">id of Comment entity</param>     
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!CommentExists(id).Result)
            {
                return NotFound();
            }

            await CommentRepo.Remove(id);
            return NoContent();
        }


        private async Task<bool> CommentExists(int id)
        {
            var result = await CommentRepo.Find(id);

            if (result == null)
                return false;

            return true;
        }
    }
}
