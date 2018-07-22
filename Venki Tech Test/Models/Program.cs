using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Venki_Tech_Test.Models
{
    [Table("PROGRAM")]
    public class Program
    {       
        
        public int PROGRAM_ID { get; set; }
        public int STATION_ID { get; set; }
        public string PROGRAM_NAME { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
        public DateTime FLIGHT_DATE { get; set; }
        
    }
}