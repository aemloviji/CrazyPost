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

            var posts = result.Value as List<PostEnhanceDTO>;
            Assert.AreEqual(dbPostList.Count, posts.Count);
        }


    }
}
