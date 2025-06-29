using BLL.DTOs;
using FluentValidation;

namespace API.Validators
{
    public class AppointmentValidator : AbstractValidator<AppointmentDto>
    {
        public AppointmentValidator()
        {
            RuleFor(a => a.PatientId)
                .NotEmpty().WithMessage("Patient ID is required.")
                .NotNull().WithMessage("Patient ID cannot be null.");

            RuleFor(a => a.Date)
                .NotNull().WithMessage("Appointment date cannot be null.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Appointment date must be in the future.");

            RuleFor(a => a.Time).NotEmpty()
                .WithMessage("Appointment time is required.")
                .Matches(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$").WithMessage("Appointment time must be in HH:mm format.");

            RuleFor(a => a.Status).NotEmpty()
                .WithMessage("Appointment status is required.")
                .Must(status => status == "Scheduled" || status == "Completed" || status == "Cancelled")
                .WithMessage("Appointment status must be either 'Scheduled', 'Completed', or 'Cancelled'.");
        }
    }
}
