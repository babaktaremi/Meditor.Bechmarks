// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkDotNetVisualizer;
using Mediator.Libraries.Benchmarks;

var summary=BenchmarkRunner.Run<MediatorBenchmarks>();

await summary.SaveAsImageAsync(
    path: DirectoryHelper.GetPathRelativeToProjectDirectory("MeidatorBenchmarks.png"),
    options: new ReportHtmlOptions
    {
        Title = "Mediator Libraries Benchmark",
        SpectrumColumns = ["Mean", "Allocated"],
        SortByColumns = ["Mean", "Allocated"],
        HighlightGroups = false
    });
