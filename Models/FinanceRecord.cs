using System.ComponentModel.DataAnnotations;

namespace LoveSyncServer.Models
{
    public class FinanceRecord
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Category { get; set; }

        public int? PayerId { get; set; }
        public User? Payer { get; set; }

        [EnumDataType(typeof(OwnerType))]
        public OwnerType OwnerType { get; set; } = OwnerType.Both;

        public int CoupleId { get; set; }
        public Couple Couple { get; set; }
    }

    public enum OwnerType
    {
        Me,
        Partner,
        Both
    }

}
