using System;
using System.Text.Json.Serialization;

namespace Mailprotector_Api.Models
{
    public class Customer
    {
        public int id { get; set; }

        public string name { get; set; }

        public Customer reseller { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }
    }
}
