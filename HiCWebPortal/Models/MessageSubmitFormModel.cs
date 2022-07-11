using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebPortal.Models
{
    public class MessageSubmitFormModel : MessageAPIModel
    {
        [Required]
        [RegularExpression(@"[a-zA-Z0-9]{3,20}$",
         ErrorMessage = "Characters are not allowed.")]
        public string PassCode { get; set; }
        /*
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        */
    }
}
