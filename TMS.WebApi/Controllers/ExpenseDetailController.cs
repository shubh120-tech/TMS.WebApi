using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using TMS.WebApi.Models;

namespace TMS.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class ExpenseDetailController : ApiController
    {

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public HttpResponseMessage GetEd()
        {
            using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
            {
                var data = tms.ExpenseDetails.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetEd(int id)
        {
            using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
            {
                var data = tms.ExpenseDetails.Find(id);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Expense Details of " + id + " not found");
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage GetETdByMrNumbe(string id)
        {
            using (TravelManagementSystemEntities ctx = new TravelManagementSystemEntities())
            {
                var data = ctx.ExpenseDetails.Where(t => t.MRNumber.ToString() == id).ToList();
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Empty");
                }
            }
        }


        [HttpPost]
        public HttpResponseMessage PostEd([FromBody] TMS.WebApi.Models.ExpenseDetail ed)
        {
            using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
            {
                try
                {
                    tms.ExpenseDetails.Add(ed);
                    tms.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        public HttpResponseMessage PutEd(int id, [FromBody] TMS.WebApi.Models.ExpenseDetail ed)
        {
            try
            {
                using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
                {
                    if (!ModelState.IsValid)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not a Valid Model");
                    var data = tms.ExpenseDetails.Find(id);
                    if (data == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Details not Found for id " + id);

                    }
                    else
                    {

                        data.ExpenseReportId = ed.ExpenseReportId;
                        data.MRNumber= ed.MRNumber;
                        data.PaymentType = ed.PaymentType;
                        data.ExpenseDate = ed.ExpenseDate;
                        data.ReimbursementAccountNo = ed.ReimbursementAccountNo;
                        data.ExpenseType = ed.ExpenseType;
                        data.ExpenseStatus = ed.ExpenseStatus;
                        data.AmountSpent = ed.AmountSpent;

                        tms.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public HttpResponseMessage DeleteEd(int id)
        {
            try
            {
                using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
                {
                    var data = tms.ExpenseDetails.Find(id);
                    if (data == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Details not Found for id" + id);
                    }
                    else
                    {
                        tms.ExpenseDetails.Remove(data);
                        tms.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, data);

                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}