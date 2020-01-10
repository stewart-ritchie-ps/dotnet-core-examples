using System;
using System.Linq;

namespace Example.Model
{
    public static class Switches
    {
        /// <summary>
        /// Loads a <see cref="Switch"/> from configuration.
        /// </summary>
        /// <typeparam name="T">The switch type to create.</typeparam>
        /// <param name="key">The configuration key.</param>
        /// <returns>A <typeparamref name="T"/> instance</returns>
        /// <remarks>
        /// This will fail if the switch class does not have a public constructor 
        /// that accepts an appropriate state.
        /// </remarks>
        public static T From<T>(this IConfigLookup config, string key) where T : Switch
        {
            if (typeof(BooleanSwitch).IsAssignableFrom(typeof(T)))
            {
                return (T)Activator.CreateInstance(
                    typeof(T), 
                    new object[] 
                    { 
                        ParseBoolean(config.Lookup(key)) 
                    });
            }

            throw new InvalidOperationException($"Cannot create switch of type {typeof(T).Name}.");
        }

        private static bool ParseBoolean(string value) => 
            new[] 
            { 
                "on", 
                "true" 
            }
            .Any(v => v.Equals(value, StringComparison.CurrentCultureIgnoreCase));
    }
}
