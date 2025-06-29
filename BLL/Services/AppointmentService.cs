using DAL.Intrfices;
using DAL.Model;
namespace BLL.Services
{
    public class AppointmentService(IAppointmentRepository _appointmentRepository)
    {
        public async Task AddAsync(Appointment appointment)
        {
           await _appointmentRepository.AddAsync(appointment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
           return await _appointmentRepository.DeleteAsync(id);
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
           return await _appointmentRepository.GetAllAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _appointmentRepository.GetByIdAsync(id);   
        }

        public async Task<List<Appointment?>> GetByPatientIdAsync(int patientId)
        {
           return await _appointmentRepository.GetByPatientIdAsync(patientId);
        }

        public async Task<List<Appointment?>> GetAppointmentsByDateAsync(DateTime date)
        {
            return await _appointmentRepository.GetAppointmentsByDateAsync(date);
        }

        public async Task<bool> UpdateAsync(Appointment appointment)
        {
            var existing = await _appointmentRepository.GetByIdAsync(appointment.Id);
            if (existing == null)
                return false;
            return await _appointmentRepository.UpdateAsync(appointment);
        }
    }
}
