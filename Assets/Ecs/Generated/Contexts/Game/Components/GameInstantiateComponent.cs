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
	static readonly Ecs.Game.Components.InstantiateComponent InstantiateComponent = new Ecs.Game.Components.InstantiateComponent();

	public bool IsInstantiate
	{
		get { return HasComponent(GameComponentsLookup.Instantiate); }
		set
		{
			if (value != IsInstantiate)
			{
				var index = GameComponentsLookup.Instantiate;
				if (value)
				{
					var componentPool = GetComponentPool(index);
					var component = componentPool.Count > 0
							? componentPool.Pop()
							: InstantiateComponent;

					AddComponent(index, component);
				}
				else
				{
					RemoveComponent(index);
				}
			}
		}
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
public partial class GameEntity : IInstantiateEntity { }

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
	static JCMG.EntitasRedux.IMatcher<GameEntity> _matcherInstantiate;

	public static JCMG.EntitasRedux.IMatcher<GameEntity> Instantiate
	{
		get
		{
			if (_matcherInstantiate == null)
			{
				var matcher = (JCMG.EntitasRedux.Matcher<GameEntity>)JCMG.EntitasRedux.Matcher<GameEntity>.AllOf(GameComponentsLookup.Instantiate);
				matcher.ComponentNames = GameComponentsLookup.ComponentNames;
				_matcherInstantiate = matcher;
			}

			return _matcherInstantiate;
		}
	}
}
