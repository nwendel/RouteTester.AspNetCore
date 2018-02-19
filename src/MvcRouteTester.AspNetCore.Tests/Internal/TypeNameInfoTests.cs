﻿#region License
// Copyright (c) Niklas Wendel 2018
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
#endregion
using System;
using Xunit;
using MvcRouteTester.AspNetCore.Internal;

namespace MvcRouteTester.AspNetCore.Tests.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class TypeNameInfoTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanCreate()
        {
            var tested = new TypeNameInfo(typeof(object));

            Assert.Equal(typeof(object).Name, tested.Name);
            Assert.Equal(typeof(object).FullName, tested.FullName);
            Assert.Equal(typeof(object).AssemblyQualifiedName, tested.AssemblyQualifiedName);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnCreateNullType()
        {
            Assert.Throws<ArgumentNullException>(() => new TypeNameInfo(null));
        }
        
    }

}