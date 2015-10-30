using System;
using System.Reflection;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage.ModelDescriptions
{
	/// <summary>
	/// Defines the structure of a documentation provider.
	/// </summary>
	public interface IModelDocumentationProvider
	{
		/// <summary>
		/// Gets the documentation for a class member.
		/// </summary>
		/// <param name="member">A MemberInfo instance containing the member info.</param>
		/// <returns>A String value containing the documentation.</returns>
		string GetDocumentation(MemberInfo member);

		/// <summary>
		/// Gets the documentation for a class.
		/// </summary>
		/// <param name="type">A Type definition for the class.</param>
		/// <returns>A String value containing the documentation.</returns>
		string GetDocumentation(Type type);
	}
}