using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using MvcRouteTester.AspNetCore.Internal;

namespace MvcRouteTester.AspNetCore.Builders;

public class RouteTesterMapsToRouteAssert :
    IRouteAssertMapsToBuilder,
    IRouteAssert
{
    private readonly ActualActionInvokeInfoCache _actionInvokeInfoCache;
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
    private readonly List<ParameterAssert> _parameterAsserts = new();
    private ExpectedActionInvokeInfo? _expectedActionInvokeInfo;

    public RouteTesterMapsToRouteAssert(
        ActualActionInvokeInfoCache actionInvokeInfoCache,
        IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    {
        _actionInvokeInfoCache = actionInvokeInfoCache;
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    }

    public void ParseActionCallExpression(LambdaExpression actionCallExpression)
    {
        _expectedActionInvokeInfo = RouteExpressionParser.Parse(actionCallExpression);

        var isActionMethod = _actionDescriptorCollectionProvider
            .ActionDescriptors
            .Items
            .OfType<ControllerActionDescriptor>()
            .Any(x => x.MethodInfo == _expectedActionInvokeInfo.ActionMethodInfo);
        if (isActionMethod)
        {
            return;
        }

        var actionText = _expectedActionInvokeInfo.ActionMethodInfo.GetActionText();
        throw new ArgumentException($"Method {actionText} is not a valid controller action", nameof(actionCallExpression));
    }

    public IRouteAssertMapsToBuilder ForParameter<T>(string name, Action<T?> action)
    {
        GuardAgainst.Null(name);
        GuardAgainst.Null(action);

        EnsureExpectedActionInvokeInfo();

        var expectedParameter = _expectedActionInvokeInfo
            .ActionMethodInfo
            .GetParameters()
            .SingleOrDefault(x => x.Name == name);
        if (expectedParameter == null)
        {
            throw new ArgumentException($"Invalid parameter name {name}", nameof(name));
        }

        if (expectedParameter.ParameterType != typeof(T))
        {
            throw new ArgumentException($"Invalid parameter type. Expected {expectedParameter.ParameterType.Name} but was {typeof(T).Name}", nameof(action));
        }

        var parameterAssert = new ParameterAssert(name, x => { action.Invoke((T?)x); });
        _parameterAsserts.Add(parameterAssert);
        return this;
    }

    public async Task AssertExpectedAsync(HttpResponseMessage responseMessage)
    {
        GuardAgainst.Null(responseMessage);

        responseMessage.EnsureSuccessStatusCode();

        var key = await responseMessage.Content.ReadAsStringAsync();
        var actualActionInvokeInfo = _actionInvokeInfoCache[key];
        _actionInvokeInfoCache.Remove(key);

        AssertExpectedMethodInfo(actualActionInvokeInfo.ActionMethodInfo);
        AssertExpectedParameterValues(actualActionInvokeInfo);
        AssertExpectedParameterAsserts(actualActionInvokeInfo);
    }

    private void AssertExpectedMethodInfo(MethodInfo actualActionMethodInfo)
    {
        EnsureExpectedActionInvokeInfo();

        TestFramework.Equal(_expectedActionInvokeInfo.ActionMethodInfo, actualActionMethodInfo);
    }

    private void AssertExpectedParameterValues(ActualActionInvokeInfo actualActionInvokeInfo)
    {
        EnsureExpectedActionInvokeInfo();

        var parameterNames = _expectedActionInvokeInfo.ActionMethodInfo.GetParameters()
            .Select(x => x.Name)
            .ToList();
        foreach (var parameterName in parameterNames)
        {
            if (parameterName == null)
            {
                throw new InvalidOperationException("No parameter name");
            }

            var expectedArgument = _expectedActionInvokeInfo.Arguments[parameterName];
            switch (expectedArgument.Kind)
            {
                case ArgumentAssertKind.Value:
                    TestFramework.Equal(
                        expectedArgument.Value,
                        actualActionInvokeInfo.Arguments.TryGetValue(parameterName, out var actual) ? actual : null);
                    break;
                case ArgumentAssertKind.Any:
                    break;
            }
        }
    }

    private void AssertExpectedParameterAsserts(ActualActionInvokeInfo actualActionInvokeInfo)
    {
        foreach (var parameterAssert in _parameterAsserts)
        {
            var value = actualActionInvokeInfo.Arguments[parameterAssert.Name];
            parameterAssert.Action(value);
        }
    }

    [MemberNotNull(nameof(_expectedActionInvokeInfo))]
    private void EnsureExpectedActionInvokeInfo()
    {
        if (_expectedActionInvokeInfo == null)
        {
            throw new InvalidOperationException("No expected action invok info");
        }
    }
}
