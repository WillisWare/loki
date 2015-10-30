using System.Collections.ObjectModel;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage.ModelDescriptions
{
	/// <summary>
	/// Data class for enumeration-type models.
	/// </summary>
	public class EnumTypeModelDescription : ModelDescription
	{
		/// <summary>
		/// Default constructor. Initializes an instance of this class.
		/// </summary>
		public EnumTypeModelDescription()
		{
			Values = new Collection<EnumValueDescription>();
		}

		/// <summary>
		/// Gets the collection of enumeration values.
		/// </summary>
		public Collection<EnumValueDescription> Values { get; private set; }
	}
}