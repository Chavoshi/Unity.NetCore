﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Reflection;

namespace Microsoft.Practices.Unity.InterceptionExtension
{
    /// <summary>
    /// A <see cref="IMatchingRule"/> that checks a member for the presence
    /// of the <see cref="TagAttribute"/> on the method, property, or class, and
    /// that the given string matches.
    /// </summary>
    public class TagAttributeMatchingRule : IMatchingRule
    {
        private readonly string tagToMatch;
        private readonly bool ignoreCase;

        /// <summary>
        /// Constructs a new <see cref="TagAttributeMatchingRule"/>, looking for
        /// the given string. The comparison is case sensitive.
        /// </summary>
        /// <param name="tagToMatch">tag string to match.</param>
        public TagAttributeMatchingRule(string tagToMatch)
            : this(tagToMatch, false)
        {
        }

        /// <summary>
        /// Constructs a new <see cref="TagAttributeMatchingRule"/>, looking for
        /// the given string. The comparison is case sensitive if <paramref name="ignoreCase"/> is
        /// false, case insensitive if <paramref name="ignoreCase"/> is true.
        /// </summary>
        /// <param name="tagToMatch">tag string to match.</param>
        /// <param name="ignoreCase">if false, case-sensitive comparison. If true, case-insensitive comparison.</param>
        public TagAttributeMatchingRule(string tagToMatch, bool ignoreCase)
        {
            this.tagToMatch = tagToMatch;
            this.ignoreCase = ignoreCase;
        }

        /// <summary>
        /// Check the given member for the presence of the <see cref="TagAttribute"/> and
        /// match the strings.
        /// </summary>
        /// <param name="member">Member to check.</param>
        /// <returns>True if tag strings match, false if they don't.</returns>
        public bool Matches(MethodBase member)
        {
            foreach (TagAttribute tagAttribute in ReflectionHelper.GetAllAttributes<TagAttribute>(member, true))
            {
                if (string.Compare(tagAttribute.Tag, tagToMatch, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
