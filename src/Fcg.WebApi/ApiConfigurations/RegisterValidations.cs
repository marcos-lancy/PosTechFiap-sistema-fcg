using Fcg.Application.Dtos.Jogo.Validations;
using Fcg.Application.Dtos.Usuario.Validations;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Fcg.WebApi.ApiConfigurations;
public static class RegisterValidations
{
    public static IServiceCollection AddAbstractValidations(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(CadastrarUsuarioDtoValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(AtualizarUsuarioDtoValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(EfetuarLoginDtoValidator).Assembly);
        
        services.AddValidatorsFromAssembly(typeof(CadastrarJogoDtoValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(AtualizarJogoDtoValidator).Assembly);

        services.AddFluentValidationAutoValidation(options =>
        {
            options.OverrideDefaultResultFactoryWith<CustomValidatorResult>();
        });
        
        return services;
    }
}