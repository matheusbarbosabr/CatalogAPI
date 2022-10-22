using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalogAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "The name must have at most {1} and at least {2}", MinimumLength = 5)]
        public string? Name { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The description must have at most {1} characters")]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 1000, ErrorMessage = "The price must be between {1} e {2}")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string? ImageUrl { get; set; }

        public float Inventory { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
