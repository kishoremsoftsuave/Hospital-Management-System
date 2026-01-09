using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Domain.Entities.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.WebAPI
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAll();
        Task<Appointment?> GetById(int id);
        Task Create(Appointment appointment);
        Task Update(Appointment appointment);
        Task Patch(Appointment appointment);
        Task Delete(int id);
    }
}
