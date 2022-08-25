using Common;

namespace DataAccessLayer
{
    public interface IToDoListDL
    {

        public bool SaveTask(ToDoList toDoListEntity);
        public List<ToDoList> ListTask();
        public bool RemoveTask(int taskId);
    }
}