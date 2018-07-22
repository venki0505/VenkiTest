using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;

namespace Venki_Tech_Test.Models
{
    [Table("STATION")]
    public class Station
    {
        public int STATION_ID { get; set; }
        public string STATION_NAME { get; set; }
    }
}