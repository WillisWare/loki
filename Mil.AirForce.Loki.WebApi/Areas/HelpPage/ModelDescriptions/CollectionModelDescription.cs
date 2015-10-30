namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage.ModelDescriptions
{
	/// <summary>
	/// Data class for collection-type models.
	/// </summary>
	public class CollectionModelDescription : ModelDescription
	{
		/// <summary>
		/// Gets or sets the description of the collection's elements.
		/// </summary>
		public ModelDescription ElementDescription { get; set; }
	}
}