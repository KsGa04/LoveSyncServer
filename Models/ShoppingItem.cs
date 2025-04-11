using System.ComponentModel.DataAnnotations;

namespace LoveSyncServer.Models
{
    public class ShoppingItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Quantity { get; set; } = 1;
        public string? Category { get; set; }
        public bool IsBought { get; set; } = false;

        public int CoupleId { get; set; }
        public Couple Couple { get; set; }
    }

}
