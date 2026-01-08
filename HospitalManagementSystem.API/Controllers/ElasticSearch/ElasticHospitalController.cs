using HospitalManagementSystem.Application.DTO.ElasticSearch;
using HospitalManagementSystem.Application.Interfaces.ElasticSearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.API.Controllers.ElasticSearch
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ElasticHospitalController : ControllerBase
    {
        private readonly IElasticHospitalService _service;
        public ElasticHospitalController(IElasticHospitalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hospital = await _service.GetAll();
            return Ok(hospital);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var hospital = await _service.GetById(id);
            return Ok(hospital);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ElasticHospitalCreateDTO hospitaldto)
        {
            await _service.Create(hospitaldto);
            return Ok("Elastic Hospital Created Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ElasticHospitalUpdateDTO hospitaldto)
        {
            await _service.Update(id, hospitaldto);
            return Ok("Elastic Hospital Updated Successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return Ok("Elastic Hospital Deleted Successfully");
        }
    }
}
