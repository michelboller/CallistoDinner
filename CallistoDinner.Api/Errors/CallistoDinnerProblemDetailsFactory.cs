using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CallistoDinner.Api.Errors
{
    public class CallistoDinnerProblemDetailsFactory : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions _options;
        public CallistoDinnerProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
        {
            statusCode ??= 500;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance
            };

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
        {
            statusCode ??= 500;

            var problemDetails = new ValidationProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance
            };

            if(title != null)
            {
                problemDetails.Title = title;
            }

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
        {
            /*
             * Poderia ser logado as infos em um arquivo e usado o traceId para obter mais informações.
             * Pro usuário final é retornado no padrão RFC-7807 mas para o Dev é logado infos adicionais (talvez)
             * visando dar um melhor suporte para ambos os lados caso ocorra algum problema
             */
            problemDetails.Status ??= statusCode;

            if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
                problemDetails.Type ??= clientErrorData.Link;
            }

            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            var exceptionType = exception?.GetType();
            problemDetails.Extensions.Add("ExceptionType", exceptionType?.Name ?? "Unknown");
        }

    }
}
