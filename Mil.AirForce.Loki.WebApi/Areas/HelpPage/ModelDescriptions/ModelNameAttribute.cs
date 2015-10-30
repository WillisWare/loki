using System;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage.ModelDescriptions
{
	/// <summary>
	/// Use this attribute to change the name of the <see cref="ModelDescription"/> generated for a type.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
	public sealed class ModelNameAttribute : Attribute
	{
		/// <summary>
		/// Default constructor. Initializes an instance of this class.
		/// </summary>
		/// <param name="name">A String value containing the name of the model.</param>
		public ModelNameAttribute(string name)
		{
			Name = name;
		}

		/// <summary>
		/// Gets the name of the model.
		/// </summary>
		public string Name { get; private set; }
	}
}