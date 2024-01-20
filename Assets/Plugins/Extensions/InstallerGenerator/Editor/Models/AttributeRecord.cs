using System;
using Plugins.Extensions.InstallerGenerator.Attributes;

namespace Plugins.Extensions.InstallerGenerator.Editor.Models
{
	public struct AttributeRecord
	{
		public readonly Type Type;
		public readonly InstallAttribute Attribute;
		public readonly string[] Features;
		public readonly AttributeChanges Changes;

		public AttributeRecord(Type type, InstallAttribute attribute)
		{
			Type = type;
			Attribute = attribute;
			Features = Attribute != null ? Attribute.Features : Array.Empty<string>();
			Changes = new AttributeChanges
			{
				type = attribute.Type,
				priority = attribute.Priority,
				name = string.Join("|", attribute.Features),
				order = attribute.Order
			};
		}
	}
}