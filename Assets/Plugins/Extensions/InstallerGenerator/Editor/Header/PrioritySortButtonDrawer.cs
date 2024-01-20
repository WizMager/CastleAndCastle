using Plugins.Extensions.InstallerGenerator.Editor.Sort.Impls;
using Plugins.Extensions.Utils;
using UnityEngine;

namespace Plugins.Extensions.InstallerGenerator.Editor.Header
{
	public class PrioritySortButtonDrawer : ASortButtonDrawer
	{
		private readonly PrioritySortStrategy _sortStrategy;

		public PrioritySortButtonDrawer(PrioritySortStrategy sortStrategy, GUILayoutOption[] style)
			: base(sortStrategy, style)
		{
			_sortStrategy = sortStrategy;
		}

		protected override string GetButtonLabel() => _sortStrategy.Name.IsNullOrEmpty()
			? "Priority"
			: _sortStrategy.Name;
	}
}