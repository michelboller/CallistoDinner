﻿using CallistoDinner.Application.Authentication.Commands.Register;
using CallistoDinner.Application.Authentication.Common;
using CallistoDinner.Application.Common.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallistoDinner.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse: AuthenticationResult
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request,
                                      CancellationToken cancellationToken,
                                      RequestHandlerDelegate<TResponse> next)
        {
            if (_validator is null)
                return await next();

            var validationResult = await _validator.ValidateAsync(request);

            if (validationResult.IsValid)
                return await next();

            var errors = validationResult.Errors;
            //var message = string.Join("; ", errors.ConvertAll(err => $"Code: {err.PropertyName}. Description: {err.ErrorMessage}"));
            //throw new SllException(message);

            throw new SllException($"Code: {errors.FirstOrDefault()?.PropertyName}. Description: {errors.FirstOrDefault()?.ErrorMessage}");
        }
    }
}