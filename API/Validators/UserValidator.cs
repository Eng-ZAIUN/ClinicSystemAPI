using BLL.Model;
using FluentValidation;

namespace API.Validators
{
    public class UserValidator : AbstractValidator<RegisterModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("اسم المستخدم مطلوب")
                .MaximumLength(50).WithMessage("اسم المستخدم لا يجب أن يتجاوز 50 حرف");

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("البريد الإلكتروني مطلوب")
                .EmailAddress().WithMessage("البريد الإلكتروني غير صالح");

            RuleFor(x => x.FullName).NotEmpty()
                .WithMessage("الاسم الكامل مطلوب")
                .MaximumLength(100).WithMessage("الاسم الكامل لا يجب أن يتجاوز 100 حرف");

            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("كلمة المرور مطلوبة")
                .MinimumLength(6).WithMessage("كلمة المرور يجب أن تكون على الأقل 6 أحرف")
                .MaximumLength(100).WithMessage("كلمة المرور لا يجب أن تتجاوز 100 حرف");
        }
    }
}
