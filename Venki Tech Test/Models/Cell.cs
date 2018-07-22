using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;

namespace Venki_Tech_Test.Models
{
    [Table("CELL")]
    public class Cell
    {
        public int CELL_ID { get; set; }
        public string CELL { get; set; }
    }
}