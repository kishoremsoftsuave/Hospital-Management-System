using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService _service;
        public HospitalController(IHospitalService service)
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
            var hospital = await _service.GetById(id);
            if (hospital == null)
                return NotFound("Hospital not found");

            return Ok(hospital);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HospitalDTO hospitalDTO)
        {
            await _service.Create(hospitalDTO);
            return Ok("Hospital created successfully");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HospitalDTO hospitalDTO)
        {
            var hospital = await _service.GetById(id);
            if (hospital == null)
                return NotFound("Invalid Hospital ID");
            await _service.Update(id, hospitalDTO);
            return Ok("Hospital updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hospital = await _service.GetById(id);
            if (hospital == null)
                return NotFound("Invalid Hospital ID");
            await _service.Delete(id);
            await _service.Delete(id);
            return Ok("Hospital deleted successfully");
        }
    }
}
