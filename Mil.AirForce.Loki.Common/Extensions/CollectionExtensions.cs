using System;
using System.Collections.Generic;
using System.Linq;

namespace Mil.AirForce.Loki.Common.Extensions
{
	/// <summary>
	/// Extensions class for <see cref="System.Collections.Generic.IEnumerable{T}"/> implementations.
	/// </summary>
	public static class CollectionExtensions
	{
		#region Methods

		/// <summary>
		/// Evaluates the contents of an <see cref="System.Collections.Generic.IEnumerable{T}"/> for the presence of any items contained in <paramref name="values"/>.
		/// </summary>
		/// <typeparam name="T">A <see cref="System.Type"/> indicating the type of items contained in the collections.</typeparam>
		/// <param name="source">An <see cref="System.Collections.Generic.IEnumerable{T}"/> implementation to be evaluated.</param>
		/// <param name="values">An <see cref="System.Collections.Generic.IEnumerable{T}"/> implementation to be compared.</param>
		/// <returns>A <see cref="System.Boolean"/> value indicating whether any of the items in <paramref name="values"/> were found in <paramref name="source"/>.</returns>
		public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> values)
		{
			if (source == null)
			{
				return false;
			}

			if (typeof(T) != typeof(String))
			{
				return values.Any(source.Contains);
			}

			// Case-insensitive comparison on string types.
			return (from item in values
					from val in source
					where string.Equals(val as string, item as string, StringComparison.InvariantCultureIgnoreCase)
					select item)
				.Any();
		}

		/// <summary>
		/// Makes an exact copy of the specified <see cref="System.Collections.Generic.IEnumerable{T}"/>.
		/// </summary>
		/// <typeparam name="T">A <see cref="System.Type"/> indicating the type of items contained in the collection.</typeparam>
		/// <param name="source">An <see cref="System.Collections.Generic.IEnumerable{T}"/> implementation to be copied.</param>
		/// <returns>An <see cref="System.Collections.Generic.IEnumerable{T}"/> implementation containing all of the items in <paramref name="source"/>.</returns>
		public static IEnumerable<T> Copy<T>(this IEnumerable<T> source)
		{
			var returnValue = new List<T>();

			returnValue.AddRange(source);

			return returnValue;
		}

		/// <summary>
		/// Performs an <see cref="System.Action"/> over every member of an <see cref="System.Collections.Generic.IEnumerable{T}"/> implementation.
		/// </summary>
		/// <typeparam name="T">A <see cref="System.Type"/> indicating the type of items contained in the collection.</typeparam>
		/// <param name="source">An <see cref="System.Collections.Generic.IEnumerable{T}"/> implementation to which the action will be applied.</param>
		/// <param name="action">An <see cref="System.Action"/> delegate to be executed on <paramref name="source"/>.</param>
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null)
			{
				return;
			}

			foreach (var item in source)
			{
				action(item);
			}
		}

		/// <summary>
		/// Evaluates the contents of an <see cref="System.Collections.Generic.IEnumerable{T}"/> for any items that are not null or empty.
		/// </summary>
		/// <typeparam name="T">A <see cref="System.Type"/> indicating the type of items contained in the collection.</typeparam>
		/// <param name="source">An <see cref="System.Collections.Generic.IEnumerable{T}"/> implementation to be evaluated.</param>
		/// <returns>A <see cref="System.Boolean"/> value indicating whether any of the items in <paramref name="source"/> have values.</returns>
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
		{
			if (source == null || !source.Any())
			{
				return true;
			}

			if (typeof(T) == typeof(String))
			{
				return ((IEnumerable<string>)source).All(string.IsNullOrWhiteSpace);
			}

			return false;
		}

		#endregion
	}
}
