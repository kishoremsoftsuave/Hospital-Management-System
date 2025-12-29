using HospitalManagementSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces
{
    public interface IHospitalService
    {
        Task<List<HospitalDTO>> GetAll();
        Task<HospitalDTO> GetById(int id);
        Task Create(HospitalDTO hospitalDTO);
        Task Update(int id, HospitalDTO hospitalDTO);  
        Task Delete(int id);
    }
}
