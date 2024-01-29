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
	public Ecs.Game.Components.Units.MainTargetComponent MainTarget { get { return (Ecs.Game.Components.Units.MainTargetComponent)GetComponent(GameComponentsLookup.MainTarget); } }
	public bool HasMainTarget { get { return HasComponent(GameComponentsLookup.MainTarget); } }

	public void AddMainTarget(GameEntity newValue)
	{
		var index = GameComponentsLookup.MainTarget;
		var component = (Ecs.Game.Components.Units.MainTargetComponent)CreateComponent(index, typeof(Ecs.Game.Components.Units.MainTargetComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = newValue;
		#endif
		AddComponent(index, component);
	}

	public void ReplaceMainTarget(GameEntity newValue)
	{
		var index = GameComponentsLookup.MainTarget;
		var component = (Ecs.Game.Components.Units.MainTargetComponent)CreateComponent(index, typeof(Ecs.Game.Components.Units.MainTargetComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = newValue;
		#endif
		ReplaceComponent(index, component);
	}

	public void CopyMainTargetTo(Ecs.Game.Components.Units.MainTargetComponent copyComponent)
	{
		var index = GameComponentsLookup.MainTarget;
		var component = (Ecs.Game.Components.Units.MainTargetComponent)CreateComponent(index, typeof(Ecs.Game.Components.Units.MainTargetComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = copyComponent.Value;
		#endif
		ReplaceComponent(index, component);
	}

	public void RemoveMainTarget()
	{
		RemoveComponent(GameComponentsLookup.MainTarget);
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
public partial class GameEntity : IMainTargetEntity { }

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
	static JCMG.EntitasRedux.IMatcher<GameEntity> _matcherMainTarget;

	public static JCMG.EntitasRedux.IMatcher<GameEntity> MainTarget
	{
		get
		{
			if (_matcherMainTarget == null)
			{
				var matcher = (JCMG.EntitasRedux.Matcher<GameEntity>)JCMG.EntitasRedux.Matcher<GameEntity>.AllOf(GameComponentsLookup.MainTarget);
				matcher.ComponentNames = GameComponentsLookup.ComponentNames;
				_matcherMainTarget = matcher;
			}

			return _matcherMainTarget;
		}
	}
}
