using FluentValidation;
using WebApi.Entity.Entities;

namespace WebApi.Service.ValidationTools.FluentValidation
{
    public class AppRoleValidator : AbstractValidator<AppRole>
    {
        public AppRoleValidator()
        {
            RuleFor(_ => _.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Rol boş bırakılamaz!");

            RuleFor(_ => _.Name)
                .MinimumLength(2)
                .WithMessage("Rol en az 2 karakterden oluşmalı!");

            RuleFor(_ => _.Name)
                .MaximumLength(20)
                .WithMessage("Rol en fazle 25 karakter içerebilir!");
        }
    }
}
