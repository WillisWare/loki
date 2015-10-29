using System.Text.RegularExpressions;

namespace Mil.AirForce.Loki.Common.Extensions
{
	/// <summary>
	/// Extensions class for <see cref="System.String"/> values.
	/// </summary>
	public static class StringExtensions
	{
		#region Methods

		/// <summary>
		/// Evaluates a <see cref="System.String"/> value for non-numeric characters.
		/// </summary>
		/// <param name="source">A <see cref="System.String"/> value to be evaluated.</param>
		/// <returns>A <see cref="System.Boolean"/> value indicating whether or not all characters in the <see cref="System.String"/> are numeric.</returns>
		public static bool IsNumeric(this string source)
		{
			return new Regex(@"^[\d]+$", RegexOptions.CultureInvariant).IsMatch(source);
		}

		/// <summary>
		/// Evaluates a <see cref="System.String"/> value for null or <see cref="System.String.Empty"/> and replaces with a specified value if true.
		/// </summary>
		/// <param name="source">A <see cref="System.String"/> value to be evaluated.</param>
		/// <param name="replaceWith">A <see cref="System.String"/> value to be used as a replacement.</param>
		/// <returns>A <see cref="System.String"/> value containing the source if not null or empty; otherwise, the replacement value.</returns>
		public static string ReplaceNullOrEmpty(this string source, string replaceWith)
		{
			return (string.IsNullOrWhiteSpace(source) ? replaceWith : source);
		}

		#endregion
	}
}
