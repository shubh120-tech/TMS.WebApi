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
    public class ExpenseRejectedController : ApiController
    {

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public HttpResponseMessage GetEr()
        {
            using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
            {
                var data = tms.ExpenseRejecteds.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetEr(int id)
        {
            using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
            {
                var data = tms.ExpenseRejecteds.Find(id);
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
        [HttpPost]
        public HttpResponseMessage PostEr([FromBody] TMS.WebApi.Models.ExpenseRejected er)
        {
            using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
            {
                try
                {
                    tms.ExpenseRejecteds.Add(er);
                    tms.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        public HttpResponseMessage PutEr(int id, [FromBody] TMS.WebApi.Models.ExpenseRejected er)
        {
            try
            {
                using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
                {
                    if (!ModelState.IsValid)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not a Valid Model");
                    var data = tms.ExpenseRejecteds.Find(id);
                    if (data == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Details not Found for id " + id);

                    }
                    else
                    {

                        data.ExpenseReportId = er.ExpenseReportId;
                        data.RejectionDate= er.RejectionDate;
                        data.ReasonForRejection = er.ReasonForRejection;


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

        public HttpResponseMessage DeleteEr(int id)
        {
            try
            {
                using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
                {
                    var data = tms.ExpenseAccepteds.Find(id);
                    if (data == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Details not Found for id" + id);
                    }
                    else
                    {
                        tms.ExpenseAccepteds.Remove(data);
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