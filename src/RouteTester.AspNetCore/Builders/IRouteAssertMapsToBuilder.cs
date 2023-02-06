﻿namespace RouteTester.AspNetCore.Builders;

public interface IRouteAssertMapsToBuilder
{
    IRouteAssertMapsToBuilder ForParameter<T>(string name, Action<T?> action);
}