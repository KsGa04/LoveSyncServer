using System.ComponentModel.DataAnnotations;

namespace LoveSyncServer.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public TimeSpan? Time { get; set; }
        public string? Category { get; set; }

        [Required]
        [EnumDataType(typeof(TaskStatus))]
        public TaskStatus Status { get; set; } = TaskStatus.Active;

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public int CoupleId { get; set; }
        public Couple Couple { get; set; }
    }

    public enum TaskStatus
    {
        Active,
        Completed
    }

}
