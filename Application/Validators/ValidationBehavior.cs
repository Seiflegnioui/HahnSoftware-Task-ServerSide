using FluentValidation;
using MediatR;
using hahn.Application.DTOs;

namespace hahn.Application.Validators
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var failures = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    var errorMessages = failures
                        .Select(f => $"{f.PropertyName}: {f.ErrorMessage}")
                        .ToList();

                    if (typeof(TResponse).IsGenericType && 
                        typeof(TResponse).GetGenericTypeDefinition() == typeof(CustomResult<>))
                    {
                        var resultType = typeof(TResponse).GetGenericArguments()[0];
                        var failMethod = typeof(CustomResult<>)
                            .MakeGenericType(resultType)
                            .GetMethod("Fail", new[] { typeof(List<string>) });
                        
                        if (failMethod != null)
                        {
                            return (TResponse)failMethod.Invoke(null, new object[] { errorMessages })!;
                        }
                    }
                    
                    throw new ValidationException(string.Join("; ", errorMessages));
                }
            }

            return await next();
        }
    }
}