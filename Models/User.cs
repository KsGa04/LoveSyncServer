using System.ComponentModel.DataAnnotations;

namespace LoveSyncServer.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password_hash { get; set; }

        public int? Couple_id { get; set; }
        public Couple? Couple { get; set; }

        public int Points { get; set; } = 0;
        public bool Theme { get; set; } = false;
        public bool Notifications_enabled { get; set; } = true;

        public ICollection<TaskItem>? Tasks { get; set; }
        public ICollection<Gift>? Gifts { get; set; }
        public ICollection<FinanceRecord>? FinanceRecords { get; set; }
    }

}
