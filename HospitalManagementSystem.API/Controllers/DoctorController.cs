using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorController(IDoctorService service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _service.GetById(id);
            if(doctor == null)
                return NotFound("Doctor Not Found");
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorDTO doctorDTO)
        {
            await _service.Create(doctorDTO);
            return Ok("Doctor Created Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,  DoctorDTO doctorDTO)
        {
            var doctor = await _service.GetById(id);
            if (doctor == null)
                return NotFound("Invalid Doctor ID");
            await _service.Update(id, doctorDTO);
            return Ok("Doctor Updated Successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _service.GetById(id);
            if (doctor == null)
                return NotFound("Invalid Doctor ID");
            await _service.Delete(id);
            return Ok("Doctor Deleted Successfully");
        }
    }
}
