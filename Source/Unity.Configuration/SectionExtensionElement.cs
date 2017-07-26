﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Configuration;
using System.Globalization;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
    /// <summary>
    /// A configuration element used to specify which extensions to
    /// add to the configuration schema.
    /// </summary>
    public class SectionExtensionElement : DeserializableConfigurationElement
    {
        private const string TypeNamePropertyName = "type";
        private const string PrefixPropertyName = "prefix";

        private SectionExtension extensionObject;

        /// <summary>
        /// Type of the section extender object that will provide new elements to the schema.
        /// </summary>
        [ConfigurationProperty(TypeNamePropertyName, IsRequired = true, IsKey = true)]
        public string TypeName
        {
            get { return (string)base[TypeNamePropertyName]; }
            set
            {
                base[TypeNamePropertyName] = value;
                this.extensionObject = null;
            }
        }

        /// <summary>
        /// Optional prefix that will be added to the element names added by this
        /// section extender. If left out, no prefix will be added.
        /// </summary>
        [ConfigurationProperty(PrefixPropertyName, IsRequired = false)]
        public string Prefix
        {
            get { return (string)base[PrefixPropertyName]; }
            set { base[PrefixPropertyName] = value; }
        }

        /// <summary>
        /// The extension object represented by this element.
        /// </summary>
        public SectionExtension ExtensionObject
        {
            get
            {
                if (this.extensionObject == null)
                {
                    this.extensionObject = (SectionExtension)Activator.CreateInstance(this.GetExtensionObjectType());
                }
                return this.extensionObject;
            }
        }

        /// <summary>
        /// Reads XML from the configuration file.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> that reads from the configuration file.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        /// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element to read is locked.
        /// - or -
        /// An attribute of the current node is not recognized.
        /// - or -
        /// The lock status of the current node cannot be determined.  
        /// </exception>
        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            base.DeserializeElement(reader, serializeCollectionKey);
            this.GetExtensionObjectType();
        }

        /// <summary>
        /// Write the contents of this element to the given <see cref="XmlWriter"/>.
        /// </summary>
        /// <remarks>The caller of this method has already written the start element tag before
        /// calling this method, so deriving classes only need to write the element content, not
        /// the start or end tags.</remarks>
        /// <param name="writer">Writer to send XML content to.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods",
            Justification = "Validation done by Guard class")]
        public override void SerializeContent(XmlWriter writer)
        {
            Guard.ArgumentNotNull(writer, "writer");
            writer.WriteAttributeString(SectionExtensionElement.TypeNamePropertyName, this.TypeName);
            if (!string.IsNullOrEmpty(this.Prefix))
            {
                writer.WriteAttributeString(SectionExtensionElement.PrefixPropertyName, this.Prefix);
            }
        }

        private void GuardIsValidExtensionType(Type extensionType)
        {
            if (extensionType == null)
            {
                throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture,
                    Resources.ExtensionTypeNotFound, this.TypeName));
            }

            if (!typeof(SectionExtension).IsAssignableFrom(extensionType))
            {
                throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture,
                    Resources.ExtensionTypeNotValid, this.TypeName));
            }
        }

        private Type GetExtensionObjectType()
        {
            Type extensionType = TypeResolver.ResolveType(this.TypeName);
            this.GuardIsValidExtensionType(extensionType);
            return extensionType;
        }
    }
}
