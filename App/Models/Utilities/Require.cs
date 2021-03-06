﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Models.Utilities
{
    public static class Require
    {
        public const string NotSpecified = "not specified";

        public static void NotNull(object item, string name)
        {
            if (item == null) throw new NullReferenceException(name ?? NotSpecified);
        }

        public static void ArgumentNotNull(object item, string name)
        {
            if (item == null) throw new ArgumentNullException(name ?? NotSpecified);
        }

        public static void ArgumentNotNullEmpty(string item, string name)
        {
            if (string.IsNullOrEmpty(item))
                throw new ArgumentException(item == null ? "null" : "empty", name ?? NotSpecified);
        }

        public static void IsEmpty<T>(IEnumerable<T> source, string name)
        {
            if (source != null && source.Any())
                throw new ArgumentException("not empty", name ?? NotSpecified);
        }

        public static void IsNotEmpty<T>(IEnumerable<T> source, string name)
        {
            if (source == null || !source.Any())
                throw new ArgumentException(source == null ? "null" : "empty", name ?? NotSpecified);
        }

        public static void ArgumentInRange(int item, int min, int max, string name)
        {
            if (item < min || item > max)
                throw new ArgumentOutOfRangeException(name ?? NotSpecified, item, $"{min} < {item} < {max}");
        }

        public static void ArgumentInRange(double item, double min, double max, string name)
        {
            if (item < min || item > max)
                throw new ArgumentOutOfRangeException(name ?? NotSpecified, item, $"{min} < {item} < {max}");
        }

        public static void True(Func<bool> func, string message = "Require True condition failed")
        {
            if (!func()) throw new InvalidProgramException(message);
        }

        public static void False(Func<bool> func, string message = "Require False condition failed")
        {
            if (func()) throw new InvalidProgramException(message);
        }

        public static void ArgumentValid<T>(T item, Func<T, bool> test, string reason)
        {
            if (!test(item)) throw new ArgumentException(reason);
        }
    }
}