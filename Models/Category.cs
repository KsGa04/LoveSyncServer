using System.ComponentModel.DataAnnotations;

namespace LoveSyncServer.Models
{
    public class Category
    {
        [Key]
        public string Name { get; set; }

        public string? Color { get; set; }
        public string? Icon { get; set; }
    }

}
