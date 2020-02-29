using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace WorkplannerCQRS.API.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // _validators contains all validators registered on any IRequest
            // we need to access only the validator concerning the current request
            // which is retrieved through the ValidationContext(request)
            // then we simply get all errors that are not null
            var context = new ValidationContext(request);
            var failures = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
            
            return next();
        }
    }
}