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
	public GameDestroyedListenerComponent GameDestroyedListener { get { return (GameDestroyedListenerComponent)GetComponent(GameComponentsLookup.GameDestroyedListener); } }
	public bool HasGameDestroyedListener { get { return HasComponent(GameComponentsLookup.GameDestroyedListener); } }

	public void AddGameDestroyedListener()
	{
		var index = GameComponentsLookup.GameDestroyedListener;
		var component = (GameDestroyedListenerComponent)CreateComponent(index, typeof(GameDestroyedListenerComponent));
		AddComponent(index, component);
	}

	public void ReplaceGameDestroyedListener()
	{
		ReplaceComponent(GameComponentsLookup.GameDestroyedListener, GameDestroyedListener);
	}

	public void RemoveGameDestroyedListener()
	{
		RemoveComponent(GameComponentsLookup.GameDestroyedListener);
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
public sealed partial class GameMatcher
{
	static JCMG.EntitasRedux.IMatcher<GameEntity> _matcherGameDestroyedListener;

	public static JCMG.EntitasRedux.IMatcher<GameEntity> GameDestroyedListener
	{
		get
		{
			if (_matcherGameDestroyedListener == null)
			{
				var matcher = (JCMG.EntitasRedux.Matcher<GameEntity>)JCMG.EntitasRedux.Matcher<GameEntity>.AllOf(GameComponentsLookup.GameDestroyedListener);
				matcher.ComponentNames = GameComponentsLookup.ComponentNames;
				_matcherGameDestroyedListener = matcher;
			}

			return _matcherGameDestroyedListener;
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
public partial class GameEntity
{
	public System.IDisposable SubscribeDestroyed(OnGameDestroyed value, bool invokeOnSubscribe = true)
	{
		var componentListener = HasGameDestroyedListener
			? GameDestroyedListener
			: (GameDestroyedListenerComponent)CreateComponent(GameComponentsLookup.GameDestroyedListener, typeof(GameDestroyedListenerComponent));
		componentListener.Delegate += value;
		ReplaceComponent(GameComponentsLookup.GameDestroyedListener, componentListener);
		if(invokeOnSubscribe && HasComponent(GameComponentsLookup.Destroyed))
		{
			value(this);
		}

		return new JCMG.EntitasRedux.Events.EventDisposable<OnGameDestroyed>(CreationIndex, value, UnsubscribeDestroyed);
	}

	private void UnsubscribeDestroyed(int creationIndex, OnGameDestroyed value)
	{
		if(!IsEnabled || CreationIndex != creationIndex)
			return;

		var index = GameComponentsLookup.GameDestroyedListener;
		var component = GameDestroyedListener;
		component.Delegate -= value;
		if (GameDestroyedListener.IsEmpty)
		{
			RemoveComponent(index);
		}
		else
		{
			ReplaceComponent(index, component);
		}
	}
}
