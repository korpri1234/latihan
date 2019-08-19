using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDDHL.Models
{
    public class AuthenticateModel
    {
        public string PurePassword { get; set; }
        public string HashPassword { get; set; }
    }
}
