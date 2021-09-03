using AppointmentScheduling.Models.ViewModels;
using AppointmentScheduling.Services;
using AppointmentScheduling.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppointmentScheduling.Controllers.Api
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;

        public AppointmentApiController(IAppointmentService appointmentService , IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentVM data)
        {
            CommanResponse<int> commanResponse = new CommanResponse<int>();
            try
            {
                commanResponse.status = _appointmentService.AddUpdate(data).Result;
                if (commanResponse.status == 1)
                {
                    commanResponse.message = Helper.appointmentUpdated;
                }
                if (commanResponse.status == 2)
                {
                    commanResponse.message = Helper.appointmentAdded;
                }
            }
            catch (Exception e)
            {
                commanResponse.message = e.Message;
                commanResponse.status = Helper.failure_code;
            }
            return Ok(commanResponse);
        }

        [HttpGet]
        [Route("GetCalenderData")]
        public IActionResult GetCalenderData(string doctorId)
        {
            CommanResponse<List<AppointmentVM>> commanResponse = new CommanResponse<List<AppointmentVM>>();
            try
            {
                if (role == Helper.Patient)
                {
                    commanResponse.dataenum = _appointmentService.PatientsEventsById(loginUserId);
                    commanResponse.status = Helper.succcess_code;
                }
                else if (role == Helper.Doctor)
                {
                    commanResponse.dataenum = _appointmentService.DoctorsEventsById(loginUserId);
                    commanResponse.status = Helper.succcess_code;
                }
                else 
                {
                    commanResponse.dataenum = _appointmentService.DoctorsEventsById(doctorId);
                    commanResponse.status = Helper.succcess_code;
                }
            }
            catch (Exception e)
            {
                commanResponse.message = e.Message;
                commanResponse.status = Helper.failure_code;
            }
            return Ok(commanResponse);
        }


        [HttpGet]
        [Route("GetCalenderDataById/{id}")]
        public IActionResult GetCalenderDataById(int id)
        {
            CommanResponse<AppointmentVM> commanResponse = new CommanResponse<AppointmentVM>();
            try
            { 
                commanResponse.dataenum = _appointmentService.GetById(id);
                commanResponse.status = Helper.succcess_code; 
            }
            catch (Exception e)
            {
                commanResponse.message = e.Message;
                commanResponse.status = Helper.failure_code;
            }
            return Ok(commanResponse);
        }
    }
}
