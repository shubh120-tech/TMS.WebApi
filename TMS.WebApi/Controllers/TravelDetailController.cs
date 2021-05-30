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
    public class TravelDetailController : ApiController
    {

        // GET: TravelDetails
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public HttpResponseMessage GetTd()
        {
            using (TravelManagementSystemEntities ctx = new TravelManagementSystemEntities())
            {
                var data = ctx.TravelDetails.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }
        
        [HttpGet]
        
        public HttpResponseMessage GetTd(int id)
        {
            using (TravelManagementSystemEntities ctx = new TravelManagementSystemEntities())
            {
                var data = ctx.TravelDetails.Where(t=>t.MRNumber==id).FirstOrDefault();
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Travel Details of " + id + " not found");
                }
            }
        }

        public HttpResponseMessage GetTdByEmployeeId(int id)
        {
            using (TravelManagementSystemEntities ctx = new TravelManagementSystemEntities())
            {
                var data = ctx.TravelDetails.Where(t => t.EmployeeId == id).ToList();
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
        public HttpResponseMessage PostTd([FromBody] TravelDetail travelDetail)
        {
            if (!ModelState.IsValid)
            {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable,"Check Fields");

            }
            using (TravelManagementSystemEntities ctx = new TravelManagementSystemEntities())
            {
                try
                {
                    ctx.TravelDetails.Add(travelDetail);
                    ctx.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        public HttpResponseMessage PutTd(int id, [FromBody] TravelDetail travelDetail)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable,"not");

            }
            try
            {
                using (TravelManagementSystemEntities ctx = new TravelManagementSystemEntities())
                {
                    var data = ctx.TravelDetails.Where(t=>t.MRNumber==id).FirstOrDefault();

                    if (data == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Details not Found for id " + id);

                    }
                    else
                    {
                        
                        data.EmployeeId = travelDetail.EmployeeId;
                        data.Applydate = travelDetail.Applydate;
                        data.ReasonForTravel = travelDetail.ReasonForTravel;
                        data.TravelDate = travelDetail.TravelDate;
                        data.TravelMode = travelDetail.TravelMode;
                        data.FromCity = travelDetail.FromCity;
                        data.ToCity = travelDetail.ToCity;
                        data.TravelDuration = travelDetail.TravelDuration;
                        data.Status = travelDetail.Status;
                        ctx.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public HttpResponseMessage DeleteTd(int id)
        {
            try
            {
                using (TravelManagementSystemEntities ctx = new TravelManagementSystemEntities())
                {
                    var data = ctx.TravelDetails.Find(id);
                    if (data == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Details not Found for id" + id);
                    }
                    else
                    {
                        ctx.TravelDetails.Remove(data);
                        ctx.SaveChanges();
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