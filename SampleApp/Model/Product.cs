using System.ComponentModel.DataAnnotations;

namespace SampleApp.Model
{
    public class Product
    {
        [Required]
        [Key]

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }


    }

}
