using CrazyPost.Context;
using CrazyPost.Controllers;
using CrazyPost.Models;
using CrazyPost.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrazyPost.Test
{
    [TestClass]
    public class CommentControllerTest
    {
        [TestMethod]
        public async Task GetAll_ShouldReturnAllComments()
        {
            var mock = new Mock<ICommentRepository>();
            var dbList = DbInitializer.GenerateListOfComment();
            mock.Setup(repo => repo.GetAll()).Returns(Task.FromResult(dbList as IEnumerable<Comment>));
            var controller = new CommentController(mock.Object);

            var actionResult = await controller.GetAll();
            Assert.IsNotNull(actionResult);

            OkObjectResult result = actionResult as OkObjectResult;
            Assert.IsNotNull(result);

            var resultValue = result.Value as List<CommentEnhanceDTO>;
            Assert.AreEqual(dbList.Count, resultValue.Count);
        }


        [DataTestMethod]
        [DataRow(100000)]
        public async Task GetById_ShouldReturn_NotFound_ForAbsentItem(int id)
        {
            var mock = new Mock<ICommentRepository>();
            mock.Setup(repo => repo.Find(id)).Returns(Task.FromResult((Comment)null));
            var controller = new CommentController(mock.Object);

            var actionResult = await controller.GetById(id);
            Assert.IsNotNull(actionResult);

            NotFoundResult result = actionResult as NotFoundResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetById_ShouldReturn_Success_ForSingleComment()
        {
            var dbComment = DbInitializer.GenerateListOfComment()[0];

            var mock = new Mock<ICommentRepository>();
            mock.Setup(repo => repo.Find(dbComment.Id)).Returns(Task.FromResult(dbComment));
            var controller = new CommentController(mock.Object);

            var actionResult = await controller.GetById(dbComment.Id);
            Assert.IsNotNull(actionResult);

            OkObjectResult result = actionResult as OkObjectResult;
            Assert.IsNotNull(result);

            var resultValue = result.Value as CommentEnhanceDTO;
            Assert.AreEqual(dbComment.Text, resultValue.Text);
        }


        [TestMethod]
        public async Task Create_ShouldReturn_BadRequest_ForInvalidModel()
        {
            var mock = new Mock<ICommentRepository>();
            var controller = new CommentController(mock.Object);
            controller.ModelState.AddModelError("error", "erro occured");

            var actionResult = await controller.Create(null);
            Assert.IsNotNull(actionResult);

            BadRequestResult result = actionResult as BadRequestResult;
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public async Task Create_ShouldReturn_CreatedComment()
        {
            const string TEXT = "Unit Test comments goes here...";

            #region Arrange
            int id = 1;
            var testComment = new Comment()
            {
                Id = 1,
                Text = TEXT,
                PostId = 1,
                InsertDate = DateTime.Now,
            };

            var mockRepo = new Mock<ICommentRepository>();
            mockRepo.Setup(repo => repo.Find(id))
                .Returns(Task.FromResult(testComment));
            var controller = new CommentController(mockRepo.Object);

            var newComment = new AddOrUpdateCommentDTO()
            {
                Text = TEXT,
                PostId = 1,
            };
            mockRepo.Setup(repo => repo.Add(testComment))
                .Returns(Task.CompletedTask)
                .Verifiable();
            #endregion


            var actionResult = await controller.Create(newComment);
            Assert.IsInstanceOfType(actionResult, typeof(CreatedAtRouteResult));

            var okResult = actionResult as CreatedAtRouteResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(Comment));
            Assert.AreEqual(TEXT, (okResult.Value as Comment).Text);
        }

    }
}
