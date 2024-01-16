using Plugins.Extensions.InstallerGenerator.Editor.Sort.Impls;
using Plugins.Extensions.Utils;
using UnityEngine;

namespace Plugins.Extensions.InstallerGenerator.Editor.Header
{
	public class TypeSortButtonDrawer : ASortButtonDrawer
	{
		private readonly TypeSortStrategy _sortStrategy;

		public TypeSortButtonDrawer(TypeSortStrategy sortStrategy, GUILayoutOption[] style)
			: base(sortStrategy, style)
		{
			_sortStrategy = sortStrategy;
		}

		protected override string GetButtonLabel() => _sortStrategy.Name.IsNullOrEmpty()
			? "Type"
			: _sortStrategy.Name;
	}
}