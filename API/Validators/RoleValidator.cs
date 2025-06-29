using BLL.Model;
using FluentValidation;

namespace API.Validators
{
    public class RoleValidator : AbstractValidator<AssignRoleModel>
    {
        public RoleValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("RoleName is required.")
                .Matches(@"^[A-Za-z]+$").WithMessage("RoleName must contain only letters.")
                .WithMessage($"RoleName must be one of the following:");
        }
    }
}
