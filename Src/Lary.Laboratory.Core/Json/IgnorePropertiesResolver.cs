using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lary.Laboratory.Core.Json
{
    /// <summary>
    /// Used by <see cref="JsonSerializer"/> to resolve a <see cref="JsonContract"/> for a given <see cref="Type"/>,
    /// ignores properties within the given set.
    /// </summary>
    public class IgnorePropertiesResolver : DefaultContractResolver
    {
        private readonly HashSet<string> _ignoreProps;

        /// <summary>
        /// Initializes a new instance of the <see cref="IgnorePropertiesResolver"/> class.
        /// </summary>
        /// <param name="propNamesToIgnore">The name of properties to be ignored while data resolving.</param>
        public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
        {
            _ignoreProps = new HashSet<string>(propNamesToIgnore);
        }

        /// <inheritdoc/>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            if (_ignoreProps.Contains(prop.PropertyName!))
            {
                prop.ShouldSerialize = _ => false;
            }

            return prop;
        }
    }
}
