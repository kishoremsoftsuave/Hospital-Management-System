using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;
        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,Reception")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Appointment = await _service.GetById(id);
            return Ok(Appointment);
        }

        [Authorize(Roles = "Admin,Reception")]
        [HttpPost]
        public async Task<IActionResult> Create(AppointmentDTO appointmentDTO)
        {
            await _service.Create(appointmentDTO);
            return Ok("Appointment Created Successfully");
        }

        [Authorize(Roles = "Admin,Reception")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AppointmentDTO appointmentDTO)
        {
            await _service.Update(id, appointmentDTO);
            return Ok("Appointment Updated Successfully");
        }

        [Authorize(Roles = "Admin,Doctor,Reception")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> Patch(int id, AppointmentStatusDTO dto)
        {
            await _service.Patch(id, dto);
            return Ok("Appointment status updated");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Appointment Deleted Successfully");
        }
    }
}
