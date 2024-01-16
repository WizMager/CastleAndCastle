using System;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Plugins.Extensions.InstallerGenerator.Editor.Models
{
	[Serializable]
	public class AttributeChanges
	{
		public bool changed = false;
		public ExecutionType type;
		public ExecutionPriority priority;
		public int order;
		public string name;
	}
}
