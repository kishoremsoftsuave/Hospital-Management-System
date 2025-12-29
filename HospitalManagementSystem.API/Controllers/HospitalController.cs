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

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(HospitalDTO hospitalDTO)
        {
            await _service.Create(hospitalDTO);
            return Ok();
        }


        [HttpPut("/{id}")]
        public async Task<IActionResult> Update(int id, HospitalDTO hospitalDTO)
        {
            await _service.Update(id, hospitalDTO);
            return Ok();
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
