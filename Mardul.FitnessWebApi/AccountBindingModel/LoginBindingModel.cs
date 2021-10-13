using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mardul.FitnessWebApi.AccountBindingModel
{
    public class LoginBindingModel
    {

        [Required]        
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    

    }
}
