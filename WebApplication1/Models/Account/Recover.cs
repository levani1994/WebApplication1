using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Account
{
    public class Recover
    {
        [Required (ErrorMessage ="dedede")]
        public string EmailToBeRecover { get; set; }
        public string NewPassword { get; set; }
    }
}