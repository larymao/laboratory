using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lary.Laboratory.Core.Json;

/// <summary>
/// Used by <see cref="JsonSerializer"/> to resolve a <see cref="JsonContract"/> for a given <see cref="Type"/>,
/// ignores properties within the given set.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="IgnorePropertiesResolver"/> class.
/// </remarks>
/// <param name="propNamesToIgnore">The name of properties to be ignored while data resolving.</param>
public class IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore) : DefaultContractResolver
{
    private readonly HashSet<string> _ignoreProps = new(propNamesToIgnore);

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
