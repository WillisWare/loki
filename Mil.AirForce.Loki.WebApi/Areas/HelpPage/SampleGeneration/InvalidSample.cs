using System;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage
{
	/// <summary>
	/// This represents an invalid sample on the help page. There's a display template named InvalidSample associated with this class.
	/// </summary>
	public class InvalidSample
	{
		/// <summary>
		/// Default constructor. Initializes an instance of this class.
		/// </summary>
		/// <param name="errorMessage">A String value containing the error message.</param>
		/// <exception cref="ArgumentNullException">Thrown if errorMessage is null.</exception>
		public InvalidSample(string errorMessage)
		{
			if (errorMessage == null)
			{
				throw new ArgumentNullException("errorMessage");
			}
			ErrorMessage = errorMessage;
		}

		/// <summary>
		/// Gets the error message.
		/// </summary>
		public string ErrorMessage { get; private set; }

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <returns>
		/// true if the specified object  is equal to the current object; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			var other = obj as InvalidSample;
			return other != null && ErrorMessage == other.ErrorMessage;
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
			return ErrorMessage.GetHashCode();
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
			return ErrorMessage;
		}
	}
}