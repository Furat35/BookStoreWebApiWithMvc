using FluentValidation;
using WebApi.Entity.Entities;

namespace WebApi.Service.ValidationTools.FluentValidation
{
    public class GenreValidator : AbstractValidator<Genre>
    {
        public GenreValidator()
        {
            #region Name

            RuleFor(_ => _.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tür adı boş bırakılamaz!");

            RuleFor(_ => _.Name)
                .MinimumLength(2)
                .WithMessage("Tür adı en az 2 karakterden oluşmalı!");

            RuleFor(_ => _.Name)
                .MaximumLength(25)
                .WithMessage("Tür adı en fazle 25 karakter içerebilir!");

            #endregion
        }
    }
}
