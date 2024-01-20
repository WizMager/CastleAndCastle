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
	public AnyBusyListenerComponent AnyBusyListener { get { return (AnyBusyListenerComponent)GetComponent(GameComponentsLookup.AnyBusyListener); } }
	public bool HasAnyBusyListener { get { return HasComponent(GameComponentsLookup.AnyBusyListener); } }

	public void AddAnyBusyListener()
	{
		var index = GameComponentsLookup.AnyBusyListener;
		var component = (AnyBusyListenerComponent)CreateComponent(index, typeof(AnyBusyListenerComponent));
		AddComponent(index, component);
	}

	public void ReplaceAnyBusyListener()
	{
		ReplaceComponent(GameComponentsLookup.AnyBusyListener, AnyBusyListener);
	}

	public void RemoveAnyBusyListener()
	{
		RemoveComponent(GameComponentsLookup.AnyBusyListener);
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
	static JCMG.EntitasRedux.IMatcher<GameEntity> _matcherAnyBusyListener;

	public static JCMG.EntitasRedux.IMatcher<GameEntity> AnyBusyListener
	{
		get
		{
			if (_matcherAnyBusyListener == null)
			{
				var matcher = (JCMG.EntitasRedux.Matcher<GameEntity>)JCMG.EntitasRedux.Matcher<GameEntity>.AllOf(GameComponentsLookup.AnyBusyListener);
				matcher.ComponentNames = GameComponentsLookup.ComponentNames;
				_matcherAnyBusyListener = matcher;
			}

			return _matcherAnyBusyListener;
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
	public System.IDisposable SubscribeAnyBusy(OnGameAnyBusy value, bool invokeOnSubscribe = true)
	{
		var componentListener = HasAnyBusyListener
			? AnyBusyListener
			: (AnyBusyListenerComponent)CreateComponent(GameComponentsLookup.AnyBusyListener, typeof(AnyBusyListenerComponent));
		componentListener.Delegate += value;
		ReplaceComponent(GameComponentsLookup.AnyBusyListener, componentListener);
		if(invokeOnSubscribe && HasComponent(GameComponentsLookup.Busy))
		{
			value(this);
		}

		return new JCMG.EntitasRedux.Events.EventDisposable<OnGameAnyBusy>(CreationIndex, value, UnsubscribeAnyBusy);
	}

	private void UnsubscribeAnyBusy(int creationIndex, OnGameAnyBusy value)
	{
		if(!IsEnabled || CreationIndex != creationIndex)
			return;

		var index = GameComponentsLookup.AnyBusyListener;
		var component = AnyBusyListener;
		component.Delegate -= value;
		if (AnyBusyListener.IsEmpty)
		{
			RemoveComponent(index);
		}
		else
		{
			ReplaceComponent(index, component);
		}
	}
}
