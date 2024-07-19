namespace Mediator.Libraries.Benchmarks;

public class MediatorFeatures
{
    public record MediatorRequest(string Message) : IRequest<string>;

    public class MediatorRequestHandler : IRequestHandler<MediatorRequest,string>
    {
        public ValueTask<string> Handle(MediatorRequest request, CancellationToken cancellationToken)
        {
            return ValueTask.FromResult<string>($"Mediator Request Response {request.Message}");
        }
    }
}