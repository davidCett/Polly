using Polly.Hedging;

namespace Polly.Core.Benchmarks.Utils;

internal static partial class Helper
{
    public const string Failure = "failure";

    public static ResiliencePipeline<string> CreateHedging()
    {
        return CreateStrategy(builder =>
        {
            builder.AddHedging(new HedgingStrategyOptions<string>
            {
                ShouldHandle = args => new ValueTask<bool>(args.Outcome.Result == Failure),
                ActionGenerator = args => () => Outcome.FromResultAsValueTask("hedged response"),
            });
        });
    }
}
