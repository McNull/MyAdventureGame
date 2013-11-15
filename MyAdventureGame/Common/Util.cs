using System;
using System.Linq;
using System.Collections.Generic;

namespace MyAdventureGame
{
    /// <summary>
    /// Utility class with shared utilities.
    /// </summary>
    public static class Util
    {
        private static Random rnd = new Random();

        /// <summary>
        /// Selects a random item from a list.
        /// </summary>
        /// <returns>The random selected item.</returns>
        /// <param name="items">The list of items.</param>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        public static T SelectRandom<T>(this IEnumerable<T> items)
        {
            if(items == null)
            {
                throw new ArgumentNullException();
            }
            
            if (!items.Any())
            {
                throw new ArgumentException("The sequence is empty.");
            }
            
            // Optimization for ICollection<T>

            if (items is ICollection<T>)
            {
                ICollection<T> col = (ICollection<T>)items;
                return col.ElementAt(rnd.Next(col.Count));
            }
            
            int count = 1;
            T selected = default(T);
            
            foreach (T element in items)
            {
                if (rnd.Next(count++) == 0)
                {
                    //Select the current element with 1/count probability
                    selected = element;
                }
            }
            
            return selected;
        }

    }
}

