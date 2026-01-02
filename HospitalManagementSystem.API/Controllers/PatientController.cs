using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;
        public PatientController(IPatientService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());

        }

        [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _service.GetById(id);
            if (patient == null)
                return NotFound("Invalid Patient Id");
            return Ok(patient);
        }
        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [HttpPost]
        public async Task<IActionResult> Create(PatientDTO patientDTO)
        {
            await _service.Create(patientDTO);
            return Ok("Patient Created Successfully");
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, PatientDTO patientDTO)
        {
            var patient = await _service.GetById(id);
            if (patient == null)
                return NotFound("Invalid Patient Id");
            await _service.Update(id, patientDTO);
            return Ok("Patient Updated Successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _service.GetById(id);
            if (patient == null)
                return NotFound("Invalid Patient Id");
            await _service.Delete(id);
            return Ok("Patient Deleted Successfully");
        }
    }
}
