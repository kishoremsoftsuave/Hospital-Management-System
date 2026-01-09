using HospitalManagementSystem.Application.DTO.WebAPI;
using HospitalManagementSystem.Application.Interfaces.WebAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.API.Controllers.WebAPI
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
            await _service.Update(id, medicalRecordDTO);
            return Ok("MedicalRecord Updated Successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("MedicalRecord Deleted Successfully");
        }
    }
}
