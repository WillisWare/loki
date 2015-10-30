namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage.ModelDescriptions
{
	/// <summary>
	/// Data class for enumeration values.
	/// </summary>
	public class EnumValueDescription
	{
		/// <summary>
		/// Gets or sets the documentation comments of the value.
		/// </summary>
		public string Documentation { get; set; }

		/// <summary>
		/// Gets or sets the name of the value.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the underlying value of the value.
		/// </summary>
		public string Value { get; set; }
	}
}