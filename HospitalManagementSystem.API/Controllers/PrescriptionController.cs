using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _service;
        public PrescriptionController(IPrescriptionService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());

        }

        [Authorize(Roles = "Admin,Doctor,patient")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var prescription = await _service.GetById(id);
            return Ok(prescription);
        }
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public async Task<IActionResult> Create(PrescriptionDTO prescriptionDTO)
        {
            await _service.Create(prescriptionDTO);
            return Ok("Prescription Created Successfully");
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, PrescriptionDTO prescriptionDTO)
        {
            await _service.Update(id, prescriptionDTO);
            return Ok("Prescription Updated Successfully");
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Prescription Deleted Successfully");
        }
    }
}
