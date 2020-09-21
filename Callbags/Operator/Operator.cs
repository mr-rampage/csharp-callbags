using System;

namespace Callbags.Operator
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
    }
}