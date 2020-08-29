using FluentValidation;
using FluentValidation.Results;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMDb.Domain.DomainObjects
{
    public abstract class BaseEntity<T> : AbstractValidator<T> where T : BaseEntity<T>
    {
        public Guid Id { get; protected set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            ValidationResult = new ValidationResult();
        }
        public abstract bool IsValid();
    }
}
