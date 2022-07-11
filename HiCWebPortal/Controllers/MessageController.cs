using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebPortal.Common;
using WebPortal.Models;
using System.Web.Mvc;
using System.Web;
using WebPortal.DB;
using System.Web.SessionState;

namespace WebPortal.Controllers
{
    public class MessageController : ApiController, IRequiresSessionState
    {
        public IHttpActionResult Get(string id)
        {
            ApiResponse response = new ApiResponse();

            MessageAPIModel msg = MessageManager.Instance.GetMessage(id);
            string link = SettingRepository.GetSetting("btn_more");

            if(msg != null)
            {
                response.Data = new {
                    Link = link,
                    Msg = msg
                };
            }
            
            return Json(response);
        }

        public IHttpActionResult Post(MessageSubmitFormModel submission)
        {
            ApiResponse response = new ApiResponse();

            if (!ModelState.IsValid)
            {
                response.Code = Common.StatusCode.InvalidFormat;
                return Json(response);
            }

            var context = new HttpContextWrapper(HttpContext.Current);
            HttpRequestBase request = context.Request;

            #region check 13 age (disable)
            /*  
            bool isAgePass = true;
            
            if (context.Session != null && context.Session["age"] != null)
            {
                isAgePass = (((string)context.Session["age"]) == "too young") ? false : true;
            }
            else
            {
                isAgePass = ageVerification(submission.Year, submission.Month, submission.Day, 13);
                if (!isAgePass)
                {
                    context.Session["age"] = "too young";
                }
            }

            if (!isAgePass)
            {
                response.Code = Common.StatusCode.InvalidAge;
                return Json(response);
            }
           */
            #endregion

            Message msg = new Message
            {
                PassCode = submission.PassCode,
                MsgId = submission.MsgId,
                CreatedAtUTC = DateTime.UtcNow,
                Sender = submission.Sender,
                Receiver = submission.Receiver,
                IpAddress = Kitchen.AppHelper.GetIPAddress(request),
            };

            string error_message = "";
            response.Code = MessageManager.Instance.AddMessage(msg, ref error_message);
            response.Message = error_message;
            response.Data = new { Link = SettingRepository.GetSetting("btn_more") };

            return Json(response);
        }


        private bool ageVerification(int yyyy, int mm, int dd, int allowAge)
        {

            DateTime tmp = DateTime.UtcNow.AddHours(8);
            DateTime HKNow = new DateTime(tmp.Year, tmp.Month, tmp.Day);
            DateTime DOB = new DateTime(yyyy, mm, dd);

            int age = HKNow.Year - DOB.Year;

            //note : the time comparison - check date only, and should skip the current time
            if (HKNow < DOB.AddYears(age)) age--;

            return age >= allowAge;

        }


    }
}
