using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lary.Laboratory.Core.Json
{
    /// <summary>
    /// Used by <see cref="JsonSerializer"/> to resolve a <see cref="JsonContract"/> for a given <see cref="Type"/>,
    /// sorts properties by inheritance level (base class properties first).
    /// </summary>
    public class BaseFirstContractResolver : DefaultContractResolver
    {
        /// <inheritdoc/>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);

            return properties.OrderBy(p => p.DeclaringType?.InheritanceLevels().Count()).ToList();
        }
    }
}
