using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectWebMVC5.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberInStock { get; set; }
        public int CategoryID { get; set; }
        public Category Categories { get; set; }
    }
}