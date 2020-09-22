using System;

namespace Callbag.Basics.Operator
{
    public static class OperatorExtension
    {
        public static ISource<TOutput> Map<TInput, TOutput>(this ISource<TInput> source, in Func<TInput, TOutput> operation)
        {
            var map = new Map<TInput, TOutput>(operation);
            source.Greet(map);
            return map;
        }

        public static ISource<T> Filter<T>(this ISource<T> source, in Predicate<T> predicate)
        {
            var filter = new Filter<T>(predicate);
            source.Greet(filter);
            return filter;
        }

        public static ISource<T> Skip<T>(this ISource<T> source, int max)
        {
            var skip = new OperatorFactory<T, T>(sink => new Skip<T>(sink, max));
            source.Greet(skip);
            return skip;
        }

        public static ISource<T> Take<T>(this ISource<T> source, int max)
        {
            var take = new OperatorFactory<T, T>(sink => new Take<T>(sink, max));
            source.Greet(take);
            return take;
        }
    }
}