using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Description { get; set; }
        public int Rating { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string CreatedBy { get; set; }
   
        public Guid CreatedOn { get; set; } = Guid.NewGuid();
        [Required]
        public string UpdatedBy { get; set; }

        public Guid UpdatedOn { get; set; } = Guid.NewGuid();
        public string Status { get; set; } = "Enable";
        public bool IsDeleted { get; set; }
    }
}