using FluentValidation;
using WebApi.Entity.Entities;

namespace WebApi.Service.ValidationTools.FluentValidation
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            #region Name

            RuleFor(_ => _.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Kitap adı boş bırakılamaz!");

            RuleFor(_ => _.Name)
                .MinimumLength(2)
                .WithMessage("Kitap adı en az 2 karakterden oluşmalı!");

            RuleFor(_ => _.Name)
                .MaximumLength(20)
                .WithMessage("Kitap adı en fazle 25 karakter içerebilir!");

            #endregion

            #region Summary

            RuleFor(_ => _.Summary)
                .NotNull()
                .NotEmpty()
                .WithMessage("Özet boş bırakılamaz!");

            RuleFor(_ => _.Summary)
                .MinimumLength(2)
                .WithMessage("Özet en az 2 karakterden oluşmalı!");

            RuleFor(_ => _.Summary)
                .MaximumLength(25)
                .WithMessage("Özet en fazle 25 karakter içerebilir!");

            #endregion

            #region Page

            RuleFor(_ => _.Page)
                .GreaterThan((short)0)
                .LessThan((short)10000);

            #endregion

            #region AuthorId

            RuleFor(_ => _.AuthorId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Yazar boş bırakılamaz!");

            #endregion

            #region PublisherId

            RuleFor(_ => _.PublisherId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Yayınevi boş bırakılamaz!");

            #endregion

        }
    }
}
