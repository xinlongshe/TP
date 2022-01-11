using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_4._0.Data.Model
{
    public class Trainer
    {
        public long Id { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required]
        public string KindOfTrainer { get; set; }
        public string Certificate { get; set; }
        public string DescribeYourself { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime CreateDateTime { get; set; }

    }
}
