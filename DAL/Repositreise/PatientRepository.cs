using DAL.Data;
using DAL.Intrfices;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositreise
{
    public class PatientRepository(ApplicationDbContext _context) : IPatientRepository
    {
        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients
                          .AsNoTracking()
                          .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Patient patient)
        {
            _context.Attach(patient);
            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

}
