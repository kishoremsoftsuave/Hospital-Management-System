using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Services
{
    public class HospitalService : IHospitalService
    {
        public Task<HospitalDTO> Create(HospitalDTO hospitalDTO)
        {
            throw new NotImplementedException();
        }

        public Task<HospitalDTO> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<HospitalDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<HospitalDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HospitalDTO> Update(HospitalDTO hospitalDTO)
        {
            throw new NotImplementedException();
        }
    }
}
