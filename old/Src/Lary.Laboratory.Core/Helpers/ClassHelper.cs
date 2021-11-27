using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lary.Laboratory.Core.Helpers
{
    /// <summary>
    ///     Helper for class.
    /// </summary>
    public static class ClassHelper
    {
        /// <summary>
        ///     Enumerate the subclasses of the type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of class being enumerated.
        /// </typeparam>
        /// <returns>
        ///     The subclasses.
        /// </returns>
        public static IEnumerable<Type> SubclassesOf<T>()
            where T : class
        {
            return Assembly.GetAssembly(typeof(T))
                           .GetTypes()
                           .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(T)));
        }

        /// <summary>
        ///     Create an instance for each subclass of the type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of base class.
        /// </typeparam>
        /// <param name="constructorArgs">
        ///     The constructor arguments.
        /// </param>
        /// <returns>
        ///     The instances of subclasses.
        /// </returns>
        public static IEnumerable<T> CreateChildren4Type<T>(params object[] constructorArgs)
            where T : class
        {
            var objects = new List<T>();

            var children = SubclassesOf<T>();

            foreach (var child in children)
            {
                objects.Add((T)Activator.CreateInstance(child, constructorArgs));
            }

            return objects;
        }

        /// <summary>
        ///     Create an instance for each subclass of the type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of base class.
        /// </typeparam>
        /// <param name="predicate">
        ///     A function to test each element for a condition.
        /// </param>
        /// <param name="constructorArgs">
        ///     The constructor arguments.
        /// </param>
        /// <returns>
        ///     The instances of subclasses.
        /// </returns>
        public static IEnumerable<T> CreateChildren4Type<T>(Func<Type, bool> predicate, params object[] constructorArgs)
            where T : class
        {
            var objects = new List<T>();

            var types = SubclassesOf<T>().Where(predicate);

            foreach (var type in types)
            {
                objects.Add((T)Activator.CreateInstance(type, constructorArgs));
            }

            return objects;
        }
    }
}
