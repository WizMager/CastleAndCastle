using Plugins.Extensions.InstallerGenerator.Editor.Models;

namespace Plugins.Extensions.InstallerGenerator.Editor.Sort.Impls
{
	public class NameSortStrategy : AStringSortStrategy
	{
		protected override string GetValue(AttributeRecord record) => record.Type.Name;
	}
}