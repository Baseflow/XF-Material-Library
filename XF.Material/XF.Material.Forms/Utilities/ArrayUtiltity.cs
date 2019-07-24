using System;
using System.Collections;
using System.Collections.Generic;

namespace XF.Material.Forms.Utilities
{
    public static class ArrayUtility
    {
        /// <summary>
        /// Create a subset from a range of indices
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T[] RangeSubset<T>(this T[] array, int startIndex, int length)
        {
            T[] subset = new T[length];
            Array.Copy(array, startIndex, subset, 0, length);
            return subset;
        }

        /// <summary>
        /// Create a subset from a specific list of indices
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="indices"></param>
        /// <returns></returns>
        public static IList<T> Subset<T>(this IList<T> array, params int[] indices)
        {
            IList<T> subset = new T[indices.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                subset[i] = array[indices[i]];
            }
            return subset;
        }

        /// <summary>
        /// Create a subset from a specific list of indices
        /// </summary>
        /// <param name="array"></param>
        /// <param name="indices"></param>
        /// <returns></returns>
        public static IList Subset(this IList array, params int[] indices)
        {
            IList subset = (IList)Activator.CreateInstance(array.GetType());
            for (int i = 0; i < indices.Length; i++)
            {
                subset.Add(array[indices[i]]);
            }
            return subset;
        }
    }
}
