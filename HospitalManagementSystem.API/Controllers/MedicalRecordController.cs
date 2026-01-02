using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _service;
        public MedicalRecordController(IMedicalRecordService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var medicalRecord = await _service.GetById(id);
            if (medicalRecord == null)
                return NotFound("MedicalRecord Not Found");
            return Ok(medicalRecord);
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost]
        public async Task<IActionResult> Create(MedicalRecordDTO medicalRecordDTO)
        {
            await _service.Create(medicalRecordDTO);
            return Ok("MedicalRecord Created Successfully");
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MedicalRecordDTO medicalRecordDTO)
        {
            var medicalRecord = await _service.GetById(id);
            if (medicalRecord == null)
                return NotFound("Invalid MedicalRecord ID");
            await _service.Update(id, medicalRecordDTO);
            return Ok("MedicalRecord Updated Successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var medicalRecord = await _service.GetById(id);
            if (medicalRecord == null)
                return NotFound("Invalid MedicalRecord ID");
            await _service.Delete(id);
            return Ok("MedicalRecord Deleted Successfully");
        }
    }
}
