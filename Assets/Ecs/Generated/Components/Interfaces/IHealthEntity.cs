//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial interface IHealthEntity
{
	Ecs.Game.Components.Units.HealthComponent Health { get; }
	bool HasHealth { get; }

	void AddHealth(float newValue);
	void ReplaceHealth(float newValue);
	void RemoveHealth();
}