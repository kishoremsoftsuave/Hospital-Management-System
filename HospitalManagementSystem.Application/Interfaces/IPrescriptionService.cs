using HospitalManagementSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces
{
    public interface IPrescriptionService
    {
        Task<List<PrescriptionDTO>> GetAll();
        Task<PrescriptionDTO> GetById(int id);
        Task Create(PrescriptionDTO prescriptionDTO);
        Task Update(int id, PrescriptionDTO prescriptionDTO);
        Task Delete(int id);
    }
}
