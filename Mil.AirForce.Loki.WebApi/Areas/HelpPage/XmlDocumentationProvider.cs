using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Xml.XPath;
using Mil.AirForce.Loki.WebApi.Areas.HelpPage.ModelDescriptions;

namespace Mil.AirForce.Loki.WebApi.Areas.HelpPage
{
	/// <summary>
	/// A custom <see cref="IDocumentationProvider"/> that reads the API documentation from an XML documentation file.
	/// </summary>
	public class XmlDocumentationProvider : IDocumentationProvider, IModelDocumentationProvider
	{
		private readonly XPathNavigator _documentNavigator;
		private const string TYPE_EXPRESSION = "/doc/members/member[@name='T:{0}']";
		private const string METHOD_EXPRESSION = "/doc/members/member[@name='M:{0}']";
		private const string PROPERTY_EXPRESSION = "/doc/members/member[@name='P:{0}']";
		private const string FIELD_EXPRESSION = "/doc/members/member[@name='F:{0}']";
		private const string PARAMETER_EXPRESSION = "param[@name='{0}']";

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlDocumentationProvider"/> class.
		/// </summary>
		/// <param name="documentPath">The physical path to XML document.</param>
		public XmlDocumentationProvider(string documentPath)
		{
			if (documentPath == null)
			{
				throw new ArgumentNullException("documentPath");
			}
			var xpath = new XPathDocument(documentPath);
			_documentNavigator = xpath.CreateNavigator();
		}

		string IDocumentationProvider.GetDocumentation(HttpControllerDescriptor controllerDescriptor)
		{
			var typeNode = GetTypeNode(controllerDescriptor.ControllerType);
			return GetTagValue(typeNode, "summary");
		}

		/// <summary>
		/// Gets the documentation based on <see cref="T:System.Web.Http.Controllers.HttpActionDescriptor"/>. 
		/// </summary>
		/// <returns>
		/// The documentation for the controller.
		/// </returns>
		/// <param name="actionDescriptor">The action descriptor.</param>
		public virtual string GetDocumentation(HttpActionDescriptor actionDescriptor)
		{
			var methodNode = GetMethodNode(actionDescriptor);
			return GetTagValue(methodNode, "summary");
		}

		/// <summary>
		/// Gets the documentation based on <see cref="T:System.Web.Http.Controllers.HttpParameterDescriptor"/>. 
		/// </summary>
		/// <returns>
		/// The documentation for the controller.
		/// </returns>
		/// <param name="parameterDescriptor">The parameter descriptor.</param>
		public virtual string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
		{
			var reflectedParameterDescriptor = parameterDescriptor as ReflectedHttpParameterDescriptor;
			if (reflectedParameterDescriptor != null)
			{
				var methodNode = GetMethodNode(reflectedParameterDescriptor.ActionDescriptor);
				if (methodNode != null)
				{
					var parameterName = reflectedParameterDescriptor.ParameterInfo.Name;
					var parameterNode = methodNode.SelectSingleNode(string.Format(CultureInfo.InvariantCulture, PARAMETER_EXPRESSION, parameterName));
					if (parameterNode != null)
					{
						return parameterNode.Value.Trim();
					}
				}
			}

			return null;
		}

		string IDocumentationProvider.GetResponseDocumentation(HttpActionDescriptor actionDescriptor)
		{
			var methodNode = GetMethodNode(actionDescriptor);
			return GetTagValue(methodNode, "returns");
		}

		string IModelDocumentationProvider.GetDocumentation(MemberInfo member)
		{
			var memberName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", GetTypeName(member.DeclaringType), member.Name);
			var expression = member.MemberType == MemberTypes.Field ? FIELD_EXPRESSION : PROPERTY_EXPRESSION;
			var selectExpression = string.Format(CultureInfo.InvariantCulture, expression, memberName);
			var propertyNode = _documentNavigator.SelectSingleNode(selectExpression);
			return GetTagValue(propertyNode, "summary");
		}

		string IModelDocumentationProvider.GetDocumentation(Type type)
		{
			var typeNode = GetTypeNode(type);
			return GetTagValue(typeNode, "summary");
		}

		private XPathNavigator GetMethodNode(HttpActionDescriptor actionDescriptor)
		{
			var reflectedActionDescriptor = actionDescriptor as ReflectedHttpActionDescriptor;
			if (reflectedActionDescriptor != null)
			{
				var selectExpression = string.Format(CultureInfo.InvariantCulture, METHOD_EXPRESSION, GetMemberName(reflectedActionDescriptor.MethodInfo));
				return _documentNavigator.SelectSingleNode(selectExpression);
			}

			return null;
		}

		private static string GetMemberName(MethodInfo method)
		{
			var name = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", GetTypeName(method.DeclaringType), method.Name);
			var parameters = method.GetParameters();
			if (parameters.Length != 0)
			{
				var parameterTypeNames = parameters.Select(param => GetTypeName(param.ParameterType)).ToArray();
				name += string.Format(CultureInfo.InvariantCulture, "({0})", string.Join(",", parameterTypeNames));
			}

			return name;
		}

		private static string GetTagValue(XPathNavigator parentNode, string tagName)
		{
			if (parentNode != null)
			{
				var node = parentNode.SelectSingleNode(tagName);
				if (node != null)
				{
					return node.Value.Trim();
				}
			}

			return null;
		}

		private XPathNavigator GetTypeNode(Type type)
		{
			var controllerTypeName = GetTypeName(type);
			var selectExpression = string.Format(CultureInfo.InvariantCulture, TYPE_EXPRESSION, controllerTypeName);
			return _documentNavigator.SelectSingleNode(selectExpression);
		}

		private static string GetTypeName(Type type)
		{
			var name = type.FullName;
			if (type.IsGenericType)
			{
				// Format the generic type name to something like: Generic{System.Int32,System.String}
				var genericType = type.GetGenericTypeDefinition();
				var genericArguments = type.GetGenericArguments();
				var genericTypeName = genericType.FullName;

				// Trim the generic parameter counts from the name
				genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
				var argumentTypeNames = genericArguments.Select(t => GetTypeName(t)).ToArray();
				name = string.Format(CultureInfo.InvariantCulture, "{0}{{{1}}}", genericTypeName, string.Join(",", argumentTypeNames));
			}
			if (type.IsNested)
			{
				// Changing the nested type name from OuterType+InnerType to OuterType.InnerType to match the XML documentation syntax.
				name = name.Replace("+", ".");
			}

			return name;
		}
	}
}
