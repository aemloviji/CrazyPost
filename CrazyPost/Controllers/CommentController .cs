using CrazyPost.Models;
using CrazyPost.Repository;
using CrazyPost.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrazyPost.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        public ICommentRepository CommentRepo { get; set; }

        public CommentController(ICommentRepository _repo)
        {
            CommentRepo = _repo;
        }

        [HttpGet]
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

        [HttpGet("{id}", Name = "GetComment")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await CommentRepo.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            var resultModel = Convertor.ToCommentEnhanceDTO(item);
            return Ok(resultModel);
        }

        [HttpPost]
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

            return CreatedAtRoute("CommentRepo", new { Controller = "Comment", id = postItem.Id }, postItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddOrUpdateCommentDTO formData)
        {
            if (formData == null)
            {
                return BadRequest();
            }

            var contactObj = await CommentRepo.Find(id);
            if (contactObj == null)
            {
                return NotFound();
            }

            var postItem = Convertor.ToComment(formData, id);
            await CommentRepo.Update(id, postItem);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await CommentRepo.Remove(id);
            return NoContent();
        }
    }
}
