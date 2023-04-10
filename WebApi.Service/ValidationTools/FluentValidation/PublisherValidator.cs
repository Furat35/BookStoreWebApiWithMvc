using FluentValidation;
using WebApi.Entity.Entities;

namespace WebApi.Service.ValidationTools.FluentValidation
{
    public class PublisherValidator : AbstractValidator<Publisher>
    {
        public PublisherValidator()
        {
            #region PublisherName

            RuleFor(_ => _.PublisherName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Yayınevi adı boş bırakılamaz!");

            RuleFor(_ => _.PublisherName)
                .MinimumLength(2)
                .WithMessage("Yayınevi adı en az 2 karakterden oluşmalı!");

            RuleFor(_ => _.PublisherName)
                .MaximumLength(50)
                .WithMessage("Yayınevi adı en fazle 50 karakter içerebilir!");

            #endregion
        }
    }
}
