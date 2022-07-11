using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.Common
{
    public class Message
    {
        public int Id { get; set; }

        public string PassCode { get; set; }

        public string MsgId { get; set; }

        public DateTime CreatedAtUTC { get; set; }

        public DateTime? ReceivedAtUTC { get; set; } = null;

        public DateTime? DeletedAtUTC { get; set; } = null;

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public bool IsDeleted { get; set; }

        public string IpAddress { get; set; }

        public bool IsFirstTimeRead()
        {
            return ReceivedAtUTC == null;
        }
    }
}
