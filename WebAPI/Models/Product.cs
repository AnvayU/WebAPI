using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebAPI.Models
{
    public class Product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productDescr { get; set; }
        public int productRating { get; set; }
        public string productReview { get; set; }

    }
}