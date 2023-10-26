using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationRestaurant.Models
{
    public class Customer
    {
        [Key]
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public List<Transactions> Transactions { get; set; }
    }
}
