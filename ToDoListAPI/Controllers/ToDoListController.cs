using Common;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.Controllers
{
    [ApiController]

    public class ToDoListController : ControllerBase
    {

        private readonly ILogger<ToDoListController> _logger;
        private readonly IToDoListSL _toDoListSL;

        public ToDoListController(ILogger<ToDoListController> logger, IToDoListSL toDoListSL)
        {
            _logger = logger;
            _toDoListSL = toDoListSL;
        }

        /// <summary>
        /// Get list of all TODO items
        /// If Empty, save first and then call List
        /// </summary>
        /// <returns></returns>      
        [HttpGet]
        [Route("ToDoList/List")]
        [ProducesResponseType(typeof(List<ToDoList>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public ActionResult List()
        {
            List<ToDoList> toDoLists = new List<ToDoList>();
            try
            {
                toDoLists = _toDoListSL.ListTask();
                return Ok(toDoLists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Server Error");
            }

        }

        /// <summary>
        /// Save a to do item
        /// </summary>
        /// <param name="toDoList"></param>
        /// <returns>true if added/updated successfully, false if not saved</returns>
        [HttpPost]
        [Route("ToDoList/Save")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult SaveTask([FromBody] ToDoList toDoList)
        {
            bool result;
            if (!ValidateInputTask(toDoList))
                return BadRequest("Invalid input");
            try
            {
                result = _toDoListSL.SaveTask(toDoList);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Server Error");
            }

        }

        /// <summary>
        /// Remove item with given task id
        /// </summary>
        /// <param name="taskId">Id of the task to be removed</param>
        /// <returns>true if removed successfully, false if the item doesn't exist</returns>
        [HttpDelete]
        [Route("ToDoList/Remove")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult RemoveTask([Required] int taskId)
        {
            bool result;
            if (taskId <= 0)
                return BadRequest("Invalid input");
            try
            {
                result = _toDoListSL.RemoveTask(taskId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Server Error");
            }

        }


        #region Private Methods
        private bool ValidateInputTask(ToDoList toDoList)
        {
            if (toDoList == null || toDoList.TaskId <= 0 || string.IsNullOrEmpty(toDoList.TaskName))
            {
                return false;
            }
            return true;

        }
        #endregion
    }
}