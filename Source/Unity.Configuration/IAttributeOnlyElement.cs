﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Xml;

namespace Microsoft.Practices.Unity.Configuration
{
    internal interface IAttributeOnlyElement
    {
        void SerializeContent(XmlWriter writer);
    }
}
