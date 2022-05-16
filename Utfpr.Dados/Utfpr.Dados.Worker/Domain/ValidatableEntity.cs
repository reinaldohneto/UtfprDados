using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using FluentValidation.Results;

namespace Utfpr.Dados.Worker.Domain;

public abstract class ValidatableEntity<T> : Entity where T : ValidatableEntity<T>
{
    // Para validar
    [NotMapped]
    public ValidationResult ValidationResult { get; protected set; }
    
    protected ValidatableEntity()
    {
    }

    protected ValidatableEntity(Guid id) : base(id)
    {
    }

    protected ValidatableEntity(DateTime data) : base(data)
    {
    }

    protected ValidatableEntity(Guid id, DateTime data) : base(id, data)
    {
    }

    public bool EhValido()
    {
        if (ValidationResult == null)
        {
            Validar();
        }

        return ValidationResult == null || ValidationResult.IsValid;
    }
    public bool EhInvalido()
    {
        return !EhValido();
    }
    private void Validar()
    {
        ValidationResult = ObterValidator()?.Validate((T)this);
    }

    protected abstract AbstractValidator<T> ObterValidator();
}