using Common;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ToDoListDL : IToDoListDL
    {
        /// <summary>
        /// List all ToDo List
        /// </summary>
        /// <returns></returns>
        public List<ToDoList> ListTask()
        {
            List<ToDoList> toDoLists = new List<ToDoList>();
            try
            {
                using (ToDoListDBContext context = new ToDoListDBContext(GetDbContextOptions()))
                {
                    toDoLists = context.ToDoListEntities.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return toDoLists;
        }

        /// <summary>
        /// Remove task based on task id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>

        public bool RemoveTask(int taskId)
        {
            bool result;
            try
            {
                using (ToDoListDBContext context = new ToDoListDBContext(GetDbContextOptions()))
                {
                    //If the item exists with the taskid, remove else return false
                    ToDoList toDoListRemove = context.ToDoListEntities.SingleOrDefault(x => x.TaskId == taskId);
                    if (toDoListRemove != null)
                    {
                        context.ToDoListEntities.Remove(toDoListRemove);
                        context.SaveChanges();
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }                   
                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Save or update task
        /// </summary>
        /// <param name="toDoListEntity"></param>
        /// <returns></returns>

        public bool SaveTask(ToDoList toDoListEntity)
        {
            bool result;
            try
            {
                using (ToDoListDBContext context = new ToDoListDBContext(GetDbContextOptions()))
                {
                    //If already exists update, else add
                    if (context.ToDoListEntities.Any(x => x.TaskId == toDoListEntity.TaskId))
                    {
                        context.ToDoListEntities.Update(toDoListEntity);
                    }
                    else
                    {
                        context.ToDoListEntities.AddRange(toDoListEntity);
                    }
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// Return Options for initializing DB context with In memory Database
        /// </summary>
        /// <returns></returns>

        private DbContextOptions<ToDoListDBContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<ToDoListDBContext>().UseInMemoryDatabase(databaseName: "test").Options;
        }
    }
}