﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity.InterceptionExtension
{
    /// <summary>
    /// An <see cref="IAdditionalInterfacesPolicy"/> that accumulates a sequence of 
    /// <see cref="Type"/> instances representing the additional interfaces for an intercepted object.
    /// </summary>
    public class AdditionalInterfacesPolicy : IAdditionalInterfacesPolicy
    {
        private readonly List<Type> additionalInterfaces;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalInterfacesPolicy"/> class.
        /// </summary>
        public AdditionalInterfacesPolicy()
        {
            this.additionalInterfaces = new List<Type>();
        }

        /// <summary>
        /// Gets the <see cref="Type"/> instances accumulated by this policy.
        /// </summary>
        public IEnumerable<Type> AdditionalInterfaces
        {
            get { return this.additionalInterfaces; }
        }

        internal void AddAdditionalInterface(Type additionalInterface)
        {
            this.additionalInterfaces.Add(additionalInterface);
        }

        internal static AdditionalInterfacesPolicy GetOrCreate(
            IPolicyList policies,
            Type typeToCreate,
            string name)
        {
            NamedTypeBuildKey key = new NamedTypeBuildKey(typeToCreate, name);
            IAdditionalInterfacesPolicy policy =
                policies.GetNoDefault<IAdditionalInterfacesPolicy>(key, false);
            if ((policy == null) || !(policy is AdditionalInterfacesPolicy))
            {
                policy = new AdditionalInterfacesPolicy();
                policies.Set<IAdditionalInterfacesPolicy>(policy, key);
            }
            return (AdditionalInterfacesPolicy)policy;
        }
    }
}
