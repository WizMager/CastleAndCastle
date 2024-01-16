using System.Collections.Generic;
using Plugins.Extensions.InstallerGenerator.Editor.Models;

namespace Plugins.Extensions.InstallerGenerator.Editor.Sort
{
	public interface ISortStrategy
	{
		string Name { get; }

		void Next();

		void Reset();

		void Sort(List<AttributeRecord> records);
	}
}