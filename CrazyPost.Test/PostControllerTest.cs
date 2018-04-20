using CrazyPost.Context;
using CrazyPost.Controllers;
using CrazyPost.Models;
using CrazyPost.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrazyPost.Test
{
    [TestClass]
    public class PostControllerTest
    {
        [TestMethod]
        public async Task GetAll_ShouldReturnAllPosts()
        {
            var mock = new Mock<IPostRepository>();
            var dbPostList = DbInitializer.GenerateListOfPost();
            mock.Setup(repo => repo.GetAll()).Returns(Task.FromResult(dbPostList as IEnumerable<Post>));
            var controller = new PostController(mock.Object);

            var actionResult = await controller.GetAll();
            Assert.IsNotNull(actionResult);

            OkObjectResult result = actionResult as OkObjectResult;
            Assert.IsNotNull(result);

            var resultValue = result.Value as List<PostEnhanceDTO>;
            Assert.AreEqual(dbPostList.Count, resultValue.Count);
        }


        [DataTestMethod]
        [DataRow(100000)]
        public async Task GetById_ShouldReturn_NotFound_ForAbsentItem(int id)
        {
            var mock = new Mock<IPostRepository>();
            mock.Setup(repo => repo.Find(id)).Returns(Task.FromResult((Post)null));
            var controller = new PostController(mock.Object);

            var actionResult = await controller.GetById(id);
            Assert.IsNotNull(actionResult);

            NotFoundResult result = actionResult as NotFoundResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetById_ShouldReturn_Success_ForSinglePost()
        {
            var dbPost = DbInitializer.GenerateListOfPost()[0];

            var mock = new Mock<IPostRepository>();
            mock.Setup(repo => repo.Find(dbPost.Id)).Returns(Task.FromResult(dbPost));
            var controller = new PostController(mock.Object);

            var actionResult = await controller.GetById(dbPost.Id);
            Assert.IsNotNull(actionResult);

            OkObjectResult result = actionResult as OkObjectResult;
            Assert.IsNotNull(result);

            var resultValue = result.Value as PostEnhanceDTO;
            Assert.AreEqual(dbPost.Text, resultValue.Text);
        }


        [TestMethod]
        public async Task Create_ShouldReturn_BadRequest_ForInvalidModel()
        {           
            var mock = new Mock<IPostRepository>();          
            var controller = new PostController(mock.Object);
            controller.ModelState.AddModelError("error", "erro occured");

            var actionResult = await controller.Create(null);
            Assert.IsNotNull(actionResult);

            BadRequestObjectResult result = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(result);
        }

    }
}
