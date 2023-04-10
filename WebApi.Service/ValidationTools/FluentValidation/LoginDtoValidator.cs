using FluentValidation;
using WebApi.Core.Models.Authentication;

namespace WebApi.Service.ValidationTools.FluentValidation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            #region Username

            RuleFor(_ => _.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş bırakılamaz!");

            RuleFor(_ => _.UserName)
                .MinimumLength(2)
                .WithMessage("Kullanıcı adı en az 2 karakterden oluşmalı!");

            RuleFor(_ => _.UserName)
                .MaximumLength(25)
                .WithMessage("Kullanıcı adı en fazle 25 karakter içerebilir!");

            #endregion

            #region Password

            RuleFor(_ => _.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Şifre adı boş bırakılamaz!");

            RuleFor(_ => _.Password)
                .MinimumLength(2)
                .WithMessage("Şifre adı en az 2 karakterden oluşmalı!");

            RuleFor(_ => _.Password)
                .MaximumLength(25)
                .WithMessage("Şifre adı en fazle 25 karakter içerebilir!");

            #endregion
        }
    }
}
