using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.Common
{
    public class ApiResponse
    {
        public StatusCode Code { get; set; } = StatusCode.Success;

        public string Message { get; set; }

        public Object Data { get; set; }

    }
}
