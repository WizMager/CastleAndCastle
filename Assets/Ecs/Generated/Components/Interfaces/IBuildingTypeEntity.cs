//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial interface IBuildingTypeEntity
{
	Ecs.Game.Components.Buildings.BuildingTypeComponent BuildingType { get; }
	bool HasBuildingType { get; }

	void AddBuildingType(Db.Buildings.EBuildingType newValue);
	void ReplaceBuildingType(Db.Buildings.EBuildingType newValue);
	void RemoveBuildingType();
}