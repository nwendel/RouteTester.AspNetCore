#region License
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
using System.Collections.Generic;
using Xunit;
using MvcRouteTester.AspNetCore.Internal;

namespace MvcRouteTester.AspNetCore.Tests.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ExtensionsRemoveRangeTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRemoveRange()
        {
            var tested = new List<string> { "one", "two", "three" };

            tested.RemoveRange(new[] { "one", "two" });

            Assert.Single(tested);
            Assert.Contains("three", tested);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnRemoveRangeNullSelf()
        {
            List<string> tested = null;

            Assert.Throws<ArgumentNullException>("self", () => tested.RemoveRange(new[] {"one"}));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnRemoveRangeNullItems()
        {
            var tested = new List<string> { "one" };

            Assert.Throws<ArgumentNullException>("items", () => tested.RemoveRange(null));
        }

    }

}
