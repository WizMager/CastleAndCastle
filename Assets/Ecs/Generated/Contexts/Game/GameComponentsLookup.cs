//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using JCMG.EntitasRedux;

public static class GameComponentsLookup
{
	public const int AnyBusyListener = 0;
	public const int AnyBusyRemovedListener = 1;
	public const int CoinsListener = 2;
	public const int Destroyed = 3;
	public const int Uid = 4;
	public const int BuildingSlot = 5;
	public const int Busy = 6;
	public const int Coins = 7;
	public const int Instantiate = 8;
	public const int Position = 9;
	public const int Prefab = 10;
	public const int Rotation = 11;
	public const int SelectedBuilding = 12;
	public const int Transform = 13;
	public const int Visible = 14;
	public const int GameDestroyedListener = 15;
	public const int PositionListener = 16;
	public const int RotationListener = 17;
	public const int VisibleListener = 18;
	public const int VisibleRemovedListener = 19;

	public const int TotalComponents = 20;

	public static readonly string[] ComponentNames =
	{
		"AnyBusyListener",
		"AnyBusyRemovedListener",
		"CoinsListener",
		"Destroyed",
		"Uid",
		"BuildingSlot",
		"Busy",
		"Coins",
		"Instantiate",
		"Position",
		"Prefab",
		"Rotation",
		"SelectedBuilding",
		"Transform",
		"Visible",
		"GameDestroyedListener",
		"PositionListener",
		"RotationListener",
		"VisibleListener",
		"VisibleRemovedListener"
	};

	public static readonly System.Type[] ComponentTypes =
	{
		typeof(AnyBusyListenerComponent),
		typeof(AnyBusyRemovedListenerComponent),
		typeof(CoinsListenerComponent),
		typeof(Ecs.Common.Components.DestroyedComponent),
		typeof(Ecs.Common.Components.UidComponent),
		typeof(Ecs.Game.Components.BuildingSlotComponent),
		typeof(Ecs.Game.Components.BusyComponent),
		typeof(Ecs.Game.Components.CoinsComponent),
		typeof(Ecs.Game.Components.InstantiateComponent),
		typeof(Ecs.Game.Components.PositionComponent),
		typeof(Ecs.Game.Components.PrefabComponent),
		typeof(Ecs.Game.Components.RotationComponent),
		typeof(Ecs.Game.Components.SelectedBuildingComponent),
		typeof(Ecs.Game.Components.TransformComponent),
		typeof(Ecs.Game.Components.VisibleComponent),
		typeof(GameDestroyedListenerComponent),
		typeof(PositionListenerComponent),
		typeof(RotationListenerComponent),
		typeof(VisibleListenerComponent),
		typeof(VisibleRemovedListenerComponent)
	};

	public static readonly Dictionary<Type, int> ComponentTypeToIndex = new Dictionary<Type, int>
	{
		{ typeof(AnyBusyListenerComponent), 0 },
		{ typeof(AnyBusyRemovedListenerComponent), 1 },
		{ typeof(CoinsListenerComponent), 2 },
		{ typeof(Ecs.Common.Components.DestroyedComponent), 3 },
		{ typeof(Ecs.Common.Components.UidComponent), 4 },
		{ typeof(Ecs.Game.Components.BuildingSlotComponent), 5 },
		{ typeof(Ecs.Game.Components.BusyComponent), 6 },
		{ typeof(Ecs.Game.Components.CoinsComponent), 7 },
		{ typeof(Ecs.Game.Components.InstantiateComponent), 8 },
		{ typeof(Ecs.Game.Components.PositionComponent), 9 },
		{ typeof(Ecs.Game.Components.PrefabComponent), 10 },
		{ typeof(Ecs.Game.Components.RotationComponent), 11 },
		{ typeof(Ecs.Game.Components.SelectedBuildingComponent), 12 },
		{ typeof(Ecs.Game.Components.TransformComponent), 13 },
		{ typeof(Ecs.Game.Components.VisibleComponent), 14 },
		{ typeof(GameDestroyedListenerComponent), 15 },
		{ typeof(PositionListenerComponent), 16 },
		{ typeof(RotationListenerComponent), 17 },
		{ typeof(VisibleListenerComponent), 18 },
		{ typeof(VisibleRemovedListenerComponent), 19 }
	};

	/// <summary>
	/// Returns a component index based on the passed <paramref name="component"/> type; where an index cannot be found
	/// -1 will be returned instead.
	/// </summary>
	/// <param name="component"></param>
	public static int GetComponentIndex(IComponent component)
	{
		return GetComponentIndex(component.GetType());
	}

	/// <summary>
	/// Returns a component index based on the passed <paramref name="componentType"/>; where an index cannot be found
	/// -1 will be returned instead.
	/// </summary>
	/// <param name="componentType"></param>
	public static int GetComponentIndex(Type componentType)
	{
		return ComponentTypeToIndex.TryGetValue(componentType, out var index) ? index : -1;
	}
}
