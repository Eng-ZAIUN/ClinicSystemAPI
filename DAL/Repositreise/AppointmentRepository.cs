using DAL.Data;
using DAL.Intrfices;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositreise
{
    public class AppointmentRepository(ApplicationDbContext _context) : IAppointmentRepository
    {
        public async Task AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appoint = await _context.Appointments.FindAsync(id);
            if (appoint != null)
            {
                _context.Appointments.Remove(appoint);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments.
                AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Appointment?>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<List<Appointment?>> GetAppointmentsByDateAsync(DateTime date)
        {
            return await _context.Appointments
                .Where(a => a.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Appointment appointment)
        {
            _context.Attach(appointment);
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
