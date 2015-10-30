using System.Collections.ObjectModel;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage.ModelDescriptions
{
	/// <summary>
	/// Data class for model parameters.
	/// </summary>
	public class ParameterDescription
	{
		/// <summary>
		/// Default constructor. Initializes an instance of this class.
		/// </summary>
		public ParameterDescription()
		{
			Annotations = new Collection<ParameterAnnotation>();
		}

		/// <summary>
		/// Gets the collection of annotations for the model parameter.
		/// </summary>
		public Collection<ParameterAnnotation> Annotations { get; private set; }

		/// <summary>
		/// Gets or sets the documentation for the model parameter.
		/// </summary>
		public string Documentation { get; set; }

		/// <summary>
		/// Gets or sets the name of the model parameter.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description of the model.
		/// </summary>
		public ModelDescription TypeDescription { get; set; }
	}
}