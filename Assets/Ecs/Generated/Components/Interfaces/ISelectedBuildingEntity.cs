//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial interface ISelectedBuildingEntity
{
	Ecs.Game.Components.SelectedBuildingComponent SelectedBuilding { get; }
	bool HasSelectedBuilding { get; }

	void AddSelectedBuilding(Db.Buildings.EBuildingType newBuildingType);
	void ReplaceSelectedBuilding(Db.Buildings.EBuildingType newBuildingType);
	void RemoveSelectedBuilding();
}