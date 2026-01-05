using HospitalManagementSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDTO>> GetAll();
        Task<AppointmentDTO> GetById(int id);
        Task Create(AppointmentDTO appointmentDTO);
        Task Update(int id, AppointmentDTO appointmentDTO);
        Task Patch(int id, AppointmentStatusDTO appointmentDTO);
        Task Delete(int id);
    }
}
