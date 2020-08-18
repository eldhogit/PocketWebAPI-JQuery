using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pocket.WebAPI.Models
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [
            Required(ErrorMessage ="Enter Price"),
            RegularExpression("([1-9][0-9]*)", 
            ErrorMessage = "Please enter valid Price")
        ]
        public int Price { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
    }
}