using Common;
using DataAccessLayer;

namespace ServiceLayer
{
    /// <summary>
    /// Service Layer calls methods in Data Access
    /// </summary>
    public class ToDoListSL : IToDoListSL
    {
        private readonly IToDoListDL _toDoListDL;

        public ToDoListSL(IToDoListDL toDoListDL)
        {
            _toDoListDL = toDoListDL;
        }
        public List<ToDoList> ListTask()
        {
            try
            {
                return _toDoListDL.ListTask();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool RemoveTask(int taskId)
        {
            try
            {
                return _toDoListDL.RemoveTask(taskId);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool SaveTask(ToDoList toDoList)
        {
            try
            {
                toDoList.LastUpdated = DateTime.Now;
                return _toDoListDL.SaveTask(toDoList);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}