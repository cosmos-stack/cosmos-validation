using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Text;

namespace Cosmos.Validation.Internals
{
    internal static class CharExtensions
    {
        public static bool IsNumberOrA(this char c)
        {
            if (c.IsNumber()) return true;
            if (c == 'A') return true;
            return false;
        }

        public static bool IsAllNumber(this ReadOnlySpan<char> idNumber)
        {
            foreach (var n in idNumber)
                if (!n.IsNumber())
                    return false;
            return true;
        }

        public static bool IsAllNumber(this ReadOnlySpan<char> idNumber, int count)
        {
            var counter = 0;
            foreach (var n in idNumber)
                if (n.IsNumber())
                    counter++;
            return count == counter;
        }

        public static bool IsAllNumberOrA(this ReadOnlySpan<char> idNumber)
        {
            foreach (var n in idNumber)
                if (!n.IsNumberOrA())
                    return false;
            return true;
        }

        public static bool IsAllNumberOrA(this ReadOnlySpan<char> idNumber, int count)
        {
            var counter = 0;
            foreach (var n in idNumber)
                if (n.IsNumberOrA())
                    counter++;
            return count == counter;
        }

        public static bool IndexOfShouldBe(this ReadOnlySpan<char> idNumber, int index, char c)
        {
            return idNumber[index] == c;
        }

        public static string GetString(this ReadOnlySpan<char> idNumber)
        {
            var sb = new StringBuilder();
            foreach (var n in idNumber)
                sb.Append(n);
            return sb.ToString();
        }

        public static int[] SelectAndTakeToInt32Array(this ReadOnlySpan<char> idNumber, int take)
        {
            var counter = 0;
            var ret = new List<int>(take);
            foreach (var n in idNumber)
            {
                if (counter++ == take)
                    break;
                ret.Add(n - 48);
            }

            return ret.ToArray();
        }
    }
}