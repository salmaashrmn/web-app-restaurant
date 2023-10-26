using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationRestaurant.Models
{
    public class Food
    {
        [Key]
        public int food_id { get; set; }
        public string food_name { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
        public List<Transactions> Transactions { get; set; }
    }
}
