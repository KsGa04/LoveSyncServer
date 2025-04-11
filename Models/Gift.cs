using System.ComponentModel.DataAnnotations;

namespace LoveSyncServer.Models
{
    public class Gift
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }
        public string? Link { get; set; }

        [EnumDataType(typeof(PriorityLevel))]
        public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;

        public string? ImageUrl { get; set; }
        public bool IsBought { get; set; } = false;

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public int CoupleId { get; set; }
        public Couple Couple { get; set; }
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }

}
