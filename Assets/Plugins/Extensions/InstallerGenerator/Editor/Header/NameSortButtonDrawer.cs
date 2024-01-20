using Plugins.Extensions.InstallerGenerator.Editor.Sort.Impls;
using UnityEngine;

namespace Plugins.Extensions.InstallerGenerator.Editor.Header
{
	public class NameSortButtonDrawer : ASortButtonDrawer
	{
		private readonly NameSortStrategy _sortStrategy;

		public NameSortButtonDrawer(NameSortStrategy sortStrategy, GUILayoutOption[] style)
			: base(sortStrategy, style)
		{
			_sortStrategy = sortStrategy;
		}

		protected override string GetButtonLabel() => "Name " + _sortStrategy.Name;
	}
}