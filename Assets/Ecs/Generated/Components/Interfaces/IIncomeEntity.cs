//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial interface IIncomeEntity
{
	Ecs.Game.Components.IncomeComponent Income { get; }
	bool HasIncome { get; }

	void AddIncome(int newValue);
	void ReplaceIncome(int newValue);
	void RemoveIncome();
}