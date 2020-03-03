using System;
using System.Collections.Generic;
using System.Text;

namespace DSCC_CW1_5591.Models
{
   public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public virtual Category CategoryName {get; set; }
    }
}
