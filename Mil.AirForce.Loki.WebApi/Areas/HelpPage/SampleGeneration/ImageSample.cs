using System;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage
{
	/// <summary>
	/// This represents an image sample on the help page. There's a display template named ImageSample associated with this class.
	/// </summary>
	public class ImageSample
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ImageSample"/> class.
		/// </summary>
		/// <param name="src">The URL of an image.</param>
		public ImageSample(string src)
		{
			if (src == null)
			{
				throw new ArgumentNullException("src");
			}
			Src = src;
		}

		/// <summary>
		/// Gets the image source.
		/// </summary>
		public string Src { get; private set; }

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <returns>
		/// true if the specified object  is equal to the current object; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			var other = obj as ImageSample;
			return other != null && Src == other.Src;
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
			return Src.GetHashCode();
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
			return Src;
		}
	}
}