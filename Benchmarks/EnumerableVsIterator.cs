using BenchmarkDotNet.Attributes;
using IncaTechnologies.Recurrence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks;

public class EnumerableVsIterator
{
    private static IEnumerable<DateTime> AsEnumerable(Daily daily, DateTime from, DateTime to)
    {
        if (daily.At.Count == 0)
        {
            daily = DefaultOccurrences.Daily;
        }

        return Enumerable.Range(0, (to - from).Days + 1)
            .SelectMany(shift => daily.At.Select(hourly =>
            {
                var date = from.AddDays(shift);
                hourly.Minutely = hourly.Minutely ?? DefaultOccurrences.Minutely;
                hourly.Minutely.Secondly = hourly.Minutely.Secondly ?? DefaultOccurrences.Secondly;

                return new DateTime(
                    date.Year,
                    date.Month,
                    date.Day,
                    hourly.Hour,
                    hourly.Minutely.Minute,
                    hourly.Minutely.Secondly.Second);
            }));
    }
    private static IEnumerable<DateTime> AsIterator(Daily daily, DateTime from, DateTime to)
    {
        var period = to - from;
        var days = period.Days;

        if (daily.At.Count == 0)
        {
            daily = DefaultOccurrences.Daily;
        }

        for (int i = 0; i <= days; i++)
        {
            foreach (var hourly in daily.At)
            {
                var date = from.AddDays(i);
                hourly.Minutely = hourly.Minutely ?? DefaultOccurrences.Minutely;
                hourly.Minutely.Secondly = hourly.Minutely.Secondly ?? DefaultOccurrences.Secondly;

                yield return new DateTime(
                    date.Year,
                    date.Month,
                    date.Day,
                    hourly.Hour,
                    hourly.Minutely.Minute,
                    hourly.Minutely.Secondly.Second);
            }
        }
    }
    private static IEnumerable<DateTime> AsEnumerableDefinitive(Daily daily, DateTime from, DateTime to) => daily.AsEnumerable(from, to);

    [Benchmark]
    public DateTime[] AsIterator() 
        => [.. AsIterator(DefaultOccurrences.Daily, new DateTime(2000, 1, 1), new DateTime(2100, 1, 1))];

    [Benchmark]
    public DateTime[] AsEnumerable()
        => [.. AsEnumerable(DefaultOccurrences.Daily, new DateTime(2000, 1, 1), new DateTime(2100, 1, 1))];

    [Benchmark]
    public DateTime[] AsEnumerableDefinitive()
        => [.. AsEnumerableDefinitive(DefaultOccurrences.Daily, new DateTime(2000, 1, 1), new DateTime(2100, 1, 1))];

}
