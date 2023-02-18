﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.CartAPI.Models.Dto
{
    public class ProductDto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 1000)]
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }

    }
}
