using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MvcRouteTester.AspNetCore.Internal;
using Xunit;

namespace MvcRouteTester.AspNetCore.Builders
{
    public class RouteTesterMapsToRouteAssert :
        IRouteAssertMapsToBuilder,
        IRouteAssert
    {
        private readonly ActualActionInvokeInfoCache _actionInvokeInfoCache;
        private readonly RouteExpressionParser _routeExpressionParser;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly List<ParameterAssert> _parameterAsserts = new();
        private ExpectedActionInvokeInfo _expectedActionInvokeInfo;

        public RouteTesterMapsToRouteAssert(
            ActualActionInvokeInfoCache actionInvokeInfoCache,
            RouteExpressionParser routeExpressionParser,
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionInvokeInfoCache = actionInvokeInfoCache;
            _routeExpressionParser = routeExpressionParser;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public void ParseActionCallExpression(LambdaExpression actionCallExpression)
        {
            _expectedActionInvokeInfo = _routeExpressionParser.Parse(actionCallExpression);

            var isActionMethod = _actionDescriptorCollectionProvider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>()
                .Any(x => x.MethodInfo == _expectedActionInvokeInfo.ActionMethodInfo);
            if (isActionMethod)
            {
                return;
            }

            var actionText = _expectedActionInvokeInfo.ActionMethodInfo.GetActionText(x => x.Name);
            throw new ArgumentException($"Method {actionText} is not a valid controller action", nameof(actionCallExpression));
        }

        public IRouteAssertMapsToBuilder ForParameter<T>(string name, Action<T> action)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

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
                throw new ArgumentException($"Invalid parameter type expected {expectedParameter.ParameterType.Name} but call was {typeof(T).Name}", nameof(T));
            }

            var parameterAssert = new ParameterAssert(name, x => { action.Invoke((T)x); });
            _parameterAsserts.Add(parameterAssert);
            return this;
        }

        public async Task AssertExpectedAsync(HttpResponseMessage responseMessage)
        {
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
            // TODO: Remove Xunit usage
            Assert.Equal(_expectedActionInvokeInfo.ActionMethodInfo, actualActionMethodInfo);
        }

        private void AssertExpectedParameterValues(ActualActionInvokeInfo actualActionInvokeInfo)
        {
            var parameterNames = _expectedActionInvokeInfo.ActionMethodInfo.GetParameters()
                .Select(x => x.Name)
                .ToList();
            foreach (var parameterName in parameterNames)
            {
                switch (_expectedActionInvokeInfo.ArgumentAssertKinds[parameterName])
                {
                    case ArgumentAssertKind.Value:
                        // TODO: Remove Xunit usage
                        Assert.Equal(
                            _expectedActionInvokeInfo.Arguments[parameterName],
                            actualActionInvokeInfo.Arguments.TryGetValue(parameterName, out var actual) ? actual : null);
                        break;
                    case ArgumentAssertKind.Any:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
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
    }
}
