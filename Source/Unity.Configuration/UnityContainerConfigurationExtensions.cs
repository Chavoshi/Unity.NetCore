﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Configuration;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
    /// <summary>
    /// Extensions to <see cref="IUnityContainer"/> to simplify
    /// loading configuration into a container.
    /// </summary>
    public static class UnityContainerExtensions
    {
        /// <summary>
        /// Apply configuration from the given section and named container
        /// into the given container.
        /// </summary>
        /// <param name="container">Microsoft.Practices.Unity container to configure.</param>
        /// <param name="section">Configuration section with config information.</param>
        /// <param name="containerName">Named container.</param>
        /// <returns><paramref name="container"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods",
            Justification = "Validation done by Guard class")]
        public static IUnityContainer LoadConfiguration(this IUnityContainer container,
            UnityConfigurationSection section, string containerName)
        {
            Guard.ArgumentNotNull(container, "container");
            Guard.ArgumentNotNull(section, "section");

            section.Configure(container, containerName);
            return container;
        }

        /// <summary>
        /// Apply configuration from the default section (named "unity" pulled out of
        /// ConfigurationManager) and the named container.
        /// </summary>
        /// <param name="container">Microsoft.Practices.Unity container to configure.</param>
        /// <param name="containerName">Named container element in configuration.</param>
        /// <returns><paramref name="container"/>.</returns>
        public static IUnityContainer LoadConfiguration(this IUnityContainer container, string containerName)
        {
            Guard.ArgumentNotNull(container, "container");
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            return container.LoadConfiguration(section, containerName);
        }

        /// <summary>
        /// Apply configuration from the default section and unnamed container element.
        /// </summary>
        /// <param name="container">Container to configure.</param>
        /// <returns><paramref name="container"/>.</returns>
        public static IUnityContainer LoadConfiguration(this IUnityContainer container)
        {
            Guard.ArgumentNotNull(container, "container");
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            return container.LoadConfiguration(section, String.Empty);
        }

        /// <summary>
        /// Apply configuration from the default container in the given section.
        /// </summary>
        /// <param name="container">Microsoft.Practices.Unity container to configure.</param>
        /// <param name="section">Configuration section.</param>
        /// <returns><paramref name="container"/>.</returns>
        public static IUnityContainer LoadConfiguration(this IUnityContainer container, UnityConfigurationSection section)
        {
            Guard.ArgumentNotNull(container, "container");
            Guard.ArgumentNotNull(section, "section");
            return container.LoadConfiguration(section, String.Empty);
        }
    }
}
