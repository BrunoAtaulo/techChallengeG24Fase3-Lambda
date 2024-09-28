using AWS_lambda_Auth.Models;
using AWS_lambda_Auth.Utils;
using FluentValidation;


namespace AWS_lambda_Auth.Validations;

public class UsuarioValidation : AbstractValidator<User>
{
    public UsuarioValidation()
    {
        RuleFor(x => x.Cpf)
           .NotEmpty().WithMessage("CPF é obrigatorio.")
           .NotNull().WithMessage("CPF é obrigatorio.");

        When(x => !string.IsNullOrEmpty(x.Cpf) && !string.IsNullOrWhiteSpace(x.Cpf), () =>
        {
            RuleFor(x => x.Cpf)
            .Must(Validator.CpfIsValid).WithMessage("O CPF nao é valido.");
        });

        When(x => !string.IsNullOrEmpty(x.Email) && !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email)
            .Must(Validator.ValidarEmail).WithMessage("O Email informado está inválido.");
        });

        // Adicionando a validação de senha
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("password deve ser informada.")
            .NotNull().WithMessage("password deve ser informada.")
            .MinimumLength(8).WithMessage("o password deve ter no mínimo 8 caracteres.");
    }
}


