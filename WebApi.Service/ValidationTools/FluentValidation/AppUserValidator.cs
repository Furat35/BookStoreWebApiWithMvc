using FluentValidation;
using WebApi.Entity.Entities;

namespace WebApi.Service.ValidationTools.FluentValidation
{
    public class AppUserValidator : AbstractValidator<AppUser>
    {
        public AppUserValidator()
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
                .MaximumLength(20)
                .WithMessage("Kullanıcı adı en fazle 25 karakter içerebilir!");

            #endregion

            #region Email

            RuleFor(_ => _.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Kullanıcı adı boş bırakılamaz!");

            RuleFor(_ => _.Email)
                .EmailAddress()
                .WithMessage("Geçersiz email adresi!");

            RuleFor(_ => _.Email)
                .MinimumLength(10)
                .WithMessage("Email en az 10 karakterden oluşmalı!");

            RuleFor(_ => _.Email)
                .MaximumLength(50)
                .WithMessage("Email en fazle 40 karakter içerebilir!");

            #endregion

            #region Password

            //RuleFor(_ => _.)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("Kullanıcı adı boş bırakılamaz!");

            //RuleFor(_ => _.UserName)
            //    .MinimumLength(2)
            //    .WithMessage("Kullanıcı adı en az 2 karakterden oluşmalı!");

            //RuleFor(_ => _.UserName)
            //    .MaximumLength(20)
            //    .WithMessage("Kullanıcı adı en fazle 25 karakter içerebilir!");

            #endregion

        }
    }
}
