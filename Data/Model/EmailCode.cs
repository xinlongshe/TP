using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_4._0.Data.Model
{
    public class EmailCode
    {
        public string SentCode { get; set; }
        [Required]
        public string ValueCode { get; set; }
    }
}
