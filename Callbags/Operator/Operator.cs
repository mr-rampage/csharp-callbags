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
    }
}