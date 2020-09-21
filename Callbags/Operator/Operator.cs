using System;

namespace Callbags.Operator
{
    public static class Operator
    {
        public static IOperator<TInput, TOutput> Map<TInput, TOutput>(in Func<TInput, TOutput> operation)
        {
            return new Map<TInput, TOutput>(operation);
        }
    }
}