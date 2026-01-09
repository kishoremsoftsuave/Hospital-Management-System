using HospitalManagementSystem.Application.DTO.CosmosDB;
using HospitalManagementSystem.Application.Interfaces.CosmosDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.API.Controllers.CosmosDB
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CosmosPatientController : ControllerBase
    {
        private readonly CosmosIPatientService _service;
        public CosmosPatientController(CosmosIPatientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _service.GetAll();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var patient = await _service.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CosmosCreatePatientDTO dto)
        {
            var createdPatient = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdPatient.Id }, createdPatient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CosmosUpdatePatientDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID mismatch");
            }
            var updatedPatient = await _service.UpdateById(dto);
            return Ok(updatedPatient);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}
