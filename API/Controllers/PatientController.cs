using AutoMapper;
using BLL.DTOs;
using BLL.Services;
using DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController(PatientService _patientService, IMapper _mapper) : ControllerBase
    {
        [HttpGet("GetAllPatients")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllPatients()
        {
            var result = await _patientService.GetAllPatientsAsync();
            var dtoList = _mapper.Map<IEnumerable<PatientDto>>(result);
            return Ok(dtoList);
        }

        [HttpGet("GetPatientById/{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
                return NotFound("Patient not found!");

            var dto = _mapper.Map<PatientDto>(patient);
            return Ok(dto);
        }

        [HttpPost("AddPatient")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPatient([FromBody] PatientDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            await _patientService.AddPatientAsync(patient);
            return Ok("Patient added successfully!");
        }

        [HttpPut("UpdatePatient")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePatient([FromBody] PatientDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            bool updated = await _patientService.UpdatePatientAsync(patient);

            if (!updated)
                return NotFound("Patient not found!");

            return Ok("Patient updated successfully!");
        }
        
        [HttpDelete("DeletePatient/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            bool deleted = await _patientService.DeletePatientAsync(id);
            if (!deleted)
                return NotFound("Patient not found!");

            return Ok("Patient deleted successfully!");
        }
    }
}
