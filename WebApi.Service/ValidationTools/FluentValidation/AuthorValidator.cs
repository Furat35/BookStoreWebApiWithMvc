using FluentValidation;
using WebApi.Entity.Entities;

namespace WebApi.Service.ValidationTools.FluentValidation
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            #region FirstName

            RuleFor(_ => _.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Ad boş bırakılamaz!");

            RuleFor(_ => _.FirstName)
                .MinimumLength(2)
                .WithMessage("Ad en az 2 karakterden oluşmalı!");

            RuleFor(_ => _.FirstName)
                .MaximumLength(25)
                .WithMessage("Ad en fazle 25 karakter içerebilir!");

            #endregion

            #region LastName

            RuleFor(_ => _.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Soyad boş bırakılamaz!");

            RuleFor(_ => _.LastName)
                .MinimumLength(2)
                .WithMessage("Soyad en az 2 karakterden oluşmalı!");

            RuleFor(_ => _.LastName)
                .MaximumLength(25)
                .WithMessage("Soyad en fazle 25 karakter içerebilir!");

            #endregion
        }
    }
}
