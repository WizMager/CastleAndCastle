//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using JCMG.EntitasRedux;

public sealed class AnyBusyRemovedEventSystem : JCMG.EntitasRedux.ReactiveSystem<GameEntity>
{
	readonly JCMG.EntitasRedux.IGroup<GameEntity> _listeners;

	public AnyBusyRemovedEventSystem(IContext<GameEntity> context) : base(context)
	{
		_listeners = context.GetGroup(GameMatcher.AnyBusyRemovedListener);
	}

	protected override JCMG.EntitasRedux.ICollector<GameEntity> GetTrigger(JCMG.EntitasRedux.IContext<GameEntity> context)
	{
		return new SlimCollector<GameEntity>(context, GameComponentsLookup.Busy, EventType.Removed);
	}

	protected override bool Filter(GameEntity entity)
	{
		return !entity.IsBusy;
	}

	protected override void Execute(System.Collections.Generic.IEnumerable<GameEntity> entities)
	{
		foreach (var e in entities)
		{
			
			using(UnityEngine.Pool.ListPool<GameEntity>.Get(out var buffer))
			{
				_listeners.GetEntities(buffer);
				foreach (var listenerEntity in buffer)
					listenerEntity.AnyBusyRemovedListener.Invoke(e);
			}
		}
	}
}