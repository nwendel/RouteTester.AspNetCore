﻿using System;

namespace MvcRouteTester.AspNetCore.Builders
{
    public class ParameterAssert
    {
        public ParameterAssert(string name, Action<object> action)
        {
            Name = name;
            Action = action;
        }

        public string Name { get; }

        public Action<object> Action { get; }
    }
}
