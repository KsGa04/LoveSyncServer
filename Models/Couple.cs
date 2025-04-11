namespace LoveSyncServer.Models
{
    public class Couple
    {
        public int Id { get; set; }
        public string InviteCode { get; set; }

        public DateTime Created_at { get; set; } = DateTime.UtcNow;

        public ICollection<User>? users { get; set; }
        public ICollection<TaskItem>? Tasks { get; set; }
        public ICollection<ShoppingItem>? ShoppingItems { get; set; }
        public ICollection<Gift>? Gifts { get; set; }
        public ICollection<FinanceRecord>? FinanceRecords { get; set; }
    }
}

