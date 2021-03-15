using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DepsWebApp.Contracts
{

    /// <summary>
    /// Transportation model for registration of new users
    /// </summary>
    public class AccountDto
    {
        /// <summary>
        /// Received login
        /// </summary>
        [MinLength(6)]
        [Required(AllowEmptyStrings = false)]
        public string Login { get; set; }
        /// <summary>
        /// Received password
        /// </summary>
        [MinLength(6)]
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
