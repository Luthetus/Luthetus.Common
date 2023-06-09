using Luthetus.Common.RazorLib.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luthetus.Common.Tests.Basics.Reactive;

public class ThrottleTests
{
    [Fact]
    public async Task YetToBeNamed()
    {
        var countMeasurements = new List<int>();
        int fireCounter = 0;
        int completedCounter = 0;

        // Is this test going to break due to concurrency?
        // i.e. if many increment this int at the same time some increments might be lost?
        ++fireCounter;

        var throttle = new Throttle<int>(TimeSpan.FromMilliseconds(500));

        var t0 = throttle.FireAsync(completedCounter, CancellationToken.None);
        var t1 = throttle.FireAsync(completedCounter, CancellationToken.None);
        var t2 = throttle.FireAsync(completedCounter, CancellationToken.None);
        var t3 = throttle.FireAsync(completedCounter, CancellationToken.None);
        var t4 = throttle.FireAsync(completedCounter, CancellationToken.None);
        var t5 = throttle.FireAsync(completedCounter, CancellationToken.None);
        var t6 = throttle.FireAsync(completedCounter, CancellationToken.None);
        var t7 = throttle.FireAsync(completedCounter, CancellationToken.None);
        var t8 = throttle.FireAsync(completedCounter, CancellationToken.None);
        var t9 = throttle.FireAsync(completedCounter, CancellationToken.None);

        await Task.WhenAll(new[] 
        {
            t0,
            t1,
            t2,
            t3,
            t4,
            t5,
            t6,
            t7,
            t8,
            t9,
        });

        countMeasurements.Add(++completedCounter);
    }
}
