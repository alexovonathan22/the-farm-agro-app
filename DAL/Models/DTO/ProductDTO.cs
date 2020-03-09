using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models.DTO
{
    public class ProductDTO
    {
        //public int ProductId { get; set; }
       
        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        [Required]
        public int QuantityInStock { get; set; }
        /*public string 
         price per unit
             */
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Details { get; set; }

        //public int TotalSales { get; set; }
        //public bool InStock { get; set; }

        //[ForeignKey(nameof(Category))]
        //public int CatId { get; set; }
        //public Category Category { get; set; }
    }
}
