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
    public class ExpenseAcceptedController : ApiController
    {

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public HttpResponseMessage GetEa()
        {
            using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
            {
                var data = tms.ExpenseAccepteds.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetEa(int id)
        {
            using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
            {
                var data = tms.ExpenseAccepteds.Find(id);
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
        public HttpResponseMessage PostEa([FromBody] TMS.WebApi.Models.ExpenseAccepted ea)
        {
            using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
            {
                try
                {
                    tms.ExpenseAccepteds.Add(ea);
                    tms.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        public HttpResponseMessage PutEa(int id, [FromBody] TMS.WebApi.Models.ExpenseAccepted ea)
        {
            try
            {
                using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
                {
                    if (!ModelState.IsValid)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not a Valid Model");
                    var data = tms.ExpenseAccepteds.Find(id);
                    if (data == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Details not Found for id " + id);

                    }
                    else
                    {

                        data.ExpenseReportId = ea.ExpenseReportId;
                        data.AmountPaid = ea.AmountPaid;
                        data.PaymentDate = ea.PaymentDate;
                        

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

        public HttpResponseMessage DeleteEa(int id)
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