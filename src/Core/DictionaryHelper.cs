using System.Collections;
using System.Linq.Expressions;

namespace Lary.Laboratory.Core;

/// <summary>
/// Provides methods for dictionary.
/// </summary><typeparam name="T">The convert type.</typeparam>
public static class DictionaryHelper<T>
    where T : class, new()
{
    private static readonly Func<Dictionary<string, object?>, T> _funcToObject = DictionaryHelper.ToObject<T>();
    private static readonly Func<T, Dictionary<string, object?>> _funcFromObject = DictionaryHelper.FromObject<T>();

    /// <summary>
    /// Converts the given dictionary to an object of the specified type.
    /// </summary>
    /// <param name="dic">The dictionary to be converted.</param>
    /// <returns>An object of the given type.</returns>
    public static T ToObject(Dictionary<string, object?> dic)
    {
        return _funcToObject(dic);
    }

    /// <summary>
    /// Converts the given object to a dictionary.
    /// </summary>
    /// <param name="obj">The object to be converted.</param>
    /// <returns>A dictionary with property name as the entry key, and property value as entry value.</returns>
    public static Dictionary<string, object?> FromObject(T obj)
    {
        return _funcFromObject(obj);
    }
}

/// <summary>
/// Provides methods for dictionary.
/// </summary>
public static class DictionaryHelper
{
    /// <summary>
    /// Converts the given <see cref="IDictionary"/> object to a generic one.
    /// </summary>
    /// <typeparam name="TKey">Key type of the output generic dictionary.</typeparam>
    /// <typeparam name="TValue">Value type of the output generic dictionary.</typeparam>
    /// <param name="dic">An <see cref="IDictionary"/> object.</param>
    /// <returns>A generic dictionary object.</returns>
    public static Dictionary<TKey, TValue> ToGeneric<TKey, TValue>(this IDictionary dic)
    {
        return dic.ToGeneric(key => (TKey)key, val => (TValue)val!);
    }

    /// <summary>
    /// Converts the given <see cref="IDictionary"/> object to a generic one.
    /// </summary>
    /// <typeparam name="TKey">Key type of the output generic dictionary.</typeparam>
    /// <typeparam name="TValue">Value type of the output generic dictionary.</typeparam>
    /// <param name="dic">An <see cref="IDictionary"/> object.</param>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
    /// <returns>A generic dictionary object.</returns>
    public static Dictionary<TKey, TValue> ToGeneric<TKey, TValue>(
        this IDictionary dic, Func<object, TKey> keySelector, Func<object?, TValue> elementSelector)
    {
        return dic.GetEntries().ToDictionary(
            x => keySelector(x.Key), x => elementSelector(x.Value));
    }

    /// <summary>
    /// Gets entries of a dictionary.
    /// </summary>
    /// <param name="dic">An <see cref="IDictionary"/> object.</param>
    /// <returns>A collection of dictionary entries.</returns>
    public static IEnumerable<DictionaryEntry> GetEntries(this IDictionary dic)
    {
        foreach (DictionaryEntry entry in dic)
        {
            yield return entry;
        }
    }

    internal static Func<Dictionary<string, object?>, T> ToObject<T>()
        where T : class, new()
    {
        var objType = typeof(T);
        var dicType = typeof(Dictionary<string, object?>);
        var objProps = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var obj = Expression.Variable(objType, "obj");
        var param = Expression.Parameter(dicType, "dic");
        var body = new List<Expression>
        {
            Expression.Assign(obj, Expression.New(objType))
        };

        foreach (var prop in objProps)
        {
            // 确保可写、有效
            if (!prop.CanWrite || prop.GetIndexParameters().Length != 0)
            {
                continue;
            }

            var key = Expression.Constant(prop.Name);
            var val = Expression.Property(param, "Item", key);
            var objProp = Expression.Property(obj, prop);
            var exp = Expression.IfThenElse(
                Expression.Call(param, "ContainsKey", null, key),
                Expression.Assign(objProp, Expression.Convert(val, prop.PropertyType)),
                Expression.Assign(objProp, Expression.Default(prop.PropertyType))
            );

            body.Add(exp);
        }

        // 返回值
        body.Add(obj);

        var block = Expression.Block([obj], body);
        var expression = Expression.Lambda<Func<Dictionary<string, object?>, T>>(block, param);
        return expression.Compile();
    }

    internal static Func<T, Dictionary<string, object?>> FromObject<T>()
        where T : class, new()
    {
        var objType = typeof(T);
        var dicType = typeof(Dictionary<string, object?>);
        var objProps = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var dic = Expression.Variable(dicType, "dic");
        var obj = Expression.Parameter(objType, "obj");
        var body = new List<Expression>
        {
            Expression.Assign(dic, Expression.New(dicType))
        };

        foreach (var prop in objProps)
        {
            if (prop.GetIndexParameters().Length != 0)
            {
                continue;
            }

            var key = Expression.Constant(prop.Name);
            var val = Expression.Property(obj, prop);
            var objVal = Expression.Convert(val, typeof(object));

            body.Add(Expression.Call(dic, "Add", null, key, objVal));
        }

        // 返回值
        body.Add(dic);

        var block = Expression.Block([dic], body);

        var lambda = Expression.Lambda<Func<T, Dictionary<string, object?>>>(block, obj);
        return lambda.Compile();
    }
}
