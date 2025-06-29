
using DAL.Intrfices;
using DAL.Model;

namespace BLL.Services
{
    public class PatientService(IPatientRepository _patientRepository)
    {
        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllAsync();
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _patientRepository.GetByIdAsync(id);
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await _patientRepository.AddAsync(patient);
        }

        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            var existing = await _patientRepository.GetByIdAsync(patient.Id);
            if (existing == null)
                return false;

            return await _patientRepository.UpdateAsync(patient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
          return await _patientRepository.DeleteAsync(id);
        }
    }

}
