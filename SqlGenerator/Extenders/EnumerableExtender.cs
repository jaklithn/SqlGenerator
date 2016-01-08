using System;
using System.Collections.Generic;

namespace SqlGenerator.Extenders
{
    public static class EnumerableExtender
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}