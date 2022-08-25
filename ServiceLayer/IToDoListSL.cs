using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public interface IToDoListSL
    {
        public bool SaveTask(ToDoList toDoList);
        public List<ToDoList> ListTask();

        public bool RemoveTask(int taskId);
    }
}