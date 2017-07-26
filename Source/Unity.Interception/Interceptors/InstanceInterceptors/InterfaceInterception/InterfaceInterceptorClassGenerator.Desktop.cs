// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Reflection.Emit;

namespace Microsoft.Practices.Unity.InterceptionExtension
{
    public partial class InterfaceInterceptorClassGenerator
    {
        private static ModuleBuilder _ModuleBuilder;

        private static ModuleBuilder GetModuleBuilder()
        {
            if (_ModuleBuilder == null)
            {
                string moduleName = Guid.NewGuid().ToString("N");
                _ModuleBuilder = AssemblyBuilder.DefineDynamicModule(moduleName);
            }

            return _ModuleBuilder;
        }
    }
}