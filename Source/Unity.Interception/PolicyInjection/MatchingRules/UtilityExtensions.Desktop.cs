﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.Practices.Unity.InterceptionExtension
{
    /// <summary>
    /// Some utility extension methods to make things portable to Silverlight.
    /// </summary>
    internal static class UtilityExtensions
    {
        internal static bool IsReturn(this ParameterInfo parameterInfo)
        {
            return parameterInfo.IsRetval;
        }
    }
}
