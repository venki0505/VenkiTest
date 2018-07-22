using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;

namespace Venki_Tech_Test.Models
{
    [Table("MARKET")]
    public class Market
    {
        public int MARKET_ID { get; set; }
        public string MARKET_NAME { get; set; }
    }
}