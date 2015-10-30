using System;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage
{
	/// <summary>
	/// This represents a preformatted text sample on the help page. There's a display template named TextSample associated with this class.
	/// </summary>
	public class TextSample
	{
		/// <summary>
		/// Default constructor. Initializes an instance of this class.
		/// </summary>
		/// <param name="text">A String value containing the text.</param>
		/// <exception cref="ArgumentNullException">Thrown if text is null.</exception>
		public TextSample(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentNullException("text");
			}
			Text = text;
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		public string Text { get; private set; }

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <returns>
		/// true if the specified object  is equal to the current object; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			var other = obj as TextSample;
			return other != null && Text == other.Text;
		}

		/// <summary>
		/// Serves as the default hash function. 
		/// </summary>
		/// <returns>
		/// A hash code for the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return Text.GetHashCode();
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return Text;
		}
	}
}