using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BrandonHaynes.Security.SipHash
{
    internal static class Utilities
    {
        [DebuggerStepThrough]
        public static void Repeat(this int n, Action f)
        { for (var index = 0; index < n; index++) f(); }

        [DebuggerStepThrough]
        public static T[] Extend<T>(this T[] array, int newSize)
        {
            var result = new T[newSize];
            array.CopyTo(result, 0);
            return result;
        }

        [DebuggerStepThrough]
        public static ulong NextWord(this IEnumerator<byte> buffer)
        {
            return BitConverter.ToUInt64(new[] { buffer.Next(), buffer.Next(), 
                                                   buffer.Next(), buffer.Next(),
                                                   buffer.Next(), buffer.Next(),
                                                   buffer.Next(), buffer.Next() }, 0);
        }

        [DebuggerStepThrough]
        private static T Next<T>(this IEnumerator<T> enumerator)
        {
            enumerator.MoveNext();
            return enumerator.Current;
        }

        [DebuggerStepThrough]
        private static T Next<T>(this IEnumerator<T> enumerator, out bool isComplete)
        {
            isComplete = enumerator.MoveNext();
            return enumerator.Current;
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            bool hasNext;
            do
            {
                var value = enumerator.Next(out hasNext);
                if (hasNext) yield return value;
            }
            while (hasNext);
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> ArraySkip<T>(this T[] array, int count)
        {
            for (var index = count; index < array.Length; index++)
                yield return array[index];
        }

        [DebuggerStepThrough]
        public static ulong RotateLeft(this ulong value, int count)
        { return (value << count) | (value >> ((sizeof(ulong) * 8) - count)); }
    }
}