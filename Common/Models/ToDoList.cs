
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class ToDoList
    {
        /// <summary>
        /// Unique task id
        /// </summary>
        [Key]
        [Required]
        public int TaskId { get; set; }

        /// <summary>
        /// Task name, any non empty name
        /// </summary>
        
        public string TaskName { get; set; }

        /// <summary>
        /// Task desctiption
        /// </summary>
        public string TaskDescription { get; set; }

        /// <summary>
        /// Last updated timestamp.
        /// </summary>        
        public DateTime LastUpdated { get; set; }
    }
}