using HospitalManagementSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces
{
    public interface IHospitalRepository
    {
        Task<List<HospitalDTO>> GetAll();
        Task<HospitalDTO> GetById(int id);
        Task<HospitalDTO> Create(HospitalDTO hospitalDTO);
        Task<HospitalDTO> Update(HospitalDTO hospitalDTO);
        Task<HospitalDTO> Delete(int id);
    }
}
