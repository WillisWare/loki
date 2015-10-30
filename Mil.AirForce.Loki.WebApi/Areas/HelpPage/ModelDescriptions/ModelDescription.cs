using System;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage.ModelDescriptions
{
	/// <summary>
	/// Describes a type model.
	/// </summary>
	public abstract class ModelDescription
	{
		/// <summary>
		/// Gets or sets the documentation comments for the model.
		/// </summary>
		public string Documentation { get; set; }

		/// <summary>
		/// Gets or sets the type of the model.
		/// </summary>
		public Type ModelType { get; set; }

		/// <summary>
		/// Gets or sets the name of the model.
		/// </summary>
		public string Name { get; set; }
	}
}