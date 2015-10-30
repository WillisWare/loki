using System.Collections.ObjectModel;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage.ModelDescriptions
{
	/// <summary>
	/// Data class for complex models.
	/// </summary>
	public class ComplexTypeModelDescription : ModelDescription
	{
		/// <summary>
		/// Default constructor. Initializes an instance of this class.
		/// </summary>
		public ComplexTypeModelDescription()
		{
			Properties = new Collection<ParameterDescription>();
		}

		/// <summary>
		/// Gets the collection of properties for the model.
		/// </summary>
		public Collection<ParameterDescription> Properties { get; private set; }
	}
}