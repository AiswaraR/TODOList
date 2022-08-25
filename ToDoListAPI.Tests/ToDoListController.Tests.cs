using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ServiceLayer;
using ToDoListAPI.Controllers;
using Xunit;

namespace ToDoListAPI.Tests
{
    /// <summary>
    /// Unit tests covering methods in ToDoListController
    /// </summary>
    public class ToDoListControllerTests
    {
        private readonly Mock<IToDoListSL> _toDoListSL;
        private readonly Mock<ILogger<ToDoListController>> _logger;
        private readonly ToDoListController _toDoListController;

        public ToDoListControllerTests()
        {
            _toDoListSL = new Mock<IToDoListSL>();
            _logger = new Mock<ILogger<ToDoListController>>();
            _toDoListController = new ToDoListController(_logger.Object, _toDoListSL.Object);
            Initialize();
        }


        [Fact]
        public void InputNullValidationTest()
        {
            ToDoList toDoList = null  ;
            var result = _toDoListController.SaveTask(toDoList);
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            string actual = (string)(result as ObjectResult)?.Value;
            Assert.Equal("Invalid input", actual);
        }

        [Fact]
        public void InvalidTaskIdValidationTest()
        {
            ToDoList toDoList = new ToDoList { TaskId = 0 };
            var result = _toDoListController.SaveTask(toDoList);
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            string actual = (string)(result as ObjectResult)?.Value;
            Assert.Equal("Invalid input", actual);
        }


        [Fact]
        public void ListTest()
        {
            var result = _toDoListController.List();
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            List<ToDoList> actual = (List<ToDoList>)(result as ObjectResult)?.Value;
            Assert.Equal(1, actual[0].TaskId);
            Assert.Equal("Task1", actual[0].TaskName);
            Assert.Equal("Task1 Description", actual[0].TaskDescription);
        }

        [Fact]
        public void SaveTest()
        {
            ToDoList toDoList = new ToDoList { TaskId = 1, TaskName = "Task1", TaskDescription = "Task1 Description" };
            var result = _toDoListController.SaveTask(toDoList);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            bool actual = (bool)(result as ObjectResult)?.Value;
            Assert.True(actual);

        }

        [Fact]
        public void RemoveTest()
        {
            var result = _toDoListController.RemoveTask(1);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            bool actual = (bool)(result as ObjectResult)?.Value;
            Assert.True(actual);

        }

        private void Initialize()
        {
            //Mock Service Layer methods
            ToDoList toDoList = new ToDoList { TaskId = 1, TaskName = "Task1", TaskDescription = "Task1 Description" };
            List<ToDoList> toDoLists = new List<ToDoList>();
            toDoLists.Add(toDoList);
            _toDoListSL.Setup(x => x.ListTask()).Returns(toDoLists);
            _toDoListSL.Setup(x => x.SaveTask(It.IsAny<ToDoList>())).Returns(true);
            _toDoListSL.Setup(x => x.RemoveTask(1)).Returns(true);
        }
    }
}