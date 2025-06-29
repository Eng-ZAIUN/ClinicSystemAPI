using BLL.DTOs;
using FluentValidation;

namespace API.Validators
{
    public class PatientValidator : AbstractValidator<PatientDto>
    {
        public PatientValidator()
        {
           
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("الاسم الكامل مطلوب")
                .MaximumLength(100).WithMessage("الاسم لا يجب أن يتجاوز 100 حرف");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("تاريخ الميلاد مطلوب")
                .LessThan(DateTime.Now).WithMessage("تاريخ الميلاد يجب أن يكون في الماضي");

            RuleFor(x => x.Phone)
               .NotEmpty().WithMessage("رقم الهاتف مطلوب")
               .MaximumLength(11).WithMessage("رقم الهاتف لا يتجاوز 11 رقم")
               .Matches(@"^07\d{9}$").WithMessage("رقم الهاتف غير صالح (مثال: 078XXXXXXXX)");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("الجنس مطلوب")
                .Must(g => g == "ذكر" || g == "أنثى").WithMessage("القيمة يجب أن تكون ذكر أو أنثى فقط");
        }
    }
}


