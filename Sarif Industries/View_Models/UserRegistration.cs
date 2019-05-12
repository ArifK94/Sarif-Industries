using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sarif_Industries.Models;

namespace Sarif_Industries.View_Models
{
    public class UserRegistration
    {
        public User User { get; set; }

        public IEnumerable<Country> Country { get; set; }
    }
}