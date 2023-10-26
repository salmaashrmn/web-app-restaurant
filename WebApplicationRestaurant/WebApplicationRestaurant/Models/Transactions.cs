using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplicationRestaurant.Models
{
    public class Transactions
    {
        [Key]
        public int transaction_id { get; set; }
        public int customer_id { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }

        public int food_id { get; set; }

        [JsonIgnore]
        public Food Food { get; set; }

        public int qty { get; set; }
        public int total_price { get; set; }
        public DateTime transaction_date { get; set; }
    }
}
