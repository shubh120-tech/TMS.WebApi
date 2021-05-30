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
    public class EmployeeMasterController : ApiController
    {

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public HttpResponseMessage GetEM()
        {
            using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
            {
                var data = tms.EmployeeMasters.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
        }
        
        [HttpGet]
        public HttpResponseMessage GetEM(int id)
        {
            using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
            {
                var data = tms.TravelDetails.Find(id);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee of " + id + " not found");
                }
            }
        }
        [HttpPost]
        public HttpResponseMessage PostEM([FromBody] EmployeeMaster em)
        {
            using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
            {
                try
                {
                    tms.EmployeeMasters.Add(em);
                    tms.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
        public HttpResponseMessage PutEM(int id, [FromBody] EmployeeMaster em)
        {
            try
            {
                using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
                {
                    if (!ModelState.IsValid)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Not a Valid Model");
                    var data = tms.EmployeeMasters.Find(id);
                    if (data == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Details not Found for id " + id);

                    }
                    else
                    {
                        
                        data.EmployeeId = em.EmployeeId;
                        data.EmployeeFirstName = em.EmployeeFirstName;
                        data.EmployeeLastName = em.EmployeeLastName;
                        data.Location = em.Location;
                        data.ReimbursementAccountNo = em.ReimbursementAccountNo;
                        
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

        public HttpResponseMessage DeleteEM(int id)
        {
            try
            {
                using (TravelManagementSystemEntities tms = new TravelManagementSystemEntities())
                {
                    var data = tms.EmployeeMasters.Find(id);
                    if (data == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Details not Found for id" + id);
                    }
                    else
                    {
                        tms.EmployeeMasters.Remove(data);
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