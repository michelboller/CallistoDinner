using CallistoDinner.Application.Authentication.Commands.Register;
using CallistoDinner.Application.Authentication.Common;
using CallistoDinner.Application.Authentication.Queries.Login;
using CallistoDinner.Contracts.Authentication;
using Mapster;

namespace CallistoDinner.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}
