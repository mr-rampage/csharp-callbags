using System;

namespace Callbags.Operator
{
    public static class Operator
    {
        public static IOperator<I, O> Map<I, O>(Func<I, O> operation)
        {
            return new Map<I, O>(operation);
        }
    }
}