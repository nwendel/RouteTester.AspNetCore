﻿namespace RouteTester.AspNetCore.Infrastructure;

[SuppressMessage("Design", "CA1064:Exceptions should be public", Justification = "Exception will never be thrown")]
internal class UnreachableCodeException : Exception
{
}
