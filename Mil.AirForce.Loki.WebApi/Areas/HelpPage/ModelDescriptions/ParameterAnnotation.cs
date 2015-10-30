using System;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage.ModelDescriptions
{
	/// <summary>
	/// Data class for model parameters.
	/// </summary>
	public class ParameterAnnotation
	{
		/// <summary>
		/// Gets or sets the model parameter attribute.
		/// </summary>
		public Attribute AnnotationAttribute { get; set; }

		/// <summary>
		/// Gets or sets the documentation comments for the model parameter.
		/// </summary>
		public string Documentation { get; set; }
	}
}