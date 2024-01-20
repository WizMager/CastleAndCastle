//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity
{
	public Ecs.Game.Components.Units.DestinationPointComponent DestinationPoint { get { return (Ecs.Game.Components.Units.DestinationPointComponent)GetComponent(GameComponentsLookup.DestinationPoint); } }
	public bool HasDestinationPoint { get { return HasComponent(GameComponentsLookup.DestinationPoint); } }

	public void AddDestinationPoint(UnityEngine.Vector3 newValue)
	{
		var index = GameComponentsLookup.DestinationPoint;
		var component = (Ecs.Game.Components.Units.DestinationPointComponent)CreateComponent(index, typeof(Ecs.Game.Components.Units.DestinationPointComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = newValue;
		#endif
		AddComponent(index, component);
	}

	public void ReplaceDestinationPoint(UnityEngine.Vector3 newValue)
	{
		var index = GameComponentsLookup.DestinationPoint;
		var component = (Ecs.Game.Components.Units.DestinationPointComponent)CreateComponent(index, typeof(Ecs.Game.Components.Units.DestinationPointComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = newValue;
		#endif
		ReplaceComponent(index, component);
	}

	public void CopyDestinationPointTo(Ecs.Game.Components.Units.DestinationPointComponent copyComponent)
	{
		var index = GameComponentsLookup.DestinationPoint;
		var component = (Ecs.Game.Components.Units.DestinationPointComponent)CreateComponent(index, typeof(Ecs.Game.Components.Units.DestinationPointComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = copyComponent.Value;
		#endif
		ReplaceComponent(index, component);
	}

	public void RemoveDestinationPoint()
	{
		RemoveComponent(GameComponentsLookup.DestinationPoint);
	}
}

//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity : IDestinationPointEntity { }

//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher
{
	static JCMG.EntitasRedux.IMatcher<GameEntity> _matcherDestinationPoint;

	public static JCMG.EntitasRedux.IMatcher<GameEntity> DestinationPoint
	{
		get
		{
			if (_matcherDestinationPoint == null)
			{
				var matcher = (JCMG.EntitasRedux.Matcher<GameEntity>)JCMG.EntitasRedux.Matcher<GameEntity>.AllOf(GameComponentsLookup.DestinationPoint);
				matcher.ComponentNames = GameComponentsLookup.ComponentNames;
				_matcherDestinationPoint = matcher;
			}

			return _matcherDestinationPoint;
		}
	}
}