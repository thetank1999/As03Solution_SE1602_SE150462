using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebAppSqlServerDataProvider.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public override string ToString() {
            return this.CategoryName;
        }
    }

    
}
