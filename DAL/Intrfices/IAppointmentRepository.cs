using DAL.Model;

namespace DAL.Intrfices
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int id);
        Task AddAsync(Appointment appointment);
        Task<bool> UpdateAsync(Appointment appointment);
        Task<bool> DeleteAsync(int id);
        Task<List<Appointment?>> GetByPatientIdAsync(int patientId);
        Task<List<Appointment?>> GetAppointmentsByDateAsync(DateTime date);

    }
}
