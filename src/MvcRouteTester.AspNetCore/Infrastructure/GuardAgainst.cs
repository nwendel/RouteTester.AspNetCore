using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MvcRouteTester.AspNetCore.Infrastructure;

internal static class GuardAgainst
{
    public static void Condition([DoesNotReturnIf(true)] bool condition, string message, string argumentName)
    {
        if (condition)
        {
            throw new ArgumentException(message, argumentName);
        }
    }

    public static void Null<T>([NotNull] T? value, [CallerArgumentExpression("value")] string? argumentName = null)
        where T : class
    {
        if (value == null)
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    public static void NullOrWhiteSpace([NotNull] string? value, [CallerArgumentExpression("value")] string? argumentName = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    public static void Undefined<T>(T value, [CallerArgumentExpression("value")] string? argumentName = null)
        where T : struct, Enum
    {
        var isDefined = Enum.IsDefined(value);
        if (!isDefined)
        {
            throw new ArgumentOutOfRangeException(argumentName, $"Value {value} is not valid for type {typeof(T).Name}");
        }
    }
}
