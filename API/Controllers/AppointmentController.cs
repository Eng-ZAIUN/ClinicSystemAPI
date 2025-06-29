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
    public class AppointmentController(AppointmentService _appointment, IMapper _mapper) : ControllerBase
    {
        [HttpGet("GetAllAppointemts")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAllAppointemts()
        {
            var Appoint = await _appointment.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<AppointmentDto>>(Appoint);
            return Ok(dto);
        }

        [HttpGet("GetAppointmentById/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAppointemtById(int id)
        {
            var patient = await _appointment.GetByIdAsync(id);
            if (patient == null)
            {
                return BadRequest("Appointemt is not Found!");
            }
            var dto = _mapper.Map<AppointmentDto>(patient);
            return Ok(dto);
        }

        [HttpPost("AddAppointment")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAppointemt([FromBody] AppointmentDto appointmentdto)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("اختبار خطأ غير متوقع");

            }

            if (appointmentdto == null)
            {
                return BadRequest("Appointemt data is null!");
            }

            var dto = _mapper.Map<Appointment>(appointmentdto);
            await _appointment.AddAsync(dto);
            return Ok("Appointemt added successfully!");
        }

        [HttpPut("UpdateAppointemt")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAppointemt([FromBody] AppointmentDto appointmentdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (appointmentdto == null)
                return BadRequest("Appointemt data is null!");
            var dto = _mapper.Map<Appointment>(appointmentdto);
            bool updated = await _appointment.UpdateAsync(dto);

            if (!updated)
                return NotFound("Appointemt not found!");

            return Ok("Appointemt updated successfully!");
        }

        [HttpDelete("DeleteAppointemt/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAppointemt(int id)
        {
            bool deleted = await _appointment.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound("Appointemt not found!");
            }
            return Ok("Appointemt deleted successfully!");
        }

        [HttpGet("GetAppointmentsByPatientId/{patientId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAppointmentsByPatientId(int patientId)
        {
            var appointments = await _appointment.GetByPatientIdAsync(patientId);
            if (appointments == null || !appointments.Any())
            {
                return NotFound("No appointments found for this patient.");
            }
            var dto = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
            return Ok(dto);
        }

        [HttpGet("GetAppointmentsByDate/{date}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAppointmentsByDate(DateTime date)
        {
            var appointments = await _appointment.GetAppointmentsByDateAsync(date);
            if (appointments == null || !appointments.Any())
            {
                return NotFound("No appointments found for this date.");
            }
            var dto = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
            return Ok(dto);
        }
    }
}
