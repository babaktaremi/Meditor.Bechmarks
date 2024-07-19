using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator.Libraries.Benchmarks;

[MemoryDiagnoser]
[Config(typeof(BenchmarkConfig))]
public class MediatorBenchmarks
{
    private MediatR.ISender _mediatRSender;
    private ISender _mediatorSender;

    [GlobalSetup]
    public void SetupServices()
    {
        var services = new ServiceCollection();

        services.AddMediator(options =>
        {
            options.Namespace = "Benchmarks.Mediator";
            options.ServiceLifetime = ServiceLifetime.Singleton;
        });

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
            configuration.Lifetime = ServiceLifetime.Singleton;
        });

        var serviceProvider = services.BuildServiceProvider();

        _mediatRSender = serviceProvider.GetRequiredService<MediatR.ISender>();
        _mediatorSender = serviceProvider.GetRequiredService<ISender>();
    }

    [Benchmark(Baseline = true)]
    public async Task PingMediatR()
    {
        var request = new MediatRFeatures.MediatRRequest("Ping");

        var response = await _mediatRSender.Send(request);
    }

    [Benchmark]
    public async Task PingMediator()
    {
        var request = new MediatorFeatures.MediatorRequest("Ping");

        var response = await _mediatorSender.Send(request);
    }

    private class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            SummaryStyle=SummaryStyle.Default.WithRatioStyle(RatioStyle.Trend);
        }
    }
}