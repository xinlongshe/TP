using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_4._0.Data.Model
{
    public class PrivacyUser
    {
        public long Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string UserType { get; set; }

        public long FKId { get; set; }
    }
}
