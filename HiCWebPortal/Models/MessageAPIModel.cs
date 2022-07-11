using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebPortal.Models
{
    public class MessageAPIModel
    {
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string MsgId { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        public string Receiver { get; set; }
    }
}
