using Common;
using DataAccessLayer;
using Moq;
using Xunit;

namespace ServiceLayer.Tests
{
    /// <summary>
    /// Unit tests covering methods in Service layer
    /// </summary>
    public class ToDoListSLTests
    {

        private readonly Mock<IToDoListDL> _toDoListDL;
        public ToDoListSLTests()
        {
            _toDoListDL = new Mock<IToDoListDL>();
            Initialize();
        }

        [Fact]
        public void ToDoListSaveTest()
        {
            IToDoListSL toDoListSL = new ToDoListSL(_toDoListDL.Object);
            ToDoList toDoList = new ToDoList { TaskId = 1, TaskName = "Task1", TaskDescription = "Task1 Description" };
            bool actual = toDoListSL.SaveTask(toDoList);
            Assert.True(actual);
        }

        [Fact]
        public void ToDoListDeleteTest()
        {
            IToDoListSL toDoListSL = new ToDoListSL(_toDoListDL.Object);
            bool actual = toDoListSL.RemoveTask(1);
            Assert.True(actual);
        }

        [Fact]
        public void ToDoListTest()
        {
            IToDoListSL toDoListSL = new ToDoListSL(_toDoListDL.Object);
            List<ToDoList> actualResult = toDoListSL.ListTask();
            Assert.Equal(1, actualResult[0].TaskId);
            Assert.Equal("Task1", actualResult[0].TaskName);
            Assert.Equal("Task1 Description", actualResult[0].TaskDescription);
        }

        private void Initialize()
        {
            //Mock DL methods
            ToDoList toDoList = new ToDoList { TaskId = 1, TaskName = "Task1", TaskDescription = "Task1 Description" };
            List<ToDoList> toDoLists = new List<ToDoList>();
            toDoLists.Add(toDoList);
            _toDoListDL.Setup(x => x.ListTask()).Returns(toDoLists);
            _toDoListDL.Setup(x => x.SaveTask(It.IsAny<ToDoList>())).Returns(true);
            _toDoListDL.Setup(x => x.RemoveTask(1)).Returns(true);
        }


    }
}