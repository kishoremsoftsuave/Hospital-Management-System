using HospitalManagementSystem.Application.DTO.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.WebAPI
{
    public interface IHospitalService
    {
        Task<List<HospitalDetailDTO>> GetAll();
        Task<HospitalDetailDTO> GetById(int id);
        Task Create(HospitalDTO hospitalDTO);
        Task Update(int id, HospitalDTO hospitalDTO);  
        Task Delete(int id);
    }
}
