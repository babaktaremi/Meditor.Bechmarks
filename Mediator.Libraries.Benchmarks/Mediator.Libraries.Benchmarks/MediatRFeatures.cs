namespace Mediator.Libraries.Benchmarks;

public class MediatRFeatures
{
    public record MediatRRequest(string Message) : MediatR.IRequest<string>;

    public class MediatRRequestHandler : MediatR.IRequestHandler<MediatRRequest, string>
    {
        public Task<string> Handle(MediatRRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult($"Reponse: {request.Message}");
        }
    }
}