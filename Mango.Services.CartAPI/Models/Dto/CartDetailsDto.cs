using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.CartAPI.Models.Dto
{
    public class CartDetailsDto
    {
        [Key]
        public int CartDetailsId { get; set; }

        public int CartHeaderId { get; set; }
        public virtual CartHeaderDto CartHeader { get; set; }
        public string ProductId { get; set; }
        public virtual ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}
