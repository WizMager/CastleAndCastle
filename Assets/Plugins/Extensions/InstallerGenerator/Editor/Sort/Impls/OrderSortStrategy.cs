using Plugins.Extensions.InstallerGenerator.Editor.Models;

namespace Plugins.Extensions.InstallerGenerator.Editor.Sort.Impls
{
	public class OrderSortStrategy : AAscendingOrDescendingSortStrategy<int>
	{
		protected override int GetValue(AttributeRecord record) => record.Attribute.Order;
	}
}