namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage.ModelDescriptions
{
	/// <summary>
	/// Data class for a keyed value-type model.
	/// </summary>
	public class KeyValuePairModelDescription : ModelDescription
	{
		/// <summary>
		/// Gets or sets the description of the key.
		/// </summary>
		public ModelDescription KeyModelDescription { get; set; }

		/// <summary>
		/// Gets or sets the description of the value.
		/// </summary>
		public ModelDescription ValueModelDescription { get; set; }
	}
}