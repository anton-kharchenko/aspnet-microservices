using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace Ordering.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();
        var context = new ValidationContext<TRequest>(request);
        var validationResult = await Task
            .WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken))).ConfigureAwait(false);
        var failures = validationResult.SelectMany(i => i.Errors).Where(e => e != null).ToArray();
        if (failures.Length > 0)
            throw new ValidationException(failures);
        return await next();
    }
}